-- Add Department
CREATE OR ALTER PROCEDURE PR_Dept_Department_AddDepartment
    @DepartmentName NVARCHAR(100),
    @Description NVARCHAR(250) = NULL,
    @IsActive BIT = 1,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    INSERT INTO Department (DepartmentName, [Description], IsActive, Modified, UserID)
    VALUES (@DepartmentName, @Description, @IsActive, @Modified, @UserID)
END



-- Edit Department
CREATE OR ALTER PROCEDURE PR_Dept_Department_EditDepartment
    @DepartmentID INT,
    @DepartmentName NVARCHAR(100),
    @Description NVARCHAR(250) = NULL,
    @IsActive BIT,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    UPDATE Department
    SET DepartmentName = @DepartmentName,
        [Description] = @Description,
        IsActive = @IsActive,
        Modified = @Modified,
        UserID = @UserID
    WHERE DepartmentID = @DepartmentID
END



-- Get All Deparments
CREATE OR ALTER PROCEDURE PR_Dept_Department_GetAllDepartments
AS
BEGIN
    SELECT 
	Department.DepartmentID,
	Department.DepartmentName,
	Department.Description,
	Department.IsActive,
	Department.Modified,
	Department.UserID,
	Department.Created
	FROM 
	Department
END



-- Delete Department
CREATE OR ALTER PROCEDURE PR_Dept_Department_DeleteDepartment
    @DepartmentID INT
AS
BEGIN
    DELETE FROM Department
    WHERE DepartmentID = @DepartmentID
END


-- SelectByPrimaryKey
CREATE OR ALTER PROCEDURE PR_Dept_Department_SelectDepartmentByPrimaryKey
    @DepartmentID INT
AS
BEGIN
    SELECT 
	Department.DepartmentID,
	Department.DepartmentName,
	Department.Description,
	Department.IsActive,
	Department.Modified,
	Department.UserID,
	Department.Created
    FROM Department
    WHERE DepartmentID = @DepartmentID
END

