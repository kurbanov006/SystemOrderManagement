create table customers (
    Id uuid primary key default gen_random_uuid(),
    FullName varchar(255) unique not null,
    Email varchar(255) unique not null,
    Phone varchar(50),
    CreatedAt date default current_date
);

create table products (
    Id uuid PRIMARY KEY default gen_random_uuid(),
    Name varchar(255) unique not null,
    Price decimal(10, 2) not null,
    StockQuantity int not null,
    CreatedAt date default current_date
);

create table orders
(
    Id uuid primary key default gen_random_uuid(),
    CustomerId uuid references Customers(Id),
    TotalAmount decimal(10, 2) not null,
    OrderDate date default current_date,
    Status varchar(50) check (Status in ('Pending', 'Completed')),
    CreatedAt date default current_date
);

create table OrderItems (
    Id uuid primary key default gen_random_uuid(),
    OrderId uuid references Orders(Id),
    ProductId uuid references Products(Id),
    Quantity int not null,
    Price decimal(10, 2) not null,
    CreatedAt date default current_date
);


