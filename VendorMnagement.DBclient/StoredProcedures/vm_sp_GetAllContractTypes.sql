/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllContractTypes]    Script Date: 24-12-2022 20:50:17 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetAllContractTypes]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllContractTypes]    Script Date: 24-12-2022 20:50:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[vm_sp_GetAllContractTypes]

AS
BEGIN
   select Guid,Code,Description from ContractTypes
END
GO
