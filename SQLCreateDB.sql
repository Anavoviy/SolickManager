--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

-- Started on 2023-05-15 07:48:41

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
-- TOC entry 3600 (class 1262 OID 32768)
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
-- TOC entry 3601 (class 0 OID 0)
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
    problem text,
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
-- TOC entry 3602 (class 0 OID 0)
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
-- TOC entry 3603 (class 0 OID 0)
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
-- TOC entry 3604 (class 0 OID 0)
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
-- TOC entry 3605 (class 0 OID 0)
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
-- TOC entry 3606 (class 0 OID 0)
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
-- TOC entry 3607 (class 0 OID 0)
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
-- TOC entry 3608 (class 0 OID 0)
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
-- TOC entry 3609 (class 0 OID 0)
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
-- TOC entry 3610 (class 0 OID 0)
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
-- TOC entry 3611 (class 0 OID 0)
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
-- TOC entry 3612 (class 0 OID 0)
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
-- TOC entry 3613 (class 0 OID 0)
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
    "Cost" money DEFAULT 1 NOT NULL,
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
-- TOC entry 3614 (class 0 OID 0)
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
-- TOC entry 3615 (class 0 OID 0)
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
-- TOC entry 3616 (class 0 OID 0)
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
-- TOC entry 3617 (class 0 OID 0)
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
-- TOC entry 3618 (class 0 OID 0)
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
-- TOC entry 3619 (class 0 OID 0)
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
-- TOC entry 3620 (class 0 OID 0)
-- Dependencies: 240
-- Name: workingshift_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.workingshift_id_seq OWNED BY public.workingshift.id;


--
-- TOC entry 3317 (class 2604 OID 41074)
-- Name: application id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application ALTER COLUMN id SET DEFAULT nextval('public.application_id_seq'::regclass);


--
-- TOC entry 3311 (class 2604 OID 41001)
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
-- TOC entry 3315 (class 2604 OID 41060)
-- Name: client id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client ALTER COLUMN id SET DEFAULT nextval('public.client_id_seq'::regclass);


--
-- TOC entry 3319 (class 2604 OID 41094)
-- Name: clientsdevice id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientsdevice ALTER COLUMN id SET DEFAULT nextval('public.clientsdevice_id_seq'::regclass);


--
-- TOC entry 3303 (class 2604 OID 32930)
-- Name: howpay id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.howpay ALTER COLUMN id SET DEFAULT nextval('public.howpay_id_seq'::regclass);


--
-- TOC entry 3284 (class 2604 OID 32792)
-- Name: operation id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation ALTER COLUMN id SET DEFAULT nextval('public.operation_id_seq'::regclass);


--
-- TOC entry 3305 (class 2604 OID 32940)
-- Name: plan id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.plan ALTER COLUMN id SET DEFAULT nextval('public.plan_id_seq'::regclass);


--
-- TOC entry 3301 (class 2604 OID 32919)
-- Name: post id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.post ALTER COLUMN id SET DEFAULT nextval('public.post_id_seq'::regclass);


--
-- TOC entry 3295 (class 2604 OID 32871)
-- Name: product id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product ALTER COLUMN id SET DEFAULT nextval('public.product_id_seq'::regclass);


--
-- TOC entry 3298 (class 2604 OID 32890)
-- Name: productpricechange id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productpricechange ALTER COLUMN id SET DEFAULT nextval('public.productpricechange_id_seq'::regclass);


--
-- TOC entry 3291 (class 2604 OID 32846)
-- Name: provider id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.provider ALTER COLUMN id SET DEFAULT nextval('public.provider_id_seq'::regclass);


--
-- TOC entry 3321 (class 2604 OID 41109)
-- Name: service id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service ALTER COLUMN id SET DEFAULT nextval('public.service_id_seq'::regclass);


--
-- TOC entry 3293 (class 2604 OID 32856)
-- Name: shipment id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shipment ALTER COLUMN id SET DEFAULT nextval('public.shipment_id_seq'::regclass);


--
-- TOC entry 3307 (class 2604 OID 40966)
-- Name: worker id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker ALTER COLUMN id SET DEFAULT nextval('public.worker_id_seq'::regclass);


--
-- TOC entry 3309 (class 2604 OID 40988)
-- Name: workingshift id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workingshift ALTER COLUMN id SET DEFAULT nextval('public.workingshift_id_seq'::regclass);


--
-- TOC entry 3587 (class 0 OID 41071)
-- Dependencies: 248
-- Data for Name: application; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.application VALUES (12, '2023-04-15', 5, 2, 'Просто принёс похвастаться, а мы его обосрали! :)', NULL, 'Принята', true, NULL, 8);
INSERT INTO public.application VALUES (10, '2023-04-11', 3, 2, 'Всё работает, необходима профилактика', NULL, 'Принята', true, NULL, 6);
INSERT INTO public.application VALUES (11, '2023-04-11', 4, 2, 'Не включается, жалоба на шатающийся разъём питания', 'Проверено питание - проблем нет, стал включаться, картинки нет. Не хватает квалификации для дальнейшей диагностики.', 'Завершена', false, NULL, 7);
INSERT INTO public.application VALUES (13, '2023-05-04', 3, 2, NULL, NULL, 'Создана', true, NULL, NULL);
INSERT INTO public.application VALUES (14, '2023-05-10', 2, 4, NULL, NULL, 'Создана', true, NULL, NULL);
INSERT INTO public.application VALUES (15, '2023-05-10', 2, 4, NULL, NULL, 'Создана', true, NULL, NULL);
INSERT INTO public.application VALUES (16, '2023-05-10', 2, 4, NULL, NULL, 'Создана', true, NULL, NULL);
INSERT INTO public.application VALUES (17, '2023-05-10', 2, 4, NULL, NULL, 'Создана', true, NULL, NULL);


--
-- TOC entry 3593 (class 0 OID 41148)
-- Dependencies: 254
-- Data for Name: applicationassembly; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3594 (class 0 OID 41162)
-- Dependencies: 255
-- Data for Name: applicationproduct; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3592 (class 0 OID 41115)
-- Dependencies: 253
-- Data for Name: applicationservice; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3582 (class 0 OID 40998)
-- Dependencies: 243
-- Data for Name: assembly; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.assembly VALUES (3, 'ПК14 (i5/8GB/GTX1050Ti/120SSD+500HDD)', 2, 2, '2023-05-07', '25 000,00 ?', NULL, NULL, false);


--
-- TOC entry 3583 (class 0 OID 41017)
-- Dependencies: 244
-- Data for Name: assemblyproduct; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.assemblyproduct VALUES (3, 16, 1, false);
INSERT INTO public.assemblyproduct VALUES (3, 18, 1, false);
INSERT INTO public.assemblyproduct VALUES (3, 27, 1, false);
INSERT INTO public.assemblyproduct VALUES (3, 28, 1, false);
INSERT INTO public.assemblyproduct VALUES (3, 31, 1, false);
INSERT INTO public.assemblyproduct VALUES (3, 34, 1, false);
INSERT INTO public.assemblyproduct VALUES (3, 39, 1, false);
INSERT INTO public.assemblyproduct VALUES (3, 40, 1, false);
INSERT INTO public.assemblyproduct VALUES (3, 50, 1, false);
INSERT INTO public.assemblyproduct VALUES (3, 53, 1, false);


