--Criação do Banco de Dados
CREATE DATABASE PIM_TERCEIRO_SEMESTRE;

USE PIM_TERCEIRO_SEMESTRE;

--CRIAÇÃO DA TABELA "Tipos de usuário"
CREATE TABLE TBtipos_usuario
(
id_tipo_usuario INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
tipo_usuario VARCHAR(50) NOT NULL
);


--INSERÇÃO DE DADOS NA TABELA "Tipos de usuário"
INSERT INTO TBtipos_usuario(tipo_usuario)
VALUES 
('Colaborador'),
('Técnico');

SELECT * FROM TBtipos_usuario;

--CRIAÇÃO DA TABELA "Usuários"
CREATE TABLE TBusuarios
(
id_usuario INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
FK_tipo_usuario INT NOT NULL,
nome VARCHAR(100) NOT NULL,
email VARCHAR(100) NOT NULL,
senha VARCHAR(100) NOT NULL

--ESTABELECENDO O RELACIONAMENTO COM A TABELA "Tipos de usuário"
CONSTRAINT FK_usuario_tipo
FOREIGN KEY(FK_tipo_usuario)
REFERENCES TBtipos_usuario(id_tipo_usuario)
);

--INSERÇÃO DE DADOS NA TABELA "Usuários"
INSERT INTO TBusuarios(FK_tipo_usuario,nome,email,senha)
VALUES
(1,'Rian','rian@teste.com','1234'),
(2,'Daniel','daniel@teste.com','1234');


SELECT * FROM TBusuarios;

--CRIAÇÃO DA TABELA "Relatórios"

CREATE TABLE TBrelatorios
(
id_relatorio INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
FK_usuario INT NOT NULL,
data_emissao DATETIME NOT NULL

--ESTABELECENDO O RELACIONAMENTO COM A TABELA "Usuários"
CONSTRAINT FK_usuario_emissor
FOREIGN KEY(FK_usuario)
REFERENCES TBusuarios(id_usuario)
);

--INSERÇÃO DE DADOS NA TABELA "Relatórios"
INSERT INTO TBrelatorios(FK_usuario,data_emissao)
VALUES
(2,GETDATE());


SELECT * FROM TBrelatorios;

--CRIAÇÃO DA TABELA "Níveis de prioridade"

CREATE TABLE TBniveis_prioridade
(
id_prioridade INT PRIMARY KEY IDENTITY (1,1) NOT NULL,
prioridade VARCHAR(50)NOT NULL
);

--INSERÇÃO DE DADOS NA TABELA "Níveis de prioridade"
INSERT INTO TBniveis_prioridade(prioridade)
VALUES
('Alta'),('Média'),('Baixa');

SELECT * FROM TBniveis_prioridade;



