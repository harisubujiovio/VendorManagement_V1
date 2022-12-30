/****** Object:  StoredProcedure [dbo].[vm_sp_CardSummaryData]    Script Date: 29-12-2022 11:56:32 ******/
DROP PROCEDURE IF EXISTS [dbo].[vm_sp_CardSummaryData]
GO
/****** Object:  StoredProcedure [dbo].[vm_sp_CardSummaryData]    Script Date: 29-12-2022 11:56:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[vm_sp_CardSummaryData]
AS
BEGIN

select count(1) as data,'Application Users' as label,'Users' as name,'' as filterKey,'Users' as icon from Users
union
select count(1) as data,'All Partners' as label,'Partners' as name,'' as filterKey,'Partners' as icon from Partners
union
select count(1) as data,'Unmapped Partners' as label,'UnmappedPartners' as name,'NA' as filterKey,'Partners' as icon from Partners where Email = 'NA'
END
GO
