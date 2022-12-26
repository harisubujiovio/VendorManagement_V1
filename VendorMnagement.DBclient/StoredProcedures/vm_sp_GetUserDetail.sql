/****** Object:  StoredProcedure [dbo].[vm_sp_GetUserDetail]    Script Date: 26-12-2022 19:46:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetUserDetail]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetUserDetail]    Script Date: 26-12-2022 19:46:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[vm_sp_GetUserDetail]
@email varchar(max),
@password varchar(max)
AS
BEGIN

 select appUser.Guid as UserId,
 appUser.firstName + ' ' + appUser.lastName as DisplayName,
 partner.Guid as PartnerId,
 appRoleUser.RolesGuid as RoleId from Users appUser
 join RoleUser appRoleUser
 on appRoleUser.UsersGuid = appUser.Guid
 left join Partners partner
 on appUser.email = partner.Email
 where appUser.email = @email and appUser.password = @password
END
GO
