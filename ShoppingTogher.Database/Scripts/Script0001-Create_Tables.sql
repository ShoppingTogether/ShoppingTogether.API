CREATE TABLE IF NOT EXISTS users (
	id serial primary key,
	name varchar(100) NOT NULL,
	sid varchar(255) unique NOT NULL,
	created_at timestamp NOT NULL default now()
);

CREATE TABLE IF NOT EXISTS carts (
	id serial primary key,
	created_at timestamp NOT NULL default now(),
	purchased_at timestamp NULL
);

CREATE TABLE IF NOT EXISTS active_cart (
	cart_id int references carts(id) NOT NULL,
	modified_at timestamp NOT NULL default now()
);

CREATE TABLE IF NOT EXISTS order_lines (
	id serial primary key,
	cart_id int references carts(id) NOT NULL,
	user_id int references users(id) NOT NULL,
	product_upn bigint NOT NULL,
	product_description varchar(255) NOT NULL,
	product_price money NOT NULL,
	product_quantity smallint NOT NULL,
	created_at timestamp NOT NULL default now(),
	modified_at timestamp NOT NULL default now(),
	removed_at timestamp NULL
);

CREATE TABLE IF NOT EXISTS receipts (
	id serial primary key,
	cart_id int references carts(id) NOT NULL,
	subtotal money NOT NULL,
	fee money NOT NULL,
	total money NOT NULL,
	created_at timestamp NOT NULL default now()
);

CREATE TABLE IF NOT EXISTS user_receipts (
	id serial primary key,
	receipt_id int references receipts(id) NOT NULL,
	user_id int references users(id) NOT NULL,
	amount_owed money NOT NULL,
	is_paid bool NOT NULL default false,
	created_at timestamp NOT NULL default now(),
	paid_at timestamp NULL
);