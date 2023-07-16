
alter table Opportunity add fundedFlag bit
alter table Opportunity add serviceId nvarchar(max)
alter table Lead add zipCode nvarchar(max)
alter table Lead add state nvarchar(max)
alter table Lead add FuneralHome nvarchar(max)
alter table Lead add DirectorName nvarchar(max)
alter table Lead add Address nvarchar(max)
alter table Lead add PersonName nvarchar(max)
alter table Lead add TypeOfService nvarchar(max)
alter table Lead add Veteran nvarchar(max)
alter table Lead add birthDate datetime



alter table Branch add zipCode nvarchar(max)
alter table Branch add state nvarchar(max)

alter table Customer add zipCode nvarchar(max)
alter table Customer add state nvarchar(max)

Create Table OpportunityService (
serviceId nvarchar(38) PRIMARY KEY,
serviceName nvarchar(max),
createdAt datetime
)


