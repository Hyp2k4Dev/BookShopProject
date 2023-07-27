drop database if exists BookShop;
create database BookShop;
use BookShop;
-- drop database bookshop;
show tables; 
create table Customers(
	customer_ID int auto_increment primary key,
    customer_name varchar(50) not null,
    phoneNumber int,
    customer_address varchar(225)
);


create table Staffs(
	staff_ID int auto_increment primary key,
    staff_name varchar(50) not null,
    user_name varchar(50) not null,
    Pass_word varchar(50) not null,
    staff_status int
);


create table Orders(
	order_ID int primary key auto_increment,
    staff_ID int not null,
    constraint fk_staff foreign key(staff_ID) references Staffs(staff_ID),
    customer_ID int not null,
    constraint fk_customers foreign key(customer_ID) references Customers(customer_ID),
    order_date datetime default now() not null,
    order_status int not null
);


create table Authors(
	author_ID int auto_increment primary key,
	author_name varchar(50) not null,
    author_address varchar(225),
    phoneNumber int
);

create table Publishers(
	publisher_ID int not null primary key auto_increment,
	publisher_name varchar(50) not null,
    publisher_address varchar(225),
    phoneNumber varchar(11),
    Website varchar(225)
);

drop table if exists Categories;
create table Categories(
	category_ID int primary key auto_increment,
    category_name varchar(100) not null
);

create table Books(
	book_ID int not null primary key auto_increment,
    ISBN int not null,
    publisher_ID int not null, 
    book_name varchar(50)not null,
    publish_year int,
    book_description varchar(200),
    unit_price decimal(10,2) not null,
    amount int not null default 0,
    book_status int not null,
    constraint fk_b_publisher foreign key(publisher_ID) references publishers(publisher_ID)
);

drop table if exists Authors;
create table Authors(
	author_ID int primary key auto_increment,
    author_Name varchar(50),
    phoneNumber int,
    author_address varchar(225)
);



create table CategoryDetails(
	book_ID int not null,
    category_ID int not null,
    index(category_ID),
    primary key(book_ID,category_ID),
    foreign key(book_ID) references Books(book_ID)
);


create table OrderDetails(
	order_ID int not null,
    constraint fk_orders foreign key(order_ID) references Orders(order_ID),
    Book_ID int not null,
    constraint fk_books foreign key(book_ID) references Books(book_ID),
    constraint pk_orders_books primary key (order_ID, book_ID),
	unit_price decimal(20,2) not null,
    quantity int not null default 1
);

drop table if exists CategoryDetails;
create table CategoryDetails(
	book_ID int not null,
    constraint fk_CD_book foreign key(book_ID) references Books(book_ID),
    category_ID int not null,
    constraint fk_CD_category foreign key(category_ID) references Categories(category_ID),
    constraint pk_CategoryDetails primary key (book_ID, category_ID)
);


create table Authors_Books(
	author_ID int not null,
constraint fk_AB_author foreign key(author_ID) references Authors(author_ID),
    book_ID int not null,
    constraint fk_AB_book foreign key(book_ID) references Books(book_ID),
    constraint pk_Authors_Books primary key (author_ID, book_ID)
);


delimiter $$
create trigger tg_before_insert before insert
	on Books for each row
    begin
		if new.amount < 0 then
            signal sqlstate '45001' set message_text = 'tg_before_insert: Quantity must > 0';
        end if;
    end $$
delimiter ;

delimiter $$
create trigger tg_CheckQuantity
	before update on Books
	for each row
	begin
		if new.amount < 0 then
            signal sqlstate '45001' set message_text = 'tg_CheckQuantity: quantity must > 0';
        end if;
    end $$
delimiter ;

delimiter $$
create procedure sp_createCustomer(IN customerName varchar(100), IN phoneNumber int, IN customerAddress varchar(200), OUT customerID int)
begin
	insert into Customers(customer_name, phoneNumber,customer_address) values (customerName, phoneNumber ,customerAddress); 
    select max(customer_ID) into customerID from Customers;
end $$
delimiter ;

