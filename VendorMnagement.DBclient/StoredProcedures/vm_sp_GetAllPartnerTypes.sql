/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllPartnerTypes]    Script Date: 18-12-2022 11:22:07 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetAllPartnerTypes]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllPartnerTypes]    Script Date: 18-12-2022 11:22:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[vm_sp_GetAllPartnerTypes]

AS
BEGIN
   select Guid,Description from PartnerTypes
END
GO
