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

INSERT INTO public."Order"(
	"Order_Id",
	"User_Id",
	"Price",
	"DayOfOrder",
	"Name",
	"Description"
	) SELECT 
		(orderItem->>'Id')::uuid,
		:Id,
		(orderItem->>'Price')::numeric,
		(orderItem->>'DayOfOrder')::date,
		orderItem->>'Name',
		orderItem->>'Description'
	FROM
		jsonb_path_query((:JsonUser)::jsonb,'$.Orders[*]') as orderItem;

INSERT INTO public."Subscription"(
	"Subscription_Id",
	"User_Id",
	"Name",
	"Description",
	"MinPrice",
	"MaxPrice"
	) SELECT 
		(subscriptionItem->>'Id')::uuid,
		:Id,
		subscriptionItem->>'Name',
		subscriptionItem->>'Description',
		(subscriptionItem->>'MinPrice')::numeric,
		(subscriptionItem->>'MaxPrice')::numeric
	FROM
		jsonb_path_query((:JsonUser)::jsonb,'$.Subscriptions[*]') as subscriptionItem;

