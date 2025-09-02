ALTER PROCEDURE [dbo].[PR_Dashboard_GetStatics]
AS
BEGIN
    SET NOCOUNT ON;

    -- First result set: Dashboard statistics
    SELECT  
        (SELECT COUNT(*) FROM [User]) AS TotalUsers,
        (SELECT COUNT(*) FROM Doctor) AS TotalDoctors,
        (SELECT COUNT(*) FROM Patient) AS TotalPatients,
        (SELECT COUNT(*) FROM Appointment) AS TotalAppointments,
        (SELECT COUNT(*) FROM Department) AS TotalDepartments;

    -- Second result set: Patients Growth (Monthly)
    SELECT 
        DATENAME(MONTH, Created) AS [Month],
        COUNT(*) AS TotalPatients
    FROM Patient
    GROUP BY DATENAME(MONTH, Created), MONTH(Created)
    ORDER BY MONTH(Created);

    -- Third result set: Appointments by Department
    SELECT 
        d.DepartmentName,
        COUNT(a.AppointmentID) AS TotalAppointments
    FROM Appointment a
    INNER JOIN Doctor doc ON a.DoctorID = doc.DoctorID
    INNER JOIN Department d ON doc.UserID = d.UserID
    GROUP BY d.DepartmentName;

    -- Fourth result set: Revenue Analysis (Quarterly)
    SELECT 
        DATEPART(QUARTER, Created) AS Quarter,
        SUM(TotalConsultedAmount) AS TotalRevenue
    FROM Appointment
    GROUP BY DATEPART(QUARTER, Created)
    ORDER BY Quarter;
END
