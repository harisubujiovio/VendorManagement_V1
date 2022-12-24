/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllContractStatus]    Script Date: 24-12-2022 20:49:43 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetAllContractStatus]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllContractStatus]    Script Date: 24-12-2022 20:49:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[vm_sp_GetAllContractStatus]

AS
BEGIN
   select Guid,Code,Description from ContractStatus
END
GO
