/****** Object:  StoredProcedure [dbo].[vm_sp_GetUserDetail]    Script Date: 29-12-2022 11:55:20 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetUserDetail]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetUserDetail]    Script Date: 29-12-2022 11:55:20 ******/
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
 partner.PartnerName as Partner,
 appRoleUser.RolesGuid as RoleId,
 appRoles.Name as role
 from Users appUser
 join RoleUser appRoleUser
 on appRoleUser.UsersGuid = appUser.Guid
 left join Partners partner
 on appUser.email = partner.Email
 left join Roles appRoles
 on appRoleUser.RolesGuid = appRoles.Guid
 where appUser.email = @email and appUser.password = @password
END
GO
