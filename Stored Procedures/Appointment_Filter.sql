CREATE OR ALTER PROC [dbo].[PR_Appointment_GetAll_With_Search]
	@DoctorName NVARCHAR(100) = NULL,
	@Name NVARCHAR(100) = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
	@AppointmentStatus NVARCHAR(20) = NULL
AS
BEGIN
	SELECT
		[dbo].[Appointment].[AppointmentID],
		[dbo].[Appointment].[DoctorID],
		[dbo].[Doctor].[Name],
		[dbo].[Appointment].[PatientID],
		[dbo].[Patient].[Name],
		[dbo].[Appointment].[AppointmentDate],
		[dbo].[Appointment].[AppointmentStatus],
		[dbo].[Appointment].[Description],
		[dbo].[Appointment].[SpecialRemarks],
		[dbo].[Appointment].[Created],
		[dbo].[Appointment].[Modified],
		[dbo].[Appointment].[UserID],
		[dbo].[Appointment].[TotalConsultedAmount]
	FROM [dbo].[Appointment]
	INNER JOIN
	[dbo].[Doctor] 
	ON [dbo].[Appointment].[DoctorID] = [dbo].[Doctor].[DoctorID]
	INNER JOIN
	[dbo].[Patient] 
	ON [dbo].[Appointment].[PatientID] = [dbo].[Patient].[PatientID]
	WHERE 
	(@DoctorName Is NULL OR [dbo].[Doctor].[Name] LIKE '%' + @DoctorName + '%')
	AND
	(@Name Is NULL OR [dbo].[Patient].[Name] LIKE '%' + @Name + '%')
	AND  
	(@StartDate IS NULL OR CAST([dbo].[Appointment].[AppointmentDate] AS DATE) >= @StartDate)
	AND
    (@EndDate IS NULL OR CAST([dbo].[Appointment].[AppointmentDate] AS DATE) <= @EndDate)	
	AND
	(@AppointmentStatus Is NULL OR [dbo].[Appointment].[AppointmentStatus] = @AppointmentStatus)
END