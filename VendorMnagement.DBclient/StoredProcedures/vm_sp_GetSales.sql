/****** Object:  StoredProcedure [dbo].[vm_sp_GetSales]    Script Date: 24-12-2022 20:57:13 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetSales]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetSales]    Script Date: 24-12-2022 20:57:13 ******/
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


   select *, COUNT(*) OVER() as TotalCount from Sales
   where PartnerId = Case when @partnerId = '00000000-0000-0000-0000-000000000000' Then PartnerId
   else @partnerId end
   ORDER BY 
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'ASC' THEN CreatedDate END,
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'DESC' THEN CreatedDate END DESC
   OFFSET ((@PageNumber -1) * @RowsPerPage) ROWS
   FETCH NEXT @RowsPerPage ROWS ONLY
END


GO
