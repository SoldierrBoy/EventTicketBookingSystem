--
-- PostgreSQL database dump
--

-- Dumped from database version 16.13
-- Dumped by pg_dump version 16.13

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
-- Name: account_statuses; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.account_statuses (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);


ALTER TABLE public.account_statuses OWNER TO "user";

--
-- Name: event_types; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.event_types (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);


ALTER TABLE public.event_types OWNER TO "user";

--
-- Name: events; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.events (
    id uuid NOT NULL,
    type_id integer NOT NULL,
    location_id uuid NOT NULL,
    title character varying(255) NOT NULL,
    description text,
    date_time timestamp with time zone NOT NULL,
    duration_minutes integer,
    created_at timestamp with time zone DEFAULT now()
);


ALTER TABLE public.events OWNER TO "user";

--
-- Name: locations; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.locations (
    id uuid NOT NULL,
    name character varying(255) NOT NULL,
    address text
);


ALTER TABLE public.locations OWNER TO "user";

--
-- Name: order_items; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.order_items (
    id uuid NOT NULL,
    order_id uuid NOT NULL,
    ticket_id uuid NOT NULL,
    price numeric(10,2) NOT NULL
);


ALTER TABLE public.order_items OWNER TO "user";

--
-- Name: order_statuses; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.order_statuses (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);


ALTER TABLE public.order_statuses OWNER TO "user";

--
-- Name: orders; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.orders (
    id uuid NOT NULL,
    user_id uuid NOT NULL,
    status_id integer NOT NULL,
    total_price numeric(10,2) NOT NULL,
    created_at timestamp with time zone DEFAULT now()
);


ALTER TABLE public.orders OWNER TO "user";

--
-- Name: payment_statuses; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.payment_statuses (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);


ALTER TABLE public.payment_statuses OWNER TO "user";

--
-- Name: payments; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.payments (
    id uuid NOT NULL,
    order_id uuid NOT NULL,
    status_id integer NOT NULL,
    amount numeric(10,2) NOT NULL,
    created_at timestamp with time zone DEFAULT now()
);


ALTER TABLE public.payments OWNER TO "user";

--
-- Name: roles; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.roles (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);


ALTER TABLE public.roles OWNER TO "user";

--
-- Name: seats; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.seats (
    id uuid NOT NULL,
    location_id uuid NOT NULL,
    "row" integer NOT NULL,
    number integer NOT NULL
);


ALTER TABLE public.seats OWNER TO "user";

--
-- Name: ticket_statuses; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.ticket_statuses (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);


ALTER TABLE public.ticket_statuses OWNER TO "user";

--
-- Name: tickets; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.tickets (
    id uuid NOT NULL,
    event_id uuid NOT NULL,
    seat_id uuid NOT NULL,
    status_id integer NOT NULL,
    price numeric(10,2) NOT NULL
);


ALTER TABLE public.tickets OWNER TO "user";

--
-- Name: users; Type: TABLE; Schema: public; Owner: user
--

CREATE TABLE public.users (
    id uuid NOT NULL,
    email character varying(255) NOT NULL,
    password_hash character varying(255) NOT NULL,
    role_id integer NOT NULL,
    account_status_id integer NOT NULL,
    created_at timestamp with time zone DEFAULT now()
);


ALTER TABLE public.users OWNER TO "user";

--
-- Data for Name: account_statuses; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.account_statuses (id, name) FROM stdin;
1	Active
2	Pending
3	Blocked
4	Deleted
\.


--
-- Data for Name: event_types; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.event_types (id, name) FROM stdin;
1	Cinema
2	Concert
3	Theatre
\.


--
-- Data for Name: events; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.events (id, type_id, location_id, title, description, date_time, duration_minutes, created_at) FROM stdin;
b2a5323e-647d-4c3e-86e4-4458f3f89111	1	e1a5323e-647d-4c3e-86e4-4458f3f89001	Dune: Part Two	\N	2026-04-09 18:12:29.725566+00	\N	2026-04-09 18:12:29.725566+00
\.


--
-- Data for Name: locations; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.locations (id, name, address) FROM stdin;
e1a5323e-647d-4c3e-86e4-4458f3f89001	Cinema City: Hall 1	Київ, ТРЦ Ocean Plaza
e1a5323e-647d-4c3e-86e4-4458f3f89002	National Opera	Львів, просп. Свободи, 28
\.


--
-- Data for Name: order_items; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.order_items (id, order_id, ticket_id, price) FROM stdin;
3e4b624b-e8f8-43b1-9d1f-8acf5235d396	c3a5323e-647d-4c3e-86e4-4458f3f89222	befb9fcd-7711-4169-befa-323cb6db56d7	250.00
\.


--
-- Data for Name: order_statuses; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.order_statuses (id, name) FROM stdin;
1	Pending
2	Paid
3	Cancelled
\.


--
-- Data for Name: orders; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.orders (id, user_id, status_id, total_price, created_at) FROM stdin;
c3a5323e-647d-4c3e-86e4-4458f3f89222	a3c0abc6-1456-4366-b10d-9013b74e407c	1	250.00	2026-04-09 18:05:33.961624+00
\.


--
-- Data for Name: payment_statuses; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.payment_statuses (id, name) FROM stdin;
1	Pending
2	Success
3	Failed
\.


--
-- Data for Name: payments; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.payments (id, order_id, status_id, amount, created_at) FROM stdin;
3334ee5a-0778-4b26-8d19-ea1463ad5889	c3a5323e-647d-4c3e-86e4-4458f3f89222	1	250.00	2026-04-09 18:19:31.546106+00
\.


