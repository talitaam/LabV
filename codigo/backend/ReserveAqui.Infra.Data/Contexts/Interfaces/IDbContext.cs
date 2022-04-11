using System;
using System.Data;

namespace ReserveAqui.Infra.Data.Contexts.Interfaces
{
    public interface IDbContext : IDisposable
    {
        IDbConnection Conexao { get; }
        IDbTransaction Transacao { get; }

        void InicializarTransacao();
        void ConfirmarTransacao();        
        void DesfazerTransacao();
    }
}
