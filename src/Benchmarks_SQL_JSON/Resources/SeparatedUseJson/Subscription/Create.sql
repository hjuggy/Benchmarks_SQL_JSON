INSERT INTO public."Subscription"(
	"Subscription_Id",
	"User_Id",
	"Name",
	"Description",
	"MinPrice",
	"MaxPrice"
	) SELECT 
		(subscriptionItem->>'Id')::uuid,
		:UserId,
		subscriptionItem->>'Name',
		subscriptionItem->>'Description',
		(subscriptionItem->>'MinPrice')::numeric,
		(subscriptionItem->>'MaxPrice')::numeric
	FROM
		jsonb_path_query((:JsonSubscriptions)::jsonb,'$[*]') as subscriptionItem;