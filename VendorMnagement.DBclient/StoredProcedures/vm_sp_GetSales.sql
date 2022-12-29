/****** Object:  StoredProcedure [dbo].[vm_sp_GetSales]    Script Date: 29-12-2022 06:54:50 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetSales]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetSales]    Script Date: 29-12-2022 06:54:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[vm_sp_GetSales]
@partnerId uniqueidentifier,
@PageNo INT = 1,
@PageSize INT = 10,
@SortingCol AS Varchar(100) = 'CreatedDate',
@SortType AS Varchar(100) = 'DESC'
AS
BEGIN

  Declare @PageNumber As INT, @RowsPerPage AS INT
  SET @PageNumber = @PageNo
  SET @RowsPerPage = @PageSize


   select *,appPartners.PartnerName as Partner, COUNT(*) OVER() as TotalCount from Sales appSales
   join Partners appPartners
   on appSales.PartnerId = appPartners.Guid
   where PartnerId = Case when @partnerId = '00000000-0000-0000-0000-000000000000' Then PartnerId
   else @partnerId end
   ORDER BY 
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'ASC' THEN appSales.CreatedDate END,
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'DESC' THEN appSales.CreatedDate END DESC
   OFFSET ((@PageNumber -1) * @RowsPerPage) ROWS
   FETCH NEXT @RowsPerPage ROWS ONLY
END
GO
