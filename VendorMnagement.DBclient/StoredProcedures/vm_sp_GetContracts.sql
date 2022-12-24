/****** Object:  StoredProcedure [dbo].[vm_sp_GetContracts]    Script Date: 24-12-2022 20:53:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetContracts]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetContracts]    Script Date: 24-12-2022 20:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[vm_sp_GetContracts]
@partnerId uniqueidentifier,
@contractTypeId uniqueidentifier,
@commissionMethodId uniqueidentifier,
@contractStatusId uniqueidentifier,
@PageNo INT = 1,
@PageSize INT = 10,
@SortingCol AS Varchar(100) = 'CreatedDate',
@SortType AS Varchar(100) = 'DESC'
AS
BEGIN

  Declare @PageNumber As INT, @RowsPerPage AS INT
  SET @PageNumber = @PageNo
  SET @RowsPerPage = @PageSize


   select *, COUNT(*) OVER() as TotalCount from Contracts
   where PartnerId = Case when @partnerId = '00000000-0000-0000-0000-000000000000' Then PartnerId
   else @partnerId end and
   ContractTypeId = Case when @contractTypeId = '00000000-0000-0000-0000-000000000000' Then ContractTypeId
   else @contractTypeId end and
   CommissionMethodId = Case when @commissionMethodId = '00000000-0000-0000-0000-000000000000' Then CommissionMethodId
   else @commissionMethodId end and
   ContractStatusId = Case when @contractStatusId = '00000000-0000-0000-0000-000000000000' Then ContractStatusId
   else @contractStatusId end
   ORDER BY 
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'ASC' THEN CreatedDate END,
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'DESC' THEN CreatedDate END DESC
   OFFSET ((@PageNumber -1) * @RowsPerPage) ROWS
   FETCH NEXT @RowsPerPage ROWS ONLY
END
GO
