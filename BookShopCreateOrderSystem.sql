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
		('Dinh Thanh Tuan','staff', '4D7D719AC0CF3D78EA8A94701913FE47', '1');
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
	('Luu Minh Tri','0987654321', 'Ha Noi'),
	('Stephen hawking','0987654321', 'Mi'),
    ('Gege Akutami','097865231', 'Nhat ban'),
    ('Fujiko F Fujio','988765231', 'Nhat ban'),
    ('Alex Prud’homme','123456789', 'Mi'),
    ('Shima Mizuki','0987654321', 'Nhat Ban'),
    ('Nha bao Phan Dang','098765231', 'Ha noi'),
    ('To Hoai','097865231', 'Ha Noi'),
    ('Cuong Tuyet An','988765231', 'Hai Phong'),
    ('Doan Gioi','123456789', 'Ha Noi'),
    ('Hwang Sun-mi', '134713912', 'Han Quoc'),
    ('E.B White','123456789', 'Mi'),
    ('Atsushi Ohkubo','123456789', 'Nhat Ban'),
    ('Riichiro Inagaki','123456789', 'Nhat Ban'),
    ('Kaidou Sakon, Taiki','123456789', 'Nhat Ban'),
    ('Khuong Le Binh','123456789', 'Ho Chi Minh'),
    ('Diep Lac Vo Tam','123456789', 'Ho Chi Minh');
select * from Authors;

insert into Publishers(publisher_name, phoneNumber, publisher_address, website) values
	('NXB Tre','0987634321', 'Ha Noi', 'nxbtre.com'),
    ('Kim Dong','09887656231', 'Ha Noi', 'nxbKimDong.com'),
    ('NXB Ha Noi','0942834205', 'Ha noi', 'nxbHaNoi.com'),
    ('Kim Dong','09887656231', 'Ha Noi', 'nxbKimDong.com'),
    ('NXB Tri Thuc', 0237845629, 'Ha Noi','nxbTriThuc.com'),
    ('Kim Dong','09887656231', 'Ha Noi', 'nxbKimDong.com'),
    ('Kim Dong','09887656231', 'Ha Noi', 'nxbKimDong.com'),
    ('Kim Dong','09887656231', 'Ha Noi', 'nxbKimDong.com'),
    ('Kim Dong','09887656231', 'Ha Noi', 'nxbKimDong.com'),
    ('Kim Dong','09887656231', 'Ha Noi', 'nxbKimDong.com'),
    ('Nha Nam','09887656231', 'Ha Noi', 'nhaNam.com'),
    ('Nha Nam','09887656231', 'Ha Noi', 'nhaNam.com'),
    ('NXB Tre','0987634321', 'Ha Noi', 'nxbtre.com'),
    ('Kim Dong','09887656231', 'Ha Noi', 'nxbKimDong.com'),
    ('NXB Ha Noi','0942834205', 'Ha noi', 'nxbHaNoi.com'),
    ('NXB tong hop TPHCM','0942834205', 'Ho Chi Minh', 'nxbHCM.com'),
    ('Phu nu','0942834205', 'Ho Chi Minh', 'nxbHCM.com');
select * from Publishers;


