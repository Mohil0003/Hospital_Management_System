-- ========================================
-- Add DoctorDepartment
-- ========================================
CREATE OR ALTER PROCEDURE PR_DoctorDepartment_Add
    @DoctorID INT,
    @DepartmentID INT,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    INSERT INTO DoctorDepartment (
        DoctorID,
        DepartmentID,
        Modified,
        UserID
    )
    VALUES (
        @DoctorID,
        @DepartmentID,
        @Modified,
        @UserID
    )
END


-- ========================================
-- Edit DoctorDepartment
-- ========================================
CREATE OR ALTER PROCEDURE PR_DoctorDepartment_Edit
    @DoctorDepartmentID INT,
    @DoctorID INT,
    @DepartmentID INT,
    @Modified DATETIME,
    @UserID INT
AS
BEGIN
    UPDATE DoctorDepartment
    SET 
        DoctorDepartment.DoctorID = @DoctorID,
        DoctorDepartment.DepartmentID = @DepartmentID,
        DoctorDepartment.Modified = @Modified,
        DoctorDepartment.UserID = @UserID
    WHERE DoctorDepartment.DoctorDepartmentID = @DoctorDepartmentID
END


-- ========================================
-- Select All DoctorDepartments
-- ========================================
CREATE OR ALTER PROCEDURE PR_DoctorDepartment_SelectAll
AS
BEGIN
    SELECT 
        DoctorDepartment.DoctorDepartmentID,
        DoctorDepartment.DoctorID,
        DoctorDepartment.DepartmentID,
        DoctorDepartment.Created,
        DoctorDepartment.Modified,
        DoctorDepartment.UserID
    FROM DoctorDepartment
    ORDER BY DoctorDepartment.DoctorDepartmentID
END


-- ========================================
-- Select DoctorDepartment by Primary Key
-- ========================================
CREATE OR ALTER PROCEDURE PR_DoctorDepartment_SelectByPK
    @DoctorDepartmentID INT
AS
BEGIN
    SELECT 
        DoctorDepartment.DoctorDepartmentID,
        DoctorDepartment.DoctorID,
        DoctorDepartment.DepartmentID,
        DoctorDepartment.Created,
        DoctorDepartment.Modified,
        DoctorDepartment.UserID
    FROM DoctorDepartment
    WHERE DoctorDepartment.DoctorDepartmentID = @DoctorDepartmentID
    ORDER BY DoctorDepartment.DoctorDepartmentID
END


-- ========================================
-- Delete DoctorDepartment
-- ========================================
CREATE OR ALTER PROCEDURE PR_DoctorDepartment_DeleteByPK
    @DoctorDepartmentID INT
AS
BEGIN
    DELETE FROM DoctorDepartment
    WHERE DoctorDepartment.DoctorDepartmentID = @DoctorDepartmentID
END



