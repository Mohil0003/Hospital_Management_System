-- ========================================
-- Add Department
-- ========================================
CREATE OR ALTER PROCEDURE PR_Department_Add
    @DepartmentName NVARCHAR(100),
    @Description NVARCHAR(250) = NULL,
    @IsActive BIT = 1,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    INSERT INTO Department (
        DepartmentName, 
        [Description], 
        IsActive, 
        Modified, 
        UserID
    )
    VALUES (
        @DepartmentName, 
        @Description, 
        @IsActive, 
        @Modified, 
        @UserID
    )
END


-- ========================================
-- Edit Department
-- ========================================
CREATE OR ALTER PROCEDURE PR_Department_Edit
    @DepartmentID INT,
    @DepartmentName NVARCHAR(100),
    @Description NVARCHAR(250) = NULL,
    @IsActive BIT,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    UPDATE Department
    SET 
        Department.DepartmentName = @DepartmentName,
        Department.[Description] = @Description,
        Department.IsActive = @IsActive,
        Department.Modified = @Modified,
        Department.UserID = @UserID
    WHERE Department.DepartmentID = @DepartmentID
END


-- ========================================
-- Select All Departments
-- ========================================
CREATE OR ALTER PROCEDURE PR_Department_SelectAll
AS
BEGIN
    SELECT 
        Department.DepartmentID,
        Department.DepartmentName,
        Department.[Description],
        Department.IsActive,
        Department.Modified,
        Department.UserID,
        Department.Created
    FROM Department
    ORDER BY Department.DepartmentName
END


-- ========================================
-- Select Department by Primary Key
-- ========================================
CREATE OR ALTER PROCEDURE PR_Department_SelectByPK
    @DepartmentID INT
AS
BEGIN
    SELECT 
        Department.DepartmentID,
        Department.DepartmentName,
        Department.[Description],
        Department.IsActive,
        Department.Modified,
        Department.UserID,
        Department.Created
    FROM Department
    WHERE Department.DepartmentID = @DepartmentID
    ORDER BY Department.DepartmentName
END


-- ========================================
-- Delete Department
-- ========================================
CREATE OR ALTER PROCEDURE PR_Department_DeleteByPK
    @DepartmentID INT
AS
BEGIN
    DELETE FROM Department
    WHERE Department.DepartmentID = @DepartmentID
END

