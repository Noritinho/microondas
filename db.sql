create database `microwave`;

create table HeatingPreset
(
    Id           int auto_increment
        primary key,
    Identifier   varchar(64)  not null,
    Name         varchar(64)  not null,
    Food         varchar(64)  not null,
    Duration     int          not null,
    Potency      int          not null,
    Instructions varchar(256) not null
);

create table User
(
    Id       int auto_increment
        primary key,
    Username varchar(255) not null,
    Password varchar(255) not null
);

