/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllRoles]    Script Date: 24-12-2022 20:51:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetAllRoles]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllRoles]    Script Date: 24-12-2022 20:51:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[vm_sp_GetAllRoles]

AS
BEGIN
   select Guid,Name,Description from Roles
END
GO
