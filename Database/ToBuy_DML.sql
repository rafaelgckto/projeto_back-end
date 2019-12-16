USE ToBuy;

INSERT INTO TipoUsuario	(permissaoTipoUsuario)
VALUES					('Administrador'),
						('Funcionário');

INSERT INTO Usuario		(nomeUsuario, telefoneUsuario, emailUsuario, senhaUsuario, statusUsuario, FK_idTipoUsuario)
VALUES					('ADM Rafael', '(11)12345-1234', 'eol4us@gmail.com', '12345', 1, 1),
						('Lucas Silva', '(11)98765-4321', 'lucas.silva@thoughtworks.com', '54321', 1, 2),
						('Carlos Gonçalves', '(11)90123-3456', 'carlos.gonçalves@thoughtworks.com', '12345', 0, 2);

INSERT INTO Fabricante	(nomeFabricante)
VALUES					('Dell'),
						('Mac'),
						('Outros');

/*INSERT INTO Ficha		(sist_opFicha, processadorFicha, placa_videoFicha, audioFicha, telaFicha, memoriaFicha, armazenamentoFicha)
VALUES					('Windows 10', 'Intel Core', 'Intel® UHD Graphics 620', 'Waves MaxxAudio® Pro', '15"', '4GB', '1T'),
						('Mac OS Sierra', 'Intel Core i5', 'Intel HD Graphics 6000', 'Não Especificado', '13.3"', '8GB', '128GB'),
						('-', '-', '-', '-', '23,8"', '-','-');*/

INSERT INTO Produto		(nomeProduto, modeloProduto, FK_idFabricante, dt_lancProduto, FK_idUsuario)
VALUES					('Dell Inspiron', 'I15-3583-A2YP', 1, '01/10/2017', 1),
						('MacBook Air', 'MQD32BZ/A', 2, '01/10/2017', 1),
						('Monitor Dell LED Full HD', 'SE2416H', 3, '05/10/2018', 1);

INSERT INTO Conservacao	(estadoConservacao)
VALUES					('Ótimo'),
						('Bom'),
						('Ruim');

INSERT INTO Anuncio		(FK_idProduto, FK_idConservacao, precoAnuncio, dt_finalAnuncio, descAnuncio, statusAnuncio)
VALUES					(1, 1, '2200.00', '01/10/2019', 'Lorem top aqui!', 1),
						(2, 1, '4000', '01/10/2019', 'Outro Lorem Top Aqui!', 1),
						(3, 2, '600', '05/10/2019', 'E o último Lorem aqui!', 0);

INSERT INTO Imagem		(imagem, FK_idAnuncio)
VALUES					('C:\', 1),
						('C:\', 2),
						('C:\', 3);

INSERT INTO Interesse	(dataInteresse, FK_idUsuario, FK_idAnuncio)
VALUES					('11/12/2019', 2, 1),
						('10/12/2019', 2, 2),
						('10/12/2019', 3, 1);