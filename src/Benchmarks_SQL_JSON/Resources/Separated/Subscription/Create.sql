INSERT INTO public."Subscription"(
	"Subscription_Id",
	"User_Id",
	"Name",
	"Description",
	"MinPrice",
	"MaxPrice")
VALUES (
	:Id,
	:UserId,
	:Name,
	:Description,
	:MinPrice,
	:MaxPrice);