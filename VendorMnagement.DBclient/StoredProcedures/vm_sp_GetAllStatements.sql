/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllStatements]    Script Date: 24-12-2022 20:53:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetAllStatements]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetAllStatements]    Script Date: 24-12-2022 20:53:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[vm_sp_GetAllStatements]

AS
BEGIN
   select Guid,StatementNo from Statements
END
GO
