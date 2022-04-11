using ReserveAqui.Infra.Data.Contexts.Interfaces;
using ReserveAqui.Shared.Exceptions;
using System;
using System.Data;

namespace ReserveAqui.Infra.Data.Contexts.Base
{
    public abstract class DbContextBase : IDbContext
    {
        public IDbConnection Conexao { get; protected set; }

        public IDbTransaction Transacao { get; private set; }

        private int TransacoesAbertas { get; set; }

        public void ConfirmarTransacao()
        {
            if (TransacoesAbertas == 0)
                throw new TransacaoInicializadaException();

            TransacoesAbertas--;

            if (TransacoesAbertas != 0)
                return;

            Transacao.Commit();
        }

        public void DesfazerTransacao()
        {
            TransacoesAbertas = 0;
            Transacao.Rollback();
            Transacao.Dispose();

            Transacao = null;
        }

        public void InicializarTransacao()
        {
            TransacoesAbertas++;

            if (TransacoesAbertas != 1)
                return;

            if (Conexao.State != ConnectionState.Open)
                Conexao.Open();

            Transacao = Conexao.BeginTransaction();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (Conexao != null)
            {
                if (Transacao != null)
                {
                    if (Conexao.State == ConnectionState.Open && TransacoesAbertas > 0)
                        Transacao.Rollback();

                    Transacao.Dispose();
                }

                Conexao.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
