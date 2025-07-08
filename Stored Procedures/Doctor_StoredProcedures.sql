-- ========================================
-- Add Doctor
-- ========================================
CREATE OR ALTER PROCEDURE PR_Doctor_Add
    @Name NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Email NVARCHAR(100),
    @Qualification NVARCHAR(100),
    @Specialization NVARCHAR(100),
    @IsActive BIT = 1,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    INSERT INTO Doctor (
        Name, 
        Phone, 
        Email, 
        Qualification, 
        Specialization, 
        IsActive, 
        Modified, 
        UserID
    )
    VALUES (
        @Name, 
        @Phone, 
        @Email, 
        @Qualification, 
        @Specialization, 
        @IsActive, 
        @Modified, 
        @UserID
    )
END


-- ========================================
-- Edit Doctor
-- ========================================
CREATE OR ALTER PROCEDURE PR_Doctor_Edit
    @DoctorID INT,
    @Name NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Email NVARCHAR(100),
    @Qualification NVARCHAR(100),
    @Specialization NVARCHAR(100),
    @IsActive BIT,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    UPDATE Doctor
    SET 
        Doctor.Name = @Name,
        Doctor.Phone = @Phone,
        Doctor.Email = @Email,
        Doctor.Qualification = @Qualification,
        Doctor.Specialization = @Specialization,
        Doctor.IsActive = @IsActive,
        Doctor.Modified = @Modified,
        Doctor.UserID = @UserID
    WHERE Doctor.DoctorID = @DoctorID
END


-- ========================================
-- Select All Doctors
-- ========================================
CREATE OR ALTER PROCEDURE PR_Doctor_SelectAll
AS
BEGIN
    SELECT 
        Doctor.DoctorID,
        Doctor.Name,
        Doctor.Phone,
        Doctor.Email,
        Doctor.Qualification,
        Doctor.Specialization,
        Doctor.IsActive,
        Doctor.Created,
        Doctor.Modified,
        Doctor.UserID
    FROM Doctor
    ORDER BY Doctor.Name
END


-- ========================================
-- Select Doctor by Primary Key
-- ========================================
CREATE OR ALTER PROCEDURE PR_Doctor_SelectByPK
    @DoctorID INT
AS
BEGIN
    SELECT 
        Doctor.DoctorID,
        Doctor.Name,
        Doctor.Phone,
        Doctor.Email,
        Doctor.Qualification,
        Doctor.Specialization,
        Doctor.IsActive,
        Doctor.Created,
        Doctor.Modified,
        Doctor.UserID
    FROM Doctor
    WHERE Doctor.DoctorID = @DoctorID
    ORDER BY Doctor.Name
END


-- ========================================
-- Delete Doctor
-- ========================================
CREATE OR ALTER PROCEDURE PR_Doctor_Delete
    @DoctorID INT
AS
BEGIN
    DELETE FROM Doctor
    WHERE DoctorID = @DoctorID
END

