-- Get all users
create or replace function get_all_users()
	returns setof users
	language sql
	security definer
as $$
	select id, name, sid, created_at
	from users;
$$;

-- Get single user by subject id
create or replace function get_user_by_sid(_sid varchar(255))
	returns users
	language sql
	security definer
as $$
	select id, name, sid, created_at
	from users
	where sid = _sid;
$$;

-- Add user and return record
create or replace function add_user(_name varchar(255), _sid varchar(255))
	returns users
	language sql
	security definer
as $$
	insert into users(name, sid)
	values(_name, _sid)
	returning id, name, sid, created_at;
$$;
