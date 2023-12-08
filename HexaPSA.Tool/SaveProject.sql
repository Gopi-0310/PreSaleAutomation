 

 CREATE TYPE [dbo].[ProjectUserRoleMapping] AS TABLE(
	[ProjectId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[RoleId] [uniqueidentifier] NULL,
	[CurrentStatus] varchar(50) NULL
)
GO


 
CREATE TYPE [dbo].[ProjectTechnologyMapping] AS TABLE(
	[ProjectId] [uniqueidentifier] NULL,
	[TechnologyId] [uniqueidentifier] NULL,
	[CurrentStatus] varchar(50) NULL
)
GO

-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[uspSaveProject] 
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
        
        INSERT INTO Project (Id, Name, ProjectTypeId, EffectiveStartDate, EffectiveEndDate, CreatedDate, CreatedBy)
        VALUES (@ProjectId, @Name, @ProjectTypeId, @EffectiveStartDate, @EffectiveEndDate, getdate(), @UserId)
         
        INSERT INTO [dbo].[ProjectUserEstimationMapping] (UserId, RoleId, ProjectId, CreatedDate, CreatedBy, CurrentStatus)
        SELECT UserId, RoleId, @ProjectId, getdate(), @UserId, 'Insert' FROM @ProjectUserRoleList;
        
        INSERT INTO [dbo].[ProjectTechMapping] (ProjectID, TechnologyId, CreatedDate, CreatedBy, CurrentStatus)
        SELECT @ProjectId, TechnologyId, getdate(), @UserId, 'Insert' FROM @ProjectTechnologyMapping;

    END
    ELSE
    BEGIN

        UPDATE Project 
        SET Name = @Name,
            EffectiveStartDate = @EffectiveStartDate,
            ProjectTypeId = @ProjectTypeId,
            EffectiveEndDate = @EffectiveEndDate
        WHERE Id = @ProjectId;


        UPDATE [dbo].[ProjectTechMapping] 
        SET IsDestroyed = 1
        FROM [dbo].[ProjectTechMapping] PTM
        INNER JOIN @ProjectTechnologyMapping PM ON PM.ProjectId = PTM.ProjectId
        WHERE PM.CurrentStatus = 'Deleted';

        MERGE INTO [dbo].[ProjectUserEstimationMapping] AS Target
        USING (SELECT UserId, RoleId, @ProjectId AS ProjectId FROM @ProjectUserRoleList WHERE CurrentStatus = 'Insert') AS Source
        ON Target.ProjectId = Source.ProjectId AND Target.UserId = Source.UserId
        WHEN MATCHED THEN
            UPDATE SET Target.RoleId = Source.RoleId
        WHEN NOT MATCHED THEN
            INSERT (UserId, RoleId, ProjectId, CreatedDate, CreatedBy, CurrentStatus)
            VALUES (Source.UserId, Source.RoleId, Source.ProjectId, getdate(), @UserId, 'Insert');
        

        UPDATE [dbo].[ProjectUserEstimationMapping] 
        SET IsDestroyed = 1
        FROM [dbo].[ProjectUserEstimationMapping] PUM
        INNER JOIN @ProjectUserRoleList PUR ON PUR.ProjectId = PUM.ProjectId
            AND PUR.UserId = PUM.UserId
            AND PUR.RoleId = PUM.RoleId 
        WHERE PUR.CurrentStatus = 'Deleted';
        

        UPDATE [dbo].[ProjectTechMapping] 
        SET IsDestroyed = 1
        FROM [dbo].[ProjectTechMapping] PTM
        INNER JOIN @ProjectTechnologyMapping PM ON PM.ProjectId = PTM.ProjectId
        WHERE PM.CurrentStatus = 'Deleted';
    END
END
