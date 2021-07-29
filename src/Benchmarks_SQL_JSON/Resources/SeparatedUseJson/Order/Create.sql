INSERT INTO public."Order"(
	"Order_Id",
	"User_Id",
	"Price",
	"DayOfOrder",
	"Name",
	"Description"
	) SELECT 
		(orderItem->>'Id')::uuid,
		:UserId,
		(orderItem->>'Price')::numeric,
		(orderItem->>'DayOfOrder')::date,
		orderItem->>'Name',
		orderItem->>'Description'
	FROM
		jsonb_path_query((:JsonOrders)::jsonb,'$[*]') as orderItem;