--
-- TOC entry 3554 (class 0 OID 32779)
-- Dependencies: 215
-- Data for Name: bankaccount; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.bankaccount VALUES (4, 'Поставщик Продавец с Farpost', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (2, 'Поставщик Продавец с Авито', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (3, 'Поставщик Продавец с Юлы', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (6, 'ФОТ (Фонд Оплаты Труда)', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (7, 'Поставщик ООО "Ozon"', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (8, 'Касса', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (10, 'Поставщик ООО "DNS"', '0,00 ?', false);
INSERT INTO public.bankaccount VALUES (11, 'Готовая продукция', '17 166,50 ?', false);
INSERT INTO public.bankaccount VALUES (12, 'Инструменты', '12 532,00 ?', false);
INSERT INTO public.bankaccount VALUES (13, 'Склад (комплектующие)', '29 430,50 ?', false);
INSERT INTO public.bankaccount VALUES (5, 'Амортизационный фонд', '15 352,60 ?', false);
INSERT INTO public.bankaccount VALUES (9, 'Карта СБЕРБАНК (дебетовая)', '24 084,80 ?', false);


--
-- TOC entry 3560 (class 0 OID 32819)
-- Dependencies: 221
-- Data for Name: category; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.category VALUES (4, 'Процессор', false);
INSERT INTO public.category VALUES (5, 'Материнская плата', false);
INSERT INTO public.category VALUES (6, 'Оперативная память', false);
INSERT INTO public.category VALUES (7, 'Видеокарта', false);
INSERT INTO public.category VALUES (8, 'Накопитель', false);
INSERT INTO public.category VALUES (9, 'Блок питания', false);
INSERT INTO public.category VALUES (10, 'Охлаждение процессора', false);
INSERT INTO public.category VALUES (11, 'Корпус', false);


--
-- TOC entry 3561 (class 0 OID 32828)
-- Dependencies: 222
-- Data for Name: categorycharacteristic; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.categorycharacteristic VALUES (4, 68, false);
INSERT INTO public.categorycharacteristic VALUES (4, 69, false);
INSERT INTO public.categorycharacteristic VALUES (4, 70, false);
INSERT INTO public.categorycharacteristic VALUES (4, 71, false);
INSERT INTO public.categorycharacteristic VALUES (4, 72, false);
INSERT INTO public.categorycharacteristic VALUES (4, 73, false);
INSERT INTO public.categorycharacteristic VALUES (4, 74, false);
INSERT INTO public.categorycharacteristic VALUES (4, 75, false);
INSERT INTO public.categorycharacteristic VALUES (4, 76, false);
INSERT INTO public.categorycharacteristic VALUES (4, 77, false);
INSERT INTO public.categorycharacteristic VALUES (4, 78, false);
INSERT INTO public.categorycharacteristic VALUES (4, 79, false);
INSERT INTO public.categorycharacteristic VALUES (5, 68, false);
INSERT INTO public.categorycharacteristic VALUES (5, 69, false);
INSERT INTO public.categorycharacteristic VALUES (5, 77, false);
INSERT INTO public.categorycharacteristic VALUES (5, 80, false);
INSERT INTO public.categorycharacteristic VALUES (5, 81, false);
INSERT INTO public.categorycharacteristic VALUES (5, 82, false);
INSERT INTO public.categorycharacteristic VALUES (5, 83, false);
INSERT INTO public.categorycharacteristic VALUES (5, 84, false);
INSERT INTO public.categorycharacteristic VALUES (5, 85, false);
INSERT INTO public.categorycharacteristic VALUES (5, 86, false);
INSERT INTO public.categorycharacteristic VALUES (5, 87, false);
INSERT INTO public.categorycharacteristic VALUES (5, 88, false);
INSERT INTO public.categorycharacteristic VALUES (5, 89, false);
INSERT INTO public.categorycharacteristic VALUES (5, 90, false);
INSERT INTO public.categorycharacteristic VALUES (5, 91, false);
INSERT INTO public.categorycharacteristic VALUES (5, 92, false);
INSERT INTO public.categorycharacteristic VALUES (5, 93, false);
INSERT INTO public.categorycharacteristic VALUES (5, 96, false);
INSERT INTO public.categorycharacteristic VALUES (5, 97, false);
INSERT INTO public.categorycharacteristic VALUES (5, 98, false);
INSERT INTO public.categorycharacteristic VALUES (5, 105, false);
INSERT INTO public.categorycharacteristic VALUES (6, 77, false);
INSERT INTO public.categorycharacteristic VALUES (6, 94, false);
INSERT INTO public.categorycharacteristic VALUES (6, 95, false);
INSERT INTO public.categorycharacteristic VALUES (6, 96, false);
INSERT INTO public.categorycharacteristic VALUES (6, 97, false);
INSERT INTO public.categorycharacteristic VALUES (6, 98, false);
INSERT INTO public.categorycharacteristic VALUES (7, 69, false);
INSERT INTO public.categorycharacteristic VALUES (7, 76, false);
INSERT INTO public.categorycharacteristic VALUES (7, 77, false);
INSERT INTO public.categorycharacteristic VALUES (7, 79, false);
INSERT INTO public.categorycharacteristic VALUES (7, 87, false);
INSERT INTO public.categorycharacteristic VALUES (7, 94, false);
INSERT INTO public.categorycharacteristic VALUES (7, 95, false);
INSERT INTO public.categorycharacteristic VALUES (7, 99, false);
INSERT INTO public.categorycharacteristic VALUES (7, 100, false);
INSERT INTO public.categorycharacteristic VALUES (7, 101, false);
INSERT INTO public.categorycharacteristic VALUES (7, 110, false);
INSERT INTO public.categorycharacteristic VALUES (6, 69, false);
INSERT INTO public.categorycharacteristic VALUES (8, 69, false);
INSERT INTO public.categorycharacteristic VALUES (8, 94, false);
INSERT INTO public.categorycharacteristic VALUES (8, 102, false);
INSERT INTO public.categorycharacteristic VALUES (8, 103, false);
INSERT INTO public.categorycharacteristic VALUES (8, 104, false);
INSERT INTO public.categorycharacteristic VALUES (8, 105, false);
INSERT INTO public.categorycharacteristic VALUES (9, 69, false);
INSERT INTO public.categorycharacteristic VALUES (9, 90, false);
INSERT INTO public.categorycharacteristic VALUES (9, 91, false);
INSERT INTO public.categorycharacteristic VALUES (9, 100, false);
INSERT INTO public.categorycharacteristic VALUES (9, 105, false);
INSERT INTO public.categorycharacteristic VALUES (9, 106, false);
INSERT INTO public.categorycharacteristic VALUES (9, 107, false);
INSERT INTO public.categorycharacteristic VALUES (9, 108, false);
INSERT INTO public.categorycharacteristic VALUES (9, 109, false);
INSERT INTO public.categorycharacteristic VALUES (9, 110, false);
INSERT INTO public.categorycharacteristic VALUES (9, 111, false);
INSERT INTO public.categorycharacteristic VALUES (9, 112, false);
INSERT INTO public.categorycharacteristic VALUES (10, 68, false);
INSERT INTO public.categorycharacteristic VALUES (10, 69, false);
INSERT INTO public.categorycharacteristic VALUES (10, 79, false);
INSERT INTO public.categorycharacteristic VALUES (10, 90, false);
INSERT INTO public.categorycharacteristic VALUES (10, 97, false);
INSERT INTO public.categorycharacteristic VALUES (10, 100, false);
INSERT INTO public.categorycharacteristic VALUES (10, 113, false);
INSERT INTO public.categorycharacteristic VALUES (10, 114, false);
INSERT INTO public.categorycharacteristic VALUES (10, 115, false);
INSERT INTO public.categorycharacteristic VALUES (10, 116, false);
INSERT INTO public.categorycharacteristic VALUES (11, 69, false);
INSERT INTO public.categorycharacteristic VALUES (11, 97, false);
INSERT INTO public.categorycharacteristic VALUES (11, 105, false);
INSERT INTO public.categorycharacteristic VALUES (11, 117, false);
INSERT INTO public.categorycharacteristic VALUES (11, 118, false);
INSERT INTO public.categorycharacteristic VALUES (11, 119, false);
INSERT INTO public.categorycharacteristic VALUES (11, 120, false);
INSERT INTO public.categorycharacteristic VALUES (11, 121, false);
INSERT INTO public.categorycharacteristic VALUES (11, 122, false);


--
-- TOC entry 3558 (class 0 OID 32809)
-- Dependencies: 219
-- Data for Name: characteristic; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.characteristic VALUES (68, 'Сокет', false);
INSERT INTO public.characteristic VALUES (69, 'Производитель', false);
INSERT INTO public.characteristic VALUES (70, 'Поколение', false);
INSERT INTO public.characteristic VALUES (71, 'Семейство', false);
INSERT INTO public.characteristic VALUES (72, 'Кол-во ядер', false);
INSERT INTO public.characteristic VALUES (73, 'Кол-во потоков', false);
INSERT INTO public.characteristic VALUES (74, 'Кеш L2', false);
INSERT INTO public.characteristic VALUES (75, 'Кэш L3', false);
INSERT INTO public.characteristic VALUES (76, 'Максимальная частота', false);
INSERT INTO public.characteristic VALUES (77, 'Тип памяти', false);
INSERT INTO public.characteristic VALUES (78, 'Встроенное видеоядро', false);
INSERT INTO public.characteristic VALUES (79, 'TDP', false);
INSERT INTO public.characteristic VALUES (80, 'Чипсет', false);
INSERT INTO public.characteristic VALUES (81, 'Кол-во DIMM-слотов', false);
INSERT INTO public.characteristic VALUES (82, 'Кол-во SATA3', false);
INSERT INTO public.characteristic VALUES (83, 'Кол-во SATA2', false);
INSERT INTO public.characteristic VALUES (84, 'Кол-во M.2', false);
INSERT INTO public.characteristic VALUES (85, 'Кол-во USB3.0 на IO-панели', false);
INSERT INTO public.characteristic VALUES (86, 'Кол-во USB2.0 на IO-панели', false);
INSERT INTO public.characteristic VALUES (87, 'Версия PCI-E', false);
INSERT INTO public.characteristic VALUES (88, 'Конфигурация PCI-E', false);
INSERT INTO public.characteristic VALUES (89, 'Фазы питания', false);
INSERT INTO public.characteristic VALUES (90, 'Разъём питания CPU', false);
INSERT INTO public.characteristic VALUES (91, 'Разъём питания MotherBoard', false);
INSERT INTO public.characteristic VALUES (93, 'Разъёмы SYSTEM_FAN', false);
INSERT INTO public.characteristic VALUES (92, 'Разъёмы CPU_FAN', false);
INSERT INTO public.characteristic VALUES (94, 'Объём памяти', false);
INSERT INTO public.characteristic VALUES (95, 'Частота памяти', false);
INSERT INTO public.characteristic VALUES (96, 'Радиаторы', false);
INSERT INTO public.characteristic VALUES (97, 'Подсветка', false);
INSERT INTO public.characteristic VALUES (98, 'Устройство', false);
INSERT INTO public.characteristic VALUES (99, 'Шина', false);
INSERT INTO public.characteristic VALUES (101, 'Видеовыходы', false);
INSERT INTO public.characteristic VALUES (102, 'Тип накопителя', false);
INSERT INTO public.characteristic VALUES (103, 'Скорость чтения', false);
INSERT INTO public.characteristic VALUES (104, 'Скорость записи', false);
INSERT INTO public.characteristic VALUES (105, 'Форм-фактор', false);
INSERT INTO public.characteristic VALUES (107, 'Мощность по 12V', false);
INSERT INTO public.characteristic VALUES (106, 'Номинальная мощность', false);
INSERT INTO public.characteristic VALUES (108, 'Мощность по 5V', false);
INSERT INTO public.characteristic VALUES (109, 'Мощность по 3.3V', false);
INSERT INTO public.characteristic VALUES (110, 'Разъём питания видеокарты', false);
INSERT INTO public.characteristic VALUES (111, 'SATA-коннекторы', false);
INSERT INTO public.characteristic VALUES (112, 'Molex-коннекторы', false);
INSERT INTO public.characteristic VALUES (100, 'Тип охлаждения', false);
INSERT INTO public.characteristic VALUES (113, 'Формат вентилятора', false);
INSERT INTO public.characteristic VALUES (114, 'Кол-во вентиляторов', false);
INSERT INTO public.characteristic VALUES (115, 'Кол-во медных трубок', false);
INSERT INTO public.characteristic VALUES (116, 'Крепление', false);
INSERT INTO public.characteristic VALUES (117, 'Форм-факторы совместимых материнских плат', false);
INSERT INTO public.characteristic VALUES (118, 'Формат мест под вентиляторы', false);
INSERT INTO public.characteristic VALUES (119, 'Окно на боковой крышке', false);
INSERT INTO public.characteristic VALUES (120, 'Передняя панель', false);
INSERT INTO public.characteristic VALUES (121, 'Максимальная высота CPU_FAN', false);
INSERT INTO public.characteristic VALUES (122, 'Максимальная длина GPU', false);


--
-- TOC entry 3585 (class 0 OID 41057)
-- Dependencies: 246
-- Data for Name: client; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.client VALUES (4, 'Матвей', 'Дубовой', '', '2004-10-12', NULL, '+79089602410', NULL, NULL, false);
INSERT INTO public.client VALUES (6, 'Максимков', 'Максим', 'Максимович', '2012-12-12', '5468 254856', '89025215454', 'ngjhgjje@gmail.com', '-', true);
INSERT INTO public.client VALUES (5, 'Курагалкин', 'Максим', 'Максимович', '1995-03-02', '9509 698745', '+79254586565', 'kuragalkin@gmail.com', 'Кураглкин есть курагалкин', true);
INSERT INTO public.client VALUES (3, 'Владимир', 'Булыгин', 'Евгеньевич', '2005-09-02', NULL, '+79025213321', 'vovan200512345@mail.ru', 'xdvdxvdxvx', true);
INSERT INTO public.client VALUES (1, 'Анастасия', 'Воронова', 'Александровна', '2005-12-16', '0519 654738', '+79841956797', 'anaanana@gmail.ru', 'Отличный покупатель', true);
INSERT INTO public.client VALUES (7, 'КР', 'КРыгин', 'Крыгович', '2004-02-02', '0418 521325', '+79246548798', 'KRRRRRigin@gmail.com', 'Он живой', true);
INSERT INTO public.client VALUES (2, 'Кристина', 'Максименко', 'Евгеньевна', '1985-09-05', '', '+79243227242', '', '', false);


--
-- TOC entry 3589 (class 0 OID 41091)
-- Dependencies: 250
-- Data for Name: clientsdevice; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.clientsdevice VALUES (7, 4, 'Acer Aspire 3 571G-736b8G75Makk', 'Серый ноутбук', '10 000,00 ?', NULL, false);
INSERT INTO public.clientsdevice VALUES (4, 2, 'ПК СУПЕР-ПУПЕР-МОЩЬЬЬ', 'Супер-пупер мащинка', '95,00 ?', NULL, true);
INSERT INTO public.clientsdevice VALUES (5, 2, 'djnvdjnvbfb', 'fcbfcbfc', '1 000 000,00 ?', NULL, true);
INSERT INTO public.clientsdevice VALUES (8, 5, 'Baikalka S150ULTRA', 'ой, байкал ))))', '0,00 ?', NULL, true);
INSERT INTO public.clientsdevice VALUES (6, 3, 'Palit GTX1660 SUPER Gaming Pro OC', 'В пользовании 3 года, покупалась в ДНС, гарантия кончилась', '20 000,00 ?', NULL, true);
INSERT INTO public.clientsdevice VALUES (9, 3, 'Пк ППУШКА', 'квивиавив', '1 000 000,00 ?', NULL, true);
INSERT INTO public.clientsdevice VALUES (10, 3, 'vdvv', 'bfcbcb', '100 000,00 ?', NULL, true);
INSERT INTO public.clientsdevice VALUES (1, 1, 'lenovo Ideapad 15-145AST', 'Серый ультрабук', '40 000,00 ?', 'Цена на б/у рынке примерно 10000-20000руб.', true);
INSERT INTO public.clientsdevice VALUES (2, 1, 'GIGABYTE B450M DS3H', 'Материнская плата под AM4 сокет', '6 700,00 ?', NULL, true);
INSERT INTO public.clientsdevice VALUES (3, 1, 'A-DATA DDR4 16GB (2x8GB, 3200MHz)', 'Живая', '3 500,00 ?', NULL, true);
INSERT INTO public.clientsdevice VALUES (12, 2, 'GIGABYTE D450M DS3H', 'Материнская плата с таким себе охлаждением', '6 700,00 ?', NULL, false);
INSERT INTO public.clientsdevice VALUES (11, 2, 'MSI MAG B770M GTXHFD SUPER', '', '1 000,00 ?', NULL, true);


--
-- TOC entry 3574 (class 0 OID 32927)
-- Dependencies: 235
-- Data for Name: howpay; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.howpay VALUES (1, 'в час', false);
INSERT INTO public.howpay VALUES (3, 'в день', false);


--
-- TOC entry 3556 (class 0 OID 32789)
-- Dependencies: 217
-- Data for Name: operation; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.operation VALUES (7, '2023-05-15', '2023-05-15', 9, 5, '', '6 999,00 ?', 'Завершена', NULL, true);
INSERT INTO public.operation VALUES (6, '2023-05-14', '2023-05-15', 5, 9, 'Перевод за стабилизатор', '6 999,00 ?', 'Завершена', NULL, true);


--
-- TOC entry 3576 (class 0 OID 32937)
-- Dependencies: 237
-- Data for Name: plan; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.plan VALUES (1, 1, '350,00 ?', false);
INSERT INTO public.plan VALUES (2, 3, '1 600,00 ?', false);


--
-- TOC entry 3572 (class 0 OID 32916)
-- Dependencies: 233
-- Data for Name: post; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.post VALUES (1, 'Директор', false);
INSERT INTO public.post VALUES (2, 'Мастер', false);


--
-- TOC entry 3567 (class 0 OID 32868)
-- Dependencies: 228
-- Data for Name: product; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.product VALUES (25, 6, 'HYPERX FURY HX316C10FB/4', 'ОЗУ DDR3 4GB', 33, '900,00 ?', 1);
INSERT INTO public.product VALUES (26, 6, 'HyperX Genesis KHX1866C9D3K2/8G', 'ОЗУ DDR3 4GB', 33, '900,00 ?', 2);
INSERT INTO public.product VALUES (53, 11, 'AeroCool Evo Mini Black RGB', 'Корпус Mini-Tower с боковой крышкой из орг. стекла и  подсветкой спереди. Поддерживает материнские платы формата Micro-ATX и меньше', 41, '3 487,00 ?', 0);
INSERT INTO public.product VALUES (29, 7, 'Palit GF9400GT', 'Видеокарта Nvidia GT9400', 35, '1,00 ?', 1);
INSERT INTO public.product VALUES (30, 7, 'MSI GT710 1GB GDDR3', 'Видеокарта MSI Nvidia GT710 1GB', 35, '1,00 ?', 1);
INSERT INTO public.product VALUES (32, 7, 'HIS HD6770 Fan', 'Видеокарта HIS AMD Radeon HD6770 Fan 1GB', 35, '500,00 ?', 1);
INSERT INTO public.product VALUES (11, 4, 'Intel Core i3-2120', 'Процессор на LGA1155', 17, '300,00 ?', 2);
INSERT INTO public.product VALUES (16, 4, 'Intel Core i5-4430', 'Процессор на LGA1155', 22, '1 500,00 ?', 0);
INSERT INTO public.product VALUES (18, 5, 'ASUS H81M-P', 'Материнская плата на LGA1150', 24, '2 500,00 ?', 0);
INSERT INTO public.product VALUES (27, 6, 'Patriot PSD34G13332', 'ОЗУ DDR3 4GB', 34, '800,00 ?', 0);
INSERT INTO public.product VALUES (28, 6, 'Hynix HMT351U6CFR8C-H9', 'ОЗУ DDR3 4GB', 34, '250,00 ?', 0);
INSERT INTO public.product VALUES (15, 4, 'Intel Core i5-2310', 'Процессор на LGA1155', 21, '1 500,00 ?', 1);
INSERT INTO public.product VALUES (17, 4, 'Intel Core i3-8100', 'Процессор на LGA1151-v2', 23, '1 500,00 ?', 1);
INSERT INTO public.product VALUES (19, 6, 'Hynix HYMP112U64CP8-S6', 'ОЗУ  DDR2', 25, '150,00 ?', 1);
INSERT INTO public.product VALUES (33, 9, '3Cott-450ATX', 'БП 400W (пищит, свистят дросселя)', 36, '650,00 ?', 1);
INSERT INTO public.product VALUES (35, 9, 'Zalman ZM600-HP', ' БП 600W (серверный, надёжный, отличное состояние)', 36, '1 500,00 ?', 1);
INSERT INTO public.product VALUES (9, 4, 'Intel Pentium DUAL-CORE E2180', 'Процессор на LGA775', 15, '1,00 ?', 1);
INSERT INTO public.product VALUES (10, 4, 'Intel Pentium DUAL-CORE E2200', 'Процессор на LGA775', 16, '1,00 ?', 1);
INSERT INTO public.product VALUES (12, 4, 'Intel Pentium G3240', 'Процессор на  LGA1150', 18, '1,00 ?', 2);
INSERT INTO public.product VALUES (31, 7, 'ASUS GTX1050Ti Cerberus', 'Видеокарта ASUS Nvidia GTX1050Ti Cerberus 4GB', 35, '5 500,00 ?', 0);
INSERT INTO public.product VALUES (34, 9, 'Velton ATX-400', 'БП 400W (отличное состояние)', 36, '600,00 ?', 1);
INSERT INTO public.product VALUES (36, 9, 'DEXP DTS-250', 'БП 350W (офисный, сомнительное качество, лёгкий!)', 36, '350,00 ?', 1);
INSERT INTO public.product VALUES (20, 6, 'Kingston KVR800D2N6/1G', ' ОЗУ DDR2 1GB', 26, '1,00 ?', 2);
INSERT INTO public.product VALUES (21, 6, 'SAMSUNG SQV4A1000536487F8D', 'ОЗУ DDR4 8GB', 27, '1 000,00 ?', 1);
INSERT INTO public.product VALUES (22, 6, 'Crucial CT102464BA16OB.C16FER', 'ОЗУ DDR3 8GB', 28, '1 000,00 ?', 2);
INSERT INTO public.product VALUES (23, 6, 'ELPIDA 2GB 1Rx8 PC3-10600U-9-10-A0', 'ОЗУ DDR3 2GB', 29, '200,00 ?', 1);
INSERT INTO public.product VALUES (24, 6, 'Kingston KVR1333D3N9K2/8G', 'ОЗУ DDR3 4GB', 32, '250,00 ?', 1);
INSERT INTO public.product VALUES (37, 9, 'FSP ATX-550PNR ', 'БП 550W (отличный, качественный, тяжёлый)', 36, '1 500,00 ?', 1);
INSERT INTO public.product VALUES (38, 9, 'FSP ATX-450PHF', 'БП 450W (отличный, надёжный, тяжёлый)', 36, '1 000,00 ?', 1);
INSERT INTO public.product VALUES (41, 8, 'Team Group CX1 ', 'SSD 240GB 2`5', 38, '1 000,00 ?', 1);
INSERT INTO public.product VALUES (42, 8, 'Seagate ST500LT012', 'HDD 500GB 3`5 (крутится, не отображается - не рабочий)', 38, '1,00 ?', 1);
INSERT INTO public.product VALUES (45, 8, 'WD BLUE WD5000AZRZ', 'HDD 500GB 3`5 (рабочий)', 38, '1 200,00 ?', 1);
INSERT INTO public.product VALUES (46, 8, 'Seagate ST31000DM003', 'HDD 1000GB 3`5 (рабочий)', 38, '900,00 ?', 1);
INSERT INTO public.product VALUES (47, 8, 'Kingston SA400S37/960G', 'SSD 960GB 2`5 (проверочный, подарил Владислав Кенкишвилли)', 38, '6 399,00 ?', 1);
INSERT INTO public.product VALUES (43, 8, 'Samsung ST1000LM024', 'HDD 1000GB 3`5 (есть BAD-сектора)', 38, '1,00 ?', 1);
INSERT INTO public.product VALUES (44, 8, 'Seagate ST3000DM001', 'HDD 3000GB 3`5 (есть BAD-сектора)', 38, '1,00 ?', 1);
INSERT INTO public.product VALUES (48, 10, 'DEEPCOOL...', 'Кулер для процессора. Крепление через переходное кольцо - кольца нет!', 39, '500,00 ?', 1);
INSERT INTO public.product VALUES (49, 10, 'Intel BOX LGA115X', 'Охлаждение процессора. Крепление на сокеты LGA115X', 39, '1,00 ?', 1);
INSERT INTO public.product VALUES (51, 10, 'DeepCool Gamma Archer LGA115X', 'Охлаждение процессора. Крепление клипсами на LGA115X', 39, '1,00 ?', 1);
INSERT INTO public.product VALUES (52, 11, 'Code GC-SX3', 'Чёрный корпус (акрил, подсветка спереди). Поддерживает материские платы Micro-ATX и меньше', 40, '2 399,00 ?', 1);
INSERT INTO public.product VALUES (39, 8, 'Toshiba DT01AC050', 'HDD 500GB 3`5', 37, '500,00 ?', 0);
INSERT INTO public.product VALUES (40, 8, 'Samsung V-NAND SSD 850', 'SSD 120GB 2`5', 38, '600,00 ?', 0);
INSERT INTO public.product VALUES (13, 4, 'Intel Pentium G3250', 'Процессор на LGA1150', 19, '1,00 ?', 1);
INSERT INTO public.product VALUES (14, 4, 'Intel Pentium G3260', 'Процессор на LGA1150', 20, '1,00 ?', 1);
INSERT INTO public.product VALUES (54, 11, 'Ginzzu Sl220 White', 'Белый корпус с боковой крышкой из акрила на петлях. Поддерживает материнские платы формата Micro-ATX и меньше', 41, '3 592,00 ?', 1);
INSERT INTO public.product VALUES (50, 10, 'NoName G20', 'Охлаждение процессора. Крепление черех переходное кольцо. Кольцо в комплекте LGA115X/LGA1200', 39, '1 290,00 ?', 1);


--
-- TOC entry 3570 (class 0 OID 32899)
-- Dependencies: 231
-- Data for Name: productcharacteristic; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.productcharacteristic VALUES (9, 68, 'LGA775', false);
INSERT INTO public.productcharacteristic VALUES (9, 69, 'Intel', false);
INSERT INTO public.productcharacteristic VALUES (9, 70, '-', false);
INSERT INTO public.productcharacteristic VALUES (9, 71, 'Pentium', false);
INSERT INTO public.productcharacteristic VALUES (9, 72, '2', false);
INSERT INTO public.productcharacteristic VALUES (9, 73, '2', false);
INSERT INTO public.productcharacteristic VALUES (9, 74, '1MB', false);
INSERT INTO public.productcharacteristic VALUES (9, 75, '0', false);
INSERT INTO public.productcharacteristic VALUES (9, 76, '2.0GHz', false);
INSERT INTO public.productcharacteristic VALUES (9, 77, 'DDR2', false);
INSERT INTO public.productcharacteristic VALUES (9, 78, 'нет', false);
INSERT INTO public.productcharacteristic VALUES (9, 79, '65W', false);
INSERT INTO public.productcharacteristic VALUES (10, 68, 'LGA775', false);
INSERT INTO public.productcharacteristic VALUES (10, 69, 'Intel', false);
INSERT INTO public.productcharacteristic VALUES (10, 70, '-', false);
INSERT INTO public.productcharacteristic VALUES (10, 71, 'Pentium', false);
INSERT INTO public.productcharacteristic VALUES (10, 72, '2', false);
INSERT INTO public.productcharacteristic VALUES (10, 73, '2', false);
INSERT INTO public.productcharacteristic VALUES (10, 74, '1MB', false);
INSERT INTO public.productcharacteristic VALUES (10, 75, '0', false);
INSERT INTO public.productcharacteristic VALUES (10, 76, '2.2GHz', false);
INSERT INTO public.productcharacteristic VALUES (10, 77, 'DDR2', false);
INSERT INTO public.productcharacteristic VALUES (10, 78, 'нет', false);
INSERT INTO public.productcharacteristic VALUES (10, 79, '65W', false);
INSERT INTO public.productcharacteristic VALUES (11, 68, 'LGA1155', false);
INSERT INTO public.productcharacteristic VALUES (11, 69, 'Intel', false);
INSERT INTO public.productcharacteristic VALUES (11, 70, '2 (Sandy Bridge)', false);
INSERT INTO public.productcharacteristic VALUES (11, 71, 'Core i3', false);
INSERT INTO public.productcharacteristic VALUES (11, 72, '2', false);
INSERT INTO public.productcharacteristic VALUES (11, 73, '4', false);
INSERT INTO public.productcharacteristic VALUES (11, 74, '512KB', false);
INSERT INTO public.productcharacteristic VALUES (11, 75, '3MB', false);
INSERT INTO public.productcharacteristic VALUES (11, 76, '3.1GHz', false);
INSERT INTO public.productcharacteristic VALUES (11, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (11, 78, 'HD Graphics 2000', false);
INSERT INTO public.productcharacteristic VALUES (11, 79, '65W', false);
INSERT INTO public.productcharacteristic VALUES (12, 68, 'LGA1150', false);
INSERT INTO public.productcharacteristic VALUES (12, 69, 'Intel', false);
INSERT INTO public.productcharacteristic VALUES (12, 70, '4 (Haswell)', false);
INSERT INTO public.productcharacteristic VALUES (12, 71, 'Pentium', false);
INSERT INTO public.productcharacteristic VALUES (12, 72, '2', false);
INSERT INTO public.productcharacteristic VALUES (12, 73, '2', false);
INSERT INTO public.productcharacteristic VALUES (12, 74, '512KB', false);
INSERT INTO public.productcharacteristic VALUES (12, 75, '3MB', false);
INSERT INTO public.productcharacteristic VALUES (12, 76, '3.1GHz', false);
INSERT INTO public.productcharacteristic VALUES (12, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (12, 78, 'HD Graphics', false);
INSERT INTO public.productcharacteristic VALUES (12, 79, '53W', false);
INSERT INTO public.productcharacteristic VALUES (13, 68, 'LGA1150', false);
INSERT INTO public.productcharacteristic VALUES (13, 69, 'Intel', false);
INSERT INTO public.productcharacteristic VALUES (13, 70, '4 (Haswell)', false);
INSERT INTO public.productcharacteristic VALUES (13, 71, 'Pentium', false);
INSERT INTO public.productcharacteristic VALUES (13, 72, '2', false);
INSERT INTO public.productcharacteristic VALUES (13, 73, '2', false);
INSERT INTO public.productcharacteristic VALUES (13, 74, '512KB', false);
INSERT INTO public.productcharacteristic VALUES (13, 75, '3MB', false);
INSERT INTO public.productcharacteristic VALUES (13, 76, '3.2GHz', false);
INSERT INTO public.productcharacteristic VALUES (13, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (13, 78, 'HD Graphics', false);
INSERT INTO public.productcharacteristic VALUES (13, 79, '53W', false);
INSERT INTO public.productcharacteristic VALUES (14, 68, 'LGA1150', false);
INSERT INTO public.productcharacteristic VALUES (14, 69, 'Intel', false);
INSERT INTO public.productcharacteristic VALUES (14, 70, '4 (Haswell)', false);
INSERT INTO public.productcharacteristic VALUES (14, 71, 'Pentium', false);
INSERT INTO public.productcharacteristic VALUES (14, 72, '2', false);
INSERT INTO public.productcharacteristic VALUES (14, 73, '2', false);
INSERT INTO public.productcharacteristic VALUES (14, 74, '512KB', false);
INSERT INTO public.productcharacteristic VALUES (14, 75, '3MB', false);
INSERT INTO public.productcharacteristic VALUES (14, 76, '3.3GHz', false);
INSERT INTO public.productcharacteristic VALUES (14, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (14, 78, 'HD Graphics', false);
INSERT INTO public.productcharacteristic VALUES (14, 79, '53W', false);
INSERT INTO public.productcharacteristic VALUES (15, 68, 'LGA1155', false);
INSERT INTO public.productcharacteristic VALUES (15, 69, 'Intel', false);
INSERT INTO public.productcharacteristic VALUES (15, 70, '2 (Sandy Bridge)', false);
INSERT INTO public.productcharacteristic VALUES (15, 71, 'Core i5', false);
INSERT INTO public.productcharacteristic VALUES (15, 72, '4', false);
INSERT INTO public.productcharacteristic VALUES (15, 73, '4', false);
INSERT INTO public.productcharacteristic VALUES (15, 74, '1MB', false);
INSERT INTO public.productcharacteristic VALUES (15, 75, '6MB', false);
INSERT INTO public.productcharacteristic VALUES (15, 76, '3.2GHz', false);
INSERT INTO public.productcharacteristic VALUES (15, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (15, 78, 'HD Graphics 2000', false);
INSERT INTO public.productcharacteristic VALUES (15, 79, '95W', false);
INSERT INTO public.productcharacteristic VALUES (16, 68, 'LGA1150', false);
INSERT INTO public.productcharacteristic VALUES (16, 69, 'Intel', false);
INSERT INTO public.productcharacteristic VALUES (16, 70, '4 (Haswell)', false);
INSERT INTO public.productcharacteristic VALUES (16, 71, 'Core i5', false);
INSERT INTO public.productcharacteristic VALUES (16, 72, '4', false);
INSERT INTO public.productcharacteristic VALUES (16, 73, '4', false);
INSERT INTO public.productcharacteristic VALUES (16, 74, '1MB', false);
INSERT INTO public.productcharacteristic VALUES (16, 75, '6MB', false);
INSERT INTO public.productcharacteristic VALUES (16, 76, '3.2GHz', false);
INSERT INTO public.productcharacteristic VALUES (16, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (16, 78, 'HD Graphics 4600', false);
INSERT INTO public.productcharacteristic VALUES (16, 79, '84W', false);
INSERT INTO public.productcharacteristic VALUES (17, 68, 'LGA1151-v2', false);
INSERT INTO public.productcharacteristic VALUES (17, 69, 'Intel', false);
INSERT INTO public.productcharacteristic VALUES (17, 70, '8 (Coffee Lake)', false);
INSERT INTO public.productcharacteristic VALUES (17, 71, 'Core i3', false);
INSERT INTO public.productcharacteristic VALUES (17, 72, '4', false);
INSERT INTO public.productcharacteristic VALUES (17, 73, '4', false);
INSERT INTO public.productcharacteristic VALUES (17, 74, '1MB', false);
INSERT INTO public.productcharacteristic VALUES (17, 75, '6MB', false);
INSERT INTO public.productcharacteristic VALUES (17, 76, '3.6GHz', false);
INSERT INTO public.productcharacteristic VALUES (17, 77, 'DDR4', false);
INSERT INTO public.productcharacteristic VALUES (17, 78, 'UHD Graphics 630', false);
INSERT INTO public.productcharacteristic VALUES (17, 79, '65W', false);
INSERT INTO public.productcharacteristic VALUES (18, 68, 'LGA1150', false);
INSERT INTO public.productcharacteristic VALUES (18, 69, 'ASUS', false);
INSERT INTO public.productcharacteristic VALUES (18, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (18, 80, 'H81', false);
INSERT INTO public.productcharacteristic VALUES (18, 81, '2', false);
INSERT INTO public.productcharacteristic VALUES (18, 82, '2', false);
INSERT INTO public.productcharacteristic VALUES (18, 83, '2', false);
INSERT INTO public.productcharacteristic VALUES (18, 84, '0', false);
INSERT INTO public.productcharacteristic VALUES (18, 85, '2', false);
INSERT INTO public.productcharacteristic VALUES (18, 86, '2', false);
INSERT INTO public.productcharacteristic VALUES (18, 87, '2.0', false);
INSERT INTO public.productcharacteristic VALUES (18, 88, '1x16, 1x1', false);
INSERT INTO public.productcharacteristic VALUES (18, 89, '4', false);
INSERT INTO public.productcharacteristic VALUES (18, 90, '4pin', false);
INSERT INTO public.productcharacteristic VALUES (18, 91, '24pin', false);
INSERT INTO public.productcharacteristic VALUES (18, 92, '4pin', false);
INSERT INTO public.productcharacteristic VALUES (18, 93, '1x4pin', false);
INSERT INTO public.productcharacteristic VALUES (18, 96, '-', false);
INSERT INTO public.productcharacteristic VALUES (18, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (18, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (18, 105, 'Mini-ATX', false);
INSERT INTO public.productcharacteristic VALUES (19, 69, 'Hynix', false);
INSERT INTO public.productcharacteristic VALUES (19, 77, 'DDR2', false);
INSERT INTO public.productcharacteristic VALUES (19, 94, '1GB', false);
INSERT INTO public.productcharacteristic VALUES (19, 95, '667MHz', false);
INSERT INTO public.productcharacteristic VALUES (19, 96, '-', false);
INSERT INTO public.productcharacteristic VALUES (19, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (19, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (20, 69, 'Kingstom', false);
INSERT INTO public.productcharacteristic VALUES (20, 77, 'DDR2', false);
INSERT INTO public.productcharacteristic VALUES (20, 94, '1GB', false);
INSERT INTO public.productcharacteristic VALUES (20, 95, '800MHz', false);
INSERT INTO public.productcharacteristic VALUES (20, 96, '-', false);
INSERT INTO public.productcharacteristic VALUES (20, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (20, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (21, 69, 'Samsung', false);
INSERT INTO public.productcharacteristic VALUES (21, 77, 'DDR4', false);
INSERT INTO public.productcharacteristic VALUES (21, 94, '8GB', false);
INSERT INTO public.productcharacteristic VALUES (21, 95, '2666MHz', false);
INSERT INTO public.productcharacteristic VALUES (21, 96, '-', false);
INSERT INTO public.productcharacteristic VALUES (21, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (21, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (22, 69, 'Crucial', false);
INSERT INTO public.productcharacteristic VALUES (22, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (22, 94, '8GB', false);
INSERT INTO public.productcharacteristic VALUES (22, 95, '1600MHz', false);
INSERT INTO public.productcharacteristic VALUES (22, 96, '-', false);
INSERT INTO public.productcharacteristic VALUES (22, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (22, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (23, 69, 'Elpida', false);
INSERT INTO public.productcharacteristic VALUES (23, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (23, 94, '2GB', false);
INSERT INTO public.productcharacteristic VALUES (23, 95, '1333MHz', false);
INSERT INTO public.productcharacteristic VALUES (23, 96, '-', false);
INSERT INTO public.productcharacteristic VALUES (23, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (23, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (24, 69, 'Kingston', false);
INSERT INTO public.productcharacteristic VALUES (24, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (24, 94, '4GB', false);
INSERT INTO public.productcharacteristic VALUES (24, 95, '1333MHz', false);
INSERT INTO public.productcharacteristic VALUES (24, 96, '-', false);
INSERT INTO public.productcharacteristic VALUES (24, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (24, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (25, 69, 'HyperX', false);
INSERT INTO public.productcharacteristic VALUES (25, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (25, 94, '4GB', false);
INSERT INTO public.productcharacteristic VALUES (25, 95, '1600MHz', false);
INSERT INTO public.productcharacteristic VALUES (25, 96, 'синий', false);
INSERT INTO public.productcharacteristic VALUES (25, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (25, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (26, 69, 'HyperX', false);
INSERT INTO public.productcharacteristic VALUES (26, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (26, 94, '4GB', false);
INSERT INTO public.productcharacteristic VALUES (26, 95, '1866MHz', false);
INSERT INTO public.productcharacteristic VALUES (26, 96, 'Светло-синие с серым иксом', false);
INSERT INTO public.productcharacteristic VALUES (26, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (26, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (27, 69, 'Patriot', false);
INSERT INTO public.productcharacteristic VALUES (27, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (27, 94, '4GB', false);
INSERT INTO public.productcharacteristic VALUES (27, 95, '1333MHz', false);
INSERT INTO public.productcharacteristic VALUES (27, 96, '-', false);
INSERT INTO public.productcharacteristic VALUES (27, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (27, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (28, 69, 'Hynix', false);
INSERT INTO public.productcharacteristic VALUES (28, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (28, 94, '4GB', false);
INSERT INTO public.productcharacteristic VALUES (28, 95, '1333MHz', false);
INSERT INTO public.productcharacteristic VALUES (28, 96, '-', false);
INSERT INTO public.productcharacteristic VALUES (28, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (28, 98, 'ПК', false);
INSERT INTO public.productcharacteristic VALUES (29, 69, 'Palit', false);
INSERT INTO public.productcharacteristic VALUES (29, 76, '550MHz', false);
INSERT INTO public.productcharacteristic VALUES (29, 77, 'DDR2', false);
INSERT INTO public.productcharacteristic VALUES (29, 79, '50W', false);
INSERT INTO public.productcharacteristic VALUES (29, 87, '2.0', false);
INSERT INTO public.productcharacteristic VALUES (29, 94, '1024MB', false);
INSERT INTO public.productcharacteristic VALUES (29, 95, '700MHz', false);
INSERT INTO public.productcharacteristic VALUES (29, 99, '128bit', false);
INSERT INTO public.productcharacteristic VALUES (29, 100, 'Пассивная (радиатор)', false);
INSERT INTO public.productcharacteristic VALUES (29, 101, 'HDMI, VGA, DVI', false);
INSERT INTO public.productcharacteristic VALUES (29, 110, '-', false);
INSERT INTO public.productcharacteristic VALUES (30, 69, 'MSI', false);
INSERT INTO public.productcharacteristic VALUES (30, 76, '954MHz', false);
INSERT INTO public.productcharacteristic VALUES (30, 77, 'GDDR3', false);
INSERT INTO public.productcharacteristic VALUES (30, 79, '19W', false);
INSERT INTO public.productcharacteristic VALUES (30, 87, '2.0', false);
INSERT INTO public.productcharacteristic VALUES (30, 94, '1024MB', false);
INSERT INTO public.productcharacteristic VALUES (30, 95, '1800MHz', false);
INSERT INTO public.productcharacteristic VALUES (30, 99, '64bit', false);
INSERT INTO public.productcharacteristic VALUES (30, 100, 'Пассивная (радиатор)', false);
INSERT INTO public.productcharacteristic VALUES (30, 101, 'HDMI, VGA, DVI', false);
INSERT INTO public.productcharacteristic VALUES (30, 110, '-', false);
INSERT INTO public.productcharacteristic VALUES (31, 69, 'ASUS', false);
INSERT INTO public.productcharacteristic VALUES (31, 76, '1392MHz', false);
INSERT INTO public.productcharacteristic VALUES (31, 77, 'GDDR5', false);
INSERT INTO public.productcharacteristic VALUES (31, 79, '75W', false);
INSERT INTO public.productcharacteristic VALUES (31, 87, '3.0', false);
INSERT INTO public.productcharacteristic VALUES (31, 94, '4GB', false);
INSERT INTO public.productcharacteristic VALUES (31, 95, '7008MHz', false);
INSERT INTO public.productcharacteristic VALUES (31, 99, '128bit', false);
INSERT INTO public.productcharacteristic VALUES (31, 100, 'Активная (2 вентилятора)', false);
INSERT INTO public.productcharacteristic VALUES (31, 101, 'HDMI, DVI, DisplayPort', false);
INSERT INTO public.productcharacteristic VALUES (31, 110, '-', false);
INSERT INTO public.productcharacteristic VALUES (32, 69, 'HIS', false);
INSERT INTO public.productcharacteristic VALUES (32, 76, '800MHz', false);
INSERT INTO public.productcharacteristic VALUES (32, 77, 'DDR3', false);
INSERT INTO public.productcharacteristic VALUES (32, 79, '108W', false);
INSERT INTO public.productcharacteristic VALUES (32, 87, '2.1', false);
INSERT INTO public.productcharacteristic VALUES (32, 94, '1024MB', false);
INSERT INTO public.productcharacteristic VALUES (32, 95, '1600MHz', false);
INSERT INTO public.productcharacteristic VALUES (32, 99, '128bit', false);
INSERT INTO public.productcharacteristic VALUES (32, 100, 'Активная (1 вентилятор)', false);
INSERT INTO public.productcharacteristic VALUES (32, 101, 'HDMI, VGA, DVI', false);
INSERT INTO public.productcharacteristic VALUES (32, 110, '6pin', false);
INSERT INTO public.productcharacteristic VALUES (33, 69, '3Cott', false);
INSERT INTO public.productcharacteristic VALUES (33, 90, '4pin', false);
INSERT INTO public.productcharacteristic VALUES (33, 91, '24pin', false);
INSERT INTO public.productcharacteristic VALUES (33, 100, 'Активное', false);
INSERT INTO public.productcharacteristic VALUES (33, 105, 'ATX', false);
INSERT INTO public.productcharacteristic VALUES (33, 106, '450W', false);
INSERT INTO public.productcharacteristic VALUES (33, 107, '23A', false);
INSERT INTO public.productcharacteristic VALUES (33, 108, '19A', false);
INSERT INTO public.productcharacteristic VALUES (33, 109, '17A', false);
INSERT INTO public.productcharacteristic VALUES (33, 110, '6pin', false);
INSERT INTO public.productcharacteristic VALUES (33, 111, '2', false);
INSERT INTO public.productcharacteristic VALUES (33, 112, '4', false);
INSERT INTO public.productcharacteristic VALUES (34, 69, 'Velton', false);
INSERT INTO public.productcharacteristic VALUES (34, 90, '4pin', false);
INSERT INTO public.productcharacteristic VALUES (34, 91, '24pin', false);
INSERT INTO public.productcharacteristic VALUES (34, 100, 'Активное', false);
INSERT INTO public.productcharacteristic VALUES (34, 105, 'ATX', false);
INSERT INTO public.productcharacteristic VALUES (34, 106, '400W', false);
INSERT INTO public.productcharacteristic VALUES (34, 107, '22A', false);
INSERT INTO public.productcharacteristic VALUES (34, 108, '40A', false);
INSERT INTO public.productcharacteristic VALUES (34, 109, '28A', false);
INSERT INTO public.productcharacteristic VALUES (34, 110, '-', false);
INSERT INTO public.productcharacteristic VALUES (34, 111, '3', false);
INSERT INTO public.productcharacteristic VALUES (34, 112, '4', false);
INSERT INTO public.productcharacteristic VALUES (35, 69, 'Zalman', false);
INSERT INTO public.productcharacteristic VALUES (35, 90, '4+4pin', false);
INSERT INTO public.productcharacteristic VALUES (35, 91, '24pin', false);
INSERT INTO public.productcharacteristic VALUES (35, 100, 'Активное', false);
INSERT INTO public.productcharacteristic VALUES (35, 105, 'ATX', false);
INSERT INTO public.productcharacteristic VALUES (35, 106, '600W', false);
INSERT INTO public.productcharacteristic VALUES (35, 107, '64A', false);
INSERT INTO public.productcharacteristic VALUES (35, 108, '24A', false);
INSERT INTO public.productcharacteristic VALUES (35, 109, '24A', false);
INSERT INTO public.productcharacteristic VALUES (35, 110, '6+6pin', false);
INSERT INTO public.productcharacteristic VALUES (35, 111, '6', false);
INSERT INTO public.productcharacteristic VALUES (35, 112, '3', false);
INSERT INTO public.productcharacteristic VALUES (36, 69, 'DEXP', false);
INSERT INTO public.productcharacteristic VALUES (36, 90, '4pin', false);
INSERT INTO public.productcharacteristic VALUES (36, 91, '24pin', false);
INSERT INTO public.productcharacteristic VALUES (36, 100, 'Активная', false);
INSERT INTO public.productcharacteristic VALUES (36, 105, 'ATX', false);
INSERT INTO public.productcharacteristic VALUES (36, 106, '350W', false);
INSERT INTO public.productcharacteristic VALUES (36, 107, '13A', false);
INSERT INTO public.productcharacteristic VALUES (36, 108, '12A', false);
INSERT INTO public.productcharacteristic VALUES (36, 109, '10A', false);
INSERT INTO public.productcharacteristic VALUES (36, 110, '-', false);
INSERT INTO public.productcharacteristic VALUES (36, 111, '2', false);
INSERT INTO public.productcharacteristic VALUES (36, 112, '2', false);
INSERT INTO public.productcharacteristic VALUES (37, 69, 'FSP Group Inc', false);
INSERT INTO public.productcharacteristic VALUES (37, 90, '4pin', false);
INSERT INTO public.productcharacteristic VALUES (37, 91, '24pin', false);
INSERT INTO public.productcharacteristic VALUES (37, 100, 'Активная', false);
INSERT INTO public.productcharacteristic VALUES (37, 105, 'ATX', false);
INSERT INTO public.productcharacteristic VALUES (37, 106, '550W', false);
INSERT INTO public.productcharacteristic VALUES (37, 107, '38A', false);
INSERT INTO public.productcharacteristic VALUES (37, 108, '20A', false);
INSERT INTO public.productcharacteristic VALUES (37, 109, '24A', false);
INSERT INTO public.productcharacteristic VALUES (37, 110, '6+2pin', false);
INSERT INTO public.productcharacteristic VALUES (37, 111, '4', false);
INSERT INTO public.productcharacteristic VALUES (37, 112, '4', false);
INSERT INTO public.productcharacteristic VALUES (38, 69, 'FSP Group Inc', false);
INSERT INTO public.productcharacteristic VALUES (38, 90, '4pin', false);
INSERT INTO public.productcharacteristic VALUES (38, 91, '24pin', false);
INSERT INTO public.productcharacteristic VALUES (38, 100, 'Активная', false);
INSERT INTO public.productcharacteristic VALUES (38, 105, 'ATX', false);
INSERT INTO public.productcharacteristic VALUES (38, 106, '450W', false);
INSERT INTO public.productcharacteristic VALUES (38, 107, '36A', false);
INSERT INTO public.productcharacteristic VALUES (38, 108, '15A', false);
INSERT INTO public.productcharacteristic VALUES (38, 109, '15A', false);
INSERT INTO public.productcharacteristic VALUES (38, 110, '6pin', false);
INSERT INTO public.productcharacteristic VALUES (38, 111, '2', false);
INSERT INTO public.productcharacteristic VALUES (38, 112, '6', false);
INSERT INTO public.productcharacteristic VALUES (39, 69, 'Toshiba', false);
INSERT INTO public.productcharacteristic VALUES (39, 94, '500GB', false);
INSERT INTO public.productcharacteristic VALUES (39, 102, 'HDD', false);
INSERT INTO public.productcharacteristic VALUES (39, 103, '-', false);
INSERT INTO public.productcharacteristic VALUES (39, 104, '-', false);
INSERT INTO public.productcharacteristic VALUES (39, 105, '3`5', false);
INSERT INTO public.productcharacteristic VALUES (40, 69, 'Samsung', false);
INSERT INTO public.productcharacteristic VALUES (40, 94, '120GB', false);
INSERT INTO public.productcharacteristic VALUES (40, 102, 'SSD', false);
INSERT INTO public.productcharacteristic VALUES (40, 103, '-', false);
INSERT INTO public.productcharacteristic VALUES (40, 104, '-', false);
INSERT INTO public.productcharacteristic VALUES (40, 105, '2`5', false);
INSERT INTO public.productcharacteristic VALUES (41, 69, 'Team Group', false);
INSERT INTO public.productcharacteristic VALUES (41, 94, '240GB', false);
INSERT INTO public.productcharacteristic VALUES (41, 102, 'SSD', false);
INSERT INTO public.productcharacteristic VALUES (41, 103, '-', false);
INSERT INTO public.productcharacteristic VALUES (41, 104, '-', false);
INSERT INTO public.productcharacteristic VALUES (41, 105, '2`5', false);
INSERT INTO public.productcharacteristic VALUES (42, 69, 'Seagate', false);
INSERT INTO public.productcharacteristic VALUES (42, 94, '500GB', false);
INSERT INTO public.productcharacteristic VALUES (42, 102, 'HDD', false);
INSERT INTO public.productcharacteristic VALUES (42, 103, '-', false);
INSERT INTO public.productcharacteristic VALUES (42, 104, '-', false);
INSERT INTO public.productcharacteristic VALUES (42, 105, '2`5', false);
INSERT INTO public.productcharacteristic VALUES (43, 69, 'Samsung', false);
INSERT INTO public.productcharacteristic VALUES (43, 94, '1000GB', false);
INSERT INTO public.productcharacteristic VALUES (43, 102, 'HDD', false);
INSERT INTO public.productcharacteristic VALUES (43, 103, '-', false);
INSERT INTO public.productcharacteristic VALUES (43, 104, '-', false);
INSERT INTO public.productcharacteristic VALUES (43, 105, '2`5', false);
INSERT INTO public.productcharacteristic VALUES (44, 69, 'Seagate', false);
INSERT INTO public.productcharacteristic VALUES (44, 94, '3TB', false);
INSERT INTO public.productcharacteristic VALUES (44, 102, 'HDD', false);
INSERT INTO public.productcharacteristic VALUES (44, 103, '-', false);
INSERT INTO public.productcharacteristic VALUES (44, 104, '-', false);
INSERT INTO public.productcharacteristic VALUES (44, 105, '3`5', false);
INSERT INTO public.productcharacteristic VALUES (45, 69, 'Western Digital', false);
INSERT INTO public.productcharacteristic VALUES (45, 94, '500GB', false);
INSERT INTO public.productcharacteristic VALUES (45, 102, 'HDD', false);
INSERT INTO public.productcharacteristic VALUES (45, 103, '-', false);
INSERT INTO public.productcharacteristic VALUES (45, 104, '-', false);
INSERT INTO public.productcharacteristic VALUES (45, 105, '3`5', false);
INSERT INTO public.productcharacteristic VALUES (46, 69, 'Seagate', false);
INSERT INTO public.productcharacteristic VALUES (46, 94, '1TB', false);
INSERT INTO public.productcharacteristic VALUES (46, 102, 'HDD', false);
INSERT INTO public.productcharacteristic VALUES (46, 103, '-', false);
INSERT INTO public.productcharacteristic VALUES (46, 104, '-', false);
INSERT INTO public.productcharacteristic VALUES (46, 105, '3`5', false);
INSERT INTO public.productcharacteristic VALUES (47, 69, 'Kingston', false);
INSERT INTO public.productcharacteristic VALUES (47, 94, '960GB', false);
INSERT INTO public.productcharacteristic VALUES (47, 102, 'SSD', false);
INSERT INTO public.productcharacteristic VALUES (47, 103, '-', false);
INSERT INTO public.productcharacteristic VALUES (47, 104, '-', false);
INSERT INTO public.productcharacteristic VALUES (47, 105, '2`5', false);
INSERT INTO public.productcharacteristic VALUES (48, 68, 'зависит от переходного кольца', false);
INSERT INTO public.productcharacteristic VALUES (48, 69, 'DEEPCOOL', false);
INSERT INTO public.productcharacteristic VALUES (48, 79, '~100W', false);
INSERT INTO public.productcharacteristic VALUES (48, 90, '4pin', false);
INSERT INTO public.productcharacteristic VALUES (48, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (48, 100, 'Башня', false);
INSERT INTO public.productcharacteristic VALUES (48, 113, '~90mm', false);
INSERT INTO public.productcharacteristic VALUES (48, 114, '1', false);
INSERT INTO public.productcharacteristic VALUES (48, 115, '2', false);
INSERT INTO public.productcharacteristic VALUES (48, 116, 'переходное кольцо', false);
INSERT INTO public.productcharacteristic VALUES (49, 68, 'LGA115X', false);
INSERT INTO public.productcharacteristic VALUES (49, 69, 'Intel', false);
INSERT INTO public.productcharacteristic VALUES (49, 79, '~75W', false);
INSERT INTO public.productcharacteristic VALUES (49, 90, '4pin', false);
INSERT INTO public.productcharacteristic VALUES (49, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (49, 100, 'Блюдечко', false);
INSERT INTO public.productcharacteristic VALUES (49, 113, '~80mm', false);
INSERT INTO public.productcharacteristic VALUES (49, 114, '1', false);
INSERT INTO public.productcharacteristic VALUES (49, 115, '0', false);
INSERT INTO public.productcharacteristic VALUES (49, 116, 'Клипсы', false);
INSERT INTO public.productcharacteristic VALUES (50, 68, 'LGA115X/LGA1200', false);
INSERT INTO public.productcharacteristic VALUES (50, 69, 'NoName', false);
INSERT INTO public.productcharacteristic VALUES (50, 79, '100W', false);
INSERT INTO public.productcharacteristic VALUES (50, 90, '3pin', false);
INSERT INTO public.productcharacteristic VALUES (50, 97, 'Разноцветная', false);
INSERT INTO public.productcharacteristic VALUES (50, 100, 'Башня', false);
INSERT INTO public.productcharacteristic VALUES (50, 113, '~92mm', false);
INSERT INTO public.productcharacteristic VALUES (50, 114, '1', false);
INSERT INTO public.productcharacteristic VALUES (50, 115, '2', false);
INSERT INTO public.productcharacteristic VALUES (50, 116, 'Переходное кольцо', false);
INSERT INTO public.productcharacteristic VALUES (51, 68, 'LGA115X', false);
INSERT INTO public.productcharacteristic VALUES (51, 69, 'DeepCool', false);
INSERT INTO public.productcharacteristic VALUES (51, 79, '~95W', false);
INSERT INTO public.productcharacteristic VALUES (51, 90, '3pin', false);
INSERT INTO public.productcharacteristic VALUES (51, 97, '-', false);
INSERT INTO public.productcharacteristic VALUES (51, 100, 'Блюдечко', false);
INSERT INTO public.productcharacteristic VALUES (51, 113, '~120mm', false);
INSERT INTO public.productcharacteristic VALUES (51, 114, '1', false);
INSERT INTO public.productcharacteristic VALUES (51, 115, '0', false);
INSERT INTO public.productcharacteristic VALUES (51, 116, 'Клипсы', false);
INSERT INTO public.productcharacteristic VALUES (52, 69, 'Code', false);
INSERT INTO public.productcharacteristic VALUES (52, 97, 'Полоса спереди', false);
INSERT INTO public.productcharacteristic VALUES (52, 105, 'Midi-Tower', false);
INSERT INTO public.productcharacteristic VALUES (52, 117, 'Micro-ATX', false);
INSERT INTO public.productcharacteristic VALUES (52, 118, '120mm x 2, 90mm x 1', false);
INSERT INTO public.productcharacteristic VALUES (52, 119, 'Акрил', false);
INSERT INTO public.productcharacteristic VALUES (52, 120, 'USB3.0x1, USB2.0x1, 3.5mm jack x 2', false);
INSERT INTO public.productcharacteristic VALUES (52, 121, '150mm', false);
INSERT INTO public.productcharacteristic VALUES (52, 122, '320mm', false);
INSERT INTO public.productcharacteristic VALUES (53, 69, 'AeroCool', false);
INSERT INTO public.productcharacteristic VALUES (53, 97, 'Спереди', false);
INSERT INTO public.productcharacteristic VALUES (53, 105, 'Mini-Tower', false);
INSERT INTO public.productcharacteristic VALUES (53, 117, 'Micro-ATX', false);
INSERT INTO public.productcharacteristic VALUES (53, 118, '-', false);
INSERT INTO public.productcharacteristic VALUES (53, 119, 'Орг. стекло', false);
INSERT INTO public.productcharacteristic VALUES (53, 120, '-', false);
INSERT INTO public.productcharacteristic VALUES (53, 121, '-', false);
INSERT INTO public.productcharacteristic VALUES (53, 122, '-', false);
INSERT INTO public.productcharacteristic VALUES (54, 69, 'Ginzzu', false);
INSERT INTO public.productcharacteristic VALUES (54, 97, 'Полоса спереди', false);
INSERT INTO public.productcharacteristic VALUES (54, 105, 'Midi-Tower', false);
INSERT INTO public.productcharacteristic VALUES (54, 117, 'Micro-ATX', false);
INSERT INTO public.productcharacteristic VALUES (54, 118, '120mm x4', false);
INSERT INTO public.productcharacteristic VALUES (54, 119, 'Акрил', false);
INSERT INTO public.productcharacteristic VALUES (54, 120, 'USB3.0x1, USB2.0x1, 3.5mm jack x 2', false);
INSERT INTO public.productcharacteristic VALUES (54, 121, '155mm', false);
INSERT INTO public.productcharacteristic VALUES (54, 122, '330mm', false);


--
-- TOC entry 3569 (class 0 OID 32887)
-- Dependencies: 230
-- Data for Name: productpricechange; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.productpricechange VALUES (5, 20, 150.000, '150,00 ?', false);
INSERT INTO public.productpricechange VALUES (6, 28, 250.000, '250,00 ?', false);


--
-- TOC entry 3563 (class 0 OID 32843)
-- Dependencies: 224
-- Data for Name: provider; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.provider VALUES (3, 'Продавец с Авито', 'физ. лицо', NULL, NULL, NULL, NULL, false);
INSERT INTO public.provider VALUES (1, 'Продавец с Юлы', 'физ. лицо', NULL, NULL, NULL, '', false);
INSERT INTO public.provider VALUES (2, 'Продавец с Farpost', 'физ. лицо', NULL, NULL, NULL, NULL, false);
INSERT INTO public.provider VALUES (7, 'ООО "Ozon"', 'юр. лицо', NULL, NULL, NULL, NULL, false);
INSERT INTO public.provider VALUES (8, 'ООО "DNS"', '', NULL, NULL, NULL, NULL, false);


--
-- TOC entry 3591 (class 0 OID 41106)
-- Dependencies: 252
-- Data for Name: service; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.service VALUES (6, 'Замена термопасты', 'Распространяется на одно устройство (Процессор, видеокарта)', '150,00 ?', false);


--
-- TOC entry 3565 (class 0 OID 32853)
-- Dependencies: 226
-- Data for Name: shipment; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.shipment VALUES (15, '2023-05-07', 2, 1, '0,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (16, '2023-05-07', 2, 1, '0,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (17, '2023-05-07', 1, 1, '300,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (18, '2023-05-07', 2, 1, '0,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (19, '2023-05-07', 2, 1, '0,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (20, '2023-05-07', 2, 1, '0,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (21, '2023-05-07', 2, 1, '1 500,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (22, '2023-05-07', 2, 1, '1 500,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (23, '2023-05-07', 1, 1, '1 500,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (24, '2023-05-07', 2, 1, '2 500,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (25, '2023-05-07', 2, 1, '150,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (26, '2023-05-07', 2, 1, '0,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (27, '2023-05-07', 3, 1, '1 000,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (28, '2023-05-07', 2, 1, '1 000,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (29, '2023-05-07', 2, 1, '200,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (32, '2023-05-07', 2, 1, '250,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (33, '2023-05-07', 2, 3, '2 700,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (34, '2023-05-07', 2, 2, '801,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (35, '2023-05-07', 2, 4, '6 002,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (36, '2023-05-10', 2, 7, '6 200,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (37, '2023-05-10', 2, 1, '500,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (38, '2023-05-10', 1, 8, '10 102,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (39, '2023-05-10', 2, 5, '3 082,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (40, '2023-05-10', 7, 1, '2 399,00 ?', NULL, false);
INSERT INTO public.shipment VALUES (41, '2023-05-10', 7, 2, '7 079,00 ?', NULL, false);


--
-- TOC entry 3578 (class 0 OID 40963)
-- Dependencies: 239
-- Data for Name: worker; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.worker VALUES (4, 'Владимир', 'Булочкин', 'Евгеньевич', '2005-09-05', 2, NULL, NULL, '0519 693321', '+79242486083', 'soulcomp@mail.ru', NULL, false, 'Vova', 'vova2005');
INSERT INTO public.worker VALUES (2, 'Владимир', 'Булыгин', 'Евгеньевич', '2005-09-02', 1, NULL, NULL, '0519 697321', '+79025213321', 'vovan200512345@mail.ru', NULL, false, 'Anavoviy', 'vova2005');
INSERT INTO public.worker VALUES (5, 'Алексей', 'Зиновьев', 'Иванович', '1962-05-02', 2, 'ПН: 10:00-18:00, ВТ: 9:00-17:00, ПТ:11:00-20:00', 2, '5684 568789', '+79246548595', 'zinovievAlex@mail.ru', 'СБЕР: 45678394935935932095329529', true, 'Zinoviev', '1234');


--
-- TOC entry 3580 (class 0 OID 40985)
-- Dependencies: 241
-- Data for Name: workingshift; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.workingshift VALUES (1, '2023-03-06', 2, 3, false);
INSERT INTO public.workingshift VALUES (2, '2023-03-31', 2, 1, false);
INSERT INTO public.workingshift VALUES (3, '2023-03-31', 2, 1, false);
INSERT INTO public.workingshift VALUES (4, '2023-04-29', 4, 1, false);
INSERT INTO public.workingshift VALUES (5, '2023-05-07', 4, 1, false);


--
-- TOC entry 3621 (class 0 OID 0)
-- Dependencies: 247
-- Name: application_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.application_id_seq', 17, true);


--
-- TOC entry 3622 (class 0 OID 0)
-- Dependencies: 242
-- Name: assembly_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.assembly_id_seq', 3, true);


--
-- TOC entry 3623 (class 0 OID 0)
-- Dependencies: 214
-- Name: bankaccount_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.bankaccount_id_seq', 13, true);


--
-- TOC entry 3624 (class 0 OID 0)
-- Dependencies: 220
-- Name: category_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.category_id_seq', 11, true);


--
-- TOC entry 3625 (class 0 OID 0)
-- Dependencies: 218
-- Name: characteristic_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.characteristic_id_seq', 122, true);


--
-- TOC entry 3626 (class 0 OID 0)
-- Dependencies: 245
-- Name: client_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.client_id_seq', 7, true);


--
-- TOC entry 3627 (class 0 OID 0)
-- Dependencies: 249
-- Name: clientsdevice_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.clientsdevice_id_seq', 12, true);


--
-- TOC entry 3628 (class 0 OID 0)
-- Dependencies: 234
-- Name: howpay_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.howpay_id_seq', 3, true);


--
-- TOC entry 3629 (class 0 OID 0)
-- Dependencies: 216
-- Name: operation_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.operation_id_seq', 7, true);


--
-- TOC entry 3630 (class 0 OID 0)
-- Dependencies: 236
-- Name: plan_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.plan_id_seq', 2, true);


--
-- TOC entry 3631 (class 0 OID 0)
-- Dependencies: 232
-- Name: post_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.post_id_seq', 4, true);


--
-- TOC entry 3632 (class 0 OID 0)
-- Dependencies: 227
-- Name: product_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.product_id_seq', 23, true);


--
-- TOC entry 3633 (class 0 OID 0)
-- Dependencies: 229
-- Name: productpricechange_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.productpricechange_id_seq', 6, true);


--
-- TOC entry 3634 (class 0 OID 0)
-- Dependencies: 223
-- Name: provider_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.provider_id_seq', 8, true);


--
-- TOC entry 3635 (class 0 OID 0)
-- Dependencies: 251
-- Name: service_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.service_id_seq', 6, true);


--
-- TOC entry 3636 (class 0 OID 0)
-- Dependencies: 225
-- Name: shipment_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.shipment_id_seq', 41, true);


--
-- TOC entry 3637 (class 0 OID 0)
-- Dependencies: 238
-- Name: worker_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.worker_id_seq', 5, true);


--
-- TOC entry 3638 (class 0 OID 0)
-- Dependencies: 240
-- Name: workingshift_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.workingshift_id_seq', 5, true);


--
-- TOC entry 3371 (class 2606 OID 41079)
-- Name: application application_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_pk PRIMARY KEY (id);


--
-- TOC entry 3379 (class 2606 OID 57381)
-- Name: applicationassembly applicationassembly_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationassembly
    ADD CONSTRAINT applicationassembly_pk PRIMARY KEY (idapplication, idassemby);


--
-- TOC entry 3381 (class 2606 OID 57377)
-- Name: applicationproduct applicationproduct_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationproduct
    ADD CONSTRAINT applicationproduct_pk PRIMARY KEY (idapplication, idproduct);


--
-- TOC entry 3377 (class 2606 OID 57379)
-- Name: applicationservice applicationservice_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_pk PRIMARY KEY (idapplication, idservice, idworker);


--
-- TOC entry 3361 (class 2606 OID 41006)
-- Name: assembly assembly_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly
    ADD CONSTRAINT assembly_pk PRIMARY KEY (id);


--
-- TOC entry 3363 (class 2606 OID 57383)
-- Name: assemblyproduct assemblyproduct_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assemblyproduct
    ADD CONSTRAINT assemblyproduct_pk PRIMARY KEY (idassembly, idproduct);


--
-- TOC entry 3327 (class 2606 OID 32787)
-- Name: bankaccount bankaccount_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bankaccount
    ADD CONSTRAINT bankaccount_pk PRIMARY KEY (id);


--
-- TOC entry 3333 (class 2606 OID 32827)
-- Name: category category_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.category
    ADD CONSTRAINT category_pk PRIMARY KEY (id);


--
-- TOC entry 3335 (class 2606 OID 57385)
-- Name: categorycharacteristic categorycharacteristic_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categorycharacteristic
    ADD CONSTRAINT categorycharacteristic_pk PRIMARY KEY (idcategory, idcharacteristic);


--
-- TOC entry 3331 (class 2606 OID 32817)
-- Name: characteristic characteristic_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.characteristic
    ADD CONSTRAINT characteristic_pk PRIMARY KEY (id);


--
-- TOC entry 3365 (class 2606 OID 41065)
-- Name: client client_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_pk PRIMARY KEY (id);


--
-- TOC entry 3367 (class 2606 OID 41067)
-- Name: client client_un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_un UNIQUE (passport);


--
-- TOC entry 3369 (class 2606 OID 41069)
-- Name: client client_un2; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_un2 UNIQUE (email);


--
-- TOC entry 3373 (class 2606 OID 41099)
-- Name: clientsdevice clientsdevice_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientsdevice
    ADD CONSTRAINT clientsdevice_pk PRIMARY KEY (id);


--
-- TOC entry 3349 (class 2606 OID 32935)
-- Name: howpay howpay_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.howpay
    ADD CONSTRAINT howpay_pk PRIMARY KEY (id);


--
-- TOC entry 3329 (class 2606 OID 32797)
-- Name: operation operation_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation
    ADD CONSTRAINT operation_pk PRIMARY KEY (id);


--
-- TOC entry 3351 (class 2606 OID 40961)
-- Name: plan plan_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.plan
    ADD CONSTRAINT plan_pk PRIMARY KEY (id);


--
-- TOC entry 3347 (class 2606 OID 32923)
-- Name: post post_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.post
    ADD CONSTRAINT post_pk PRIMARY KEY (id);


--
-- TOC entry 3341 (class 2606 OID 32875)
-- Name: product product_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_pk PRIMARY KEY (id);


--
-- TOC entry 3345 (class 2606 OID 57387)
-- Name: productcharacteristic productcharacteristic_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productcharacteristic
    ADD CONSTRAINT productcharacteristic_pk PRIMARY KEY (idproduct, idcharacteristic);


--
-- TOC entry 3343 (class 2606 OID 32893)
-- Name: productpricechange productpricechange_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productpricechange
    ADD CONSTRAINT productpricechange_pk PRIMARY KEY (id);


--
-- TOC entry 3337 (class 2606 OID 32851)
-- Name: provider provider_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.provider
    ADD CONSTRAINT provider_pk PRIMARY KEY (id);


--
-- TOC entry 3375 (class 2606 OID 41114)
-- Name: service service_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.service
    ADD CONSTRAINT service_pk PRIMARY KEY (id);


--
-- TOC entry 3339 (class 2606 OID 32861)
-- Name: shipment shipment_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shipment
    ADD CONSTRAINT shipment_pk PRIMARY KEY (id);


--
-- TOC entry 3353 (class 2606 OID 40971)
-- Name: worker worker_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_pk PRIMARY KEY (id);


--
-- TOC entry 3355 (class 2606 OID 40973)
-- Name: worker worker_un; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_un UNIQUE (passport);


--
-- TOC entry 3357 (class 2606 OID 49153)
-- Name: worker worker_un1; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_un1 UNIQUE (login);


--
-- TOC entry 3359 (class 2606 OID 40991)
-- Name: workingshift workingshift_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workingshift
    ADD CONSTRAINT workingshift_pk PRIMARY KEY (id);


--
-- TOC entry 3400 (class 2606 OID 41080)
-- Name: application application_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_fk FOREIGN KEY (idclient) REFERENCES public.client(id);


--
-- TOC entry 3401 (class 2606 OID 57365)
-- Name: application application_fk2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_fk2 FOREIGN KEY (iddevice) REFERENCES public.clientsdevice(id);


--
-- TOC entry 3402 (class 2606 OID 41085)
-- Name: application application_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.application
    ADD CONSTRAINT application_fk_1 FOREIGN KEY (idmanager) REFERENCES public.worker(id);


--
-- TOC entry 3409 (class 2606 OID 41166)
-- Name: applicationproduct applicationproduct_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationproduct
    ADD CONSTRAINT applicationproduct_fk FOREIGN KEY (idapplication) REFERENCES public.application(id);


--
-- TOC entry 3410 (class 2606 OID 41171)
-- Name: applicationproduct applicationproduct_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationproduct
    ADD CONSTRAINT applicationproduct_fk_1 FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3404 (class 2606 OID 41118)
-- Name: applicationservice applicationservice_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_fk FOREIGN KEY (idapplication) REFERENCES public.application(id);


--
-- TOC entry 3405 (class 2606 OID 41123)
-- Name: applicationservice applicationservice_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_fk_1 FOREIGN KEY (idservice) REFERENCES public.service(id);


--
-- TOC entry 3406 (class 2606 OID 41128)
-- Name: applicationservice applicationservice_fk_2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationservice
    ADD CONSTRAINT applicationservice_fk_2 FOREIGN KEY (idworker) REFERENCES public.worker(id);


--
-- TOC entry 3396 (class 2606 OID 41007)
-- Name: assembly assembly_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly
    ADD CONSTRAINT assembly_fk FOREIGN KEY (idmasterconfiguration) REFERENCES public.worker(id);


--
-- TOC entry 3397 (class 2606 OID 41012)
-- Name: assembly assembly_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assembly
    ADD CONSTRAINT assembly_fk_1 FOREIGN KEY (idmasterassembler) REFERENCES public.worker(id);


--
-- TOC entry 3398 (class 2606 OID 41022)
-- Name: assemblyproduct assemblyproduct_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assemblyproduct
    ADD CONSTRAINT assemblyproduct_fk FOREIGN KEY (idassembly) REFERENCES public.assembly(id);


--
-- TOC entry 3399 (class 2606 OID 41027)
-- Name: assemblyproduct assemblyproduct_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.assemblyproduct
    ADD CONSTRAINT assemblyproduct_fk_1 FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3384 (class 2606 OID 32831)
-- Name: categorycharacteristic categorycharacteristic_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categorycharacteristic
    ADD CONSTRAINT categorycharacteristic_fk FOREIGN KEY (idcategory) REFERENCES public.category(id);


--
-- TOC entry 3385 (class 2606 OID 32836)
-- Name: categorycharacteristic categorycharacteristic_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categorycharacteristic
    ADD CONSTRAINT categorycharacteristic_fk_1 FOREIGN KEY (idcharacteristic) REFERENCES public.characteristic(id);


--
-- TOC entry 3403 (class 2606 OID 41100)
-- Name: clientsdevice clientsdevice_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientsdevice
    ADD CONSTRAINT clientsdevice_fk FOREIGN KEY (idclient) REFERENCES public.client(id);


--
-- TOC entry 3407 (class 2606 OID 41152)
-- Name: applicationassembly newtable_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationassembly
    ADD CONSTRAINT newtable_fk FOREIGN KEY (idapplication) REFERENCES public.application(id);


--
-- TOC entry 3408 (class 2606 OID 41157)
-- Name: applicationassembly newtable_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.applicationassembly
    ADD CONSTRAINT newtable_fk_1 FOREIGN KEY (idassemby) REFERENCES public.assembly(id);


--
-- TOC entry 3382 (class 2606 OID 32798)
-- Name: operation operation_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation
    ADD CONSTRAINT operation_fk FOREIGN KEY (debet) REFERENCES public.bankaccount(id);


--
-- TOC entry 3383 (class 2606 OID 32803)
-- Name: operation operation_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation
    ADD CONSTRAINT operation_fk_1 FOREIGN KEY (credit) REFERENCES public.bankaccount(id);


--
-- TOC entry 3392 (class 2606 OID 32942)
-- Name: plan plan_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.plan
    ADD CONSTRAINT plan_fk FOREIGN KEY (idhowpay) REFERENCES public.howpay(id);


--
-- TOC entry 3390 (class 2606 OID 32904)
-- Name: productcharacteristic product_characteristic_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productcharacteristic
    ADD CONSTRAINT product_characteristic_fk FOREIGN KEY (idcharacteristic) REFERENCES public.characteristic(id);


--
-- TOC entry 3391 (class 2606 OID 32909)
-- Name: productcharacteristic product_characteristic_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productcharacteristic
    ADD CONSTRAINT product_characteristic_fk_1 FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3387 (class 2606 OID 32876)
-- Name: product product_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_fk FOREIGN KEY (idcategory) REFERENCES public.category(id);


--
-- TOC entry 3388 (class 2606 OID 32881)
-- Name: product product_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_fk_1 FOREIGN KEY (idshipment) REFERENCES public.shipment(id);


--
-- TOC entry 3389 (class 2606 OID 32894)
-- Name: productpricechange productpricechange_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.productpricechange
    ADD CONSTRAINT productpricechange_fk FOREIGN KEY (idproduct) REFERENCES public.product(id);


--
-- TOC entry 3386 (class 2606 OID 32862)
-- Name: shipment shipment_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.shipment
    ADD CONSTRAINT shipment_fk FOREIGN KEY (idprovider) REFERENCES public.provider(id);


--
-- TOC entry 3393 (class 2606 OID 40974)
-- Name: worker worker_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_fk FOREIGN KEY (idplan) REFERENCES public.plan(id);


--
-- TOC entry 3394 (class 2606 OID 40979)
-- Name: worker worker_fk_1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.worker
    ADD CONSTRAINT worker_fk_1 FOREIGN KEY (idpost) REFERENCES public.post(id);


--
-- TOC entry 3395 (class 2606 OID 40992)
-- Name: workingshift workingshift_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.workingshift
    ADD CONSTRAINT workingshift_fk FOREIGN KEY (idworker) REFERENCES public.worker(id);


-- Completed on 2023-05-15 07:48:41

--
-- PostgreSQL database dump complete
--

