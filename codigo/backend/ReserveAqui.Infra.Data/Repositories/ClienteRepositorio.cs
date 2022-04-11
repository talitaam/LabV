using Dapper;
using ReserveAqui.Infra.Data.Contexts.Interfaces;
using ReserveAqui.Infra.Data.Entities;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Infra.Data.Repositories.Base;
using System;
using System.Data;

namespace ReserveAqui.Infra.Data.Repositories
{
    public class ClienteRepositorio : RepositorioBase<Cliente>, IClienteRepositorio
    {
        private const string SQL_OBTER_CLIENTE_POR_LOGIN = @"
            SELECT id_cliente AS IdCliente, 
                   login AS Login, 
                   nome AS Nome,
                   email AS Email,
    	           celular AS Celular
            FROM reserveaqui.cliente
            WHERE login = @login";

        private const string SQL_OBTER_CLIENTE_POR_LOGIN_SENHA = @"
            SELECT id_cliente AS IdCliente, 
                   login AS Login, 
                   nome AS Nome,
                   email AS Email,
    	           celular AS Celular
            FROM reserveaqui.cliente
            WHERE login = @login 
            AND senha = @senha";

        private const string SQL_OBTER_CLIENTE_POR_ID = @"
            SELECT id_cliente AS IdCliente, 
                   login AS Login, 
                   nome AS Nome,
                   email AS Email,
    	           celular AS Celular
            FROM reserveaqui.cliente
            WHERE id_cliente = @id_cliente";

        private const string SQL_OBTER_CLIENTE_POR_LOGIN_OU_EMAIL = @"
            SELECT id_cliente AS IdCliente, 
                   login AS Login, 
                   nome AS Nome,
                   email AS Email,
    	           celular AS Celular
            FROM reserveaqui.cliente
            WHERE login = @login OR
                  email = @email";

        private const string SQL_INSERIR_CLIENTE = @"
            INSERT INTO reserveaqui.cliente 
                (login, senha, nome, email, celular) 
            VALUES 
                (@login, @senha, @nome, @email, @celular)";

        private const string SQL_ATUALIZAR_CLIENTE = @"
            UPDATE reserveaqui.cliente 
            SET email = @email, nome = @nome, celular = @celular, senha = @senha
            WHERE login = @login";

        private const string SQL_DELETAR_CLIENTE = @"
            DELETE FROM reserveaqui.cliente WHERE LOGIN = @login";

        public ClienteRepositorio(IDbMySqlContext contexto) : base(contexto) { }

        public Cliente ObterPorId(int id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@id_cliente", id, DbType.Int32);

            return Obter(SQL_OBTER_CLIENTE_POR_ID, parametros);
        }

        public Cliente ObterPorLogin(string login)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@login", login, DbType.AnsiString);

            return Obter(SQL_OBTER_CLIENTE_POR_LOGIN, parametros);
        }

        public Cliente ObterPorLoginSenha(string login, string senha)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@login", login, DbType.AnsiString);
            parametros.Add("@senha", senha, DbType.AnsiString);

            return Obter(SQL_OBTER_CLIENTE_POR_LOGIN_SENHA, parametros);
        }

        public Cliente ObterPorLoginOuEmail(string login, string email)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@login", login, DbType.AnsiString);
            parametros.Add("@email", email, DbType.AnsiString);

            return Obter(SQL_OBTER_CLIENTE_POR_LOGIN_OU_EMAIL, parametros);
        }

        public void DeletarPorLogin(string login)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@login", login, DbType.AnsiString);

            Execute(SQL_DELETAR_CLIENTE, parametros);
        }

        public void InserirCliente(Cliente cliente)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@login", cliente.Login, DbType.AnsiString);
            parametros.Add("@senha", cliente.Senha, DbType.AnsiString);
            parametros.Add("@nome", cliente.Nome, DbType.AnsiString);
            parametros.Add("@email", cliente.Email, DbType.AnsiString);
            parametros.Add("@celular", cliente.Celular, DbType.AnsiString);

            Execute(SQL_INSERIR_CLIENTE, parametros);
        }

        public void AtualizarCliente(Cliente cliente)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@login", cliente.Login, DbType.AnsiString);
            parametros.Add("@senha", cliente.Senha, DbType.AnsiString);
            parametros.Add("@nome", cliente.Nome, DbType.AnsiString);
            parametros.Add("@email", cliente.Email, DbType.AnsiString);
            parametros.Add("@celular", cliente.Celular, DbType.AnsiString);

            Execute(SQL_ATUALIZAR_CLIENTE, parametros);
        }
    }
}
