/****** Object:  StoredProcedure [dbo].[vm_sp_GetContractStatus]    Script Date: 24-12-2022 20:54:31 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetContractStatus]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetContractStatus]    Script Date: 24-12-2022 20:54:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[vm_sp_GetContractStatus]
@PageNo INT = 1,
@PageSize INT = 10,
@SortingCol AS Varchar(100) = 'CreatedDate',
@SortType AS Varchar(100) = 'DESC'
AS
BEGIN

  Declare @PageNumber As INT, @RowsPerPage AS INT
  SET @PageNumber = @PageNo
  SET @RowsPerPage = @PageSize
   select GUID,Code,Description,CreatedBy,CreatedDate,lastModifiedBy,lastModifiedDate, COUNT(*) OVER() as TotalCount from ContractStatus
   ORDER BY 
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'ASC' THEN CreatedDate END,
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'DESC' THEN CreatedDate END DESC
   OFFSET ((@PageNumber -1) * @RowsPerPage) ROWS
   FETCH NEXT @RowsPerPage ROWS ONLY
END
GO
