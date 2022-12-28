/****** Object:  StoredProcedure [dbo].[vm_sp_AssignUserRole]    Script Date: 24-12-2022 20:47:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_AssignUserRole]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_AssignUserRole]    Script Date: 24-12-2022 20:47:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[vm_sp_AssignUserRole]
@userId uniqueidentifier,
@roleId uniqueidentifier
AS
BEGIN
  delete from RoleUser where [UsersGuid] = @userId and [RolesGuid] = @roleId
  if not exists(select * from RoleUser where [UsersGuid] = @userId and [RolesGuid] = @roleId)
  BEGIN
     insert into [RoleUser]([UsersGuid],[RolesGuid]) values(@userId,@roleId)
  END
END
GO
