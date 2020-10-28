USE master
GO

CREATE DATABASE FeedBackCollectionDB
GO

USE [FeedBackCollectionDB]
GO
/****** Object:  Table [dbo].[Membership]    Script Date: 26/9/2020 4:42:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership](
	[UserCode] [varchar](36) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionType] [varchar](6) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[IsLockedOut] [numeric](1, 0) NOT NULL,
	[IsFirstLogin] [numeric](1, 0) NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangeDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPassAtmptCount] [numeric](3, 0) NOT NULL,
	[FailedPassAnsAtmptCount] [numeric](3, 0) NOT NULL,
	[PasswordSalt] [varchar](256) NOT NULL,
	[Email] [varchar](256) NOT NULL,
	[PasswordQuestion] [varchar](256) NULL,
	[PasswordAnswer] [varchar](256) NULL,
	[Comment] [varchar](256) NULL,
 CONSTRAINT [Membership_PK] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Module]    Script Date: 26/9/2020 4:42:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Module](
	[ModuleId] [varchar](36) NOT NULL,
	[UserCode] [varchar](36) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionType] [varchar](6) NOT NULL,
	[ModuleName] [varchar](256) NOT NULL,
	[SortOrder] [numeric](3, 0) NOT NULL,
	[ModuleShortName] [varchar](10) NULL,
 CONSTRAINT [Module_PK] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 26/9/2020 4:42:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[UserCode] [varchar](36) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionType] [varchar](6) NOT NULL,
	[RoleId] [varchar](36) NOT NULL,
	[RoleName] [varchar](256) NOT NULL,
 CONSTRAINT [Role_PK] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 26/9/2020 4:42:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserCode] [varchar](36) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionType] [varchar](6) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[UserName] [varchar](256) NOT NULL,
	[EmailVerified] [int] NULL,
	[RequestedOTP] [varchar](36) NULL,
 CONSTRAINT [User_PK] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserComments]    Script Date: 26/9/2020 4:42:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserComments](
	[CommentsCode] [varchar](36) NOT NULL,
	[UserCode] [varchar](36) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionType] [varchar](6) NOT NULL,
	[Comments] [varchar](max) NULL,
	[PostCode] [varchar](36) NOT NULL,
 CONSTRAINT [UserComments_PK] PRIMARY KEY CLUSTERED 
(
	[CommentsCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 26/9/2020 4:42:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserCode] [varchar](36) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionType] [varchar](6) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[UserFullName] [varchar](100) NULL,
 CONSTRAINT [UserInfo_PK] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInRole]    Script Date: 26/9/2020 4:42:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInRole](
	[UserCode] [varchar](36) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionType] [varchar](6) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[RoleId] [varchar](36) NOT NULL,
	[UserRoleId] [varchar](36) NOT NULL,
	[IsDeleted] [numeric](1, 0) NOT NULL,
 CONSTRAINT [UserInRole_PK] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPassword]    Script Date: 26/9/2020 4:42:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPassword](
	[UserCode] [varchar](36) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionType] [varchar](6) NOT NULL,
	[UserId] [varchar](36) NOT NULL,
	[Password] [varchar](256) NOT NULL,
 CONSTRAINT [UserPassword_PK] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ActionDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPost]    Script Date: 26/9/2020 4:42:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPost](
	[PostCode] [varchar](36) NOT NULL,
	[UserCode] [varchar](36) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionType] [varchar](6) NOT NULL,
	[Post] [varchar](max) NULL,
 CONSTRAINT [UserPost_PK] PRIMARY KEY CLUSTERED 
(
	[PostCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserVotes]    Script Date: 26/9/2020 4:42:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserVotes](
	[VotesCode] [varchar](36) NOT NULL,
	[UserCode] [varchar](36) NOT NULL,
	[ActionDate] [datetime] NOT NULL,
	[ActionType] [varchar](6) NOT NULL,
	[VoteType] [numeric](1, 0) NOT NULL,
	[CommentsCode] [varchar](36) NOT NULL,
 CONSTRAINT [UserVotes_PK] PRIMARY KEY CLUSTERED 
(
	[VotesCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Membership] ([UserCode], [ActionDate], [ActionType], [UserId], [IsLockedOut], [IsFirstLogin], [LastLoginDate], [LastPasswordChangeDate], [LastLockoutDate], [FailedPassAtmptCount], [FailedPassAnsAtmptCount], [PasswordSalt], [Email], [PasswordQuestion], [PasswordAnswer], [Comment]) VALUES (N'59012642-530A-42C1-AA6C-933D8B4F6A43', CAST(N'2020-10-27T12:54:55.293' AS DateTime), N'Insert', N'23c76bb8-da9d-4a51-ba6d-6f262a2adcdd', CAST(0 AS Numeric(1, 0)), CAST(1 AS Numeric(1, 0)), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(0 AS Numeric(3, 0)), CAST(0 AS Numeric(3, 0)), N'TwBRAFUAQgBLAEwATABYAEkA', N'raju@mail.com', N'abc', N'80-1F-C6-FA-41-4A-44-CC-49-45-AF-DB-5B-E8-38-5E', NULL)
GO
INSERT [dbo].[Membership] ([UserCode], [ActionDate], [ActionType], [UserId], [IsLockedOut], [IsFirstLogin], [LastLoginDate], [LastPasswordChangeDate], [LastLockoutDate], [FailedPassAtmptCount], [FailedPassAnsAtmptCount], [PasswordSalt], [Email], [PasswordQuestion], [PasswordAnswer], [Comment]) VALUES (N'09D1CE99-B364-487B-A218-643A1D1C9BCB', CAST(N'2020-10-27T15:59:57.390' AS DateTime), N'Insert', N'70e4957b-8aaf-4a54-a1ac-043bbe0f363a', CAST(0 AS Numeric(1, 0)), CAST(1 AS Numeric(1, 0)), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(0 AS Numeric(3, 0)), CAST(0 AS Numeric(3, 0)), N'RgBCAFgATQBLAFMASgBOAFQA', N'r@gmail.com', N'abc', N'FD-25-AE-2B-9A-E6-4D-7C-3F-83-4F-95-AB-93-7D-54', NULL)
GO
INSERT [dbo].[Membership] ([UserCode], [ActionDate], [ActionType], [UserId], [IsLockedOut], [IsFirstLogin], [LastLoginDate], [LastPasswordChangeDate], [LastLockoutDate], [FailedPassAtmptCount], [FailedPassAnsAtmptCount], [PasswordSalt], [Email], [PasswordQuestion], [PasswordAnswer], [Comment]) VALUES (N'8055EC47-EDE4-47AB-851B-0B007ACE90A7', CAST(N'2020-10-27T15:28:33.313' AS DateTime), N'Insert', N'a1c64f77-7e6c-4fe8-9658-0392502143ac', CAST(0 AS Numeric(1, 0)), CAST(1 AS Numeric(1, 0)), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(0 AS Numeric(3, 0)), CAST(0 AS Numeric(3, 0)), N'VwBOAEoATABIAEYAVgA=', N'raju.329@gmail.com', N'abc', N'81-F6-A9-E2-47-3E-DC-DE-10-78-42-3E-60-69-3E-C5', NULL)
GO
INSERT [dbo].[Membership] ([UserCode], [ActionDate], [ActionType], [UserId], [IsLockedOut], [IsFirstLogin], [LastLoginDate], [LastPasswordChangeDate], [LastLockoutDate], [FailedPassAtmptCount], [FailedPassAnsAtmptCount], [PasswordSalt], [Email], [PasswordQuestion], [PasswordAnswer], [Comment]) VALUES (N'90B721D8-1EA4-43C0-AC1F-F0298842100D', CAST(N'2020-10-27T13:29:59.397' AS DateTime), N'Insert', N'e1366d2b-a265-4931-b4c0-0b1b90a52d48', CAST(0 AS Numeric(1, 0)), CAST(1 AS Numeric(1, 0)), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(N'1800-01-01T00:00:00.000' AS DateTime), CAST(0 AS Numeric(3, 0)), CAST(0 AS Numeric(3, 0)), N'RQBQAFYASQA=', N'rahmed@hotmail.com', N'abc', N'A0-CC-F7-BC-04-12-D7-D0-23-68-F6-08-9A-6A-56-5B', NULL)
GO
INSERT [dbo].[Role] ([UserCode], [ActionDate], [ActionType], [RoleId], [RoleName]) VALUES (N'0E2C7D4B-B442-4A75-8D53-581EA847C0CB', CAST(N'2020-10-27' AS DateTime), N'INSERT', N'0025FA6B-BC0D-4989-8A0A-F5B4200E9915', N'BasicUser')
GO
INSERT [dbo].[Role] ([UserCode], [ActionDate], [ActionType], [RoleId], [RoleName]) VALUES (N'0E2C7D4B-B442-4A75-8D53-581EA847C0CB', CAST(N'2020-10-27' AS DateTime), N'INSERT', N'EA3FDBAE-CC08-4FE0-8EB6-95DB14985D49', N'SysAdmin')
GO
INSERT [dbo].[User] ([UserCode], [ActionDate], [ActionType], [UserId], [UserName], [EmailVerified], [RequestedOTP]) VALUES (N'8E17B0AE-E17D-4372-AB79-B879959F7E4A', CAST(N'2020-10-27' AS DateTime), N'Insert', N'23C76BB8-DA9D-4A51-BA6D-6F262A2ADCDD', N'raju@mail.com', NULL, NULL)
GO
INSERT [dbo].[User] ([UserCode], [ActionDate], [ActionType], [UserId], [UserName], [EmailVerified], [RequestedOTP]) VALUES (N'CEDC2F95-920B-478C-AF31-9EB9BD45F006', CAST(N'2020-10-27' AS DateTime), N'Insert', N'70E4957B-8AAF-4A54-A1AC-043BBE0F363A', N'kajol', NULL, NULL)
GO
INSERT [dbo].[User] ([UserCode], [ActionDate], [ActionType], [UserId], [UserName], [EmailVerified], [RequestedOTP]) VALUES (N'BD052CE7-B3F6-4213-A3D7-AF5A96E6715B', CAST(N'2020-10-27' AS DateTime), N'Insert', N'A1C64F77-7E6C-4FE8-9658-0392502143AC', N'raju', NULL, NULL)
GO
INSERT [dbo].[User] ([UserCode], [ActionDate], [ActionType], [UserId], [UserName], [EmailVerified], [RequestedOTP]) VALUES (N'55A9FAF0-DAF0-4420-B50F-0BE9F49B938E', CAST(N'2020-10-27' AS DateTime), N'Insert', N'E1366D2B-A265-4931-B4C0-0B1B90A52D48', N'ahmed.raju', NULL, NULL)
GO
INSERT [dbo].[UserComments] ([CommentsCode], [UserCode], [ActionDate], [ActionType], [Comments], [PostCode]) VALUES (N'0F57FE51-30B5-444C-A927-AB6C5872B2F9', N'E1366D2B-A265-4931-B4C0-0B1B90A52D48', CAST(N'2020-10-27' AS DateTime), N'INSERT', N'Hello, This is is Post', N'069B698E-E339-4F12-877B-3447814F50F2')
GO
INSERT [dbo].[UserComments] ([CommentsCode], [UserCode], [ActionDate], [ActionType], [Comments], [PostCode]) VALUES (N'C414215C-5111-4C40-B74D-9376859BCB35', N'A1C64F77-7E6C-4FE8-9658-0392502143AC', CAST(N'2020-10-27' AS DateTime), N'INSERT', N'Hello, This is my comment from User-2', N'DA4C217B-8723-4777-8D3F-FD7DA3C9FD0A')
GO
INSERT [dbo].[UserInfo] ([UserCode], [ActionDate], [ActionType], [UserId], [UserFullName]) VALUES (N'23c76bb8-da9d-4a51-ba6d-6f262a2adcdd', CAST(N'2020-10-27' AS DateTime), N'Insert', N'23C76BB8-DA9D-4A51-BA6D-6F262A2ADCDD', N'  ahmed  raju')
GO
INSERT [dbo].[UserInfo] ([UserCode], [ActionDate], [ActionType], [UserId], [UserFullName]) VALUES (N'70e4957b-8aaf-4a54-a1ac-043bbe0f363a', CAST(N'2020-10-27' AS DateTime), N'Insert', N'70E4957B-8AAF-4A54-A1AC-043BBE0F363A', N'kajol')
GO
INSERT [dbo].[UserInfo] ([UserCode], [ActionDate], [ActionType], [UserId], [UserFullName]) VALUES (N'a1c64f77-7e6c-4fe8-9658-0392502143ac', CAST(N'2020-10-27' AS DateTime), N'Insert', N'A1C64F77-7E6C-4FE8-9658-0392502143AC', N'raju')
GO
INSERT [dbo].[UserInfo] ([UserCode], [ActionDate], [ActionType], [UserId], [UserFullName]) VALUES (N'e1366d2b-a265-4931-b4c0-0b1b90a52d48', CAST(N'2020-10-27' AS DateTime), N'Insert', N'E1366D2B-A265-4931-B4C0-0B1B90A52D48', N'ahmed raju')
GO
INSERT [dbo].[UserInRole] ([UserCode], [ActionDate], [ActionType], [UserId], [RoleId], [UserRoleId], [IsDeleted]) VALUES (N'23c76bb8-da9d-4a51-ba6d-6f262a2adcdd', CAST(N'2020-10-27' AS DateTime), N'INSERT', N'23c76bb8-da9d-4a51-ba6d-6f262a2adcdd', N'EA3FDBAE-CC08-4FE0-8EB6-95DB14985D49', N'5AE01E38-AAE0-4CD6-9C09-63B637E761FA', CAST(0 AS Numeric(1, 0)))
GO
INSERT [dbo].[UserInRole] ([UserCode], [ActionDate], [ActionType], [UserId], [RoleId], [UserRoleId], [IsDeleted]) VALUES (N'70e4957b-8aaf-4a54-a1ac-043bbe0f363a', CAST(N'2020-10-27' AS DateTime), N'Insert', N'70e4957b-8aaf-4a54-a1ac-043bbe0f363a', N'0025FA6B-BC0D-4989-8A0A-F5B4200E9915', N'66A2FBBE-C404-4B47-8C28-140FC7D3243C', CAST(0 AS Numeric(1, 0)))
GO
INSERT [dbo].[UserInRole] ([UserCode], [ActionDate], [ActionType], [UserId], [RoleId], [UserRoleId], [IsDeleted]) VALUES (N'e1366d2b-a265-4931-b4c0-0b1b90a52d48', CAST(N'2020-10-27' AS DateTime), N'Insert', N'e1366d2b-a265-4931-b4c0-0b1b90a52d48', N'0025FA6B-BC0D-4989-8A0A-F5B4200E9915', N'75DCDFBC-1359-44C6-8BB0-2F0F0A52FA06', CAST(0 AS Numeric(1, 0)))
GO
INSERT [dbo].[UserInRole] ([UserCode], [ActionDate], [ActionType], [UserId], [RoleId], [UserRoleId], [IsDeleted]) VALUES (N'a1c64f77-7e6c-4fe8-9658-0392502143ac', CAST(N'2020-10-27' AS DateTime), N'Insert', N'a1c64f77-7e6c-4fe8-9658-0392502143ac', N'0025FA6B-BC0D-4989-8A0A-F5B4200E9915', N'EF4AE3B8-46AA-4614-8FBE-03DF3A1610E5', CAST(0 AS Numeric(1, 0)))
GO
INSERT [dbo].[UserPassword] ([UserCode], [ActionDate], [ActionType], [UserId], [Password]) VALUES (N'3070B117-0AD4-40AF-9CE0-AA39AD35A7C9', CAST(N'2020-10-27' AS DateTime), N'Insert', N'23c76bb8-da9d-4a51-ba6d-6f262a2adcdd', N'7C-E2-1E-AD-98-40-A5-C8-43-27-EB-72-1C-1B-FB-21')
GO
INSERT [dbo].[UserPassword] ([UserCode], [ActionDate], [ActionType], [UserId], [Password]) VALUES (N'4AA5F0A5-9A35-4D80-961D-8A8F5FE74DEB', CAST(N'2020-10-27' AS DateTime), N'Insert', N'70e4957b-8aaf-4a54-a1ac-043bbe0f363a', N'B9-78-AF-51-56-6F-2F-3E-D7-68-F5-B9-36-EA-89-5D')
GO
INSERT [dbo].[UserPassword] ([UserCode], [ActionDate], [ActionType], [UserId], [Password]) VALUES (N'E566BAB5-FE46-4954-9CEA-37D418CB12D8', CAST(N'2020-10-27' AS DateTime), N'Insert', N'a1c64f77-7e6c-4fe8-9658-0392502143ac', N'8C-A2-C8-C3-88-91-CD-F3-FA-70-82-53-4E-45-DF-A3')
GO
INSERT [dbo].[UserPassword] ([UserCode], [ActionDate], [ActionType], [UserId], [Password]) VALUES (N'68B498CD-9C80-4430-BDA5-0D0F2F21EB3B', CAST(N'2020-10-27' AS DateTime), N'Insert', N'e1366d2b-a265-4931-b4c0-0b1b90a52d48', N'0D-8F-70-E8-91-E2-57-D8-CC-ED-65-EA-6B-1F-C4-86')
GO
INSERT [dbo].[UserPost] ([PostCode], [UserCode], [ActionDate], [ActionType], [Post]) VALUES (N'069B698E-E339-4F12-877B-3447814F50F2', N'E1366D2B-A265-4931-B4C0-0B1B90A52D48', CAST(N'2020-10-27' AS DateTime), N'INSERT', N'Hello, This is my another Post in this app')
GO
INSERT [dbo].[UserPost] ([PostCode], [UserCode], [ActionDate], [ActionType], [Post]) VALUES (N'DA4C217B-8723-4777-8D3F-FD7DA3C9FD0A', N'A1C64F77-7E6C-4FE8-9658-0392502143AC', CAST(N'2020-10-27' AS DateTime), N'INSERT', N'Hi, This is post by User-2')
GO
INSERT [dbo].[UserVotes] ([VotesCode], [UserCode], [ActionDate], [ActionType], [VoteType], [CommentsCode]) VALUES (N'647B0011-FF94-4DDA-BAA7-ACDD280E8C19', N'E1366D2B-A265-4931-B4C0-0B1B90A52D48', CAST(N'2020-10-27' AS DateTime), N'INSERT', CAST(1 AS Numeric(1, 0)), N'0F57FE51-30B5-444C-A927-AB6C5872B2F9')
GO
INSERT [dbo].[UserVotes] ([VotesCode], [UserCode], [ActionDate], [ActionType], [VoteType], [CommentsCode]) VALUES (N'69DF58C5-E087-4B6B-99E5-2B0AB6236EB8', N'E1366D2B-A265-4931-B4C0-0B1B90A52D48', CAST(N'2020-10-27' AS DateTime), N'INSERT', CAST(2 AS Numeric(1, 0)), N'0F57FE51-30B5-444C-A927-AB6C5872B2F9')
GO
INSERT [dbo].[UserVotes] ([VotesCode], [UserCode], [ActionDate], [ActionType], [VoteType], [CommentsCode]) VALUES (N'A2635289-2B9C-4370-A225-ED5C4BC89BF2', N'A1C64F77-7E6C-4FE8-9658-0392502143AC', CAST(N'2020-10-27' AS DateTime), N'INSERT', CAST(1 AS Numeric(1, 0)), N'C414215C-5111-4C40-B74D-9376859BCB35')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Module_UK1]    Script Date: 26/9/2020 4:42:51 pm ******/
ALTER TABLE [dbo].[Module] ADD  CONSTRAINT [Module_UK1] UNIQUE NONCLUSTERED 
(
	[ModuleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Role_UK1]    Script Date: 26/9/2020 4:42:51 pm ******/
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [Role_UK1] UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [User_UK1]    Script Date: 26/9/2020 4:42:51 pm ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [User_UK1] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD  CONSTRAINT [Membership_FK2] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Membership] CHECK CONSTRAINT [Membership_FK2]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [UserInfo_FK1] FOREIGN KEY([UserCode])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [UserInfo_FK1]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [UserInfo_FK2] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [UserInfo_FK2]
GO
ALTER TABLE [dbo].[UserInRole]  WITH CHECK ADD  CONSTRAINT [UserInRole_FK1] FOREIGN KEY([UserCode])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserInRole] CHECK CONSTRAINT [UserInRole_FK1]
GO
ALTER TABLE [dbo].[UserInRole]  WITH CHECK ADD  CONSTRAINT [UserInRole_FK2] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserInRole] CHECK CONSTRAINT [UserInRole_FK2]
GO
ALTER TABLE [dbo].[UserInRole]  WITH CHECK ADD  CONSTRAINT [UserInRole_FK3] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[UserInRole] CHECK CONSTRAINT [UserInRole_FK3]
GO
ALTER TABLE [dbo].[UserPassword]  WITH CHECK ADD  CONSTRAINT [UserPassword_FK2] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserPassword] CHECK CONSTRAINT [UserPassword_FK2]
GO
