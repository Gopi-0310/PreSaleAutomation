USE [PreSalesAutomation]
GO
/****** Object:  UserDefinedTableType [dbo].[ProjectTechnologyMapping]    Script Date: 25-10-2023 12:36:24 ******/
CREATE TYPE [dbo].[ProjectTechnologyMapping] AS TABLE(
	[ProjectId] [uniqueidentifier] NULL,
	[TechnologyId] [uniqueidentifier] NULL,
	[CurrentStatus] [varchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ProjectUserRoleMapping]    Script Date: 25-10-2023 12:36:24 ******/
CREATE TYPE [dbo].[ProjectUserRoleMapping] AS TABLE(
	[ProjectId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[RoleId] [uniqueidentifier] NULL,
	[CurrentStatus] [varchar](50) NULL
)
GO
/****** Object:  Table [dbo].[CapacityUtilization]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CapacityUtilization](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[Hours] [float] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__Capacity__3214EC0768CAB3CB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PresalesTimeTracker]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PresalesTimeTracker](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Activity] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Hours] [float] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[ActivityDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__Presales__3214EC07C782C31F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ProjectTypeId] [uniqueidentifier] NOT NULL,
	[EffectiveStartDate] [datetime] NOT NULL,
	[EffectiveEndDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__Project__3214EC07AEF15139] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectTechMapping]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTechMapping](
	[Id] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[TechnologyId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NULL,
	[CurrentStatus] [varchar](255) NULL,
 CONSTRAINT [PK__ProjectT__3214EC07ECBB7E2C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectType]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__ProjectT__3214EC07F8D94D64] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectUserEstimationMapping]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectUserEstimationMapping](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
	[CurrentStatus] [varchar](255) NULL,
 CONSTRAINT [PK__ProjectU__3214EC07ED0CB615] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceRate]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceRate](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__Resource__3214EC078FF159B6] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Code] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__Role__3214EC07E6B3B717] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeamConfiguration]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamConfiguration](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__TeamConf__3214EC07FE656D56] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Technology]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Technology](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__Technolo__3214EC07305EFE88] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[EMail] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__User__3214EC07D500480F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkStream]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkStream](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
	[ProjectId] [uniqueidentifier] NULL,
 CONSTRAINT [PK__WorkStre__3214EC07AA424E9E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkStreamActivity]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkStreamActivity](
	[Id] [uniqueidentifier] NOT NULL,
	[Activity] [nvarchar](255) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[WorkStreamActivityId] [uniqueidentifier] NULL,
	[Hours] [float] NOT NULL,
	[Week] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
	[Description] [varchar](900) NULL,
	[ParentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK__WorkStre__3214EC0789C0A408] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CapacityUtilization] ([Id], [ProjectId], [RoleId], [Hours], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'87583818-cd66-4293-a452-03eec062b44d', N'e1b43adf-86a4-4cdf-b4cb-ee8c63d24654', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', 190, CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'87583818-cd66-4293-a452-03eec062b44d', NULL, NULL, 0)
GO
INSERT [dbo].[PresalesTimeTracker] ([Id], [UserId], [Activity], [Description], [Hours], [ProjectId], [ActivityDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'c830a76a-d91c-4b69-9314-4d44bf3f8edf', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'MedicalProject', N'SampleData', 160, N'e1b43adf-86a4-4cdf-b4cb-ee8c63d24654', CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'c830a76a-d91c-4b69-9314-4d44bf3f8edf', NULL, NULL, 0)
GO
INSERT [dbo].[Project] ([Id], [Name], [ProjectTypeId], [EffectiveStartDate], [EffectiveEndDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'44d9494c-70e5-4014-b190-08182b5b1395', N'Checkeight', N'da62651a-8f95-4252-a0e6-5516aa8794ea', CAST(N'2023-10-25T00:00:00.000' AS DateTime), CAST(N'2023-10-31T00:00:00.000' AS DateTime), CAST(N'2023-10-25T12:11:36.373' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
INSERT [dbo].[Project] ([Id], [Name], [ProjectTypeId], [EffectiveStartDate], [EffectiveEndDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'f8f2cf8c-05d7-4f6c-a6e7-314fbfd965da', N'Checkthree', N'0cafc569-e338-4e1d-886c-8b767365d0e8', CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(N'2023-11-30T00:00:00.000' AS DateTime), CAST(N'2023-10-21T21:21:29.320' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
INSERT [dbo].[Project] ([Id], [Name], [ProjectTypeId], [EffectiveStartDate], [EffectiveEndDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'cc4597e9-5524-4e50-a6c1-339d9f11fac1', N'Check two', N'0cafc569-e338-4e1d-886c-8b767365d0e8', CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(N'2023-11-09T00:00:00.000' AS DateTime), CAST(N'2023-10-21T18:37:38.433' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 1)
INSERT [dbo].[Project] ([Id], [Name], [ProjectTypeId], [EffectiveStartDate], [EffectiveEndDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'8e011c0f-a491-477b-9990-589b0bf79ccb', N'Chekcinf', N'6008394e-958c-45bd-806e-bd61303df08b', CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(N'2023-10-31T00:00:00.000' AS DateTime), CAST(N'2023-10-21T18:54:44.913' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 1)
INSERT [dbo].[Project] ([Id], [Name], [ProjectTypeId], [EffectiveStartDate], [EffectiveEndDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'90f4258f-38b0-496a-a982-79d4e7278cf8', N'delete', N'da62651a-8f95-4252-a0e6-5516aa8794ea', CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(N'2023-11-30T00:00:00.000' AS DateTime), CAST(N'2023-10-21T19:40:58.887' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 1)
INSERT [dbo].[Project] ([Id], [Name], [ProjectTypeId], [EffectiveStartDate], [EffectiveEndDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'96e4dfbe-7568-4f86-9c29-93badf707d9b', N'Checking', N'da62651a-8f95-4252-a0e6-5516aa8794ea', CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(N'2023-10-30T00:00:00.000' AS DateTime), CAST(N'2023-10-21T18:27:27.567' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 1)
INSERT [dbo].[Project] ([Id], [Name], [ProjectTypeId], [EffectiveStartDate], [EffectiveEndDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'5035903f-b248-4ad6-b786-a29d31d5c7ff', N'Checktwo', N'0cafc569-e338-4e1d-886c-8b767365d0e8', CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(N'2023-11-30T00:00:00.000' AS DateTime), CAST(N'2023-10-21T21:19:09.080' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
INSERT [dbo].[Project] ([Id], [Name], [ProjectTypeId], [EffectiveStartDate], [EffectiveEndDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'533c70fd-4be7-455a-b55a-db877473389a', N'Check four', N'6008394e-958c-45bd-806e-bd61303df08b', CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(N'2023-11-30T00:00:00.000' AS DateTime), CAST(N'2023-10-21T21:36:20.767' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
INSERT [dbo].[Project] ([Id], [Name], [ProjectTypeId], [EffectiveStartDate], [EffectiveEndDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'e1b43adf-86a4-4cdf-b4cb-ee8c63d24654', N'AHF', N'da62651a-8f95-4252-a0e6-5516aa8794ea', CAST(N'2023-10-01T00:00:00.000' AS DateTime), CAST(N'2024-03-10T00:00:00.000' AS DateTime), CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'e1b43adf-86a4-4cdf-b4cb-ee8c63d24654', NULL, NULL, 0)
INSERT [dbo].[Project] ([Id], [Name], [ProjectTypeId], [EffectiveStartDate], [EffectiveEndDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'688d4643-4a68-44bc-8063-f17c6e613002', N'check', N'8019c9b8-5da6-4011-ab08-13ad72e408aa', CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(N'2023-11-30T00:00:00.000' AS DateTime), CAST(N'2023-10-21T21:14:46.300' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
GO
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'ab8c01fd-585a-4475-8f1b-004df578ef1c', N'533c70fd-4be7-455a-b55a-db877473389a', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T21:36:20.770' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'4cdb41c8-2e1a-45c2-a65c-036d9609e262', N'533c70fd-4be7-455a-b55a-db877473389a', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T21:36:20.770' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'fa7934ab-85a2-46a9-a9e1-08fa6634bc09', N'96e4dfbe-7568-4f86-9c29-93badf707d9b', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T18:27:27.570' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'5c24c7f1-9bc4-43cd-bc65-148947de62af', N'f8f2cf8c-05d7-4f6c-a6e7-314fbfd965da', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T21:21:29.320' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'ca317a86-4bdf-47f3-a41e-3398c845e200', N'688d4643-4a68-44bc-8063-f17c6e613002', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T21:14:46.300' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'd5dee745-3c1e-405a-8f12-349450000ec2', N'cc4597e9-5524-4e50-a6c1-339d9f11fac1', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T18:37:38.440' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'179e5bba-4468-4c80-8bde-3b935e3acc86', N'5035903f-b248-4ad6-b786-a29d31d5c7ff', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T21:19:09.083' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'fd9abc78-3f0f-4f7b-b841-44ced8198e88', N'44d9494c-70e5-4014-b190-08182b5b1395', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-25T12:11:36.377' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'c593a82e-7d4e-4699-a8cb-5a28ad9cc94b', N'90f4258f-38b0-496a-a982-79d4e7278cf8', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T19:40:58.890' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'eeac9d6b-e3f1-427b-8e27-5ef89df0462e', N'5035903f-b248-4ad6-b786-a29d31d5c7ff', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T21:19:09.087' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'fc6421fd-d677-4d83-a668-78cc2dbf5dc2', N'cc4597e9-5524-4e50-a6c1-339d9f11fac1', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T18:37:38.437' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'1d7ce8b8-9a8c-408f-98a9-78e0152d2286', N'8e011c0f-a491-477b-9990-589b0bf79ccb', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T18:54:44.917' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'ba998cec-f8a0-4b75-832a-80b85b612d31', N'96e4dfbe-7568-4f86-9c29-93badf707d9b', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T18:27:27.573' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'c7ae3e08-c7c4-41ed-8739-b301d0f55ad8', N'e1b43adf-86a4-4cdf-b4cb-ee8c63d24654', N'1a21755e-8142-43c0-a5b6-acd61cba9ada', CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'c7ae3e08-c7c4-41ed-8739-b301d0f55ad8', NULL, NULL, 0, N'1')
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'56de7ffd-68a4-45d7-86dd-b5903a79973e', N'90f4258f-38b0-496a-a982-79d4e7278cf8', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T19:40:58.890' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'bbdba068-5386-4c70-9f7e-cdc302e37799', N'688d4643-4a68-44bc-8063-f17c6e613002', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T21:14:46.303' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'7e4be2ce-55a6-4c24-a160-d624186d9e3a', N'8e011c0f-a491-477b-9990-589b0bf79ccb', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T18:54:44.913' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'6f2e839d-fa20-4cd3-bd8a-d7c427d3c1dd', N'44d9494c-70e5-4014-b190-08182b5b1395', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-25T12:11:36.377' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
INSERT [dbo].[ProjectTechMapping] ([Id], [ProjectId], [TechnologyId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'a10b8d7c-a5a7-4cda-aa25-fc4c1a041ffe', N'f8f2cf8c-05d7-4f6c-a6e7-314fbfd965da', N'8e811294-5448-4908-88e8-2e26bccfec53', CAST(N'2023-10-21T21:21:29.323' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[ProjectType] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'8019c9b8-5da6-4011-ab08-13ad72e408aa', N'Billing', CAST(N'2023-09-21T00:00:00.000' AS DateTime), N'8019c9b8-5da6-4011-ab08-13ad72e408aa', NULL, NULL, 0)
INSERT [dbo].[ProjectType] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'da62651a-8f95-4252-a0e6-5516aa8794ea', N'Medical', CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'da62651a-8f95-4252-a0e6-5516aa8794ea', NULL, NULL, 0)
INSERT [dbo].[ProjectType] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'0cafc569-e338-4e1d-886c-8b767365d0e8', N'LawFrim', CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'0cafc569-e338-4e1d-886c-8b767365d0e8', NULL, NULL, 0)
INSERT [dbo].[ProjectType] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'6008394e-958c-45bd-806e-bd61303df08b', N'Ecommers', CAST(N'2023-10-17T15:05:29.210' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
GO
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'd4ea2da4-387f-49e2-af44-03ed6104bac6', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'5035903f-b248-4ad6-b786-a29d31d5c7ff', CAST(N'2023-10-21T21:19:09.083' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'c65ef82f-4722-4edd-a0cc-05f0bf8eb97b', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'90f4258f-38b0-496a-a982-79d4e7278cf8', CAST(N'2023-10-21T19:40:58.890' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'b6548af6-4364-40a1-b599-0e75b032a875', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'f8f2cf8c-05d7-4f6c-a6e7-314fbfd965da', CAST(N'2023-10-21T21:21:29.320' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'36540f10-f008-4d33-90f5-18062fc30479', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'e1b43adf-86a4-4cdf-b4cb-ee8c63d24654', CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'36540f10-f008-4d33-90f5-18062fc30479', NULL, NULL, 0, N'1')
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'8f816e34-7885-4d4a-a5bd-2021afc335bc', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'533c70fd-4be7-455a-b55a-db877473389a', CAST(N'2023-10-21T21:36:20.767' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'1a66e5dc-e0e3-4817-8251-2de64865f2d7', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'cc4597e9-5524-4e50-a6c1-339d9f11fac1', CAST(N'2023-10-21T18:37:38.433' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'dab044cf-f634-4bbc-b30b-33c701bea518', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'8e011c0f-a491-477b-9990-589b0bf79ccb', CAST(N'2023-10-21T18:54:44.913' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'095d36cc-4b84-4a7f-93d9-3579366e6405', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'5035903f-b248-4ad6-b786-a29d31d5c7ff', CAST(N'2023-10-21T21:19:09.083' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'444bb468-38ca-4ffd-9e69-43fd7dd70c2f', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'90f4258f-38b0-496a-a982-79d4e7278cf8', CAST(N'2023-10-21T19:40:58.890' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'876b6111-b94b-4af6-8edb-4f26637e750e', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'533c70fd-4be7-455a-b55a-db877473389a', CAST(N'2023-10-21T21:36:20.770' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'eb97bee2-df79-4ad5-9841-4ff5b7db582b', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'96e4dfbe-7568-4f86-9c29-93badf707d9b', CAST(N'2023-10-21T18:27:27.570' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'd6d65206-b92a-45a9-be70-6d2c0057d78f', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'96e4dfbe-7568-4f86-9c29-93badf707d9b', CAST(N'2023-10-21T18:27:27.570' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'99f0d632-6d73-471e-ad20-7bb5ad58d054', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'8e011c0f-a491-477b-9990-589b0bf79ccb', CAST(N'2023-10-21T18:54:44.917' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'efe85de5-f1b4-4ba0-8142-80e31490623b', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'44d9494c-70e5-4014-b190-08182b5b1395', CAST(N'2023-10-25T12:11:36.377' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'6ca2ba0c-d570-43a3-ba24-9787e4329d81', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'44d9494c-70e5-4014-b190-08182b5b1395', CAST(N'2023-10-25T12:11:36.373' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'828cf760-6b5e-4ecd-ab4c-ae5adf015b26', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'688d4643-4a68-44bc-8063-f17c6e613002', CAST(N'2023-10-21T21:14:46.300' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'f2a6d9bb-b6b2-4dea-aea4-b7523025ef53', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'cc4597e9-5524-4e50-a6c1-339d9f11fac1', CAST(N'2023-10-21T18:37:38.437' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'0b753a08-0883-4592-8a67-bacda97affe1', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'688d4643-4a68-44bc-8063-f17c6e613002', CAST(N'2023-10-21T21:14:46.300' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
INSERT [dbo].[ProjectUserEstimationMapping] ([Id], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [CurrentStatus]) VALUES (N'c4f7d6ca-8167-4dcb-94e6-f5ca42d902fe', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'f8f2cf8c-05d7-4f6c-a6e7-314fbfd965da', CAST(N'2023-10-21T21:21:29.320' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, NULL)
GO
INSERT [dbo].[ResourceRate] ([Id], [RoleId], [ProjectId], [Rate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'b3baa865-e6e3-4e2b-bf97-3d2e0b337e8a', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'e1b43adf-86a4-4cdf-b4cb-ee8c63d24654', CAST(600000.00 AS Decimal(18, 2)), CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'b3baa865-e6e3-4e2b-bf97-3d2e0b337e8a', NULL, NULL, 0)
GO
INSERT [dbo].[Role] ([Id], [Name], [Code], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'07810acc-8edb-41c9-a758-c2f277431774', N'Programmer', N'Pro', CAST(N'2023-09-19T22:58:55.740' AS DateTime), N'fc836240-9437-46d7-be32-9c6582fd751c', NULL, NULL, 1)
INSERT [dbo].[Role] ([Id], [Name], [Code], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'FullStackDeveloper', N'FSD', CAST(N'2023-10-17T15:03:10.163' AS DateTime), N'940f81c3-6159-4d82-99dc-5466da119332', NULL, NULL, 0)
GO
INSERT [dbo].[TeamConfiguration] ([Id], [UserId], [ProjectId], [RoleId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'8ac13b03-94e1-4ba0-94f0-d66eab625e4d', N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'e1b43adf-86a4-4cdf-b4cb-ee8c63d24654', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', CAST(N'2032-10-21T00:00:00.000' AS DateTime), N'8ac13b03-94e1-4ba0-94f0-d66eab625e4d', NULL, NULL, 0)
GO
INSERT [dbo].[Technology] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'8e811294-5448-4908-88e8-2e26bccfec53', N'spfx', CAST(N'2023-10-17T14:49:50.273' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
INSERT [dbo].[Technology] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'38c48b09-0234-4de3-a17a-40697998472c', N'SharePoint', CAST(N'2023-09-27T00:00:00.000' AS DateTime), N'38c48b09-0234-4de3-a17a-40697998472c', NULL, NULL, 0)
INSERT [dbo].[Technology] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'6e841646-9701-4c21-b295-5de1831f450f', N'spfx', CAST(N'2023-10-17T14:58:42.183' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
INSERT [dbo].[Technology] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'd6ada42f-3261-4af3-9b27-7d57167003d0', N'spfx', CAST(N'2023-10-17T14:55:16.783' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
INSERT [dbo].[Technology] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'88af90ee-3452-4694-8e71-7fb3c0be0bea', N'Php', CAST(N'2023-10-17T14:59:42.613' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
INSERT [dbo].[Technology] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'1a21755e-8142-43c0-a5b6-acd61cba9ada', N'Angular', CAST(N'2023-09-18T00:00:00.000' AS DateTime), N'1a21755e-8142-43c0-a5b6-acd61cba9ada', NULL, NULL, 0)
INSERT [dbo].[Technology] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'53b6a26f-35ab-4985-8990-b1245484bf8c', N'spfx', CAST(N'2023-10-17T14:56:58.270' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
INSERT [dbo].[Technology] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'f896d467-c1bb-4788-b119-c8d155011ded', N'sddasdad', CAST(N'2023-10-17T20:22:54.527' AS DateTime), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0)
GO
INSERT [dbo].[User] ([Id], [UserName],  [FullName], [RoleId], [EMail], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', N'Rakesh',  N'RakeshKrishna', N'07810acc-8edb-41c9-a758-c2f277431774', N'rakesh@gmail.com', CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'e031d1e5-5ca5-4557-afd7-be363eb08b8b', NULL, NULL, 0)
GO
INSERT [dbo].[WorkStream] ([Id], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [ProjectId]) VALUES (N'b472631f-6587-447f-9eec-9596ab742dd3', N'Sample', CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'b472631f-6587-447f-9eec-9596ab742dd3', NULL, NULL, 0, N'e1b43adf-86a4-4cdf-b4cb-ee8c63d24654')
GO
INSERT [dbo].[WorkStreamActivity] ([Id], [Activity], [RoleId], [WorkStreamActivityId], [Hours], [Week], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [Description], [ParentId]) VALUES (N'48589f55-679f-47f2-b953-7e38b3d4399d', N'ASSADAS', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'b472631f-6587-447f-9eec-9596ab742dd3', 5, 1, CAST(N'2023-10-21T18:05:00.990' AS DateTime), N'f0ca5ae5-fdbc-44cd-a0c8-400857c91111', NULL, NULL, 0, N'ASSADAS', NULL)
INSERT [dbo].[WorkStreamActivity] ([Id], [Activity], [RoleId], [WorkStreamActivityId], [Hours], [Week], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed], [Description], [ParentId]) VALUES (N'115eb1c8-ea3f-4c56-9785-bf122d35e385', N'Sample', N'43edcea9-8432-4abb-a99f-e39e22f80bd6', N'b472631f-6587-447f-9eec-9596ab742dd3', 200, 6, CAST(N'2023-10-21T00:00:00.000' AS DateTime), N'115eb1c8-ea3f-4c56-9785-bf122d35e385', NULL, NULL, 0, N'Sample', N'b472631f-6587-447f-9eec-9596ab742dd3')
GO
ALTER TABLE [dbo].[CapacityUtilization] ADD  CONSTRAINT [DF_CapacityUtilization_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[CapacityUtilization] ADD  CONSTRAINT [DF_CapacityUtilization_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[PresalesTimeTracker] ADD  CONSTRAINT [DF_PresalesTimeTracker_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[PresalesTimeTracker] ADD  CONSTRAINT [DF_PresalesTimeTracker_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_Project_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[ProjectTechMapping] ADD  CONSTRAINT [DF_ProjectTechMapping_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ProjectType] ADD  CONSTRAINT [DF_ProjectType_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ProjectType] ADD  CONSTRAINT [DF_ProjectType_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[ProjectUserEstimationMapping] ADD  CONSTRAINT [DF_ProjectUserEstimationMapping_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ProjectUserEstimationMapping] ADD  CONSTRAINT [DF_ProjectUserEstimationMapping_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[ResourceRate] ADD  CONSTRAINT [DF_ResourceRate_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ResourceRate] ADD  CONSTRAINT [DF_ResourceRate_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[TeamConfiguration] ADD  CONSTRAINT [DF_TeamConfiguration_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[TeamConfiguration] ADD  CONSTRAINT [DF_TeamConfiguration_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[Technology] ADD  CONSTRAINT [DF_Technology_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Technology] ADD  CONSTRAINT [DF_Technology_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[WorkStream] ADD  CONSTRAINT [DF_WorkStream_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[WorkStream] ADD  CONSTRAINT [DF_WorkStream_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[WorkStreamActivity] ADD  CONSTRAINT [DF_WorkStreamActivity_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[WorkStreamActivity] ADD  CONSTRAINT [DF_WorkStreamActivity_IsDestroyed]  DEFAULT ((0)) FOR [IsDestroyed]
GO
ALTER TABLE [dbo].[CapacityUtilization]  WITH CHECK ADD  CONSTRAINT [FK_CapacityUtilization_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[CapacityUtilization] CHECK CONSTRAINT [FK_CapacityUtilization_Project]
GO
ALTER TABLE [dbo].[CapacityUtilization]  WITH CHECK ADD  CONSTRAINT [FK_CapacityUtilization_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[CapacityUtilization] CHECK CONSTRAINT [FK_CapacityUtilization_Role]
GO
ALTER TABLE [dbo].[PresalesTimeTracker]  WITH CHECK ADD  CONSTRAINT [FK_PresalesTimeTracker_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[PresalesTimeTracker] CHECK CONSTRAINT [FK_PresalesTimeTracker_Project]
GO
ALTER TABLE [dbo].[PresalesTimeTracker]  WITH CHECK ADD  CONSTRAINT [FK_PresalesTimeTracker_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PresalesTimeTracker] CHECK CONSTRAINT [FK_PresalesTimeTracker_User]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_ProjectType] FOREIGN KEY([ProjectTypeId])
REFERENCES [dbo].[ProjectType] ([Id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_ProjectType]
GO
ALTER TABLE [dbo].[ProjectTechMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTechMapping_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectTechMapping] CHECK CONSTRAINT [FK_ProjectTechMapping_Project]
GO
ALTER TABLE [dbo].[ProjectTechMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTechMapping_Technology] FOREIGN KEY([TechnologyId])
REFERENCES [dbo].[Technology] ([Id])
GO
ALTER TABLE [dbo].[ProjectTechMapping] CHECK CONSTRAINT [FK_ProjectTechMapping_Technology]
GO
ALTER TABLE [dbo].[ProjectUserEstimationMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProjectUserEstimationMapping_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectUserEstimationMapping] CHECK CONSTRAINT [FK_ProjectUserEstimationMapping_Project]
GO
ALTER TABLE [dbo].[ProjectUserEstimationMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProjectUserEstimationMapping_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[ProjectUserEstimationMapping] CHECK CONSTRAINT [FK_ProjectUserEstimationMapping_Role]
GO
ALTER TABLE [dbo].[ProjectUserEstimationMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProjectUserEstimationMapping_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ProjectUserEstimationMapping] CHECK CONSTRAINT [FK_ProjectUserEstimationMapping_User]
GO
ALTER TABLE [dbo].[ResourceRate]  WITH CHECK ADD  CONSTRAINT [FK_ResourceRate_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ResourceRate] CHECK CONSTRAINT [FK_ResourceRate_Project]
GO
ALTER TABLE [dbo].[ResourceRate]  WITH CHECK ADD  CONSTRAINT [FK_ResourceRate_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[ResourceRate] CHECK CONSTRAINT [FK_ResourceRate_Role]
GO
ALTER TABLE [dbo].[TeamConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_TeamConfiguration_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[TeamConfiguration] CHECK CONSTRAINT [FK_TeamConfiguration_Project]
GO
ALTER TABLE [dbo].[TeamConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_TeamConfiguration_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[TeamConfiguration] CHECK CONSTRAINT [FK_TeamConfiguration_Role]
GO
ALTER TABLE [dbo].[TeamConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_TeamConfiguration_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[TeamConfiguration] CHECK CONSTRAINT [FK_TeamConfiguration_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[WorkStream]  WITH CHECK ADD  CONSTRAINT [FK_WorkStream_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[WorkStream] CHECK CONSTRAINT [FK_WorkStream_Project]
GO
ALTER TABLE [dbo].[WorkStreamActivity]  WITH CHECK ADD  CONSTRAINT [FK_WorkStreamActivity_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[WorkStreamActivity] CHECK CONSTRAINT [FK_WorkStreamActivity_Role]
GO
/****** Object:  StoredProcedure [dbo].[uspSaveProject]    Script Date: 25-10-2023 12:36:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[uspSaveProject] 
@Name varchar(100),
@ProjectTypeId uniqueidentifier,
@EffectiveStartDate Datetime,
@EffectiveEndDate DateTime,
@ProjectId uniqueidentifier = NULL,    
 @UserId uniqueidentifier,                                    
@ProjectUserRoleList ProjectUserRoleMapping Readonly,
@ProjectTechnologyMapping ProjectTechnologyMapping Readonly
   
AS
BEGIN
	 
	 IF @ProjectId IS NULL 
		BEGIN
		SET @ProjectId = NEWID();

		    INSERT INTO Project (Id,Name, ProjectTypeId, EffectiveStartDate, EffectiveEndDate, CreatedDate,CreatedBy)
 
 VALUES (@ProjectId,@Name, @ProjectTypeId, @EffectiveStartDate, @EffectiveEndDate,getdate(),@UserId)
         
		  
		    INSERT INTO [dbo].[ProjectUserEstimationMapping] (UserId, RoleId, ProjectId, CreatedDate,CreatedBy)
			SELECT UserId, RoleId, @ProjectId,getdate(),@UserId FROM @ProjectUserRoleList Where CurrentStatus= 'Insert';

INSERT INTO [dbo].[ProjectTechMapping](ProjectID,TechnologyId,CreatedDate,CreatedBy)
SELECT @ProjectId, TechnologyId, getdate(),@UserId FROM @ProjectTechnologyMapping Where CurrentStatus= 'Insert';


		END

		ELSE

		Update Project 
			SET Name=@Name,
			EffectiveStartDate=@EffectiveStartDate,
			EffectiveEndDate=@EffectiveEndDate
		Where Id=@ProjectId

		Update ProjectTechMapping 
			SET IsDestroyed=1
			FROM ProjectTechMapping PTM
		INNER JOIN @ProjectTechnologyMapping PM ON PM.ProjectId=PTM.ProjectId
		WHERE PM.CurrentStatus= 'Deleted'

		Update [dbo].[ProjectUserEstimationMapping] 
			SET IsDestroyed=1
			FROM [ProjectUserEstimationMapping] PUM
		INNER JOIN @ProjectUserRoleList PUR ON PUR.ProjectId=PUM.ProjectId
		and PUR.UserId=PUM.UserId and PUR.RoleId=PUM.RoleId 
		WHERE PUR.CurrentStatus= 'Deleted'

		 INSERT INTO [dbo].[ProjectUserEstimationMapping] (UserId, RoleId, ProjectId, CreatedDate,CreatedBy)
			SELECT UserId, RoleId, @ProjectId,getdate(),@UserId FROM @ProjectUserRoleList Where CurrentStatus= 'Insert';

INSERT INTO [dbo].[ProjectTechMapping](ProjectID,TechnologyId,CreatedDate,CreatedBy)
SELECT @ProjectId, TechnologyId, getdate(),@UserId FROM @ProjectTechnologyMapping Where CurrentStatus= 'Insert';

END
GO
