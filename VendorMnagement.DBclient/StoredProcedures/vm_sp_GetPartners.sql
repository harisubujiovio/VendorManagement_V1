/****** Object:  StoredProcedure [dbo].[vm_sp_GetPartners]    Script Date: 29-12-2022 11:59:32 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_GetPartners]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_GetPartners]    Script Date: 29-12-2022 11:59:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[vm_sp_GetPartners]
@filterKey as Varchar(100),
@PageNo INT = 1,
@PageSize INT = 10,
@SortingCol AS Varchar(100) = 'CreatedDate',
@SortType AS Varchar(100) = 'DESC'
AS
BEGIN

  Declare @PageNumber As INT, @RowsPerPage AS INT
  SET @PageNumber = @PageNo
  SET @RowsPerPage = @PageSize
   select appPartner.GUID,PartnerNo,PartnerName,PartnerTypeId,MobileNumber,Email, 
   appPartner.CreatedBy,appPartner.CreatedDate,appPartner.lastModifiedBy,appPartner.lastModifiedDate, 
   appPartnerTypes.Description as PartnerType, COUNT(*) OVER() as TotalCount 
   from Partners appPartner
   join PartnerTypes appPartnerTypes 
   on appPartner.PartnerTypeId = appPartnerTypes.Guid
   where Email = case when LEN(@filterKey) > 0 then 'NA' else Email end
   ORDER BY 
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'ASC' THEN appPartner.CreatedDate END,
   CASE WHEN @SortingCol = 'CreatedDate' AND @SortType = 'DESC' THEN appPartner.CreatedDate END DESC
   OFFSET ((@PageNumber -1) * @RowsPerPage) ROWS
   FETCH NEXT @RowsPerPage ROWS ONLY
END
GO
