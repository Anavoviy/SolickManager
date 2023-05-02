<<<<<<< HEAD:CreateDB.sql
﻿--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

-- Started on 2023-05-03 07:33:29

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE "SolickManager";
--
-- TOC entry 3599 (class 1262 OID 32768)
-- Name: SolickManager; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "SolickManager" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';


ALTER DATABASE "SolickManager" OWNER TO postgres;

\connect "SolickManager"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 3600 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 248 (class 1259 OID 41071)
-- Name: application; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.application (
    id integer NOT NULL,
    "Data" date NOT NULL,
    idclient integer NOT NULL,
    idmanager integer NOT NULL,
    problem text NOT NULL,
    "Diagnostics" text,
    status text NOT NULL,
    deleted boolean DEFAULT false NOT NULL,
    notes text,
    iddevice integer
);


ALTER TABLE public.application OWNER TO postgres;

--
-- TOC entry 247 (class 1259 OID 41070)
-- Name: application_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.application_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.application_id_seq OWNER TO postgres;

--
-- TOC entry 3601 (class 0 OID 0)
-- Dependencies: 247
-- Name: application_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.application_id_seq OWNED BY public.application.id;


--
-- TOC entry 254 (class 1259 OID 41148)
-- Name: applicationassembly; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.applicationassembly (
    idapplication integer NOT NULL,
    idassemby integer NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.applicationassembly OWNER TO postgres;

--
-- TOC entry 255 (class 1259 OID 41162)
-- Name: applicationproduct; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.applicationproduct (
    idapplication integer NOT NULL,
    idproduct integer NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.applicationproduct OWNER TO postgres;

--
-- TOC entry 253 (class 1259 OID 41115)
-- Name: applicationservice; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.applicationservice (
    idapplication integer NOT NULL,
    idservice integer NOT NULL,
    idworker integer NOT NULL,
    deleted boolean DEFAULT false NOT NULL,
    notes text
);


ALTER TABLE public.applicationservice OWNER TO postgres;

--
-- TOC entry 243 (class 1259 OID 40998)
-- Name: assembly; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.assembly (
    id integer NOT NULL,
    title character varying,
    idmasterconfiguration integer NOT NULL,
    idmasterassembler integer NOT NULL,
    "Data" date NOT NULL,
    "Cost" money NOT NULL,
    description text,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.assembly OWNER TO postgres;

--
-- TOC entry 242 (class 1259 OID 40997)
-- Name: assembly_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.assembly_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.assembly_id_seq OWNER TO postgres;

--
-- TOC entry 3602 (class 0 OID 0)
-- Dependencies: 242
-- Name: assembly_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.assembly_id_seq OWNED BY public.assembly.id;


--
-- TOC entry 244 (class 1259 OID 41017)
-- Name: assemblyproduct; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.assemblyproduct (
    idassembly integer NOT NULL,
    idproduct integer NOT NULL,
    count integer DEFAULT 1 NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.assemblyproduct OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 32779)
-- Name: bankaccount; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.bankaccount (
    id integer NOT NULL,
    title character varying NOT NULL,
    balance money,
    deleted boolean DEFAULT false
);


ALTER TABLE public.bankaccount OWNER TO postgres;

--
-- TOC entry 3603 (class 0 OID 0)
-- Dependencies: 215
-- Name: TABLE bankaccount; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public.bankaccount IS 'Таблица содержит счета (внутренние счета) компании (организации)';


--
-- TOC entry 214 (class 1259 OID 32778)
-- Name: bankaccount_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.bankaccount_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.bankaccount_id_seq OWNER TO postgres;

--
-- TOC entry 3604 (class 0 OID 0)
-- Dependencies: 214
-- Name: bankaccount_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.bankaccount_id_seq OWNED BY public.bankaccount.id;


--
-- TOC entry 221 (class 1259 OID 32819)
-- Name: category; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.category (
    id integer NOT NULL,
    title character varying NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.category OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 32818)
-- Name: category_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.category_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.category_id_seq OWNER TO postgres;

--
-- TOC entry 3605 (class 0 OID 0)
-- Dependencies: 220
-- Name: category_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.category_id_seq OWNED BY public.category.id;


--
-- TOC entry 222 (class 1259 OID 32828)
-- Name: categorycharacteristic; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.categorycharacteristic (
    idcategory integer NOT NULL,
    idcharacteristic integer NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.categorycharacteristic OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 32809)
-- Name: characteristic; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.characteristic (
    id integer NOT NULL,
    title character varying NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.characteristic OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 32808)
-- Name: characteristic_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.characteristic_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.characteristic_id_seq OWNER TO postgres;

--
-- TOC entry 3606 (class 0 OID 0)
-- Dependencies: 218
-- Name: characteristic_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.characteristic_id_seq OWNED BY public.characteristic.id;


--
-- TOC entry 246 (class 1259 OID 41057)
-- Name: client; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.client (
    id integer NOT NULL,
    firstname character varying NOT NULL,
    secondname character varying NOT NULL,
    patronymic character varying,
    birthday date,
    passport character varying,
    phone character varying,
    email character varying,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.client OWNER TO postgres;

--
-- TOC entry 245 (class 1259 OID 41056)
-- Name: client_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.client_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.client_id_seq OWNER TO postgres;

--
-- TOC entry 3607 (class 0 OID 0)
-- Dependencies: 245
-- Name: client_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.client_id_seq OWNED BY public.client.id;


--
-- TOC entry 250 (class 1259 OID 41091)
-- Name: clientsdevice; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.clientsdevice (
    id integer NOT NULL,
    idclient integer NOT NULL,
    model character varying,
    description character varying,
    "Cost" money,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.clientsdevice OWNER TO postgres;

--
-- TOC entry 249 (class 1259 OID 41090)
-- Name: clientsdevice_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.clientsdevice_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.clientsdevice_id_seq OWNER TO postgres;

--
-- TOC entry 3608 (class 0 OID 0)
-- Dependencies: 249
-- Name: clientsdevice_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.clientsdevice_id_seq OWNED BY public.clientsdevice.id;


--
-- TOC entry 235 (class 1259 OID 32927)
-- Name: howpay; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.howpay (
    id integer NOT NULL,
    title character varying NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.howpay OWNER TO postgres;

--
-- TOC entry 234 (class 1259 OID 32926)
-- Name: howpay_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.howpay_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.howpay_id_seq OWNER TO postgres;

--
-- TOC entry 3609 (class 0 OID 0)
-- Dependencies: 234
-- Name: howpay_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.howpay_id_seq OWNED BY public.howpay.id;


--
-- TOC entry 217 (class 1259 OID 32789)
-- Name: operation; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.operation (
    id integer NOT NULL,
    dataopen date NOT NULL,
    dataclose date,
    debet integer,
    credit integer,
    "Object" character varying,
    amount money NOT NULL,
    status character varying NOT NULL,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.operation OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 32788)
-- Name: operation_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.operation_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.operation_id_seq OWNER TO postgres;

--
-- TOC entry 3610 (class 0 OID 0)
-- Dependencies: 216
-- Name: operation_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.operation_id_seq OWNED BY public.operation.id;


--
-- TOC entry 237 (class 1259 OID 32937)
-- Name: plan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.plan (
    id integer NOT NULL,
    idhowpay integer NOT NULL,
    costofone money NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.plan OWNER TO postgres;

--
-- TOC entry 236 (class 1259 OID 32936)
-- Name: plan_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.plan_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.plan_id_seq OWNER TO postgres;

--
-- TOC entry 3611 (class 0 OID 0)
-- Dependencies: 236
-- Name: plan_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.plan_id_seq OWNED BY public.plan.id;


--
-- TOC entry 233 (class 1259 OID 32916)
-- Name: post; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.post (
    id integer NOT NULL,
    title character varying NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.post OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 32915)
-- Name: post_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.post_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.post_id_seq OWNER TO postgres;

--
-- TOC entry 3612 (class 0 OID 0)
-- Dependencies: 232
-- Name: post_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.post_id_seq OWNED BY public.post.id;


--
-- TOC entry 228 (class 1259 OID 32868)
-- Name: product; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.product (
    id integer NOT NULL,
    idcategory integer NOT NULL,
    model character varying NOT NULL,
    description text,
    idshipment integer NOT NULL,
    "Cost" money NOT NULL,
    amount integer DEFAULT 0 NOT NULL
);


ALTER TABLE public.product OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 32867)
-- Name: product_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.product_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.product_id_seq OWNER TO postgres;

--
-- TOC entry 3613 (class 0 OID 0)
-- Dependencies: 227
-- Name: product_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.product_id_seq OWNED BY public.product.id;


--
-- TOC entry 231 (class 1259 OID 32899)
-- Name: productcharacteristic; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.productcharacteristic (
    idproduct integer NOT NULL,
    idcharacteristic integer NOT NULL,
    meaning character varying,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.productcharacteristic OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 32887)
-- Name: productpricechange; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.productpricechange (
    id integer NOT NULL,
    idproduct integer NOT NULL,
    ratio numeric(10,3) NOT NULL,
    newcost money NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.productpricechange OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 32886)
-- Name: productpricechange_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.productpricechange_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.productpricechange_id_seq OWNER TO postgres;

--
-- TOC entry 3614 (class 0 OID 0)
-- Dependencies: 229
-- Name: productpricechange_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.productpricechange_id_seq OWNED BY public.productpricechange.id;


--
-- TOC entry 224 (class 1259 OID 32843)
-- Name: provider; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.provider (
    id integer NOT NULL,
    title character varying NOT NULL,
    director_manager character varying NOT NULL,
    phone character varying,
    email character varying,
    requisites text,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.provider OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 32842)
-- Name: provider_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.provider_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.provider_id_seq OWNER TO postgres;

--
-- TOC entry 3615 (class 0 OID 0)
-- Dependencies: 223
-- Name: provider_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.provider_id_seq OWNED BY public.provider.id;


--
-- TOC entry 252 (class 1259 OID 41106)
-- Name: service; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.service (
    id integer NOT NULL,
    title character varying,
    description text,
    "Cost" money NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.service OWNER TO postgres;

--
-- TOC entry 251 (class 1259 OID 41105)
-- Name: service_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.service_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.service_id_seq OWNER TO postgres;

--
-- TOC entry 3616 (class 0 OID 0)
-- Dependencies: 251
-- Name: service_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.service_id_seq OWNED BY public.service.id;


--
-- TOC entry 226 (class 1259 OID 32853)
-- Name: shipment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.shipment (
    id integer NOT NULL,
    "Data" date NOT NULL,
    idprovider integer NOT NULL,
    numberproducts integer,
    amount money,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.shipment OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 32852)
-- Name: shipment_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.shipment_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.shipment_id_seq OWNER TO postgres;

--
-- TOC entry 3617 (class 0 OID 0)
-- Dependencies: 225
-- Name: shipment_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.shipment_id_seq OWNED BY public.shipment.id;


--
-- TOC entry 239 (class 1259 OID 40963)
-- Name: worker; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.worker (
    id integer NOT NULL,
    firstname character varying NOT NULL,
    surname character varying NOT NULL,
    patronymic character varying,
    birthday date NOT NULL,
    idpost integer NOT NULL,
    graphic character varying,
    idplan integer,
    passport character varying NOT NULL,
    phone character varying NOT NULL,
    email character varying NOT NULL,
    requisites character varying,
    deleted boolean DEFAULT false NOT NULL,
    login character varying NOT NULL,
    password character varying NOT NULL
);


ALTER TABLE public.worker OWNER TO postgres;

--
-- TOC entry 238 (class 1259 OID 40962)
-- Name: worker_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.worker_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.worker_id_seq OWNER TO postgres;

--
-- TOC entry 3618 (class 0 OID 0)
-- Dependencies: 238
-- Name: worker_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.worker_id_seq OWNED BY public.worker.id;


--
-- TOC entry 241 (class 1259 OID 40985)
-- Name: workingshift; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.workingshift (
    id integer NOT NULL,
    "Data" date NOT NULL,
    idworker integer NOT NULL,
    spendunits integer NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.workingshift OWNER TO postgres;

--
-- TOC entry 240 (class 1259 OID 40984)
-- Name: workingshift_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.workingshift_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.workingshift_id_seq OWNER TO postgres;

--
-- TOC entry 3619 (class 0 OID 0)
-- Dependencies: 240
-- Name: workingshift_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.workingshift_id_seq OWNED BY public.workingshift.id;


--
-- TOC entry 3316 (class 2604 OID 41074)
-- Name: application id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application ALTER COLUMN id SET DEFAULT nextval('public.application_id_seq'::regclass);


--
-- TOC entry 3310 (class 2604 OID 41001)
-- Name: assembly id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly ALTER COLUMN id SET DEFAULT nextval('public.assembly_id_seq'::regclass);


--
-- TOC entry 3282 (class 2604 OID 32782)
-- Name: bankaccount id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bankaccount ALTER COLUMN id SET DEFAULT nextval('public.bankaccount_id_seq'::regclass);


--
-- TOC entry 3288 (class 2604 OID 32822)
-- Name: category id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.category ALTER COLUMN id SET DEFAULT nextval('public.category_id_seq'::regclass);


--
-- TOC entry 3286 (class 2604 OID 32812)
-- Name: characteristic id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.characteristic ALTER COLUMN id SET DEFAULT nextval('public.characteristic_id_seq'::regclass);


--
-- TOC entry 3314 (class 2604 OID 41060)
-- Name: client id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client ALTER COLUMN id SET DEFAULT nextval('public.client_id_seq'::regclass);


--
-- TOC entry 3318 (class 2604 OID 41094)
-- Name: clientsdevice id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientsdevice ALTER COLUMN id SET DEFAULT nextval('public.clientsdevice_id_seq'::regclass);


--
-- TOC entry 3302 (class 2604 OID 32930)
-- Name: howpay id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.howpay ALTER COLUMN id SET DEFAULT nextval('public.howpay_id_seq'::regclass);


--
-- TOC entry 3284 (class 2604 OID 32792)
-- Name: operation id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation ALTER COLUMN id SET DEFAULT nextval('public.operation_id_seq'::regclass);


--
-- TOC entry 3304 (class 2604 OID 32940)
-- Name: plan id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.plan ALTER COLUMN id SET DEFAULT nextval('public.plan_id_seq'::regclass);


--
-- TOC entry 3300 (class 2604 OID 32919)
-- Name: post id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.post ALTER COLUMN id SET DEFAULT nextval('public.post_id_seq'::regclass);


--
-- TOC entry 3295 (class 2604 OID 32871)
-- Name: product id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product ALTER COLUMN id SET DEFAULT nextval('public.product_id_seq'::regclass);


--
-- TOC entry 3297 (class 2604 OID 32890)
-- Name: productpricechange id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productpricechange ALTER COLUMN id SET DEFAULT nextval('public.productpricechange_id_seq'::regclass);


--
-- TOC entry 3291 (class 2604 OID 32846)
-- Name: provider id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.provider ALTER COLUMN id SET DEFAULT nextval('public.provider_id_seq'::regclass);


--
-- TOC entry 3320 (class 2604 OID 41109)
-- Name: service id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service ALTER COLUMN id SET DEFAULT nextval('public.service_id_seq'::regclass);


--
-- TOC entry 3293 (class 2604 OID 32856)
-- Name: shipment id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shipment ALTER COLUMN id SET DEFAULT nextval('public.shipment_id_seq'::regclass);


--
-- TOC entry 3306 (class 2604 OID 40966)
-- Name: worker id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker ALTER COLUMN id SET DEFAULT nextval('public.worker_id_seq'::regclass);


--
-- TOC entry 3308 (class 2604 OID 40988)
-- Name: workingshift id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workingshift ALTER COLUMN id SET DEFAULT nextval('public.workingshift_id_seq'::regclass);


--
-- TOC entry 3586 (class 0 OID 41071)
-- Dependencies: 248
-- Data for Name: application; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3592 (class 0 OID 41148)
-- Dependencies: 254
-- Data for Name: applicationassembly; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3593 (class 0 OID 41162)
-- Dependencies: 255
-- Data for Name: applicationproduct; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3591 (class 0 OID 41115)
-- Dependencies: 253
-- Data for Name: applicationservice; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3581 (class 0 OID 40998)
-- Dependencies: 243
-- Data for Name: assembly; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3582 (class 0 OID 41017)
-- Dependencies: 244
-- Data for Name: assemblyproduct; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3553 (class 0 OID 32779)
-- Dependencies: 215
-- Data for Name: bankaccount; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.bankaccount VALUES (1, 'Основной фонд', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (2, 'Накопительный фонд', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (3, 'Активы', '0,00 ?', false
INSERT INTO public.bankaccount VALUES (3, 'Пассивы', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (4, 'ФОТ (Фонд Оплаты Труда)', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (5, 'Амортизационный фонд', '0,00 ?', false);


--
-- TOC entry 3559 (class 0 OID 32819)
-- Dependencies: 221
-- Data for Name: category; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3560 (class 0 OID 32828)
-- Dependencies: 222
-- Data for Name: categorycharacteristic; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3557 (class 0 OID 32809)
-- Dependencies: 219
-- Data for Name: characteristic; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3584 (class 0 OID 41057)
-- Dependencies: 246
-- Data for Name: client; Type: TABLE DATA; Schema: public; Owner: postgres
--


--
-- TOC entry 3588 (class 0 OID 41091)
-- Dependencies: 250
-- Data for Name: clientsdevice; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3573 (class 0 OID 32927)
-- Dependencies: 235
-- Data for Name: howpay; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.howpay VALUES (1, 'в час', false);
INSERT INTO public.howpay VALUES (2, 'в день', false);
INSERT INTO public.howpay VALUES (3, 'в неделю', false);
INSERT INTO public.howpay VALUES (4, 'в месяц', false);
INSERT INTO public.howpay VALUES (5, 'в полмесяца', false);

--
-- TOC entry 3555 (class 0 OID 32789)
-- Dependencies: 217
-- Data for Name: operation; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3575 (class 0 OID 32937)
-- Dependencies: 237
-- Data for Name: plan; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.plan VALUES (1, 1, '250,00 ?', false);
INSERT INTO public.plan VALUES (2, 2, '1500,00 ?', false);
INSERT INTO public.plan VALUES (3, 3, '10000,00 ?', false);
INSERT INTO public.plan VALUES (4, 4, '35000,00 ?', false);
INSERT INTO public.plan VALUES (5, 5, '18500,00 ?', false);



--
-- TOC entry 3571 (class 0 OID 32916)
-- Dependencies: 233
-- Data for Name: post; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.post VALUES (1, 'ADMIN', false);


--
-- TOC entry 3566 (class 0 OID 32868)
-- Dependencies: 228
-- Data for Name: product; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3569 (class 0 OID 32899)
-- Dependencies: 231
-- Data for Name: productcharacteristic; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3568 (class 0 OID 32887)
-- Dependencies: 230
-- Data for Name: productpricechange; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3562 (class 0 OID 32843)
-- Dependencies: 224
-- Data for Name: provider; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3590 (class 0 OID 41106)
-- Dependencies: 252
-- Data for Name: service; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3564 (class 0 OID 32853)
-- Dependencies: 226
-- Data for Name: shipment; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3577 (class 0 OID 40963)
-- Dependencies: 239
-- Data for Name: worker; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.worker VALUES (1, 'ADMIN', 'ADMIN', 'ADMIN', '2005-09-05', 1, NULL, NULL, '1', '1', '1', NULL, false, 'admin', 'admin');


--
-- TOC entry 3579 (class 0 OID 40985)
-- Dependencies: 241
-- Data for Name: workingshift; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3620 (class 0 OID 0)
-- Dependencies: 247
-- Name: application_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.application_id_seq', 12, true);


--
-- TOC entry 3621 (class 0 OID 0)
-- Dependencies: 242
-- Name: assembly_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.assembly_id_seq', 2, true);


--
-- TOC entry 3622 (class 0 OID 0)
-- Dependencies: 214
-- Name: bankaccount_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.bankaccount_id_seq', 6, true);


--
-- TOC entry 3623 (class 0 OID 0)
-- Dependencies: 220
-- Name: category_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.category_id_seq', 3, true);


--
-- TOC entry 3624 (class 0 OID 0)
-- Dependencies: 218
-- Name: characteristic_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.characteristic_id_seq', 67, true);


--
-- TOC entry 3625 (class 0 OID 0)
-- Dependencies: 245
-- Name: client_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.client_id_seq', 7, true);


--
-- TOC entry 3626 (class 0 OID 0)
-- Dependencies: 249
-- Name: clientsdevice_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.clientsdevice_id_seq', 12, true);


--
-- TOC entry 3627 (class 0 OID 0)
-- Dependencies: 234
-- Name: howpay_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.howpay_id_seq', 3, true);


--
-- TOC entry 3628 (class 0 OID 0)
-- Dependencies: 216
-- Name: operation_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.operation_id_seq', 4, true);


--
-- TOC entry 3629 (class 0 OID 0)
-- Dependencies: 236
-- Name: plan_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.plan_id_seq', 2, true);


--
-- TOC entry 3630 (class 0 OID 0)
-- Dependencies: 232
-- Name: post_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.post_id_seq', 4, true);


--
-- TOC entry 3631 (class 0 OID 0)
-- Dependencies: 227
-- Name: product_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.product_id_seq', 6, true);


--
-- TOC entry 3632 (class 0 OID 0)
-- Dependencies: 229
-- Name: productpricechange_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.productpricechange_id_seq', 4, true);


--
-- TOC entry 3633 (class 0 OID 0)
-- Dependencies: 223
-- Name: provider_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.provider_id_seq', 6, true);


--
-- TOC entry 3634 (class 0 OID 0)
-- Dependencies: 251
-- Name: service_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.service_id_seq', 6, true);


--
-- TOC entry 3635 (class 0 OID 0)
-- Dependencies: 225
-- Name: shipment_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.shipment_id_seq', 12, true);


--
-- TOC entry 3636 (class 0 OID 0)
-- Dependencies: 238
-- Name: worker_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.worker_id_seq', 5, true);


--
-- TOC entry 3637 (class 0 OID 0)
-- Dependencies: 240
-- Name: workingshift_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.workingshift_id_seq', 4, true);


--
-- TOC entry 3370 (class 2606 OID 41079)
-- Name: application application_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_pk PRIMARY KEY (id);


--
-- TOC entry 3378 (class 2606 OID 57381)
-- Name: applicationassembly applicationassembly_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationassembly
    ADD CONSTRAINT applicationassembly_pk PRIMARY KEY (idapplication, idassemby);


--
-- TOC entry 3380 (class 2606 OID 57377)
-- Name: applicationproduct applicationproduct_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationproduct
    ADD CONSTRAINT applicationproduct_pk PRIMARY KEY (idapplication, idproduct);


--
-- TOC entry 3376 (class 2606 OID 57379)
-- Name: applicationservice applicationservice_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_pk PRIMARY KEY (idapplication, idservice, idworker);


--
-- TOC entry 3360 (class 2606 OID 41006)
-- Name: assembly assembly_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly
    ADD CONSTRAINT assembly_pk PRIMARY KEY (id);


--
-- TOC entry 3362 (class 2606 OID 57383)
-- Name: assemblyproduct assemblyproduct_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assemblyproduct
    ADD CONSTRAINT assemblyproduct_pk PRIMARY KEY (idassembly, idproduct);


--
-- TOC entry 3326 (class 2606 OID 32787)
-- Name: bankaccount bankaccount_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bankaccount
    ADD CONSTRAINT bankaccount_pk PRIMARY KEY (id);


--
-- TOC entry 3332 (class 2606 OID 32827)
-- Name: category category_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.category
    ADD CONSTRAINT category_pk PRIMARY KEY (id);


--
-- TOC entry 3334 (class 2606 OID 57385)
-- Name: categorycharacteristic categorycharacteristic_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categorycharacteristic
    ADD CONSTRAINT categorycharacteristic_pk PRIMARY KEY (idcategory, idcharacteristic);


--
-- TOC entry 3330 (class 2606 OID 32817)
-- Name: characteristic characteristic_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.characteristic
    ADD CONSTRAINT characteristic_pk PRIMARY KEY (id);


--
-- TOC entry 3364 (class 2606 OID 41065)
-- Name: client client_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_pk PRIMARY KEY (id);


--
-- TOC entry 3366 (class 2606 OID 41067)
-- Name: client client_un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_un UNIQUE (passport);


--
-- TOC entry 3368 (class 2606 OID 41069)
-- Name: client client_un2; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_un2 UNIQUE (email);


--
-- TOC entry 3372 (class 2606 OID 41099)
-- Name: clientsdevice clientsdevice_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientsdevice
    ADD CONSTRAINT clientsdevice_pk PRIMARY KEY (id);


--
-- TOC entry 3348 (class 2606 OID 32935)
-- Name: howpay howpay_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.howpay
    ADD CONSTRAINT howpay_pk PRIMARY KEY (id);


--
-- TOC entry 3328 (class 2606 OID 32797)
-- Name: operation operation_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation
    ADD CONSTRAINT operation_pk PRIMARY KEY (id);


--
-- TOC entry 3350 (class 2606 OID 40961)
-- Name: plan plan_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.plan
    ADD CONSTRAINT plan_pk PRIMARY KEY (id);


--
-- TOC entry 3346 (class 2606 OID 32923)
-- Name: post post_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.post
    ADD CONSTRAINT post_pk PRIMARY KEY (id);


--
-- TOC entry 3340 (class 2606 OID 32875)
-- Name: product product_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_pk PRIMARY KEY (id);


--
-- TOC entry 3344 (class 2606 OID 57387)
-- Name: productcharacteristic productcharacteristic_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productcharacteristic
    ADD CONSTRAINT productcharacteristic_pk PRIMARY KEY (idproduct, idcharacteristic);


--
-- TOC entry 3342 (class 2606 OID 32893)
-- Name: productpricechange productpricechange_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productpricechange
    ADD CONSTRAINT productpricechange_pk PRIMARY KEY (id);


--
-- TOC entry 3336 (class 2606 OID 32851)
-- Name: provider provider_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.provider
    ADD CONSTRAINT provider_pk PRIMARY KEY (id);


--
-- TOC entry 3374 (class 2606 OID 41114)
-- Name: service service_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service
    ADD CONSTRAINT service_pk PRIMARY KEY (id);


--
-- TOC entry 3338 (class 2606 OID 32861)
-- Name: shipment shipment_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shipment
    ADD CONSTRAINT shipment_pk PRIMARY KEY (id);


--
-- TOC entry 3352 (class 2606 OID 40971)
-- Name: worker worker_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_pk PRIMARY KEY (id);


--
-- TOC entry 3354 (class 2606 OID 40973)
-- Name: worker worker_un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_un UNIQUE (passport);


--
-- TOC entry 3356 (class 2606 OID 49153)
-- Name: worker worker_un1; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_un1 UNIQUE (login);


--
-- TOC entry 3358 (class 2606 OID 40991)
-- Name: workingshift workingshift_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workingshift
    ADD CONSTRAINT workingshift_pk PRIMARY KEY (id);


--
-- TOC entry 3399 (class 2606 OID 41080)
-- Name: application application_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_fk FOREIGN KEY (idclient) REFERENCES public.client(id);


--
-- TOC entry 3400 (class 2606 OID 57365)
-- Name: application application_fk2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_fk2 FOREIGN KEY (iddevice) REFERENCES public.clientsdevice(id);


--
-- TOC entry 3401 (class 2606 OID 41085)
-- Name: application application_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_fk_1 FOREIGN KEY (idmanager) REFERENCES public.worker(id);


--
-- TOC entry 3408 (class 2606 OID 41166)
-- Name: applicationproduct applicationproduct_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationproduct
    ADD CONSTRAINT applicationproduct_fk FOREIGN KEY (idapplication) REFERENCES public.application(id);


--
-- TOC entry 3409 (class 2606 OID 41171)
-- Name: applicationproduct applicationproduct_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationproduct
    ADD CONSTRAINT applicationproduct_fk_1 FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3403 (class 2606 OID 41118)
-- Name: applicationservice applicationservice_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_fk FOREIGN KEY (idapplication) REFERENCES public.application(id);


--
-- TOC entry 3404 (class 2606 OID 41123)
-- Name: applicationservice applicationservice_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_fk_1 FOREIGN KEY (idservice) REFERENCES public.service(id);


--
-- TOC entry 3405 (class 2606 OID 41128)
-- Name: applicationservice applicationservice_fk_2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_fk_2 FOREIGN KEY (idworker) REFERENCES public.worker(id);


--
-- TOC entry 3395 (class 2606 OID 41007)
-- Name: assembly assembly_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly
    ADD CONSTRAINT assembly_fk FOREIGN KEY (idmasterconfiguration) REFERENCES public.worker(id);


--
-- TOC entry 3396 (class 2606 OID 41012)
-- Name: assembly assembly_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly
    ADD CONSTRAINT assembly_fk_1 FOREIGN KEY (idmasterassembler) REFERENCES public.worker(id);


--
-- TOC entry 3397 (class 2606 OID 41022)
-- Name: assemblyproduct assemblyproduct_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assemblyproduct
    ADD CONSTRAINT assemblyproduct_fk FOREIGN KEY (idassembly) REFERENCES public.assembly(id);


--
-- TOC entry 3398 (class 2606 OID 41027)
-- Name: assemblyproduct assemblyproduct_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assemblyproduct
    ADD CONSTRAINT assemblyproduct_fk_1 FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3383 (class 2606 OID 32831)
-- Name: categorycharacteristic categorycharacteristic_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categorycharacteristic
    ADD CONSTRAINT categorycharacteristic_fk FOREIGN KEY (idcategory) REFERENCES public.category(id);


--
-- TOC entry 3384 (class 2606 OID 32836)
-- Name: categorycharacteristic categorycharacteristic_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categorycharacteristic
    ADD CONSTRAINT categorycharacteristic_fk_1 FOREIGN KEY (idcharacteristic) REFERENCES public.characteristic(id);


--
-- TOC entry 3402 (class 2606 OID 41100)
-- Name: clientsdevice clientsdevice_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientsdevice
    ADD CONSTRAINT clientsdevice_fk FOREIGN KEY (idclient) REFERENCES public.client(id);


--
-- TOC entry 3406 (class 2606 OID 41152)
-- Name: applicationassembly newtable_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationassembly
    ADD CONSTRAINT newtable_fk FOREIGN KEY (idapplication) REFERENCES public.application(id);


--
-- TOC entry 3407 (class 2606 OID 41157)
-- Name: applicationassembly newtable_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationassembly
    ADD CONSTRAINT newtable_fk_1 FOREIGN KEY (idassemby) REFERENCES public.assembly(id);


--
-- TOC entry 3381 (class 2606 OID 32798)
-- Name: operation operation_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation
    ADD CONSTRAINT operation_fk FOREIGN KEY (debet) REFERENCES public.bankaccount(id);


--
-- TOC entry 3382 (class 2606 OID 32803)
-- Name: operation operation_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation
    ADD CONSTRAINT operation_fk_1 FOREIGN KEY (credit) REFERENCES public.bankaccount(id);


--
-- TOC entry 3391 (class 2606 OID 32942)
-- Name: plan plan_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.plan
    ADD CONSTRAINT plan_fk FOREIGN KEY (idhowpay) REFERENCES public.howpay(id);


--
-- TOC entry 3389 (class 2606 OID 32904)
-- Name: productcharacteristic product_characteristic_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productcharacteristic
    ADD CONSTRAINT product_characteristic_fk FOREIGN KEY (idcharacteristic) REFERENCES public.characteristic(id);


--
-- TOC entry 3390 (class 2606 OID 32909)
-- Name: productcharacteristic product_characteristic_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productcharacteristic
    ADD CONSTRAINT product_characteristic_fk_1 FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3386 (class 2606 OID 32876)
-- Name: product product_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_fk FOREIGN KEY (idcategory) REFERENCES public.category(id);


--
-- TOC entry 3387 (class 2606 OID 32881)
-- Name: product product_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_fk_1 FOREIGN KEY (idshipment) REFERENCES public.shipment(id);


--
-- TOC entry 3388 (class 2606 OID 32894)
-- Name: productpricechange productpricechange_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productpricechange
    ADD CONSTRAINT productpricechange_fk FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3385 (class 2606 OID 32862)
-- Name: shipment shipment_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shipment
    ADD CONSTRAINT shipment_fk FOREIGN KEY (idprovider) REFERENCES public.provider(id);


--
-- TOC entry 3392 (class 2606 OID 40974)
-- Name: worker worker_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_fk FOREIGN KEY (idplan) REFERENCES public.plan(id);


--
-- TOC entry 3393 (class 2606 OID 40979)
-- Name: worker worker_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_fk_1 FOREIGN KEY (idpost) REFERENCES public.post(id);


--
-- TOC entry 3394 (class 2606 OID 40992)
-- Name: workingshift workingshift_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workingshift
    ADD CONSTRAINT workingshift_fk FOREIGN KEY (idworker) REFERENCES public.worker(id);


-- Completed on 2023-05-03 07:33:29

--
-- PostgreSQL database dump complete
--

=======
﻿--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

-- Started on 2023-05-03 07:33:29

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE "SolickManager";
--
-- TOC entry 3599 (class 1262 OID 32768)
-- Name: SolickManager; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "SolickManager" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';


ALTER DATABASE "SolickManager" OWNER TO postgres;

\connect "SolickManager"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 3600 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 248 (class 1259 OID 41071)
-- Name: application; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.application (
    id integer NOT NULL,
    "Data" date NOT NULL,
    idclient integer NOT NULL,
    idmanager integer NOT NULL,
    problem text NOT NULL,
    "Diagnostics" text,
    status text NOT NULL,
    deleted boolean DEFAULT false NOT NULL,
    notes text,
    iddevice integer
);


ALTER TABLE public.application OWNER TO postgres;

--
-- TOC entry 247 (class 1259 OID 41070)
-- Name: application_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.application_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.application_id_seq OWNER TO postgres;

--
-- TOC entry 3601 (class 0 OID 0)
-- Dependencies: 247
-- Name: application_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.application_id_seq OWNED BY public.application.id;


--
-- TOC entry 254 (class 1259 OID 41148)
-- Name: applicationassembly; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.applicationassembly (
    idapplication integer NOT NULL,
    idassemby integer NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.applicationassembly OWNER TO postgres;

--
-- TOC entry 255 (class 1259 OID 41162)
-- Name: applicationproduct; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.applicationproduct (
    idapplication integer NOT NULL,
    idproduct integer NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.applicationproduct OWNER TO postgres;

--
-- TOC entry 253 (class 1259 OID 41115)
-- Name: applicationservice; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.applicationservice (
    idapplication integer NOT NULL,
    idservice integer NOT NULL,
    idworker integer NOT NULL,
    deleted boolean DEFAULT false NOT NULL,
    notes text
);


ALTER TABLE public.applicationservice OWNER TO postgres;

--
-- TOC entry 243 (class 1259 OID 40998)
-- Name: assembly; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.assembly (
    id integer NOT NULL,
    title character varying,
    idmasterconfiguration integer NOT NULL,
    idmasterassembler integer NOT NULL,
    "Data" date NOT NULL,
    "Cost" money NOT NULL,
    description text,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.assembly OWNER TO postgres;

--
-- TOC entry 242 (class 1259 OID 40997)
-- Name: assembly_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.assembly_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.assembly_id_seq OWNER TO postgres;

--
-- TOC entry 3602 (class 0 OID 0)
-- Dependencies: 242
-- Name: assembly_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.assembly_id_seq OWNED BY public.assembly.id;


--
-- TOC entry 244 (class 1259 OID 41017)
-- Name: assemblyproduct; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.assemblyproduct (
    idassembly integer NOT NULL,
    idproduct integer NOT NULL,
    count integer DEFAULT 1 NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.assemblyproduct OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 32779)
-- Name: bankaccount; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.bankaccount (
    id integer NOT NULL,
    title character varying NOT NULL,
    balance money,
    deleted boolean DEFAULT false
);


ALTER TABLE public.bankaccount OWNER TO postgres;

--
-- TOC entry 3603 (class 0 OID 0)
-- Dependencies: 215
-- Name: TABLE bankaccount; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public.bankaccount IS 'Таблица содержит счета (внутренние счета) компании (организации)';


--
-- TOC entry 214 (class 1259 OID 32778)
-- Name: bankaccount_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.bankaccount_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.bankaccount_id_seq OWNER TO postgres;

--
-- TOC entry 3604 (class 0 OID 0)
-- Dependencies: 214
-- Name: bankaccount_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.bankaccount_id_seq OWNED BY public.bankaccount.id;


--
-- TOC entry 221 (class 1259 OID 32819)
-- Name: category; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.category (
    id integer NOT NULL,
    title character varying NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.category OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 32818)
-- Name: category_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.category_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.category_id_seq OWNER TO postgres;

--
-- TOC entry 3605 (class 0 OID 0)
-- Dependencies: 220
-- Name: category_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.category_id_seq OWNED BY public.category.id;


--
-- TOC entry 222 (class 1259 OID 32828)
-- Name: categorycharacteristic; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.categorycharacteristic (
    idcategory integer NOT NULL,
    idcharacteristic integer NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.categorycharacteristic OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 32809)
-- Name: characteristic; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.characteristic (
    id integer NOT NULL,
    title character varying NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.characteristic OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 32808)
-- Name: characteristic_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.characteristic_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.characteristic_id_seq OWNER TO postgres;

--
-- TOC entry 3606 (class 0 OID 0)
-- Dependencies: 218
-- Name: characteristic_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.characteristic_id_seq OWNED BY public.characteristic.id;


--
-- TOC entry 246 (class 1259 OID 41057)
-- Name: client; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.client (
    id integer NOT NULL,
    firstname character varying NOT NULL,
    secondname character varying NOT NULL,
    patronymic character varying,
    birthday date,
    passport character varying,
    phone character varying,
    email character varying,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.client OWNER TO postgres;

--
-- TOC entry 245 (class 1259 OID 41056)
-- Name: client_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.client_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.client_id_seq OWNER TO postgres;

--
-- TOC entry 3607 (class 0 OID 0)
-- Dependencies: 245
-- Name: client_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.client_id_seq OWNED BY public.client.id;


--
-- TOC entry 250 (class 1259 OID 41091)
-- Name: clientsdevice; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.clientsdevice (
    id integer NOT NULL,
    idclient integer NOT NULL,
    model character varying,
    description character varying,
    "Cost" money,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.clientsdevice OWNER TO postgres;

--
-- TOC entry 249 (class 1259 OID 41090)
-- Name: clientsdevice_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.clientsdevice_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.clientsdevice_id_seq OWNER TO postgres;

--
-- TOC entry 3608 (class 0 OID 0)
-- Dependencies: 249
-- Name: clientsdevice_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.clientsdevice_id_seq OWNED BY public.clientsdevice.id;


--
-- TOC entry 235 (class 1259 OID 32927)
-- Name: howpay; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.howpay (
    id integer NOT NULL,
    title character varying NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.howpay OWNER TO postgres;

--
-- TOC entry 234 (class 1259 OID 32926)
-- Name: howpay_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.howpay_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.howpay_id_seq OWNER TO postgres;

--
-- TOC entry 3609 (class 0 OID 0)
-- Dependencies: 234
-- Name: howpay_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.howpay_id_seq OWNED BY public.howpay.id;


--
-- TOC entry 217 (class 1259 OID 32789)
-- Name: operation; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.operation (
    id integer NOT NULL,
    dataopen date NOT NULL,
    dataclose date,
    debet integer,
    credit integer,
    "Object" character varying,
    amount money NOT NULL,
    status character varying NOT NULL,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.operation OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 32788)
-- Name: operation_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.operation_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.operation_id_seq OWNER TO postgres;

--
-- TOC entry 3610 (class 0 OID 0)
-- Dependencies: 216
-- Name: operation_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.operation_id_seq OWNED BY public.operation.id;


--
-- TOC entry 237 (class 1259 OID 32937)
-- Name: plan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.plan (
    id integer NOT NULL,
    idhowpay integer NOT NULL,
    costofone money NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.plan OWNER TO postgres;

--
-- TOC entry 236 (class 1259 OID 32936)
-- Name: plan_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.plan_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.plan_id_seq OWNER TO postgres;

--
-- TOC entry 3611 (class 0 OID 0)
-- Dependencies: 236
-- Name: plan_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.plan_id_seq OWNED BY public.plan.id;


--
-- TOC entry 233 (class 1259 OID 32916)
-- Name: post; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.post (
    id integer NOT NULL,
    title character varying NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.post OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 32915)
-- Name: post_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.post_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.post_id_seq OWNER TO postgres;

--
-- TOC entry 3612 (class 0 OID 0)
-- Dependencies: 232
-- Name: post_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.post_id_seq OWNED BY public.post.id;


--
-- TOC entry 228 (class 1259 OID 32868)
-- Name: product; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.product (
    id integer NOT NULL,
    idcategory integer NOT NULL,
    model character varying NOT NULL,
    description text,
    idshipment integer NOT NULL,
    "Cost" money NOT NULL,
    amount integer DEFAULT 0 NOT NULL
);


ALTER TABLE public.product OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 32867)
-- Name: product_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.product_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.product_id_seq OWNER TO postgres;

--
-- TOC entry 3613 (class 0 OID 0)
-- Dependencies: 227
-- Name: product_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.product_id_seq OWNED BY public.product.id;


--
-- TOC entry 231 (class 1259 OID 32899)
-- Name: productcharacteristic; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.productcharacteristic (
    idproduct integer NOT NULL,
    idcharacteristic integer NOT NULL,
    meaning character varying,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.productcharacteristic OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 32887)
-- Name: productpricechange; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.productpricechange (
    id integer NOT NULL,
    idproduct integer NOT NULL,
    ratio numeric(10,3) NOT NULL,
    newcost money NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.productpricechange OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 32886)
-- Name: productpricechange_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.productpricechange_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.productpricechange_id_seq OWNER TO postgres;

--
-- TOC entry 3614 (class 0 OID 0)
-- Dependencies: 229
-- Name: productpricechange_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.productpricechange_id_seq OWNED BY public.productpricechange.id;


--
-- TOC entry 224 (class 1259 OID 32843)
-- Name: provider; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.provider (
    id integer NOT NULL,
    title character varying NOT NULL,
    director_manager character varying NOT NULL,
    phone character varying,
    email character varying,
    requisites text,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.provider OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 32842)
-- Name: provider_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.provider_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.provider_id_seq OWNER TO postgres;

--
-- TOC entry 3615 (class 0 OID 0)
-- Dependencies: 223
-- Name: provider_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.provider_id_seq OWNED BY public.provider.id;


--
-- TOC entry 252 (class 1259 OID 41106)
-- Name: service; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.service (
    id integer NOT NULL,
    title character varying,
    description text,
    "Cost" money NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.service OWNER TO postgres;

--
-- TOC entry 251 (class 1259 OID 41105)
-- Name: service_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.service_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.service_id_seq OWNER TO postgres;

--
-- TOC entry 3616 (class 0 OID 0)
-- Dependencies: 251
-- Name: service_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.service_id_seq OWNED BY public.service.id;


--
-- TOC entry 226 (class 1259 OID 32853)
-- Name: shipment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.shipment (
    id integer NOT NULL,
    "Data" date NOT NULL,
    idprovider integer NOT NULL,
    numberproducts integer,
    amount money,
    notes text,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.shipment OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 32852)
-- Name: shipment_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.shipment_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.shipment_id_seq OWNER TO postgres;

--
-- TOC entry 3617 (class 0 OID 0)
-- Dependencies: 225
-- Name: shipment_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.shipment_id_seq OWNED BY public.shipment.id;


--
-- TOC entry 239 (class 1259 OID 40963)
-- Name: worker; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.worker (
    id integer NOT NULL,
    firstname character varying NOT NULL,
    surname character varying NOT NULL,
    patronymic character varying,
    birthday date NOT NULL,
    idpost integer NOT NULL,
    graphic character varying,
    idplan integer,
    passport character varying NOT NULL,
    phone character varying NOT NULL,
    email character varying NOT NULL,
    requisites character varying,
    deleted boolean DEFAULT false NOT NULL,
    login character varying NOT NULL,
    password character varying NOT NULL
);


ALTER TABLE public.worker OWNER TO postgres;

--
-- TOC entry 238 (class 1259 OID 40962)
-- Name: worker_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.worker_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.worker_id_seq OWNER TO postgres;

--
-- TOC entry 3618 (class 0 OID 0)
-- Dependencies: 238
-- Name: worker_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.worker_id_seq OWNED BY public.worker.id;


--
-- TOC entry 241 (class 1259 OID 40985)
-- Name: workingshift; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.workingshift (
    id integer NOT NULL,
    "Data" date NOT NULL,
    idworker integer NOT NULL,
    spendunits integer NOT NULL,
    deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.workingshift OWNER TO postgres;

--
-- TOC entry 240 (class 1259 OID 40984)
-- Name: workingshift_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.workingshift_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.workingshift_id_seq OWNER TO postgres;

--
-- TOC entry 3619 (class 0 OID 0)
-- Dependencies: 240
-- Name: workingshift_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.workingshift_id_seq OWNED BY public.workingshift.id;


--
-- TOC entry 3316 (class 2604 OID 41074)
-- Name: application id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application ALTER COLUMN id SET DEFAULT nextval('public.application_id_seq'::regclass);


--
-- TOC entry 3310 (class 2604 OID 41001)
-- Name: assembly id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly ALTER COLUMN id SET DEFAULT nextval('public.assembly_id_seq'::regclass);


--
-- TOC entry 3282 (class 2604 OID 32782)
-- Name: bankaccount id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bankaccount ALTER COLUMN id SET DEFAULT nextval('public.bankaccount_id_seq'::regclass);


--
-- TOC entry 3288 (class 2604 OID 32822)
-- Name: category id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.category ALTER COLUMN id SET DEFAULT nextval('public.category_id_seq'::regclass);


--
-- TOC entry 3286 (class 2604 OID 32812)
-- Name: characteristic id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.characteristic ALTER COLUMN id SET DEFAULT nextval('public.characteristic_id_seq'::regclass);


--
-- TOC entry 3314 (class 2604 OID 41060)
-- Name: client id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client ALTER COLUMN id SET DEFAULT nextval('public.client_id_seq'::regclass);


--
-- TOC entry 3318 (class 2604 OID 41094)
-- Name: clientsdevice id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientsdevice ALTER COLUMN id SET DEFAULT nextval('public.clientsdevice_id_seq'::regclass);


--
-- TOC entry 3302 (class 2604 OID 32930)
-- Name: howpay id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.howpay ALTER COLUMN id SET DEFAULT nextval('public.howpay_id_seq'::regclass);


--
-- TOC entry 3284 (class 2604 OID 32792)
-- Name: operation id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation ALTER COLUMN id SET DEFAULT nextval('public.operation_id_seq'::regclass);


--
-- TOC entry 3304 (class 2604 OID 32940)
-- Name: plan id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.plan ALTER COLUMN id SET DEFAULT nextval('public.plan_id_seq'::regclass);


--
-- TOC entry 3300 (class 2604 OID 32919)
-- Name: post id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.post ALTER COLUMN id SET DEFAULT nextval('public.post_id_seq'::regclass);


--
-- TOC entry 3295 (class 2604 OID 32871)
-- Name: product id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product ALTER COLUMN id SET DEFAULT nextval('public.product_id_seq'::regclass);


--
-- TOC entry 3297 (class 2604 OID 32890)
-- Name: productpricechange id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productpricechange ALTER COLUMN id SET DEFAULT nextval('public.productpricechange_id_seq'::regclass);


--
-- TOC entry 3291 (class 2604 OID 32846)
-- Name: provider id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.provider ALTER COLUMN id SET DEFAULT nextval('public.provider_id_seq'::regclass);


--
-- TOC entry 3320 (class 2604 OID 41109)
-- Name: service id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service ALTER COLUMN id SET DEFAULT nextval('public.service_id_seq'::regclass);


--
-- TOC entry 3293 (class 2604 OID 32856)
-- Name: shipment id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shipment ALTER COLUMN id SET DEFAULT nextval('public.shipment_id_seq'::regclass);


--
-- TOC entry 3306 (class 2604 OID 40966)
-- Name: worker id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker ALTER COLUMN id SET DEFAULT nextval('public.worker_id_seq'::regclass);


--
-- TOC entry 3308 (class 2604 OID 40988)
-- Name: workingshift id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workingshift ALTER COLUMN id SET DEFAULT nextval('public.workingshift_id_seq'::regclass);


--
-- TOC entry 3586 (class 0 OID 41071)
-- Dependencies: 248
-- Data for Name: application; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3592 (class 0 OID 41148)
-- Dependencies: 254
-- Data for Name: applicationassembly; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3593 (class 0 OID 41162)
-- Dependencies: 255
-- Data for Name: applicationproduct; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3591 (class 0 OID 41115)
-- Dependencies: 253
-- Data for Name: applicationservice; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3581 (class 0 OID 40998)
-- Dependencies: 243
-- Data for Name: assembly; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3582 (class 0 OID 41017)
-- Dependencies: 244
-- Data for Name: assemblyproduct; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3553 (class 0 OID 32779)
-- Dependencies: 215
-- Data for Name: bankaccount; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.bankaccount VALUES (1, 'Основной фонд', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (2, 'Накопительный фонд', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (3, 'Активы', '0,00 ?', false
INSERT INTO public.bankaccount VALUES (3, 'Пассивы', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (4, 'ФОТ (Фонд Оплаты Труда)', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (5, 'Амортизационный фонд', '0,00 ?', false);


--
-- TOC entry 3559 (class 0 OID 32819)
-- Dependencies: 221
-- Data for Name: category; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3560 (class 0 OID 32828)
-- Dependencies: 222
-- Data for Name: categorycharacteristic; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3557 (class 0 OID 32809)
-- Dependencies: 219
-- Data for Name: characteristic; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3584 (class 0 OID 41057)
-- Dependencies: 246
-- Data for Name: client; Type: TABLE DATA; Schema: public; Owner: postgres
--


--
-- TOC entry 3588 (class 0 OID 41091)
-- Dependencies: 250
-- Data for Name: clientsdevice; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3573 (class 0 OID 32927)
-- Dependencies: 235
-- Data for Name: howpay; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.howpay VALUES (1, 'в час', false);
INSERT INTO public.howpay VALUES (2, 'в день', false);
INSERT INTO public.howpay VALUES (3, 'в неделю', false);
INSERT INTO public.howpay VALUES (4, 'в месяц', false);
INSERT INTO public.howpay VALUES (5, 'в полмесяца', false);

--
-- TOC entry 3555 (class 0 OID 32789)
-- Dependencies: 217
-- Data for Name: operation; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3575 (class 0 OID 32937)
-- Dependencies: 237
-- Data for Name: plan; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.plan VALUES (1, 1, '250,00 ?', false);
INSERT INTO public.plan VALUES (2, 2, '1500,00 ?', false);
INSERT INTO public.plan VALUES (3, 3, '10000,00 ?', false);
INSERT INTO public.plan VALUES (4, 4, '35000,00 ?', false);
INSERT INTO public.plan VALUES (5, 5, '18500,00 ?', false);



--
-- TOC entry 3571 (class 0 OID 32916)
-- Dependencies: 233
-- Data for Name: post; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.post VALUES (1, 'ADMIN', false);


--
-- TOC entry 3566 (class 0 OID 32868)
-- Dependencies: 228
-- Data for Name: product; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3569 (class 0 OID 32899)
-- Dependencies: 231
-- Data for Name: productcharacteristic; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3568 (class 0 OID 32887)
-- Dependencies: 230
-- Data for Name: productpricechange; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3562 (class 0 OID 32843)
-- Dependencies: 224
-- Data for Name: provider; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3590 (class 0 OID 41106)
-- Dependencies: 252
-- Data for Name: service; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3564 (class 0 OID 32853)
-- Dependencies: 226
-- Data for Name: shipment; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3577 (class 0 OID 40963)
-- Dependencies: 239
-- Data for Name: worker; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.worker VALUES (1, 'ADMIN', 'ADMIN', 'ADMIN', '2005-09-05', 1, NULL, NULL, '1', '1', '1', NULL, false, 'admin', 'admin');


--
-- TOC entry 3579 (class 0 OID 40985)
-- Dependencies: 241
-- Data for Name: workingshift; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3620 (class 0 OID 0)
-- Dependencies: 247
-- Name: application_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.application_id_seq', 12, true);


--
-- TOC entry 3621 (class 0 OID 0)
-- Dependencies: 242
-- Name: assembly_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.assembly_id_seq', 2, true);


--
-- TOC entry 3622 (class 0 OID 0)
-- Dependencies: 214
-- Name: bankaccount_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.bankaccount_id_seq', 6, true);


--
-- TOC entry 3623 (class 0 OID 0)
-- Dependencies: 220
-- Name: category_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.category_id_seq', 3, true);


--
-- TOC entry 3624 (class 0 OID 0)
-- Dependencies: 218
-- Name: characteristic_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.characteristic_id_seq', 67, true);


--
-- TOC entry 3625 (class 0 OID 0)
-- Dependencies: 245
-- Name: client_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.client_id_seq', 7, true);


--
-- TOC entry 3626 (class 0 OID 0)
-- Dependencies: 249
-- Name: clientsdevice_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.clientsdevice_id_seq', 12, true);


--
-- TOC entry 3627 (class 0 OID 0)
-- Dependencies: 234
-- Name: howpay_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.howpay_id_seq', 3, true);


--
-- TOC entry 3628 (class 0 OID 0)
-- Dependencies: 216
-- Name: operation_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.operation_id_seq', 4, true);


--
-- TOC entry 3629 (class 0 OID 0)
-- Dependencies: 236
-- Name: plan_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.plan_id_seq', 2, true);


--
-- TOC entry 3630 (class 0 OID 0)
-- Dependencies: 232
-- Name: post_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.post_id_seq', 4, true);


--
-- TOC entry 3631 (class 0 OID 0)
-- Dependencies: 227
-- Name: product_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.product_id_seq', 6, true);


--
-- TOC entry 3632 (class 0 OID 0)
-- Dependencies: 229
-- Name: productpricechange_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.productpricechange_id_seq', 4, true);


--
-- TOC entry 3633 (class 0 OID 0)
-- Dependencies: 223
-- Name: provider_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.provider_id_seq', 6, true);


--
-- TOC entry 3634 (class 0 OID 0)
-- Dependencies: 251
-- Name: service_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.service_id_seq', 6, true);


--
-- TOC entry 3635 (class 0 OID 0)
-- Dependencies: 225
-- Name: shipment_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.shipment_id_seq', 12, true);


--
-- TOC entry 3636 (class 0 OID 0)
-- Dependencies: 238
-- Name: worker_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.worker_id_seq', 5, true);


--
-- TOC entry 3637 (class 0 OID 0)
-- Dependencies: 240
-- Name: workingshift_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.workingshift_id_seq', 4, true);


--
-- TOC entry 3370 (class 2606 OID 41079)
-- Name: application application_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_pk PRIMARY KEY (id);


--
-- TOC entry 3378 (class 2606 OID 57381)
-- Name: applicationassembly applicationassembly_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationassembly
    ADD CONSTRAINT applicationassembly_pk PRIMARY KEY (idapplication, idassemby);


--
-- TOC entry 3380 (class 2606 OID 57377)
-- Name: applicationproduct applicationproduct_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationproduct
    ADD CONSTRAINT applicationproduct_pk PRIMARY KEY (idapplication, idproduct);


--
-- TOC entry 3376 (class 2606 OID 57379)
-- Name: applicationservice applicationservice_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_pk PRIMARY KEY (idapplication, idservice, idworker);


--
-- TOC entry 3360 (class 2606 OID 41006)
-- Name: assembly assembly_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly
    ADD CONSTRAINT assembly_pk PRIMARY KEY (id);


--
-- TOC entry 3362 (class 2606 OID 57383)
-- Name: assemblyproduct assemblyproduct_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assemblyproduct
    ADD CONSTRAINT assemblyproduct_pk PRIMARY KEY (idassembly, idproduct);


--
-- TOC entry 3326 (class 2606 OID 32787)
-- Name: bankaccount bankaccount_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bankaccount
    ADD CONSTRAINT bankaccount_pk PRIMARY KEY (id);


--
-- TOC entry 3332 (class 2606 OID 32827)
-- Name: category category_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.category
    ADD CONSTRAINT category_pk PRIMARY KEY (id);


--
-- TOC entry 3334 (class 2606 OID 57385)
-- Name: categorycharacteristic categorycharacteristic_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categorycharacteristic
    ADD CONSTRAINT categorycharacteristic_pk PRIMARY KEY (idcategory, idcharacteristic);


--
-- TOC entry 3330 (class 2606 OID 32817)
-- Name: characteristic characteristic_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.characteristic
    ADD CONSTRAINT characteristic_pk PRIMARY KEY (id);


--
-- TOC entry 3364 (class 2606 OID 41065)
-- Name: client client_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_pk PRIMARY KEY (id);


--
-- TOC entry 3366 (class 2606 OID 41067)
-- Name: client client_un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_un UNIQUE (passport);


--
-- TOC entry 3368 (class 2606 OID 41069)
-- Name: client client_un2; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_un2 UNIQUE (email);


--
-- TOC entry 3372 (class 2606 OID 41099)
-- Name: clientsdevice clientsdevice_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientsdevice
    ADD CONSTRAINT clientsdevice_pk PRIMARY KEY (id);


--
-- TOC entry 3348 (class 2606 OID 32935)
-- Name: howpay howpay_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.howpay
    ADD CONSTRAINT howpay_pk PRIMARY KEY (id);


--
-- TOC entry 3328 (class 2606 OID 32797)
-- Name: operation operation_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation
    ADD CONSTRAINT operation_pk PRIMARY KEY (id);


--
-- TOC entry 3350 (class 2606 OID 40961)
-- Name: plan plan_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.plan
    ADD CONSTRAINT plan_pk PRIMARY KEY (id);


--
-- TOC entry 3346 (class 2606 OID 32923)
-- Name: post post_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.post
    ADD CONSTRAINT post_pk PRIMARY KEY (id);


--
-- TOC entry 3340 (class 2606 OID 32875)
-- Name: product product_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_pk PRIMARY KEY (id);


--
-- TOC entry 3344 (class 2606 OID 57387)
-- Name: productcharacteristic productcharacteristic_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productcharacteristic
    ADD CONSTRAINT productcharacteristic_pk PRIMARY KEY (idproduct, idcharacteristic);


--
-- TOC entry 3342 (class 2606 OID 32893)
-- Name: productpricechange productpricechange_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productpricechange
    ADD CONSTRAINT productpricechange_pk PRIMARY KEY (id);


--
-- TOC entry 3336 (class 2606 OID 32851)
-- Name: provider provider_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.provider
    ADD CONSTRAINT provider_pk PRIMARY KEY (id);


--
-- TOC entry 3374 (class 2606 OID 41114)
-- Name: service service_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service
    ADD CONSTRAINT service_pk PRIMARY KEY (id);


--
-- TOC entry 3338 (class 2606 OID 32861)
-- Name: shipment shipment_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shipment
    ADD CONSTRAINT shipment_pk PRIMARY KEY (id);


--
-- TOC entry 3352 (class 2606 OID 40971)
-- Name: worker worker_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_pk PRIMARY KEY (id);


--
-- TOC entry 3354 (class 2606 OID 40973)
-- Name: worker worker_un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_un UNIQUE (passport);


--
-- TOC entry 3356 (class 2606 OID 49153)
-- Name: worker worker_un1; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_un1 UNIQUE (login);


--
-- TOC entry 3358 (class 2606 OID 40991)
-- Name: workingshift workingshift_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workingshift
    ADD CONSTRAINT workingshift_pk PRIMARY KEY (id);


--
-- TOC entry 3399 (class 2606 OID 41080)
-- Name: application application_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_fk FOREIGN KEY (idclient) REFERENCES public.client(id);


--
-- TOC entry 3400 (class 2606 OID 57365)
-- Name: application application_fk2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_fk2 FOREIGN KEY (iddevice) REFERENCES public.clientsdevice(id);


--
-- TOC entry 3401 (class 2606 OID 41085)
-- Name: application application_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_fk_1 FOREIGN KEY (idmanager) REFERENCES public.worker(id);


--
-- TOC entry 3408 (class 2606 OID 41166)
-- Name: applicationproduct applicationproduct_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationproduct
    ADD CONSTRAINT applicationproduct_fk FOREIGN KEY (idapplication) REFERENCES public.application(id);


--
-- TOC entry 3409 (class 2606 OID 41171)
-- Name: applicationproduct applicationproduct_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationproduct
    ADD CONSTRAINT applicationproduct_fk_1 FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3403 (class 2606 OID 41118)
-- Name: applicationservice applicationservice_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_fk FOREIGN KEY (idapplication) REFERENCES public.application(id);


--
-- TOC entry 3404 (class 2606 OID 41123)
-- Name: applicationservice applicationservice_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_fk_1 FOREIGN KEY (idservice) REFERENCES public.service(id);


--
-- TOC entry 3405 (class 2606 OID 41128)
-- Name: applicationservice applicationservice_fk_2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_fk_2 FOREIGN KEY (idworker) REFERENCES public.worker(id);


--
-- TOC entry 3395 (class 2606 OID 41007)
-- Name: assembly assembly_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly
    ADD CONSTRAINT assembly_fk FOREIGN KEY (idmasterconfiguration) REFERENCES public.worker(id);


--
-- TOC entry 3396 (class 2606 OID 41012)
-- Name: assembly assembly_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly
    ADD CONSTRAINT assembly_fk_1 FOREIGN KEY (idmasterassembler) REFERENCES public.worker(id);


--
-- TOC entry 3397 (class 2606 OID 41022)
-- Name: assemblyproduct assemblyproduct_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assemblyproduct
    ADD CONSTRAINT assemblyproduct_fk FOREIGN KEY (idassembly) REFERENCES public.assembly(id);


--
-- TOC entry 3398 (class 2606 OID 41027)
-- Name: assemblyproduct assemblyproduct_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assemblyproduct
    ADD CONSTRAINT assemblyproduct_fk_1 FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3383 (class 2606 OID 32831)
-- Name: categorycharacteristic categorycharacteristic_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categorycharacteristic
    ADD CONSTRAINT categorycharacteristic_fk FOREIGN KEY (idcategory) REFERENCES public.category(id);


--
-- TOC entry 3384 (class 2606 OID 32836)
-- Name: categorycharacteristic categorycharacteristic_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categorycharacteristic
    ADD CONSTRAINT categorycharacteristic_fk_1 FOREIGN KEY (idcharacteristic) REFERENCES public.characteristic(id);


--
-- TOC entry 3402 (class 2606 OID 41100)
-- Name: clientsdevice clientsdevice_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientsdevice
    ADD CONSTRAINT clientsdevice_fk FOREIGN KEY (idclient) REFERENCES public.client(id);


--
-- TOC entry 3406 (class 2606 OID 41152)
-- Name: applicationassembly newtable_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationassembly
    ADD CONSTRAINT newtable_fk FOREIGN KEY (idapplication) REFERENCES public.application(id);


--
-- TOC entry 3407 (class 2606 OID 41157)
-- Name: applicationassembly newtable_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationassembly
    ADD CONSTRAINT newtable_fk_1 FOREIGN KEY (idassemby) REFERENCES public.assembly(id);


--
-- TOC entry 3381 (class 2606 OID 32798)
-- Name: operation operation_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation
    ADD CONSTRAINT operation_fk FOREIGN KEY (debet) REFERENCES public.bankaccount(id);


--
-- TOC entry 3382 (class 2606 OID 32803)
-- Name: operation operation_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation
    ADD CONSTRAINT operation_fk_1 FOREIGN KEY (credit) REFERENCES public.bankaccount(id);


--
-- TOC entry 3391 (class 2606 OID 32942)
-- Name: plan plan_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.plan
    ADD CONSTRAINT plan_fk FOREIGN KEY (idhowpay) REFERENCES public.howpay(id);


--
-- TOC entry 3389 (class 2606 OID 32904)
-- Name: productcharacteristic product_characteristic_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productcharacteristic
    ADD CONSTRAINT product_characteristic_fk FOREIGN KEY (idcharacteristic) REFERENCES public.characteristic(id);


--
-- TOC entry 3390 (class 2606 OID 32909)
-- Name: productcharacteristic product_characteristic_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productcharacteristic
    ADD CONSTRAINT product_characteristic_fk_1 FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3386 (class 2606 OID 32876)
-- Name: product product_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_fk FOREIGN KEY (idcategory) REFERENCES public.category(id);


--
-- TOC entry 3387 (class 2606 OID 32881)
-- Name: product product_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_fk_1 FOREIGN KEY (idshipment) REFERENCES public.shipment(id);


--
-- TOC entry 3388 (class 2606 OID 32894)
-- Name: productpricechange productpricechange_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productpricechange
    ADD CONSTRAINT productpricechange_fk FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3385 (class 2606 OID 32862)
-- Name: shipment shipment_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shipment
    ADD CONSTRAINT shipment_fk FOREIGN KEY (idprovider) REFERENCES public.provider(id);


--
-- TOC entry 3392 (class 2606 OID 40974)
-- Name: worker worker_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_fk FOREIGN KEY (idplan) REFERENCES public.plan(id);


--
-- TOC entry 3393 (class 2606 OID 40979)
-- Name: worker worker_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_fk_1 FOREIGN KEY (idpost) REFERENCES public.post(id);


--
-- TOC entry 3394 (class 2606 OID 40992)
-- Name: workingshift workingshift_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workingshift
    ADD CONSTRAINT workingshift_fk FOREIGN KEY (idworker) REFERENCES public.worker(id);


-- Completed on 2023-05-03 07:33:29

--
-- PostgreSQL database dump complete
--

>>>>>>> Изменил вроде всё:DB SQL Script