--
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.roles (id, name) FROM stdin;
1	Admin
2	Manager
3	User
\.


--
-- Data for Name: seats; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.seats (id, location_id, "row", number) FROM stdin;
fd3b818c-64a9-44fb-a188-7a39b15f420c	e1a5323e-647d-4c3e-86e4-4458f3f89001	1	1
8a032cc5-cdb2-4ef0-8be2-3c63ca96339f	e1a5323e-647d-4c3e-86e4-4458f3f89001	1	2
d43df676-ee9c-45a0-b7c9-3bb89d72b379	e1a5323e-647d-4c3e-86e4-4458f3f89001	1	3
11a5323e-647d-4c3e-86e4-4458f3f89001	e1a5323e-647d-4c3e-86e4-4458f3f89001	1	1
\.


--
-- Data for Name: ticket_statuses; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.ticket_statuses (id, name) FROM stdin;
1	Available
2	Reserved
3	Sold
\.


--
-- Data for Name: tickets; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.tickets (id, event_id, seat_id, status_id, price) FROM stdin;
befb9fcd-7711-4169-befa-323cb6db56d7	b2a5323e-647d-4c3e-86e4-4458f3f89111	11a5323e-647d-4c3e-86e4-4458f3f89001	1	250.00
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: user
--

COPY public.users (id, email, password_hash, role_id, account_status_id, created_at) FROM stdin;
a3c0abc6-1456-4366-b10d-9013b74e407c	admin@test.com	hash_admin_123	1	1	2026-04-09 17:55:00.050215+00
caef7f53-d874-4811-abde-326f7c76b264	test@test.com	hash_user_456	3	1	2026-04-09 17:55:00.050215+00
\.


--
-- Name: account_statuses account_statuses_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.account_statuses
    ADD CONSTRAINT account_statuses_pkey PRIMARY KEY (id);


--
-- Name: event_types event_types_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.event_types
    ADD CONSTRAINT event_types_pkey PRIMARY KEY (id);


--
-- Name: events events_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.events
    ADD CONSTRAINT events_pkey PRIMARY KEY (id);


--
-- Name: locations locations_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.locations
    ADD CONSTRAINT locations_pkey PRIMARY KEY (id);


--
-- Name: order_items order_items_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT order_items_pkey PRIMARY KEY (id);


--
-- Name: order_items order_items_ticket_id_key; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT order_items_ticket_id_key UNIQUE (ticket_id);


--
-- Name: order_statuses order_statuses_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.order_statuses
    ADD CONSTRAINT order_statuses_pkey PRIMARY KEY (id);


--
-- Name: orders orders_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);


--
-- Name: payment_statuses payment_statuses_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.payment_statuses
    ADD CONSTRAINT payment_statuses_pkey PRIMARY KEY (id);


--
-- Name: payments payments_order_id_key; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.payments
    ADD CONSTRAINT payments_order_id_key UNIQUE (order_id);


--
-- Name: payments payments_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.payments
    ADD CONSTRAINT payments_pkey PRIMARY KEY (id);


--
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (id);


--
-- Name: seats seats_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.seats
    ADD CONSTRAINT seats_pkey PRIMARY KEY (id);


--
-- Name: ticket_statuses ticket_statuses_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.ticket_statuses
    ADD CONSTRAINT ticket_statuses_pkey PRIMARY KEY (id);


--
-- Name: tickets tickets_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.tickets
    ADD CONSTRAINT tickets_pkey PRIMARY KEY (id);


--
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: tickets_index_0; Type: INDEX; Schema: public; Owner: user
--

CREATE UNIQUE INDEX tickets_index_0 ON public.tickets USING btree (event_id, seat_id);


--
-- Name: events events_location_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.events
    ADD CONSTRAINT events_location_id_fkey FOREIGN KEY (location_id) REFERENCES public.locations(id);


--
-- Name: events events_type_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.events
    ADD CONSTRAINT events_type_id_fkey FOREIGN KEY (type_id) REFERENCES public.event_types(id);


--
-- Name: order_items fk_order_items_ticket; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT fk_order_items_ticket FOREIGN KEY (ticket_id) REFERENCES public.tickets(id);


--
-- Name: payments fk_payment_order; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.payments
    ADD CONSTRAINT fk_payment_order FOREIGN KEY (order_id) REFERENCES public.orders(id) ON DELETE CASCADE;


--
-- Name: order_items order_items_order_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT order_items_order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(id);


--
-- Name: orders orders_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.order_statuses(id);


--
-- Name: orders orders_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id);


--
-- Name: payments payments_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.payments
    ADD CONSTRAINT payments_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.payment_statuses(id);


--
-- Name: seats seats_location_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.seats
    ADD CONSTRAINT seats_location_id_fkey FOREIGN KEY (location_id) REFERENCES public.locations(id);


--
-- Name: tickets tickets_event_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.tickets
    ADD CONSTRAINT tickets_event_id_fkey FOREIGN KEY (event_id) REFERENCES public.events(id);


--
-- Name: tickets tickets_seat_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.tickets
    ADD CONSTRAINT tickets_seat_id_fkey FOREIGN KEY (seat_id) REFERENCES public.seats(id);


--
-- Name: tickets tickets_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.tickets
    ADD CONSTRAINT tickets_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.ticket_statuses(id);


--
-- Name: users users_account_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_account_status_id_fkey FOREIGN KEY (account_status_id) REFERENCES public.account_statuses(id);


--
-- Name: users users_role_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: user
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_role_id_fkey FOREIGN KEY (role_id) REFERENCES public.roles(id);


--
-- PostgreSQL database dump complete
--

