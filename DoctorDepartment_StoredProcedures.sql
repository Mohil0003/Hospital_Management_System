-- Add DoctorDeparmtent
CREATE OR ALTER PROCEDURE PR_DocDept_DoctorDepartment_AddDoctorDepartment
    @DoctorID INT,
    @DepartmentID INT,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    INSERT INTO DoctorDepartment (DoctorID, DepartmentID, Modified, UserID)
    VALUES (@DoctorID, @DepartmentID, @Modified, @UserID)
END




-- Edit DoctorDepartment
CREATE OR ALTER PROCEDURE PR_DocDept_DoctorDepartment_EditDoctorDepartment
    @DoctorDepartmentID INT,
    @DoctorID INT,
    @DepartmentID INT,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    UPDATE DoctorDepartment
    SET DoctorID = @DoctorID,
        DepartmentID = @DepartmentID,
        Modified = @Modified,
        UserID = @UserID
    WHERE DoctorDepartmentID = @DoctorDepartmentID
END



--Get All DoctorDepartments
CREATE OR ALTER PROCEDURE PR_DocDept_DoctorDepartment_GetAllDoctorDepartments
AS
BEGIN
    SELECT 
        DoctorDepartmentID, DoctorID, DepartmentID, Created, Modified, UserID
    FROM DoctorDepartment
END



--Delete DoctorDepartment
CREATE OR ALTER PROCEDURE PR_DocDept_DoctorDepartment_DeleteDoctorDepartment
    @DoctorDepartmentID INT
AS
BEGIN
    DELETE FROM DoctorDepartment
    WHERE DoctorDepartmentID = @DoctorDepartmentID
END




--Select DoctorDepartment By Primary Key
CREATE OR ALTER PROCEDURE PR_DocDept_DoctorDepartment_SelectDoctorDepartmentByPrimaryKey
    @DoctorDepartmentID INT
AS
BEGIN
    SELECT 
        DoctorDepartmentID, DoctorID, DepartmentID, Created, Modified, UserID
    FROM DoctorDepartment
    WHERE DoctorDepartmentID = @DoctorDepartmentID
END
