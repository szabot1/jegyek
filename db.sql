create table grades (
	id varchar(36) not null,
	value tinyint(4) not null,
	description varchar(255) not null,
	created_at timestamp not null default current_timestamp(),
	primary key (id)
);