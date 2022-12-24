/****** Object:  StoredProcedure [dbo].[vm_sp_GetCommissionMethods]    Script Date: 24-12-2022 20:53:29 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetCommissionMethods]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetCommissionMethods]    Script Date: 24-12-2022 20:53:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[vm_sp_GetCommissionMethods]
@PageNo INT = 1,
@PageSize INT = 10,
@SortingCol AS Varchar(100) = 'CreatedDate',
@SortType AS Varchar(100) = 'DESC'
AS
BEGIN

  Declare @PageNumber As INT, @RowsPerPage AS INT
  SET @PageNumber = @PageNo
  SET @RowsPerPage = @PageSize
   select *, COUNT(*) OVER() as TotalCount from CommissionMethods
   ORDER BY 
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'ASC' THEN CreatedDate END,
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'DESC' THEN CreatedDate END DESC
   OFFSET ((@PageNumber -1) * @RowsPerPage) ROWS
   FETCH NEXT @RowsPerPage ROWS ONLY
END
GO
