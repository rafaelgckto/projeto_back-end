USE ToBuy;

SELECT * FROM TipoUsuario;
SELECT * FROM Usuario;
SELECT * FROM Fabricante;
/*SELECT * FROM Ficha;*/
SELECT * FROM Produto;
SELECT * FROM Conservacao;
SELECT * FROM Anuncio;
SELECT * FROM Imagem;
SELECT * FROM Interesse;

delete from Usuario where idUsuario = 1003;

SELECT [u].[idUsuario], [u].[emailUsuario], [u].[nomeUsuario], [u].[senhaUsuario], [u].[statusUsuario], [u].[telefoneUsuario], [t].[permissaoTipoUsuario]
FROM [Usuario] AS [u]
JOIN [TipoUsuario] AS [t] ON [u].[FK_idTipoUsuario] = [t].[idTipoUsuario]