call sp_createCustomer('no name','0','any where', @cusId);
select @cusId;


insert into Staffs(staff_name, user_name, pass_word, staff_status) values
		('Dinh Thanh Tuan','staff1', 'staff1', '1'),
        ('Nguyen Hoang Hiep','staff2', 'staff2', '1'),
        ('Ma Van N','staff3', 'staff3', '2'),
        ('xbvvcb','staff4', 'staff4', '2'),
        ('dzvclvcb','staff5', 'staff5', '2');
select * from Staffs;

insert into Customers(customer_name, phoneNumber, customer_address) values
	('Customer 1','0987654321', 'Ha Noi'),
    ('Customer 2','098765231', 'Da Nang'),
    ('Customer 3','097865231', 'Hai Phong'),
    ('Customer 4','988765231', 'Italya'),
    ('Customer 5','123456789', 'Binh Duong');
select * from Customers;

insert into Authors(author_name, phoneNumber, author_address) values
	('Author 1','0987654321', 'Ha Noi'),
    ('Author 2','098765231', 'Da Nang'),
    ('Author 3','097865231', 'Hai Phong'),
    ('Author 4','988765231', 'Italya'),
    ('Author 5','123456789', 'Binh Duong');
select * from Authors;

insert into Publishers(publisher_name, phoneNumber, publisher_address, website) values
	('Publisher 1','0987634321', 'Ha Noi', 'pu1.com'),
    ('Publisher 2','0945765231', 'Da Nang', 'pu2.com'),
    ('Publisher 3','0978565231', 'Hai Phong', 'pu3.com'),
    ('Publisher 4','9887656231', 'Italya', 'pu4.com'),
    ('Publisher 5','1234566789', 'Binh Duong', 'pu5.com');
select * from Publishers;


insert into Books(book_name, publisher_ID, ISBN, publish_year, book_description, unit_price, amount, book_status) values
	('Book 1', 1, 1234567890, 1990, 'book1', 45.5, 1, 1),
    ('Book 2', 2, 1234527890, 1992, 'book2', 44.5, 2, 1),
    ('Book 3', 3, 1234525890, 1994, 'book3', 56.5, 3, 2),
    ('Book 4', 4, 1264527890, 1996, 'book4', 50, 4, 1),
    ('Book 5', 5, 1237527890, 1997, 'book5', 64.2, 2, 2);
select * from Books;

insert into Categories(category_name) values
	('category 1'),
    ('category 2'),
    ('category 3'),
    ('category 4'),
    ('category 5');
select * from Categories;

insert into Orders(staff_ID, customer_ID, order_status) values
(1, 1, 1), (1, 2, 1), (1, 3, 1);
select * from Orders;

insert into OrderDetails(order_ID, book_ID, unit_price, quantity) values
	(1, 1, 12.2, 2), (2, 2, 2, 3), (3, 4, 3, 3);
select * from OrderDetails;

insert into CategoryDetails(book_ID, category_ID) values
	(1, 1), (2, 2), (3, 3), (4,1);
select * from OrderDetails;

insert into Authors_Books(book_ID, author_ID) values
	(1, 1), (2, 2), (3, 3),(4,1);
select * from OrderDetails;


-- /* CREATE & GRANT USER */
DROP USER if exists 'H&T'@'localhost';
create user if not exists 'H&T'@'localhost' identified by 'Tuannb12345';
grant all on BookShop.* to 'H&T'@'localhost';
-- -- grant all on Items to 'vtca'@'localhost';
-- -- grant all on Customers to 'vtca'@'localhost';
-- -- grant all on Orders to 'vtca'@'localhost';
-- -- grant all on OrderDetails to 'vtca'@'localhost';
    
-- select customer_ID, customer_name,
--     ifnull(customer_address, '') as customer_address
-- from Customers where customer_id=1;
--                         
-- select order_id from Orders order by order_id desc limit 1;

-- select LAST_INSERT_ID();
-- select customer_id from Customers order by customer_id desc limit 1;

-- update Books set amount=10 where book_ID=3;
