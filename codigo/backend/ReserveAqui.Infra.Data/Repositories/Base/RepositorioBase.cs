using Dapper;
using ReserveAqui.Infra.Data.Contexts.Interfaces;
using ReserveAqui.Infra.Data.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReserveAqui.Infra.Data.Repositories.Base
{
    public abstract class RepositorioBase
    {
        public ITratamentoSql TratamentoSql { get; set; }
    }

    public abstract class RepositorioBase<TEntity> : RepositorioBase
    {
        protected RepositorioBase(IDbContext contexto)
        {
            this.Contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public IDbContext Contexto { get; protected set; }

        public List<TEntity> Listar(string sql, object parametros)
        {
            return TrimStrings(this.Query<TEntity>(sql, parametros)).ToList();
        }

        public TEntity Obter(string sql, object parametros)
        {
            return TrimStrings(this.Query<TEntity>(sql, parametros)).FirstOrDefault();
        }

        public IEnumerable<T> Query<T>(string sql, object parametros)
        {
            if (TratamentoSql != null)
            {
                (sql, parametros) = TratamentoSql.Executar(sql, (DynamicParameters)parametros);
            }

            return Dapper.SqlMapper.Query<T>(Contexto.Conexao, sql, parametros, Contexto.Transacao, true, null, null);
        }

        public int Execute(string sql, object parametros)
        {
            if (TratamentoSql != null)
            {
                (sql, parametros) = TratamentoSql.Executar(sql, (DynamicParameters)parametros);
            }

            return Dapper.SqlMapper.Execute(Contexto.Conexao, sql, parametros, Contexto.Transacao, null, null);
        }

        private static IEnumerable<T> TrimStrings<T>(IEnumerable<T> objetos)
        {
            var propriedadesTipoString = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.PropertyType == typeof(string) && x.CanRead && x.CanWrite);

            foreach (PropertyInfo prop in propriedadesTipoString)
            {
                foreach (T obj in objetos)
                {
                    if (obj != null)
                    {
                        var valor = (string)prop.GetValue(obj);
                        var valorTrim = SafeTrim(valor);
                        prop.SetValue(obj, valorTrim);
                    }
                }
            }

            return objetos;
        }

        private static string SafeTrim(string original)
        {
            return original?.Trim();
        }
    }
}
