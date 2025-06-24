-- Add Doctor
CREATE OR ALTER PROCEDURE PR_Doc_Doctor_AddDoctor
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
    INSERT INTO Doctor (Name, Phone, Email, Qualification, Specialization, IsActive, Modified, UserID)
    VALUES (@Name, @Phone, @Email, @Qualification, @Specialization, @IsActive, @Modified, @UserID)
END




-- Edit Doctor
CREATE OR ALTER PROCEDURE PR_Doc_Doctor_EditDoctor
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
    SET Name = @Name,
        Phone = @Phone,
        Email = @Email,
        Qualification = @Qualification,
        Specialization = @Specialization,
        IsActive = @IsActive,
        Modified = @Modified,
        UserID = @UserID
    WHERE DoctorID = @DoctorID
END




-- Get All Doctors
CREATE OR ALTER PROCEDURE PR_Doc_Doctor_GetAllDoctors
AS
BEGIN
    SELECT 
        DoctorID, Name, Phone, Email, Qualification, Specialization,
        IsActive, Created, Modified, UserID
    FROM Doctor
END




--Delete Doctor
CREATE OR ALTER PROCEDURE PR_Doc_Doctor_DeleteDoctor
    @DoctorID INT
AS
BEGIN
    DELETE FROM Doctor
    WHERE DoctorID = @DoctorID
END




-- Select Doctor By Primary Key
CREATE OR ALTER PROCEDURE PR_Doc_Doctor_SelectDoctorByPrimaryKey
    @DoctorID INT
AS
BEGIN
    SELECT 
        DoctorID, Name, Phone, Email, Qualification, Specialization,
        IsActive, Created, Modified, UserID
    FROM Doctor
    WHERE DoctorID = @DoctorID
END

