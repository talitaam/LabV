using System;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.WebApi.SignalRChat
{
    public class ChatCache
    {
        private static readonly List<Cliente> clientes = new List<Cliente>();

        private static readonly Dictionary<int, BuscaMesas>
            conversasAtivasPorCodigoCliente = new Dictionary<int, BuscaMesas>();

        public void RegistrarClienteAtivo(int codigoCliente, string ConnectionId)
        {
            var cliente = clientes.FirstOrDefault(a => a.IdCliente == codigoCliente);

            if (cliente == null)
            {
                cliente = new Cliente(codigoCliente, ConnectionId);
                clientes.Add(cliente);
            }
        }

        public void AdicionarConversaAoAtentente(int codigoCliente, DateTime dataReserva, List<int> idsTurnos)
        {
            var novaConversa = new BuscaMesas()
            {
                CodigoCliente = codigoCliente,
                DataReserva = dataReserva,
                IdsTurnos = idsTurnos
            };

            conversasAtivasPorCodigoCliente.Add(codigoCliente, novaConversa);
        }

        public Cliente ObterClienteAtivo(int idCliente)
        {
            return clientes.FirstOrDefault(i => i.IdCliente == idCliente);
        }

        public Cliente ObterClienteAtivoPorConexao(string connectionId)
        {
            return clientes.FirstOrDefault(i => i.ConnectionId == connectionId);
        }

        public void RemoverClienteAtivo(string connectionId)
        {
            var cliente = clientes
                .FirstOrDefault(at => at.ConnectionId == connectionId);

            if (cliente == null)
                return;

            var conversaCliente = conversasAtivasPorCodigoCliente.Values
                .FirstOrDefault(c => c.CodigoCliente == cliente.IdCliente);

            if (conversaCliente != null)
                conversasAtivasPorCodigoCliente.Remove(cliente.IdCliente);

            clientes.Remove(cliente);
        }

        public BuscaMesas ObterConversaCliente(int idCliente)
        {
            return conversasAtivasPorCodigoCliente.Values
                .FirstOrDefault(c => c.CodigoCliente == idCliente);
        }


        #region Classes auxiliares

        public class Cliente
        {
            public int IdCliente { get; set; }
            public string ConnectionId { get; set; }

            public Cliente(int IdCliente, string ConnectionId)
            {
                this.IdCliente = IdCliente;
                this.ConnectionId = ConnectionId;
            }
        }

        public class BuscaMesas
        {
            public int CodigoCliente { get; set; }
            public DateTime DataReserva { get; set; }
            public List<int> IdsTurnos { get; set; }
        }

        #endregion
    }
}
