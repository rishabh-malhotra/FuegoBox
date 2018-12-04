create table Product(
ID uniqueidentifier primary Key,
CategoryID uniqueidentifier not null,
Name nvarchar(100) not null,
Title nvarchar(150) not null,
OrderLimit int,
Description nvarchar(250)  
)
create Table Variant(
ID uniqueidentifier primary Key,
ProductID uniqueidentifier not null,
ListingPrice float not null,
Discount float not null,
QuantitySold int not null,
Inventory int not null,
)
create table Category(
ID uniqueidentifier primary Key,
Name varchar(50) not null,
ProductsSold int not null
)
create table VariantImage(
ID uniqueidentifier primary Key,
VariantID uniqueidentifier not null,
ImageURL varchar(200) not null,
)
create table Property(
 ID uniqueidentifier primary Key,
 Name varchar(50) not null
)

create table Value(
 ID uniqueidentifier primary key,
 Name varchar(50) not null
)

create table VariantPropertyValue(
 ID uniqueidentifier primary key ,
 PropertyID uniqueidentifier not null,
 ValueID uniqueidentifier not null
)
create table VariantProperty(
 ID uniqueidentifier primary key,
 VariantID uniqueidentifier not null,
 PropertyValueID uniqueidentifier not null
)
CREATE TABLE Cart
    (
              ID uniqueidentifier primary key,
              VariantID uniqueidentifier not null, 
              SellingPrice float not null,
              Qty int not null,
    )

----------------------OrderProduct Table-----------------------------
CREATE TABLE OrderProduct
    (
              ID uniqueidentifier primary key ,
              OrderID uniqueidentifier not null,
              VariantID uniqueidentifier not null, 
              SellingPrice float not null,
              Qty int not null
              
    )

       ----------------------Order Table(YYYY-MM-DD hh:mm:ss)-----------------------------
CREATE TABLE [Order]
    (
              ID uniqueidentifier primary key,
              UserID uniqueidentifier not null,
              OrderDate smalldatetime not null,
              TotalAmount float not null,
              DeliveryAddressID uniqueidentifier not null,
              DeliveryDate smalldatetime not null,
              StatusID uniqueidentifier not null,
              isCancelled char(2) not null
    )

       ----------------------Status Table-----------------------------
CREATE TABLE [Status]
    (
              ID uniqueidentifier primary key,
              [description] varchar(100)
    )

 create table Role (
 ID uniqueidentifier primary key ,
 Name nvarchar(100) not null
)


create table [User] (
 ID uniqueidentifier primary Key,
 Name nvarchar(100) not null,
 HashPassword nvarchar(100) not null,
 Email nvarchar(100) not null,
 PhoneNumber int not null,
 RoleID uniqueidentifier not null,
 DefaultAddressID uniqueidentifier not null
)

create table Address(
  ID uniqueidentifier primary Key,
  AddressLine1 nvarchar(100) not null,
  AddressLine2 nvarchar(100),
  Pin int not null,
  City nvarchar(50) not null,
  State nvarchar(50) not null,
  Country nvarchar(50) not null
)

create table UserAddress (
 UserID uniqueidentifier primary key,
 AddressID uniqueidentifier not null
)