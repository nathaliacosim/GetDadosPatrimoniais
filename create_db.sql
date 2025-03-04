-- Criando a tabela de fornecedores
CREATE TABLE fornecedores (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(255) NOT NULL,
    cpf_cnpj VARCHAR(20) NOT NULL,
    tipo_valor VARCHAR(50), 
    tipo_descricao VARCHAR(255),
    situacao_valor VARCHAR(50), 
    situacao_descricao VARCHAR(255),
    data_inclusao DATE,
    optante_simples BOOLEAN,
    CONSTRAINT unique_cpf_cnpj UNIQUE (cpf_cnpj)
);

-- Criando a tabela de telefones
CREATE TABLE telefones (
    id SERIAL PRIMARY KEY,
    fornecedor_id INT REFERENCES fornecedores(id) ON DELETE CASCADE,
    numero VARCHAR(20),
    observacao VARCHAR(255),
    tipo VARCHAR(50),
    ordem INT
);

-- Criando a tabela de links
CREATE TABLE links (
    id SERIAL PRIMARY KEY,
    fornecedor_id INT REFERENCES fornecedores(id) ON DELETE CASCADE,
    rel VARCHAR(50),
    href VARCHAR(255)
);
