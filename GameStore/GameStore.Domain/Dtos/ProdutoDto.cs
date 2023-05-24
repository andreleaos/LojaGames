﻿using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using GameStore.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Dtos
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
        public string Categoria { get; set; }
        public CategoriaProduto CategoriaProduto { get; set; }
        public string UrlImagem { get; set; }
        public ImagemProduto ImagemProduto { get; set; }

        public Produto ConvertToEntity()
        {
            CategoriaProduto = ConversorEnums.ConvertToEnum(this.Categoria);
            ImagemProduto = new ImagemProduto(this.UrlImagem);

            return new Produto
            {
                Id = this.Id,
                Descricao = this.Descricao,
                PrecoUnitario = this.PrecoUnitario,
                Categoria = CategoriaProduto,
                ImagemProduto = this.ImagemProduto
            };
        }
        public static Produto ConvertToEntity(ProdutoDto produto)
        {
            CategoriaProduto categoriaProduto = ConversorEnums.ConvertToEnum(produto.Categoria);
            ImagemProduto imagemProduto = new ImagemProduto(produto.UrlImagem);

            return new Produto
            {
                Id = produto.Id,
                Descricao = produto.Descricao,
                PrecoUnitario = produto.PrecoUnitario,
                Categoria = categoriaProduto,
                ImagemProduto = imagemProduto
            };
        }
        public static List<Produto> ConvertToEntity(List<ProdutoDto> produtosDto)
        {
            var produtos = new List<Produto>();

            foreach(ProdutoDto item in produtosDto)
            {
                produtos.Add(ConvertToEntity(item));
            }

            return produtos;
        }
    }
}
