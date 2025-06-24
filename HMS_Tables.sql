
CREATE TABLE [User] (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    [Password] NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    MobileNo NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME NOT NULL
);

CREATE TABLE Department (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(250) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    UserID INT NOT NULL,
    CONSTRAINT FK_Department_User FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

CREATE TABLE Doctor (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Qualification NVARCHAR(100) NOT NULL,
    Specialization NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    UserID INT NOT NULL,
    CONSTRAINT FK_Doctor_User FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

CREATE TABLE DoctorDepartment (
    DoctorDepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DoctorID INT NOT NULL,
    DepartmentID INT NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    UserID INT NOT NULL,
    CONSTRAINT FK_DoctorDepartment_Doctor FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID),
    CONSTRAINT FK_DoctorDepartment_Department FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID),
    CONSTRAINT FK_DoctorDepartment_User FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

CREATE TABLE Patient (
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    DateOfBirth DATETIME NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(100) NOT NULL,
    Address NVARCHAR(250) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    State NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    UserID INT NOT NULL,
    CONSTRAINT FK_Patient_User FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

CREATE TABLE Appointment (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    DoctorID INT NOT NULL,
    PatientID INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    AppointmentStatus NVARCHAR(20) NOT NULL,
    [Description] NVARCHAR(250) NOT NULL,
    SpecialRemarks NVARCHAR(100) NOT NULL,
    Created DATETIME NOT NULL DEFAULT GETDATE(),
    Modified DATETIME NOT NULL,
    UserID INT NOT NULL,
    TotalConsultedAmount DECIMAL(5,2) NULL,
    CONSTRAINT FK_Appointment_Doctor FOREIGN KEY (DoctorID) REFERENCES Doctor(DoctorID),
    CONSTRAINT FK_Appointment_Patient FOREIGN KEY (PatientID) REFERENCES Patient(PatientID),
    CONSTRAINT FK_Appointment_User FOREIGN KEY (UserID) REFERENCES [User](UserID)
);