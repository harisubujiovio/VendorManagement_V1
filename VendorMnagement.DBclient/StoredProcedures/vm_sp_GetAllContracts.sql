/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllContracts]    Script Date: 24-12-2022 20:49:11 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetAllContracts]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllContracts]    Script Date: 24-12-2022 20:49:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[vm_sp_GetAllContracts]

AS
BEGIN
   select Guid,ContractNo from Contracts
END
GO
