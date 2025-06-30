-- ========================================
-- Add Patient
-- ========================================
CREATE OR ALTER PROCEDURE PR_Patient_Add
    @Name NVARCHAR(100),
    @DateOfBirth DATETIME,
    @Gender NVARCHAR(10),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(100),
    @Address NVARCHAR(250),
    @City NVARCHAR(100),
    @State NVARCHAR(100),
    @IsActive BIT = 1,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    INSERT INTO Patient (
        Name, 
        DateOfBirth, 
        Gender, 
        Email, 
        Phone, 
        Address, 
        City, 
        State, 
        IsActive, 
        Modified, 
        UserID
    )
    VALUES (
        @Name, 
        @DateOfBirth, 
        @Gender, 
        @Email, 
        @Phone, 
        @Address, 
        @City, 
        @State, 
        @IsActive, 
        @Modified, 
        @UserID
    )
END


-- ========================================
-- Edit Patient
-- ========================================
CREATE OR ALTER PROCEDURE PR_Patient_Edit
    @PatientID INT,
    @Name NVARCHAR(100),
    @DateOfBirth DATETIME,
    @Gender NVARCHAR(10),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(100),
    @Address NVARCHAR(250),
    @City NVARCHAR(100),
    @State NVARCHAR(100),
    @IsActive BIT,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    UPDATE Patient
    SET 
        Patient.Name = @Name,
        Patient.DateOfBirth = @DateOfBirth,
        Patient.Gender = @Gender,
        Patient.Email = @Email,
        Patient.Phone = @Phone,
        Patient.Address = @Address,
        Patient.City = @City,
        Patient.State = @State,
        Patient.IsActive = @IsActive,
        Patient.Modified = @Modified,
        Patient.UserID = @UserID
    WHERE Patient.PatientID = @PatientID
END


-- ========================================
-- Select All Patients
-- ========================================
CREATE OR ALTER PROCEDURE PR_Patient_SelectAll
AS
BEGIN
    SELECT 
        Patient.PatientID,
        Patient.Name,
        Patient.DateOfBirth,
        Patient.Gender,
        Patient.Email,
        Patient.Phone,
        Patient.Address,
        Patient.City,
        Patient.State,
        Patient.IsActive,
        Patient.Created,
        Patient.Modified,
        Patient.UserID
    FROM Patient
    ORDER BY Patient.Name
END


-- ========================================
-- Select Patient by Primary Key
-- ========================================
CREATE OR ALTER PROCEDURE PR_Patient_SelectByPK
    @PatientID INT
AS
BEGIN
    SELECT 
        Patient.PatientID,
        Patient.Name,
        Patient.DateOfBirth,
        Patient.Gender,
        Patient.Email,
        Patient.Phone,
        Patient.Address,
        Patient.City,
        Patient.State,
        Patient.IsActive,
        Patient.Created,
        Patient.Modified,
        Patient.UserID
    FROM Patient
    WHERE Patient.PatientID = @PatientID
    ORDER BY Patient.Name
END


-- ========================================
-- Delete Patient
-- ========================================
CREATE OR ALTER PROCEDURE PR_Patient_DeleteByPK
    @PatientID INT
AS
BEGIN
    DELETE FROM Patient
    WHERE Patient.PatientID = @PatientID
END

