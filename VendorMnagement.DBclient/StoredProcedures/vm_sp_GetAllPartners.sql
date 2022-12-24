/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllPartners]    Script Date: 24-12-2022 20:50:48 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetAllPartners]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllPartners]    Script Date: 24-12-2022 20:50:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[vm_sp_GetAllPartners]

AS
BEGIN
   select Guid,PartnerNo,PartnerName from Partners
END
GO
