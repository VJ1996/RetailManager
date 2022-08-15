CREATE PROCEDURE [dbo].[spUserLookup]
	@Id nvarchar(128)
As
begin;
	set nocount on;

	SELECT Id, FirstName, LastName, EmailAddress, CreatedDate
	from [dbo].[User]
	where Id = @Id;
end