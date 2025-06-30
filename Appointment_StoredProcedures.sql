-- ========================================
-- Add Appointment
-- ========================================
CREATE OR ALTER PROCEDURE PR_Appointment_Add
    @DoctorID INT,
    @PatientID INT,
    @AppointmentDate DATETIME,
    @AppointmentStatus NVARCHAR(20),
    @Description NVARCHAR(250),
    @SpecialRemarks NVARCHAR(100),
    @Modified DATETIME,
    @UserID INT,
    @TotalConsultedAmount DECIMAL(5,2) = NULL
AS
BEGIN
    INSERT INTO Appointment (
        DoctorID,
        PatientID,
        AppointmentDate,
        AppointmentStatus,
        [Description],
        SpecialRemarks,
        Modified,
        UserID,
        TotalConsultedAmount
    )
    VALUES (
        @DoctorID,
        @PatientID,
        @AppointmentDate,
        @AppointmentStatus,
        @Description,
        @SpecialRemarks,
        @Modified,
        @UserID,
        @TotalConsultedAmount
    )
END


-- ========================================
-- Edit Appointment
-- ========================================
CREATE OR ALTER PROCEDURE PR_Appointment_Edit
    @AppointmentID INT,
    @DoctorID INT,
    @PatientID INT,
    @AppointmentDate DATETIME,
    @AppointmentStatus NVARCHAR(20),
    @Description NVARCHAR(250),
    @SpecialRemarks NVARCHAR(100),
    @Modified DATETIME,
    @UserID INT,
    @TotalConsultedAmount DECIMAL(5,2) = NULL
AS
BEGIN
    UPDATE Appointment
    SET 
        Appointment.DoctorID = @DoctorID,
        Appointment.PatientID = @PatientID,
        Appointment.AppointmentDate = @AppointmentDate,
        Appointment.AppointmentStatus = @AppointmentStatus,
        Appointment.[Description] = @Description,
        Appointment.SpecialRemarks = @SpecialRemarks,
        Appointment.Modified = @Modified,
        Appointment.UserID = @UserID,
        Appointment.TotalConsultedAmount = @TotalConsultedAmount
    WHERE Appointment.AppointmentID = @AppointmentID
END


-- ========================================
-- Select All Appointments
-- ========================================
CREATE OR ALTER PROCEDURE PR_Appointment_SelectAll
AS
BEGIN
    SELECT 
        Appointment.AppointmentID,
        Appointment.DoctorID,
        Appointment.PatientID,
        Appointment.AppointmentDate,
        Appointment.AppointmentStatus,
        Appointment.[Description],
        Appointment.SpecialRemarks,
        Appointment.Created,
        Appointment.Modified,
        Appointment.UserID,
        Appointment.TotalConsultedAmount
    FROM Appointment
    ORDER BY Appointment.AppointmentDate
END


-- ========================================
-- Select Appointment by Primary Key
-- ========================================
CREATE OR ALTER PROCEDURE PR_Appointment_SelectByPK
    @AppointmentID INT
AS
BEGIN
    SELECT 
        Appointment.AppointmentID,
        Appointment.DoctorID,
        Appointment.PatientID,
        Appointment.AppointmentDate,
        Appointment.AppointmentStatus,
        Appointment.[Description],
        Appointment.SpecialRemarks,
        Appointment.Created,
        Appointment.Modified,
        Appointment.UserID,
        Appointment.TotalConsultedAmount
    FROM Appointment
    WHERE Appointment.AppointmentID = @AppointmentID
    ORDER BY Appointment.AppointmentDate
END


-- ========================================
-- Delete Appointment
-- ========================================
CREATE OR ALTER PROCEDURE PR_Appointment_DeleteByPK
    @AppointmentID INT
AS
BEGIN
    DELETE FROM Appointment
    WHERE Appointment.AppointmentID = @AppointmentID
END

