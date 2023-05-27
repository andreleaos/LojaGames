
insert into categoria (descricao) values ('Console')
insert into categoria (descricao) values ('Game')
insert into categoria (descricao) values ('Acessorio')
insert into categoria (descricao) values ('Periferico')

select id, descricao from categoria
select id, descricao, precoUnitario, categoriaId, imagemId from produto
select id, url_path from imagemProduto
select id, url_path from imagemProduto where url_path = @url_path 

insert into imagemProduto (url_path) values (@url_path)

select id, url_path from imagemProduto

insert into imagemProduto (url_path) values ('C:\Repo\LojaGames\GameStore\GameStore.Infrastructure\Images\Fifa23.png')
insert into imagemProduto (url_path) values ('C:\Repo\LojaGames\GameStore\GameStore.Infrastructure\Images\f1_22_ps4.jpg')
insert into imagemProduto (url_path) values ('C:\Repo\LojaGames\GameStore\GameStore.Infrastructure\Images\mk_11_ps4.jpg')
insert into imagemProduto (url_path) values ('C:\Repo\LojaGames\GameStore\GameStore.Infrastructure\Images\sfv_ps4.jpg')

insert into produto (descricao, precoUnitario, categoriaId, imagemId) values (@descricao, @precoUnitario, @categoriaId, @imagemId)

insert into produto (descricao, precoUnitario, categoriaId, imagemId) values ('Fifa 23 PS4',79.90,2,1)
insert into produto (descricao, precoUnitario, categoriaId, imagemId) values ('F1 22 PS4','129.90',2,2)
insert into produto (descricao, precoUnitario, categoriaId, imagemId) values ('Mortal Kombat PS4', 109.90,2,3)
insert into produto (descricao, precoUnitario, categoriaId, imagemId) values ('Street Fighter V PS4', 89.90,2,4)

update produto set descricao = @descricao, precoUnitario = @precoUnitario where id = @id

delete from produto where id = @id

select p.id, p.descricao, precoUnitario, categoriaId, c.descricao 'categoria', imagemId, url_path 'Url' from produto p inner join imagemProduto ip on p.imagemId = ip.id inner join categoria c on p.categoriaId = c.id

select p.id, p.descricao, precoUnitario, categoriaId, c.descricao 'categoria', imagemId, url_path 'Url' from produto p inner join imagemProduto ip on p.imagemId = ip.id inner join categoria c on p.categoriaId = c.id
where p.id = @id
