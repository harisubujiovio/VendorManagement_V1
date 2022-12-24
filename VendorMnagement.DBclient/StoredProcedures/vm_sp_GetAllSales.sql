/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllSales]    Script Date: 24-12-2022 20:52:32 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetAllSales]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllSales]    Script Date: 24-12-2022 20:52:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[vm_sp_GetAllSales]

AS
BEGIN
   select Guid,DocumentNo from Sales
END
GO
