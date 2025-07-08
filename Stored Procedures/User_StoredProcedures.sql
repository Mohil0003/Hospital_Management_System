-- ========================================
-- Add User
-- ========================================
CREATE OR ALTER PROCEDURE PR_User_AddUser
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @MobileNo NVARCHAR(100),
    @IsActive BIT = 1,
    @Modified DATETIME
AS
BEGIN
    INSERT INTO [User] 
    (
        UserName, 
        [Password], 
        Email, 
        MobileNo, 
        IsActive, 
        Modified
    )
    VALUES 
    (
        @UserName, 
        @Password, 
        @Email, 
        @MobileNo, 
        @IsActive, 
        @Modified
    )
END


-- ========================================
-- Edit User
-- ========================================
CREATE OR ALTER PROCEDURE PR_User_EditUser
    @UserID INT,
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @MobileNo NVARCHAR(100),
    @IsActive BIT,
    @Modified DATETIME
AS
BEGIN
    UPDATE [User]
    SET 
        [User].UserName = @UserName,
        [User].[Password] = @Password,
        [User].Email = @Email,
        [User].MobileNo = @MobileNo,
        [User].IsActive = @IsActive,
        [User].Modified = @Modified
    WHERE [User].UserID = @UserID
END


-- ========================================
-- Select All Users
-- ========================================
CREATE OR ALTER PROCEDURE PR_User_SelectAll
AS
BEGIN
    SELECT 
        [User].UserID,
        [User].UserName,
        [User].[Password],
        [User].Email,
        [User].MobileNo,
        [User].IsActive,
        [User].Created,
        [User].Modified
    FROM [User]
    ORDER BY [User].UserName
END


-- ========================================
-- Select User by Primary Key
-- ========================================
CREATE OR ALTER PROCEDURE PR_User_SelectByPK
    @UserID INT
AS
BEGIN
    SELECT 
        [User].UserID,
        [User].UserName,
        [User].[Password],
        [User].Email,
        [User].MobileNo,
        [User].IsActive,
        [User].Created,
        [User].Modified
    FROM [User]
    WHERE [User].UserID = @UserID
    ORDER BY [User].UserName
END


-- ========================================
-- Delete User
-- ========================================
CREATE OR ALTER PROCEDURE PR_User_DeleteByPK
    @UserID INT
AS
BEGIN
    DELETE FROM [User]
    WHERE [User].UserID = @UserID
END


