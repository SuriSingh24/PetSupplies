Create Procedure PopulateMissingEntry
As
Begin

Declare @CurrentMonth table(Id int identity, AdMonthID int,	[Year] int,[Month] int,	LockOutEndDate datetime ,LockOutStartDate datetime,	DropNumber int ,PetpartnerInfo nvarchar(50),CorpInHomeDate datetime,AdOptionID int,CorpPlanText nvarchar(max),AdOptionName nvarchar(50))

Declare @PreviousMonth table(Id int identity,AdMonthID int,	[Year] int,[Month] int,	LockOutEndDate datetime ,LockOutStartDate datetime,	DropNumber int ,PetpartnerInfo nvarchar(50),CorpInHomeDate datetime,AdOptionID int,CorpPlanText nvarchar(max),AdOptionName nvarchar(50))

Declare @MissingStores table(Id int identity,StoreID int,Ownergroup nvarchar(50),City nvarchar(50),State nvarchar(50),Storename nvarchar(50),MarketID int,Shootover nvarchar(100),ArtworkCode nvarchar(100))

-- Get Current Month detail
Insert Into @CurrentMonth
Select top 1 *,'' from AdMonth where [Month] = Month(GetDate()) and [Year] = Year(GetDate()) order by LockOutEndDate desc

-- Get previous Month detail 
Insert Into @PreviousMonth
Select top 1 *,'' from AdMonth where [Month] < Month(GetDate()) and [Year] <= Year(GetDate()) order by LockOutEndDate desc

-- get store list which have missing entry into current month
Insert Into @MissingStores
Select * from Store Where StoreID not in (Select StoreID from StoreAdChoice where AdMonthID = 1)

-- check store last month detail and set follow corporate if previously has follow corporate.


-- set all rest store to alternative choice 


-- if store last month and current month ad o

End

Select * from StoreAdChoice
Select * from StoreAdChoiceHistory

select * from AdMonth

Select * from AdMonth where [Month] = Month(Getdate())

select * from Coupon
select * from [User]
select * from Store
select * from Store as s 
		 Join UserStore as us




 



