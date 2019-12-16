CREATE DATABASE ToBuy;

USE ToBuy;

CREATE TABLE TipoUsuario (
	idTipoUsuario	INT PRIMARY KEY IDENTITY(1,1),
	permissaoTipoUsuario	VARCHAR(20) DEFAULT('Funcionário') UNIQUE NOT NULL
);

CREATE TABLE Usuario (
	idUsuario	INT PRIMARY KEY IDENTITY(1,1),
	nomeUsuario	VARCHAR(50) UNIQUE NOT NULL,
	telefoneUsuario	VARCHAR(14),
	emailUsuario	VARCHAR(50) UNIQUE NOT NULL,
	senhaUsuario	VARCHAR(300) NOT NULL,
	statusUsuario	BIT DEFAULT(0) NOT NULL,
	FK_idTipoUsuario	INT FOREIGN KEY REFERENCES TipoUsuario(idTipoUsuario)
);

CREATE TABLE Fabricante (
	idFabricante	INT PRIMARY KEY IDENTITY(1,1),
	nomeFabricante	VARCHAR(50) NOT NULL
);

/*CREATE TABLE Ficha (
	idFicha	INT PRIMARY KEY IDENTITY(1,1),
	sist_opFicha	VARCHAR(100) NOT NULL,
	processadorFicha	VARCHAR(100) NOT NULL,
	placa_videoFicha	VARCHAR(100) NOT NULL,
	audioFicha	VARCHAR(100) NOT NULL,
	telaFicha	VARCHAR(100) NOT NULL,
	memoriaFicha	VARCHAR(100) NOT NULL,
	armazenamentoFicha	VARCHAR(100) NOT NULL
);*/

CREATE TABLE Produto (
	idProduto	INT PRIMARY KEY IDENTITY(1,1),
	nomeProduto	VARCHAR(60) NOT NULL,
	modeloProduto	VARCHAR(60) NOT NULL,
	FK_idFabricante	INT FOREIGN KEY REFERENCES Fabricante(idFabricante),
	dt_lancProduto	DATE NOT NULL,
	/*FK_idFicha	INT FOREIGN KEY REFERENCES Ficha(idFicha),*/
	FK_idUsuario	INT FOREIGN KEY REFERENCES Usuario(idUsuario)
);

CREATE TABLE Conservacao (
	idConservacao	INT PRIMARY KEY IDENTITY(1,1),
	estadoConservacao	VARCHAR(10) NOT NULL
);

CREATE TABLE Anuncio (
	idAnuncio	INT PRIMARY KEY IDENTITY(1,1),
	FK_idProduto	INT FOREIGN KEY REFERENCES Produto(idProduto),
	FK_idConservacao	INT FOREIGN KEY REFERENCES Conservacao(idConservacao),
	precoAnuncio	DECIMAL(7,2) NOT NULL,
	dt_finalAnuncio	DATE NOT NULL,
	descAnuncio	VARCHAR(200),
	statusAnuncio	BIT DEFAULT(0) NOT NULL
);

CREATE TABLE Imagem (
	IdImagem	INT PRIMARY KEY IDENTITY(1,1),
	imagem	VARCHAR(300),
	FK_idAnuncio	INT FOREIGN KEY REFERENCES Anuncio(idAnuncio)
);

CREATE TABLE Interesse (
	IdInteresse	INT PRIMARY KEY IDENTITY(1,1),
	dataInteresse	DATE,
	FK_idUsuario	INT FOREIGN KEY REFERENCES Usuario(idUsuario),
	FK_idAnuncio	INT FOREIGN KEY REFERENCES Anuncio(idAnuncio)
);