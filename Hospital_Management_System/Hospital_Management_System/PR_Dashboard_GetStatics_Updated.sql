-- Dashboard Statistics using only: [User], Department, Doctor, DoctorDepartment, Patient, Appointment
-- Run this in your SQL Server database

CREATE OR ALTER PROCEDURE PR_Dashboard_GetStatics
AS
BEGIN
    SET NOCOUNT ON;

    /* =====================
       1) Main single-row stats
       ===================== */
    SELECT 
        (SELECT COUNT(*) FROM [User]) AS TotalUsers,
        (SELECT COUNT(*) FROM Doctor) AS TotalDoctors,
        (SELECT COUNT(*) FROM Patient) AS TotalPatients,
        (SELECT COUNT(*) FROM Appointment) AS TotalAppointments,
        (SELECT COUNT(*) FROM Department) AS TotalDepartments,

        -- Dynamic KPIs (derived only from available tables)
        (SELECT COUNT(*) FROM Appointment WHERE CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)) AS TodaysAppointments,
        (SELECT COUNT(*) FROM Patient WHERE YEAR(Created) = YEAR(GETDATE()) AND MONTH(Created) = MONTH(GETDATE())) AS NewPatientsThisMonth,
        (SELECT COUNT(*) FROM Appointment WHERE AppointmentStatus = 'Pending') AS PendingAppointments,
        (SELECT COUNT(*) FROM Appointment WHERE AppointmentStatus = 'Completed') AS CompletedAppointments,
        (SELECT COUNT(*) FROM Appointment WHERE AppointmentStatus = 'Cancelled') AS CancelledAppointments,
        (
            SELECT CASE WHEN COUNT(*) = 0 THEN 0 
                        ELSE CAST(SUM(CASE WHEN AppointmentStatus = 'Completed' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS INT) 
                   END 
            FROM Appointment
        ) AS CompletionRatePercent,

        -- Financial-like insights using Appointment.TotalConsultedAmount
        ISNULL((SELECT SUM(TotalConsultedAmount) FROM Appointment), 0) AS TotalRevenue,
        (
            SELECT CASE WHEN COUNT(*) = 0 THEN 0 
                        ELSE CAST(ISNULL(SUM(TotalConsultedAmount), 0) / NULLIF(COUNT(*), 0) AS DECIMAL(18,2)) 
                   END 
            FROM Appointment
        ) AS AvgRevenuePerAppointment,
        (
            SELECT CASE WHEN COUNT(*) = 0 THEN 0 
                        ELSE CAST(ISNULL((SELECT SUM(TotalConsultedAmount) FROM Appointment), 0) / NULLIF(COUNT(*), 0) AS DECIMAL(18,2)) 
                   END 
            FROM Patient
        ) AS AvgRevenuePerPatient;

    /* =====================
       2) Patients growth by month (last 6 months including current)
       ===================== */
    ;WITH Months AS (
        SELECT CAST(DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1) AS DATE) AS MonthStart
        UNION ALL
        SELECT DATEADD(MONTH, -1, MonthStart) FROM Months WHERE DATEADD(MONTH, -1, MonthStart) >= DATEADD(MONTH, -5, CAST(DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1) AS DATE))
    )
    SELECT 
        FORMAT(m.MonthStart, 'MMM yyyy') AS [Month],
        (SELECT COUNT(*) FROM Patient p WHERE YEAR(p.Created) = YEAR(m.MonthStart) AND MONTH(p.Created) = MONTH(m.MonthStart)) AS TotalPatients
    FROM Months m
    ORDER BY m.MonthStart OPTION (MAXRECURSION 6);

    /* =====================
       3) Appointments by Department
       ===================== */
    SELECT 
        d.DepartmentName,
        COUNT(a.AppointmentID) AS TotalAppointments
    FROM Department d
    LEFT JOIN DoctorDepartment dd ON dd.DepartmentID = d.DepartmentID
    LEFT JOIN Doctor doc ON doc.DoctorID = dd.DoctorID
    LEFT JOIN Appointment a ON a.DoctorID = doc.DoctorID
    GROUP BY d.DepartmentName
    ORDER BY TotalAppointments DESC;

    /* =====================
       4) Revenue (TotalConsultedAmount) by Quarter (last 4 quarters)
       ===================== */
    ;WITH LastQuarters AS (
        SELECT DATEADD(QUARTER, -3, DATEFROMPARTS(YEAR(GETDATE()), ((DATEPART(QUARTER, GETDATE()) - 1) * 3) + 1, 1)) AS QStart, 0 AS Step
        UNION ALL
        SELECT DATEADD(QUARTER, 1, QStart), Step + 1 FROM LastQuarters WHERE Step < 3
    )
    SELECT 
        CONCAT('Q', DATEPART(QUARTER, QStart), ' ', YEAR(QStart)) AS Quarter,
        ISNULL((SELECT SUM(TotalConsultedAmount) FROM Appointment a WHERE a.AppointmentDate >= QStart AND a.AppointmentDate < DATEADD(QUARTER, 1, QStart)), 0) AS Revenue
    FROM LastQuarters
    ORDER BY QStart OPTION (MAXRECURSION 3);

    /* =====================
       5) Appointment status overview
       ===================== */
    SELECT 
        AppointmentStatus AS [Status],
        COUNT(*) AS [Count]
    FROM Appointment
    GROUP BY AppointmentStatus;
END
