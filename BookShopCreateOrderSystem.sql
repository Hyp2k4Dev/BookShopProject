drop database if exists BookShop;
create database BookShop;
use BookShop;
-- drop database bookshop;
show tables; 
create table Customers(
	customer_ID int auto_increment primary key,
    customer_name varchar(50) not null,
    phoneNumber varchar(11),
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


create table Categories(
	category_ID int primary key auto_increment,
    category_name varchar(100) not null
);

create table Books(
	book_ID int not null primary key auto_increment,
    ISBN int not null unique,
    publisher_ID int not null, 
    book_name varchar(50)not null,
    publish_year int,
    book_description varchar(200),
    price decimal(10,2) not null,
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
delimiter $$
DELIMITER //

CREATE PROCEDURE sp_createBook(
    IN book_name VARCHAR(255),
    IN ISBN INT,
    IN author_name VARCHAR(255),
    IN amount INT,
    IN category_name VARCHAR(255),
    IN publisher_name VARCHAR(255),
    IN price DECIMAL(10, 2),
    IN book_description TEXT,
    IN book_status INT,
    OUT new_book_id INT
)
BEGIN
    -- Insert the book into the appropriate tables
    DECLARE category_id INT;
    DECLARE publisher_id INT;
    DECLARE author_id INT;
    
    -- ... (existing code for getting or inserting Category, Publisher, and Author)
    
    -- Insert the book
    INSERT INTO Books (book_name, ISBN, publisher_ID, price, book_description, amount, book_status)
    VALUES (book_name, ISBN, publisher_ID, price, book_description, amount, book_status);
    SET new_book_id = LAST_INSERT_ID();

    -- Associate book with category and author
    INSERT INTO CategoryDetails (book_ID, category_ID) VALUES (new_book_id, category_id);
    INSERT INTO Authors_Books (book_ID, author_ID) VALUES (new_book_id, author_id);
    INSERT INTO Books (book_name, publisher_ID, ISBN, publish_year, book_description, price, amount, book_status)
VALUES ('Sample Book', 1, 1234567890, 2023, 'A sample book description', 25.99, 50, 1);
    -- Update book_ID in the CategoryDetails table
    UPDATE CategoryDetails SET book_ID = new_book_id WHERE book_ID IS NULL AND category_ID = category_id;

END //

DELIMITER ;







insert into Staffs(staff_name, user_name, pass_word, staff_status) values
		('Dinh Thanh Tuan','staff1', '4D7D719AC0CF3D78EA8A94701913FE47', '1'),
        ('Nguyen Hoang Hiep','staff2', '8BC01711B8163EC3F2AA0688D12CDF3B', '1');
select * from Staffs;

insert into Customers(customer_name, phoneNumber, customer_address) values
	('Customer 1','0987654321', 'Ha Noi'),
    ('Customer 2','098765231', 'Da Nang'),
    ('Customer 3','097865231', 'Hai Phong'),
    ('Customer 4','988765231', 'Italya'),
    ('Customer 5','123456789', 'Binh Duong'),
    ('Customer 6','0917654321', 'Ha Noi'),
    ('Customer 7','092765231', 'Da Nang'),
    ('Customer 8','093865231', 'Hai Phong'),
    ('Customer 9','0984765231', 'Italya'),
    ('Customer 10','153456789', 'Binh Duong');
select * from Customers;

insert into Authors(author_name, phoneNumber, author_address) values
	('Auhtor 1','0987654321', 'Ha Noi'),
    ('Auhtor 2','098765231', 'Da Nang'),
    ('Auhtor 3','097865231', 'Hai Phong'),
    ('Auhtor 4','988765231', 'Italya'),
    ('Auhtor 5','123456789', 'Binh Duong'),
    ('Auhtor 6','0987654321', 'Ha Noi'),
    ('Auhtor 7','098765231', 'Da Nang'),
    ('Auhtor 8','097865231', 'Hai Phong'),
    ('Auhtor 9','988765232', 'Italya'),
    ('Auhtor 10','98855231', 'Italya'),
    ('Auhtor 11','9065231', 'Italya'),
    ('Auhtor 12','98265231', 'Italya'),
    ('Auhtor 13','98876231', 'Italya'),
    ('Auhtor 14','99765231', 'Italya'),
    ('Auhtor 15','978765231', 'Italya'),
    ('Auhtor 16','96765231', 'Italya'),
    ('Auhtor 17','950765231', 'Italya'),
    ('Auhtor 18','941765231', 'Italya'),
    ('Auhtor 19','932765231', 'Italya'),
    ('Auhtor 20','223456789', 'Binh Duong');
select * from Authors;

insert into Publishers(publisher_name, phoneNumber, publisher_address, website) values
	('Publisher 1','0987634321', 'Ha Noi', 'pu1.com'),
    ('Publisher 2','0945765231', 'Da Nang', 'pu2.com'),
    ('Publisher 3','0978565231', 'Hai Phong', 'pu3.com'),
    ('Publisher 4','09887656231', 'Italya', 'pu4.com'),
    ('Publisher 5','9428342058', 'Binh Duong', 'pu5.com'),
    ('Publisher 6','0670563280', 'Binh Duong', 'pu5.com'),
    ('Publisher 7','1622026688', 'Binh Duong', 'pu5.com'),
    ('Publisher 8','3181349953', 'Binh Duong', 'pu5.com'),
    ('Publisher 9','5006582541', 'Binh Duong', 'pu5.com'),
    ('Publisher 10','6427637737', 'Binh Duong', 'pu5.com');
select * from Publishers;


insert into Books(book_name, publisher_ID, ISBN, publish_year, book_description, price, amount, book_status) values
	('IT Liệu đã hết thời ?', 1, 1, 1990, 'book1', 45500, 1, 1),
    ('aaaaaaaaaaaaaaaaaaaaaaaaaaaaa', 2, 2, 1992, 'book2', 44500, 2, 1),
    ('English book2', 3, 3, 1994, 'book3', 56500, 3, 2),
    ('EBook 4', 4, 4, 1996, 'book4', 50000, 4, 1),
    ('Book 5', 5, 5, 1997, 'book5', 64200, 20, 2),
    ('Book 6', 6, 6, 1997, 'book6', 64200, 12, 1),
    ('Book 7', 7, 7, 1997, 'book7', 64200, 54, 1),
    ('Book 8', 8, 8, 1997, 'book8', 64200, 1, 1),
    ('Book 9', 9, 9, 1997, 'book9', 64200, 11, 1),
    ('Book 10', 10, 10, 1997, 'book10', 100200, 5, 1),
    ('Book 11', 10, 11, 1997, 'book11', 24200, 5, 2),
    ('Book 12', 10, 12, 1997, 'book12', 14200, 5, 1),
    ('Book 13', 10, 13, 1997, 'book13', 87200, 5, 1),
    ('Book 14', 10, 14, 1997, 'book14', 70000, 5, 1),
    ('Book 15', 10, 15, 1997, 'book15', 0, 5, 2),
    ('Book 16', 10, 16, 1997, 'book16', 58000, 5, 1),
    ('Book 17', 10, 17, 1997, 'book17', 64000, 5, 1),
    ('Book 18', 10, 18, 1997, 'book18', 642200, 5, 2),
    ('Book 19', 10, 19, 1997, 'book19', 1235000, 5, 1),
    ('Book 20', 10, 20, 1997, 'book20', 64200, 5, 1);
select * from Books;

insert into Categories(category_name) values
	('category 1'),
    ('category 2'),
    ('category 3'),
    ('category 4'),
    ('category 5'),
    ('category 6'),
    ('category 7'),
    ('category 8'),
    ('category 9'),
    ('category 10'),
    ('category 11'),
    ('category 12'),
    ('category 13'),
    ('category 14'),
    ('category 15'),
    ('category 16'),
    ('category 17'),
    ('category 18'),
    ('category 19'),
    ('category 20');
select * from Categories;





insert into CategoryDetails(book_ID, category_ID) values
	(1, 1), (2, 2), (3, 3),(4,4),(5,5),(6,6),(7,7),(8,8),(9,9),(10,10),(11, 11), (12, 12), (13,13), (14,14),(15,15),(16,16),(17,17),(18,18),(19,19),(20,20);
select * from OrderDetails;

insert into Authors_Books(book_ID, author_ID) values
	(1, 1), (2, 2), (3, 3), (4,4),(5,5),(6,6),(7,7),(8,8),(9,9),(10,10),(11, 11), (12, 12), (13,13), (14,14),(15,15),(16,16),(17,17),(18,18),(19,19),(20,20);
select * from OrderDetails;


-- /* CREATE & GRANT USER */
DROP USER if exists 'H&T'@'localhost';
create user if not exists 'H&T'@'localhost' identified by 'Tuannb12345';
grant all on BookShop.* to 'H&T'@'localhost';
-- -- grant all on Items to 'vtca'@'localhost';
-- -- grant all on Customers to 'vtca'@'localhost';
-- -- grant all on Orders to 'vtca'@'localhost';
-- -- grant all on OrderDetails to 'vtca'@'localhost';

select b.book_ID, b.ISBN, b.book_name, c.category_name, b.publish_year, 
	b.book_description, a.author_name, p.publisher_name, b.price, b.amount, b.book_status
from Books b inner join CategoryDetails cd on b.book_ID=cd.book_ID 
	inner join Categories c on cd.category_ID=cd.category_ID 
	inner join Authors_Books ab on b.book_ID = ab.book_ID 
	inner join Authors a on ab.author_ID = a.author_ID 
	inner join Publishers p on b.publisher_ID = p.publisher_ID
    where b.book_ID;
    

-- select customer_ID, customer_name,
--     ifnull(customer_address, '') as customer_address
-- from Customers where customer_id=1;
--                         
-- select order_id from Orders order by order_id desc limit 1;

-- select LAST_INSERT_ID();
-- select customer_id from Customers order by customer_id desc limit 1;

update Books set amount=100 where book_ID=1;
update Books set amount=100 where book_ID=2;
update Books set amount=100 where book_ID=3;
update Books set amount=100 where book_ID=4;
update Books set amount=100 where book_ID=5;
update Books set amount=100 where book_ID=6;
update Books set amount=100 where book_ID=7;
update Books set amount=100 where book_ID=8;
update Books set amount=100 where book_ID=9;
update Books set amount=100 where book_ID=10;
update Books set amount=100 where book_ID=11;
update Books set amount=100 where book_ID=12;
update Books set amount=100 where book_ID=13;
update Books set amount=100 where book_ID=14;
update Books set amount=100 where book_ID=15;
update Books set amount=100 where book_ID=16;
update Books set amount=100 where book_ID=17;
update Books set amount=100 where book_ID=18;
update Books set amount=100 where book_ID=19;
update Books set amount=100 where book_ID=20;

-- SELECT o.order_ID, o.order_date, c.customer_name, c.phoneNumber, c.customer_address, b.book_name, b.price, b.amount
-- FROM Orders o INNER JOIN Customers c ON o.customer_ID = c.customer_ID
-- 	INNER JOIN OrderDetails od ON o.order_ID = od.order_ID
-- 	INNER JOIN Books b ON b.Book_ID = od.Book_ID
-- 	WHERE o.order_ID = 1;
-- SELECT
--     o.order_ID,
--     o.order_date,
--     c.customer_name,
--     c.phoneNumber,
--     c.customer_address,
--     SUM(b.price + od.quantity) AS TotalAmount
-- FROM
--     Orders o
-- INNER JOIN
--     Customers c ON o.customer_ID = c.customer_ID
-- INNER JOIN
--     OrderDetails od ON o.order_ID = od.order_ID
-- INNER JOIN
--     Books b ON b.Book_ID = od.Book_ID
-- WHERE
--     o.order_ID = 1;

SELECT
        o.order_ID,
        o.order_date,
        c.customer_name,
        c.phoneNumber,
        c.customer_address,
        b.book_name,
        b.price,
        od.quantity,
        SUM(b.price * od.quantity) AS TotalAmount
    FROM
        Orders o
    INNER JOIN
        Customers c ON o.customer_ID = c.customer_ID
    INNER JOIN
        OrderDetails od ON o.order_ID = od.order_ID
    INNER JOIN
        Books b ON b.Book_ID = od.Book_ID
    WHERE
        o.order_ID = 1
    GROUP BY
        o.order_ID,
        o.order_date,
        c.customer_name,
        c.phoneNumber,
        c.customer_address,
        b.book_name,
        b.price,
        od.quantity;
        
        
        SELECT o.order_ID, o.order_date, o.order_status, s.staff_name, c.customer_name, c.phoneNumber, c.customer_address, b.book_name, b.price, od.quantity
                FROM Orders o
                INNER JOIN Staffs s ON o.staff_ID = s.staff_ID
                INNER JOIN Customers c ON o.customer_ID = c.customer_ID
                INNER JOIN OrderDetails od ON o.order_ID = od.order_ID
                INNER JOIN Books b ON b.Book_ID = od.Book_ID
                WHERE o.staff_ID = 1;