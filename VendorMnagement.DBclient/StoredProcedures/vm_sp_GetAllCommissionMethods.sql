/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllCommissionMethods]    Script Date: 24-12-2022 20:48:38 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetAllCommissionMethods]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllCommissionMethods]    Script Date: 24-12-2022 20:48:38 ******/
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
