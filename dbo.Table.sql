CREATE TABLE [dbo].Movies
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(10) NOT NULL, 
    [Category] NCHAR(10) NOT NULL, 
    [ImdbRatting] NCHAR(10) NOT NULL, 
    [image] IMAGE NULL
)
