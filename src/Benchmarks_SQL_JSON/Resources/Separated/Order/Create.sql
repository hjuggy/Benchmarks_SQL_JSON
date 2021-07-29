INSERT INTO public."Order"(
	"Order_Id",
	"User_Id",
	"Price",
	"DayOfOrder",
	"Name",
	"Description")
VALUES(
	:Id,
	:UserId,
	:Price,
	:DayOfOrder,
	:Name,
	:Description);