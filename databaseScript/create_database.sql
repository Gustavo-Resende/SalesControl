-- SQL (PostgreSQL >= 15 recomendado)
-- Execute externamente (psql / pgAdmin). Habilita extensão para UUID.
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- Tabela de clientes
CREATE TABLE clients (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    name varchar(200) NOT NULL,
    email varchar(255) NOT NULL,
    phone varchar(50),
    created_at timestamptz NOT NULL DEFAULT now(),
    updated_at timestamptz NOT NULL DEFAULT now()
);

-- Índice único case-insensitive no email (expressão)
CREATE UNIQUE INDEX ux_clients_email ON clients ((lower(email)));

-- Tabela de produtos
CREATE TABLE products (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    name varchar(200) NOT NULL,
    description text,
    price numeric(18,4) NOT NULL CHECK (price > 0),
    stock integer NOT NULL CHECK (stock >= 0),
    created_at timestamptz NOT NULL DEFAULT now(),
    updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX ix_products_name ON products ((lower(name)));

-- Tabela de vendas (não armazenamos total aqui; calculamos via agregação nas consultas/relatórios)
CREATE TABLE sales (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    client_id uuid NOT NULL REFERENCES clients(id) ON DELETE RESTRICT,
    created_at timestamptz NOT NULL DEFAULT now(),
    updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX ix_sales_created_at ON sales (created_at);
CREATE INDEX ix_sales_client_id ON sales (client_id);

-- Itens da venda
CREATE TABLE sale_items (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    sale_id uuid NOT NULL REFERENCES sales(id) ON DELETE CASCADE,
    product_id uuid NOT NULL REFERENCES products(id) ON DELETE RESTRICT,
    quantity integer NOT NULL CHECK (quantity > 0),
    unit_price numeric(18,4) NOT NULL CHECK (unit_price > 0),
    -- coluna gerada para evitar inconsistência em consultas; ajustável conforme preferência
    line_total numeric(18,4) GENERATED ALWAYS AS (quantity * unit_price) STORED,
    created_at timestamptz NOT NULL DEFAULT now(),
    updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX ix_saleitems_sale_id ON sale_items (sale_id);
CREATE INDEX ix_saleitems_product_id ON sale_items (product_id);