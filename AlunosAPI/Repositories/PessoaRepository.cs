using AlunosAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace AlunosAPI.Repositories
{
    public class PessoaRepository
    {
        private readonly string _connectionString;

        public PessoaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection => 
            new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Pessoa>> ListarTodasPessoas()
        {
            var sql = "SELECT * FROM tb_pessoas";

            using (var conn = Connection)
            {
                return await conn.QueryAsync<Pessoa>(sql);
            }
        }
        public async Task<Pessoa> BuscarPorId(int id)
        {
            var sql = "SELECT * FROM tb_pessoas WHERE Id = @Id";

            using (var conn = Connection)
            {
                return await conn.QueryFirstOrDefaultAsync<Pessoa>(sql, new { Id = id });
            }
        }

        public async Task<int> DeletarPorId(int id)
        {
            var sql = "DELETE FROM tb_pessoas WHERE Id = @Id";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, new { Id = id });
            }
        }
        public async Task<int> Salvar(Pessoa dados)
        {
            var sql = "insert into tb_pessoas(Nome, Idade, Email) values(@Nome, @Idade, @Email)";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, new { Nome  = dados.Nome, Email = dados.Email, Idade = dados.Idade });
            }
        }

        public async Task<int> Atualizar(Pessoa dados)
        {
            var sql = "update tb_pessoas  set Nome = @Nome , Idade = @Idade, Email = @Email where  Id=@id";

            using (var conn = Connection)
            {
                return await conn.ExecuteAsync(sql, dados);
            }
        }

    }
}
