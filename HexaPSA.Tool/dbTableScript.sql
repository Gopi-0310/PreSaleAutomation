USE [PreSalesAutomation]
GO
/****** Object:  Table [dbo].[CapacityUtilization]    Script Date: 20-09-2023 12:25:14 ******/
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
/****** Object:  Table [dbo].[PresalesTimeTracker]    Script Date: 20-09-2023 12:25:14 ******/
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
/****** Object:  Table [dbo].[Project]    Script Date: 20-09-2023 12:25:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ProjectTypeId] [uniqueidentifier] NOT NULL,
	[TechnologyId] [uniqueidentifier] NOT NULL,
	[EffectiveStartDate] [datetime] NOT NULL,
	[EffectiveEndDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__Project__3214EC07AEF15139] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectType]    Script Date: 20-09-2023 12:25:14 ******/
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
/****** Object:  Table [dbo].[ProjectUserEstimationMapping]    Script Date: 20-09-2023 12:25:14 ******/
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
 CONSTRAINT [PK__ProjectU__3214EC07ED0CB615] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceRate]    Script Date: 20-09-2023 12:25:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceRate](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ProjectId] [uniqueidentifier] NOT NULL,
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
/****** Object:  Table [dbo].[Role]    Script Date: 20-09-2023 12:25:14 ******/
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
/****** Object:  Table [dbo].[TeamConfiguration]    Script Date: 20-09-2023 12:25:14 ******/
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
/****** Object:  Table [dbo].[Technology]    Script Date: 20-09-2023 12:25:14 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 20-09-2023 12:25:14 ******/
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
/****** Object:  Table [dbo].[WorkStream]    Script Date: 20-09-2023 12:25:14 ******/
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
 CONSTRAINT [PK__WorkStre__3214EC07AA424E9E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkStreamActivity]    Script Date: 20-09-2023 12:25:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkStreamActivity](
	[Id] [uniqueidentifier] NOT NULL,
	[Activity] [nvarchar](255) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[WorkStreamActivityId] [uniqueidentifier] NOT NULL,
	[Hours] [float] NOT NULL,
	[Week] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsDestroyed] [bit] NOT NULL,
 CONSTRAINT [PK__WorkStre__3214EC0789C0A408] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Role] ([Id], [Name], [Code], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsDestroyed]) VALUES (N'07810acc-8edb-41c9-a758-c2f277431774', N'Programmer', N'Pro', CAST(N'2023-09-19T22:58:55.740' AS DateTime), N'fc836240-9437-46d7-be32-9c6582fd751c', NULL, NULL, 0)
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
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Technology] FOREIGN KEY([TechnologyId])
REFERENCES [dbo].[Technology] ([Id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Technology]
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
ALTER TABLE [dbo].[WorkStreamActivity]  WITH CHECK ADD  CONSTRAINT [FK_WorkStreamActivity_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[WorkStreamActivity] CHECK CONSTRAINT [FK_WorkStreamActivity_Role]
GO
