--Add Appointment
CREATE OR ALTER PROCEDURE PR_App_Appointment_AddAppointment
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
        DoctorID, PatientID, AppointmentDate, AppointmentStatus, [Description],
        SpecialRemarks, Modified, UserID, TotalConsultedAmount
    )
    VALUES (
        @DoctorID, @PatientID, @AppointmentDate, @AppointmentStatus, @Description,
        @SpecialRemarks, @Modified, @UserID, @TotalConsultedAmount
    )
END




-- Edit Appointment
CREATE OR ALTER PROCEDURE PR_App_Appointment_EditAppointment
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
    SET DoctorID = @DoctorID,
        PatientID = @PatientID,
        AppointmentDate = @AppointmentDate,
        AppointmentStatus = @AppointmentStatus,
        [Description] = @Description,
        SpecialRemarks = @SpecialRemarks,
        Modified = @Modified,
        UserID = @UserID,
        TotalConsultedAmount = @TotalConsultedAmount
    WHERE AppointmentID = @AppointmentID
END





-- Get All Appointments
CREATE OR ALTER PROCEDURE PR_App_Appointment_GetAllAppointments
AS
BEGIN
    SELECT 
        AppointmentID, DoctorID, PatientID, AppointmentDate, AppointmentStatus,
        [Description], SpecialRemarks, Created, Modified, UserID, TotalConsultedAmount
    FROM Appointment
END





--Delete Appointment
CREATE OR ALTER PROCEDURE PR_App_Appointment_DeleteAppointment
    @AppointmentID INT
AS
BEGIN
    DELETE FROM Appointment
    WHERE AppointmentID = @AppointmentID
END




--Select Appointment By Primary Key
CREATE OR ALTER PROCEDURE PR_App_Appointment_SelectAppointmentByPrimaryKey
    @AppointmentID INT
AS
BEGIN
    SELECT 
        AppointmentID, DoctorID, PatientID, AppointmentDate, AppointmentStatus,
        [Description], SpecialRemarks, Created, Modified, UserID, TotalConsultedAmount
    FROM Appointment
    WHERE AppointmentID = @AppointmentID
END
