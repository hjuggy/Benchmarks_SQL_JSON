INSERT INTO public."User"(
	"User_id",
	"FirstName",
	"LastName",
	"Email",
	"Phone",
	"Country",
	"Sity",
	"Address",
	"Gender",
	"DayOfBirth")
	VALUES (
	:Id,
	:FirstName,
	:LastName,
	:Email,
	:Phone,
	:Country,
	:Sity,
	:Address,
	:Gender,
	:DayOfBirth);