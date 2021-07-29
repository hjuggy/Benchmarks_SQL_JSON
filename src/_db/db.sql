-- Database: SqlJson

-- DROP DATABASE "SqlJson";

CREATE DATABASE "SqlJson"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_United States.1251'
    LC_CTYPE = 'English_United States.1251'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

-- Table: public.User

-- DROP TABLE public."User";

CREATE TABLE public."User"
(
    "User_id" uuid NOT NULL,
    "FirstName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "LastName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(200) COLLATE pg_catalog."default",
    "Phone" character varying(30) COLLATE pg_catalog."default",
    "Country" character varying(50) COLLATE pg_catalog."default",
    "Sity" character varying(50) COLLATE pg_catalog."default",
    "Address" character varying(150) COLLATE pg_catalog."default",
    "Gender" character varying(10) COLLATE pg_catalog."default",
    "DayOfBirth" date,
    CONSTRAINT "User_pkey" PRIMARY KEY ("User_id")
)

TABLESPACE pg_default;

ALTER TABLE public."User"
    OWNER to postgres;

-- Table: public.Subscription

-- DROP TABLE public."Subscription";

CREATE TABLE public."Subscription"
(
    "Subscription_Id" uuid NOT NULL,
    "User_Id" uuid NOT NULL,
    "Name" character varying(100) COLLATE pg_catalog."default",
    "Description" character varying(500) COLLATE pg_catalog."default",
    "MinPrice" numeric,
    "MaxPrice" numeric,
    CONSTRAINT "Subscription_pkey" PRIMARY KEY ("Subscription_Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Subscription"
    OWNER to postgres;

-- Table: public.Order

-- DROP TABLE public."Order";

CREATE TABLE public."Order"
(
    "Order_Id" uuid NOT NULL,
    "User_Id" uuid NOT NULL,
    "Price" numeric,
    "DayOfOrder" date,
    "Name" character varying(100) COLLATE pg_catalog."default",
    "Description" character varying(500) COLLATE pg_catalog."default",
    CONSTRAINT "Order_pkey" PRIMARY KEY ("Order_Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Order"
    OWNER to postgres;