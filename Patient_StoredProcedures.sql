-- Add Patient
CREATE OR ALTER PROCEDURE PR_Pat_Patient_AddPatient
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
    INSERT INTO Patient (Name, DateOfBirth, Gender, Email, Phone, Address, City, State, IsActive, Modified, UserID)
    VALUES (@Name, @DateOfBirth, @Gender, @Email, @Phone, @Address, @City, @State, @IsActive, @Modified, @UserID)
END




-- Edit Patient
CREATE OR ALTER PROCEDURE PR_Pat_Patient_EditPatient
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
    SET Name = @Name,
        DateOfBirth = @DateOfBirth,
        Gender = @Gender,
        Email = @Email,
        Phone = @Phone,
        Address = @Address,
        City = @City,
        State = @State,
        IsActive = @IsActive,
        Modified = @Modified,
        UserID = @UserID
    WHERE PatientID = @PatientID
END





--Get All Patients
CREATE OR ALTER PROCEDURE PR_Pat_Patient_GetAllPatients
AS
BEGIN
    SELECT 
        PatientID, Name, DateOfBirth, Gender, Email, Phone,
        Address, City, State, IsActive, Created, Modified, UserID
    FROM Patient
END





-- Delete Patient
CREATE OR ALTER PROCEDURE PR_Pat_Patient_DeletePatient
    @PatientID INT
AS
BEGIN
    DELETE FROM Patient
    WHERE PatientID = @PatientID
END




--Select Patient By Primary Key
CREATE OR ALTER PROCEDURE PR_Pat_Patient_SelectPatientByPrimaryKey
    @PatientID INT
AS
BEGIN
    SELECT 
        PatientID, Name, DateOfBirth, Gender, Email, Phone,
        Address, City, State, IsActive, Created, Modified, UserID
    FROM Patient
    WHERE PatientID = @PatientID
END

	
