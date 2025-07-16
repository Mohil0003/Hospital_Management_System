-- ========================================
-- USERS
-- ========================================
CREATE OR ALTER PROCEDURE PR_User_SelectAll
AS
BEGIN
    SELECT 
        U.UserID,
        U.UserName,
        U.[Password],
        U.Email,
        U.MobileNo,
        U.IsActive,
        U.Created,
        U.Modified
    FROM [User] U
    ORDER BY U.UserName
END
GO

CREATE OR ALTER PROCEDURE PR_User_SelectByPK
    @UserID INT
AS
BEGIN
    SELECT 
        U.UserID,
        U.UserName,
        U.[Password],
        U.Email,
        U.MobileNo,
        U.IsActive,
        U.Created,
        U.Modified
    FROM [User] U
    WHERE U.UserID = @UserID
END
GO

-- ========================================
-- DOCTOR
-- ========================================
CREATE OR ALTER PROCEDURE PR_Doctor_SelectAll
AS
BEGIN
    SELECT 
        D.DoctorID,
        D.Name,
        D.Phone,
        D.Email,
        D.Qualification,
        D.Specialization,
        D.IsActive,
        D.Created,
        D.Modified,
        D.UserID,
        U.UserName
    FROM Doctor D
    INNER JOIN [User] U ON D.UserID = U.UserID
    ORDER BY D.Name
END
GO

CREATE OR ALTER PROCEDURE PR_Doctor_SelectByPK
    @DoctorID INT
AS
BEGIN
    SELECT 
        D.DoctorID,
        D.Name,
        D.Phone,
        D.Email,
        D.Qualification,
        D.Specialization,
        D.IsActive,
        D.Created,
        D.Modified,
        D.UserID,
        U.UserName
    FROM Doctor D
    INNER JOIN [User] U ON D.UserID = U.UserID
    WHERE D.DoctorID = @DoctorID
END
GO

-- ========================================
-- PATIENT
-- ========================================
CREATE OR ALTER PROCEDURE PR_Patient_SelectAll
AS
BEGIN
    SELECT 
        P.PatientID,
        P.Name,
        P.DateOfBirth,
        P.Gender,
        P.Email,
        P.Phone,
        P.Address,
        P.City,
        P.State,
        P.IsActive,
        P.Created,
        P.Modified,
        P.UserID,
        U.UserName
    FROM Patient P
    INNER JOIN [User] U ON P.UserID = U.UserID
    ORDER BY P.Name
END
GO

CREATE OR ALTER PROCEDURE PR_Patient_SelectByPK
    @PatientID INT
AS
BEGIN
    SELECT 
        P.PatientID,
        P.Name,
        P.DateOfBirth,
        P.Gender,
        P.Email,
        P.Phone,
        P.Address,
        P.City,
        P.State,
        P.IsActive,
        P.Created,
        P.Modified,
        P.UserID,
        U.UserName
    FROM Patient P
    INNER JOIN [User] U ON P.UserID = U.UserID
    WHERE P.PatientID = @PatientID
END
GO

-- ========================================
-- DEPARTMENT
-- ========================================
CREATE OR ALTER PROCEDURE PR_Department_SelectAll
AS
BEGIN
    SELECT 
        DP.DepartmentID,
        DP.DepartmentName,
        DP.[Description],
        DP.IsActive,
        DP.Created,
        DP.Modified,
        DP.UserID,
        U.UserName
    FROM Department DP
    INNER JOIN [User] U ON DP.UserID = U.UserID
    ORDER BY DP.DepartmentName
END
GO

CREATE OR ALTER PROCEDURE PR_Department_SelectByPK
    @DepartmentID INT
AS
BEGIN
    SELECT 
        DP.DepartmentID,
        DP.DepartmentName,
        DP.[Description],
        DP.IsActive,
        DP.Created,
        DP.Modified,
        DP.UserID,
        U.UserName
    FROM Department DP
    INNER JOIN [User] U ON DP.UserID = U.UserID
    WHERE DP.DepartmentID = @DepartmentID
END
GO

-- ========================================
-- DOCTOR DEPARTMENT
-- ========================================
CREATE OR ALTER PROCEDURE PR_DoctorDepartment_SelectAll
AS
BEGIN
    SELECT 
        DD.DoctorDepartmentID,
        DD.DoctorID,
        D.Name AS DoctorName,
        DD.DepartmentID,
        DP.DepartmentName,
        DD.Created,
        DD.Modified,
        DD.UserID,
        U.UserName
    FROM DoctorDepartment DD
    INNER JOIN Doctor D ON DD.DoctorID = D.DoctorID
    INNER JOIN Department DP ON DD.DepartmentID = DP.DepartmentID
    INNER JOIN [User] U ON DD.UserID = U.UserID
    ORDER BY DD.DoctorDepartmentID
END
GO

CREATE OR ALTER PROCEDURE PR_DoctorDepartment_SelectByPK
    @DoctorDepartmentID INT
AS
BEGIN
    SELECT 
        DD.DoctorDepartmentID,
        DD.DoctorID,
        D.Name AS DoctorName,
        DD.DepartmentID,
        DP.DepartmentName,
        DD.Created,
        DD.Modified,
        DD.UserID,
        U.UserName
    FROM DoctorDepartment DD
    INNER JOIN Doctor D ON DD.DoctorID = D.DoctorID
    INNER JOIN Department DP ON DD.DepartmentID = DP.DepartmentID
    INNER JOIN [User] U ON DD.UserID = U.UserID
    WHERE DD.DoctorDepartmentID = @DoctorDepartmentID
END
GO

-- ========================================
-- APPOINTMENT
-- ========================================
CREATE OR ALTER PROCEDURE PR_Appointment_SelectAll
AS
BEGIN
    SELECT 
        A.AppointmentID,
        A.DoctorID,
        D.Name AS DoctorName,
        A.PatientID,
        P.Name AS PatientName,
        A.AppointmentDate,
        A.AppointmentStatus,
        A.[Description],
        A.SpecialRemarks,
        A.Created,
        A.Modified,
        A.UserID,
        U.UserName,
        A.TotalConsultedAmount
    FROM Appointment A
    INNER JOIN Doctor D ON A.DoctorID = D.DoctorID
    INNER JOIN Patient P ON A.PatientID = P.PatientID
    INNER JOIN [User] U ON A.UserID = U.UserID
    ORDER BY A.AppointmentDate DESC
END
GO

CREATE OR ALTER PROCEDURE PR_Appointment_SelectByPK
    @AppointmentID INT
AS
BEGIN
    SELECT 
        A.AppointmentID,
        A.DoctorID,
        D.Name AS DoctorName,
        A.PatientID,
        P.Name AS PatientName,
        A.AppointmentDate,
        A.AppointmentStatus,
        A.[Description],
        A.SpecialRemarks,
        A.Created,
        A.Modified,
        A.UserID,
        U.UserName,
        A.TotalConsultedAmount
    FROM Appointment A
    INNER JOIN Doctor D ON A.DoctorID = D.DoctorID
    INNER JOIN Patient P ON A.PatientID = P.PatientID
    INNER JOIN [User] U ON A.UserID = U.UserID
    WHERE A.AppointmentID = @AppointmentID
END
GO
