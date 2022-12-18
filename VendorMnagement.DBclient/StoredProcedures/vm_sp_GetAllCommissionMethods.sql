/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllCommissionMethods]    Script Date: 18-12-2022 11:21:16 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetAllCommissionMethods]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllCommissionMethods]    Script Date: 18-12-2022 11:21:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[vm_sp_GetAllCommissionMethods]

AS
BEGIN
   select Guid,Description from CommissionMethods
END
GO
