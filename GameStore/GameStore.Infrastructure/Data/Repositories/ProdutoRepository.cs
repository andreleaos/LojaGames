using Dapper;
using GameStore.Domain.ConfigApp;
using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using GameStore.Infrastructure.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Data.Repositories
{
    public class ProdutoRepository : SqlServerBaseRepository, IProdutoRepository
    {
        #region Atributos

        private readonly IConfigParameters _configParameters;
        private readonly IConfiguration _configuration;
        private static string _connectionString = string.Empty;
        private readonly ILogger<ProdutoRepository> _logger;

        #endregion

        #region Construtor

        public ProdutoRepository(
            IConfiguration configuration, 
            IConfigParameters configParameters,
            ILogger<ProdutoRepository> logger)
        {
            _configuration = configuration;
            _configParameters = configParameters;
            SetConnectionConfig();
            _logger = logger;
        }

        #endregion

        #region Métodos Públicos

        public void Create(Produto produto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = SqlManager.GetSql(TSqlQuery.CADASTRAR_IMAGEM);
                        var imagemId = connection.ExecuteScalar<int>(sql, produto, transaction);

                        produto.SetImagemProdutoId(imagemId);
                        produto.SetImagemId(imagemId);

                        sql = SqlManager.GetSql(TSqlQuery.CADASTRAR_PRODUTO);
                        connection.Execute(
                            sql,
                            new
                            {
                                Descricao = produto.GetDescricao(),
                                PrecoUnitario = produto.GetPrecoUnitario(),
                                CategoriaId = produto.CategoriaId,
                                ImagemId = produto.GetImagemId()
                            },
                            transaction
                        );

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        var msg = $"[Repository] - Erro ao realizar o cadastro. {ex.Message}";
                        _logger.LogInformation(msg);
                        throw new Exception(msg);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public bool Delete(int id)
        {
            if (id > 0)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = SqlManager.GetSql(TSqlQuery.PESQUISAR_PRODUTO);
                    var produtoPesquisado = connection.QueryFirstOrDefault<Produto>(sql, new { Id = id });

                    if (produtoPesquisado != null)
                    {
                        sql = SqlManager.GetSql(TSqlQuery.EXCLUIR_PRODUTO);
                        connection.Execute(sql, new { Id = produtoPesquisado.GetId() });

                        sql = SqlManager.GetSql(TSqlQuery.EXCLUIR_IMAGEM);
                        connection.Execute(sql, new { Id = produtoPesquisado.GetImagemId() });

                        return true;
                    }

                    connection.Close();
                }
            }

            return false;
        }
        public List<Produto> GetAll()
        {
            List<Produto> result = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = SqlManager.GetSql(TSqlQuery.LISTAR_PRODUTO);
                result = connection.Query<Produto>(sql).ToList();
            }
            foreach (var produto in result)
            {
                produto.SetUrl(produto.GetUrlBlobStorage());

                if (GeneralConfigApp.ENABLE_CONNECTION_LOCAL_DB)
                    CompleteData(produto);
            }

            if (!GeneralConfigApp.ENABLE_CONNECTION_LOCAL_DB)
                result = CleanerImageContentData(result);

            return result;
        }
        public Produto? GetById(int id)
        {
            Produto? produto = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = SqlManager.GetSql(TSqlQuery.PESQUISAR_PRODUTO);
                produto = connection.QueryFirstOrDefault<Produto>(sql, new { Id = id });
            }
            produto.SetUrlBlobStorage(produto.GetUrl());
            CompleteData(produto);
            return produto;
        }
        public void Update(Produto produto)
        {
            var produtoPesquisado = GetById(produto.GetId());

            if (produtoPesquisado != null)
            {
                produto.SetImagemId(produtoPesquisado.GetImagemId());
                produto.SetImagemProdutoId(produtoPesquisado.GetImagemId());

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var sql = SqlManager.GetSql(TSqlQuery.ATUALIZAR_PRODUTO);
                            connection.Execute(
                                sql, 

                                new { descricao = produto.GetDescricao(), precoUnitario = produto.GetPrecoUnitario(), categoriaId = produto.CategoriaId, id = produto.GetId() }, 

                                transaction
                            );

                            sql = SqlManager.GetSql(TSqlQuery.ATUALIZAR_IMAGEM);
                            connection.Execute(
                                sql, 
                                new { Url = produto.Url_path, UrlBlobStorage = produto.Url_Blob_Storage, id = produto.GetImagemId() },
                                transaction
                            );

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Erro ao realizar a atualizacao. {ex.Message}");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }

        public async Task<List<Catetoria>> GetAllCategoria()
        {
            List<Catetoria> result = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = SqlManager.GetSql(TSqlQuery.LISTAR_CATEGORIA);
                result = (List<Catetoria>?) await connection.QueryAsync<Catetoria>(sql);
            }

            return result;
        }

        #endregion

        #region Métodos Privados Auxiliares

        private List<Produto> CleanerImageContentData(List<Produto> produtos)
        {
            var result = new List<Produto>();

            foreach (var produto in produtos)
            {
                var item = CleanerImageContentData(produto);
                result.Add(item);
            }

            return result;
        }
        private Produto CleanerImageContentData(Produto produto)
        {
            var result = produto.CreateProductByObjectCopy();

            if (produto.GetImagemProduto() != null)
            {
                result.SetImagemProduto(produto.GetImagemProduto().CreateImageByObjectCopy());
                result.GetImagemProduto().CleanStreamData();
            }

            return result;
        }
        private void CompleteData(Produto produto)
        {
            if (produto != null)
                produto.SetImagemProduto(new ImagemProduto(produto.GetImagemId(), produto.GetUrl()));
        }
        private void SetConnectionConfig()
        {
            _configParameters.SetGeneralConfig();
            if (GeneralConfigApp.ENABLE_CONNECTION_LOCAL_DB)
                _connectionString = _configuration.GetConnectionString("lojaGamesDB_local");
            else
                _connectionString = _configuration.GetConnectionString("lojaGamesDB");
        }
        public void CreateTablesAndSeed(int retry = 0)
        {
            var retryForAvailability = retry;
            string pasta = string.Empty;

            List<string> listaTabelas = new List<string>();
            listaTabelas.Add("categoria");
            listaTabelas.Add("imagemProduto");
            listaTabelas.Add("produto");

            foreach (var nomeTabela in listaTabelas.OrderBy(x => x))
            {
                pasta = "DDL";

                try
                {
                    _logger.LogInformation("Criando a tabela " + nomeTabela);
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        string nomeArquivoSql = string.Empty;

                        switch (nomeTabela)
                        {
                            case "categoria":
                                nomeArquivoSql = "Create Table - 01 - categoria";
                                break;

                            case "imagemProduto":
                                nomeArquivoSql = "Create Table - 02 - imagemProduto";
                                break;

                            case "produto":
                                nomeArquivoSql = "Create Table - 03 - produto";
                                break;

                            default:
                                break;
                        }

                        int tabelaCount = 0;
                        string? query = string.Empty;
                        string queryVerificarSeTabelaExiste = "EXECUTE SP_SEL_VERIFICAR_TABELA_EXISTENTE_POR_NOME @tableName";

                        tabelaCount = connection.ExecuteScalar<int>(queryVerificarSeTabelaExiste, new { tableName = nomeTabela });
                        if (tabelaCount == 0)
                        {
                            query = ReadResource(pasta, nomeArquivoSql);
                            connection.Execute(query);
                            if (connection.ExecuteScalar<int>(queryVerificarSeTabelaExiste, new { tableName = nomeTabela }) > 0)
                                _logger.LogInformation("A tabela " + nomeTabela + " foi criada com sucesso!");
                        }
                        else
                            _logger.LogInformation("A tabela " + nomeTabela + " já existe e não precisa ser criada");

                        int resultInsert = 0;
                        if (nomeTabela.Equals("categoria"))
                        {
                            _logger.LogInformation("Criando o Seed da tabela " + nomeTabela);

                            pasta = "DML";

                            var fazerInsert = false;

                            string queryVerificarSeCategoriaPreenchida = "EXECUTE SP_SEL_VERIFICAR_TABELA_CATEGORIA_SEED";
                            var countPendente = connection.ExecuteScalar<int>(queryVerificarSeCategoriaPreenchida);

                            if (countPendente > 0)
                                fazerInsert = true;

                            if (countPendente == 0)
                            {
                                queryVerificarSeCategoriaPreenchida = "EXECUTE SP_SEL_COUNT_CATEGORIA";
                                var countExistente = connection.ExecuteScalar<int>(queryVerificarSeCategoriaPreenchida);
                                if (countExistente == 0)
                                    fazerInsert = true;
                            }

                            if (fazerInsert)
                            {
                                query = ReadResource(pasta, "Insert categoria");
                                resultInsert = connection.Execute(query);

                                if (resultInsert > 0)
                                    _logger.LogInformation("O Seed da tabela " + nomeTabela + " foi criado com sucesso.");
                            }
                            else
                                _logger.LogInformation("O Seed da tabela " + nomeTabela + " já foi criado anteriormente.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (retryForAvailability >= 10) throw;

                    retryForAvailability++;

                    if (pasta == "DDL")
                        _logger.LogError(ex.Message, "Ocorreu erro para criar a tabela " + nomeTabela);
                    else if (pasta == "DML")
                        _logger.LogError(ex.Message, "Ocorreu erro para fazer o Seed da tabela " + nomeTabela);

                    CreateTablesAndSeed(retryForAvailability);
                    throw;
                }
            }
        }
        public void CreateProceduresIniciais(int retry = 0)
        {
            var retryForAvailability = retry;
            string nomeProcedure = string.Empty;
            try
            {   
                List<string> NomesArquivos = new List<string>();

                NomesArquivos.Add("SP_SEL_VERIFICAR_PROCEDURE_EXISTENTE_POR_NOME");
                NomesArquivos.Add("SP_SEL_COUNT_CATEGORIA");
                NomesArquivos.Add("SP_SEL_VERIFICAR_TABELA_CATEGORIA_SEED");
                NomesArquivos.Add("SP_SEL_VERIFICAR_TABELA_EXISTENTE_POR_NOME");

                _logger.LogInformation("Validando a criação das procedures iniciais.");
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string nomeArquivoSql = string.Empty;
                    string pasta = "Procedures";

                    foreach (string nomeArquivo in NomesArquivos)
                    {
                        nomeProcedure = nomeArquivo;

                        _logger.LogInformation("Validando a criação da procedure " + nomeProcedure);
                        string queryVerificarSeProcedureExiste = "SELECT COUNT(*) FROM sys.procedures WHERE name = @nomeProcedure";
                        int procedureCount = connection.ExecuteScalar<int>(queryVerificarSeProcedureExiste, new { nomeProcedure });

                        if (procedureCount == 0)
                        {
                            var query = ReadResource(pasta, nomeArquivo);
                            connection.Execute(query);

                            procedureCount = connection.ExecuteScalar<int>(queryVerificarSeProcedureExiste, new { nomeProcedure });

                            if (procedureCount > 0)
                                _logger.LogInformation("A procedure " + nomeProcedure + " foi criada com sucesso!");
                        }
                        else
                            _logger.LogInformation("A procedure " + nomeProcedure + " já existe.");
                    }
                }
                _logger.LogInformation("Procedures iniciais concluídas.");
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                _logger.LogError(ex.Message, "Ocorreu erro para criar a procedure " + nomeProcedure);

                CreateProcedures(retryForAvailability);
                throw;
            }
        }
        public void CreateProcedures(int retry = 0)
        {
            var retryForAvailability = retry;
            string nomeProcedure = string.Empty;
            try
            {
                var proceduresValidadas = false;

                List<string> arquivos = new List<string>();
                arquivos.Add("SP_DEL_EXCLUIR_IMAGEM");
                arquivos.Add("SP_DEL_EXCLUIR_PRODUTO");
                arquivos.Add("SP_INS_CADASTRAR_IMAGEM");
                arquivos.Add("SP_INS_CADASTRAR_PRODUTO");
                arquivos.Add("SP_SEL_LISTAR_CATEGORIA");
                arquivos.Add("SP_SEL_LISTAR_PRODUTO");
                arquivos.Add("SP_SEL_PESQUISAR_IMAGEM_PELA_URL");
                arquivos.Add("SP_SEL_PESQUISAR_PRODUTO");
                arquivos.Add("SP_UPD_ATUALIZAR_IMAGEM");
                arquivos.Add("SP_UPD_ATUALIZAR_PRODUTO");

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string nomeArquivoSql = string.Empty;
                    string pasta = "Procedures";

                    foreach (string nomeArquivo in arquivos)
                    {
                        nomeProcedure = nomeArquivo;

                        _logger.LogInformation("Validando a criação da procedure " + nomeProcedure);
                        string queryVerificarSeProcedureExiste = "EXECUTE SP_SEL_VERIFICAR_PROCEDURE_EXISTENTE_POR_NOME @nomeProcedure";
                        int procedureCount = connection.ExecuteScalar<int>(queryVerificarSeProcedureExiste, new { nomeProcedure });

                        if(procedureCount == 0)
                        {
                            var query = ReadResource(pasta, nomeArquivo);
                            var result = connection.Execute(query);

                            procedureCount = connection.ExecuteScalar<int>(queryVerificarSeProcedureExiste, new { nomeProcedure });

                            if(procedureCount > 0)
                                _logger.LogInformation("A procedure " + nomeProcedure + " foi criada com sucesso!");
                        }
                        else
                            _logger.LogInformation("A procedure " + nomeProcedure + " já existe.");
                    }
                    proceduresValidadas = true;
                }

                if(!proceduresValidadas)
                    _logger.LogWarning("Não foi possível validar as procedures!");
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                _logger.LogError(ex.Message, "Ocorreu erro para criar a procedure " + nomeProcedure);

                CreateProcedures(retryForAvailability);
                throw;
            }
        }

        public void CreateDataBaseLocal(int retry = 0)
        {
            var retryForAvailability = retry;
            //_configParameters.SetGeneralConfig();
            if (GeneralConfigApp.ENABLE_CONNECTION_LOCAL_DB)
            {
                try
                {
                    _connectionString = _connectionString.Replace("Database=LojaGamesDB", "Database=master");
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        _logger.LogInformation("Verificando se o banco de dados existe.");
                        string queryVerificarSeBdExiste = "select IIF(DB_ID('lojaGamesDB') IS NULL, 0, 1)";
                        var bdExiste = connection.ExecuteScalar<bool>(queryVerificarSeBdExiste);

                        if (!bdExiste) 
                        {
                            var pasta = "DDL";
                            var nomeArquivo = "Create Database";
                            var query = ReadResource(pasta, nomeArquivo);
                            connection.Execute(query);

                            bdExiste = connection.ExecuteScalar<bool>(queryVerificarSeBdExiste);
                            if(bdExiste)
                                _logger.LogInformation("Banco de dados criado com sucesso!");
                        }
                        else
                            _logger.LogInformation("O banco de dados já existe.");
                    }
                }
                catch (Exception ex)
                {
                    if (retryForAvailability >= 10) throw;

                    retryForAvailability++;

                    _logger.LogError(ex.Message, "Ocorreu erro para criar o banco de dados.");

                    CreateProcedures(retryForAvailability);
                    throw;
                }
            }
        }

        #endregion
    }
}
