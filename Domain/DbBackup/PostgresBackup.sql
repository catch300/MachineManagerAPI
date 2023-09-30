--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0 (Debian 16.0-1.pgdg120+1)
-- Dumped by pg_dump version 16.0 (Debian 16.0-1.pgdg120+1)

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

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Faults; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Faults" (
    "FaultId" integer NOT NULL,
    "Name" text,
    "Priority" text,
    "StartTime" timestamp without time zone,
    "EndTime" timestamp without time zone,
    "Description" text NOT NULL,
    "IsResolved" boolean,
    "MachineId" integer
);


ALTER TABLE public."Faults" OWNER TO postgres;

--
-- Name: Faults_FaultId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Faults_FaultId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Faults_FaultId_seq" OWNER TO postgres;

--
-- Name: Faults_FaultId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Faults_FaultId_seq" OWNED BY public."Faults"."FaultId";


--
-- Name: Machines; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Machines" (
    "MachineId" integer NOT NULL,
    "Name" text NOT NULL
);


ALTER TABLE public."Machines" OWNER TO postgres;

--
-- Name: Machines_MachineId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Machines_MachineId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Machines_MachineId_seq" OWNER TO postgres;

--
-- Name: Machines_MachineId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Machines_MachineId_seq" OWNED BY public."Machines"."MachineId";


--
-- Name: Faults FaultId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Faults" ALTER COLUMN "FaultId" SET DEFAULT nextval('public."Faults_FaultId_seq"'::regclass);


--
-- Name: Machines MachineId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Machines" ALTER COLUMN "MachineId" SET DEFAULT nextval('public."Machines_MachineId_seq"'::regclass);


--
-- Data for Name: Faults; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Faults" ("FaultId", "Name", "Priority", "StartTime", "EndTime", "Description", "IsResolved", "MachineId") FROM stdin;
\.


--
-- Data for Name: Machines; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Machines" ("MachineId", "Name") FROM stdin;
\.


--
-- Name: Faults_FaultId_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Faults_FaultId_seq"', 1, false);


--
-- Name: Machines_MachineId_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Machines_MachineId_seq"', 1, false);


--
-- Name: Faults Faults_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Faults"
    ADD CONSTRAINT "Faults_pkey" PRIMARY KEY ("FaultId");


--
-- Name: Machines Machines_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Machines"
    ADD CONSTRAINT "Machines_pkey" PRIMARY KEY ("MachineId");


--
-- Name: Faults Faults_MachineId_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Faults"
    ADD CONSTRAINT "Faults_MachineId_fkey" FOREIGN KEY ("MachineId") REFERENCES public."Machines"("MachineId");


--
-- PostgreSQL database dump complete
--

