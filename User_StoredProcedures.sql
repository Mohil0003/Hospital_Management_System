-- Add User
CREATE OR ALTER PROCEDURE PR_User_User_AddUser
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @MobileNo NVARCHAR(100),
    @IsActive BIT = 1,
    @Modified DATETIME
AS
BEGIN
    INSERT INTO [User] (UserName, [Password], Email, MobileNo, IsActive, Modified)
    VALUES (@UserName, @Password, @Email, @MobileNo, @IsActive, @Modified)
END



-- Edit User
CREATE OR ALTER PROCEDURE PR_User_User_EditUser
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
    SET UserName = @UserName,
        [Password] = @Password,
        Email = @Email,
        MobileNo = @MobileNo,
        IsActive = @IsActive,
        Modified = @Modified
    WHERE UserID = @UserID
END



-- Get All Users
CREATE OR ALTER PROCEDURE PR_User_User_GetAllUsers
AS
BEGIN
    SELECT 
        UserID,
        UserName,
        [Password],
        Email,
        MobileNo,
        IsActive,
        Created,
        Modified
    FROM [User]
END


-- Delete User
CREATE OR ALTER PROCEDURE PR_User_User_DeleteUser
    @UserID INT
AS
BEGIN
    DELETE FROM [User]
    WHERE UserID = @UserID
END



-- Select User by Primary Key
CREATE OR ALTER PROCEDURE PR_User_User_SelectUserByPrimaryKey
    @UserID INT
AS
BEGIN
    SELECT 
        UserID,
        UserName,
        [Password],
        Email,
        MobileNo,
        IsActive,
        Created,
        Modified
    FROM [User]
    WHERE UserID = @UserID
END


