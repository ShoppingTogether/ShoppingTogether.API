-- revoke privileges from the 'public' role
revoke create on schema public from public;
revoke all on database shoppingdb from public;

-- Create procudeure/function only role
do $$
begin
	create role shop_sproc_runner;
	exception when duplicate_object then raise notice '%, skipping', sqlerrm USING errcode = sqlstate;
END
$$;
grant connect on database shoppingdb to shop_sproc_runner;
grant usage on schema public to shop_sproc_runner;

-- Grant access to procedures
grant execute on function get_all_users to shop_sproc_runner;
grant execute on function get_user_by_sid to shop_sproc_runner;
grant execute on function add_user to shop_sproc_runner;

-- Create api user with procedure.function role
do $$
begin
	create user shopping_api with password '$api_pwd$';
	exception when duplicate_object then raise notice '%, skipping', sqlerrm USING errcode = sqlstate;
END
$$;
grant shop_sproc_runner to shopping_api; 