insert into Books(book_name, publisher_ID, ISBN, publish_year, book_description, price, amount, book_status) values
	('LANG CO HA NOI', 3, 1, 2019, 'Bo sach "lang co Ha Noi" la de tai thuoc mang sach kinh te, van hoa, xa hoi của Du an “Tu sach Thang Long ngan nam van hien” (giai doan 2)', 25000, 30, 1),
    ('BAN THIET KE VI DAI', 1, 2, 2016, 'noi dung cuon sach, nhu tac gia noi ngay dau cua chuong mot, la chuyen "bi an kiep nhan sinh", la nhung cau hoi toi hau ve su song, vu tru va van vat', 100000, 40, 1),
    ('CHU THUAT HOI CHIEN', 2, 3, 1994, 'chu thuat hoi chien-tap20', 27000, 60, 1),
    ('DORAEMON', 4, 4, 2006, 'Doraemon-tap 4', 20000, 50, 1),
    ('KI NGUYEN KHO HAN', 5, 5, 2022, 'Hang nghin nguoi da song ma khong co tinh yeu - khong mot ai song ma khong co nuoc', 189000, 20, 1),
    ('THAM TU LUNG DANH CONAN', 6, 6, 2023, 'Tham tu lung danh Conan: tau ngam sat mau den', 45000, 35, 1),
    ('36 DOAN THIEN DE THAY', 7, 7, 2022, 'La nhung doan van, ghi lai cam nhan cua tac gia bang con mat thien', 125000, 28, 1),
    ('DE MEN PHIEU LUU KI', 8, 8, 1941, 'De Men Phieu Luu Ki la tac pham van xuoi dac sac va noi tieng nhat của nha van To Hoai viet ve loai vat, danh cho lua tuoi thieu nhi', 45000, 40, 1),
    ('HO SO TAM LI TOI PHAM', 9, 9, 2023, 'Series Ho So Tam Li Toi Pham (gom 5 tap) la mot bo tieu thuyet viet ve cuoc dieu tra hinh su dua tren tam li toi pham ung dung', 126000, 20, 1),
    ('DAT RUNG PHUONG NAM', 10, 10, 1957, 'Dat Rung Phuong Nam la mot trong nhung tac pham viet ve Nam Bo xuat sac nhat, lam bat len tron ven ve dep con nguoi va thien nhien noi day', 76000, 34, 1),
    ('CO GA MAI XONG CHUONG', 11, 11, 2023, 'Mot trong nhung tac pham duoc yeu thich nhat cua van hoc thieu nhi han quoc', 87000, 23, 1),
    ('TIENG KEN THIEN NGA', 12, 12, 2023, 'Di dom, dang yeu, tran day tinh yeu gia dinh, ban be va tham dam ve dep thien nhien hoang da, them mot tac pham kinh dien tu E. B. White khien ta nao long', 64000, 60, 1),
    ('FIRE FORCE', 13, 13, 2023, 'Truyen lay boi canh the gioi khi con doi mat voi hien tuong “nhan the boc hoa”, tuc con nguoi tu boc chay' , 40000, 50, 1),
    ('DR.STONE', 14, 14, 2023, 'Dr.STONE - Tap 19: thanh pho ngo 1 trieu dan', 25000, 60, 1),
    ('INFINITE DENDROGRAM', 15, 15, 2023, 'Infinite Dendrogram - Tap 5 - Nhung nguoi ket noi cac kha nang', 97000, 30, 1),
    ('GIAO TRINH CHUAN HSK 1', 16, 16, 2022, 'Duoc chia thanh sau cap do voi 18 cuon', 1064000, 20, 1),
    ('NU HON CUA SOI', 17, 17, 2023, 'Neu An Di Phong khong tinh la dan ong, tren the gioi nay khong ai dam noi minh la dan ong!', 80000, 10, 1);
select * from Books;

insert into Categories(category_name) values
	('Van Hoa-Xa Hoi'),
    ('Khoa Hoc'),
    ('Manga'),
    ('Tieu Thuyet'),
    ('But Ki'),
    ('Truyen Ngan'),
    ('Sach hoc ngoai ngu'),
    ('Ngon tinh'),
    ('Light novel');
select * from Categories;



insert into CategoryDetails(book_ID, category_ID) values
	(1, 1), (2, 2), (3, 3),(4,3),(5,4),(6,3),(7,1),(8,5),(9,4),(10,4), (11,6), (12,4), (13,3), (14,3), (15,9), (16,7), (17,8);
select * from OrderDetails;

insert into Authors_Books(book_ID, author_ID) values
	(1, 1), (2, 2), (3, 3), (4,4),(5,5),(6,6),(7,7),(8,8),(9,9),(10,10),(11,11),(12,12), (13,13), (14,14), (15,15), (16,16), (17,17);
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