using ReserveAqui.Infra.Data.Entities;
using System;
using System.Collections.Generic;

namespace ReserveAqui.Infra.Data.Interfaces
{
    public interface IMesaRepositorio
    {
        IEnumerable<Mesa> Listar();
        IEnumerable<Mesa> ListarPorIds(List<int> idsMesas);
        IEnumerable<Mesa> ListarMesasDisponiveisPorTurno(DateTime dataReserva, int idTurno, int? idReservaDesconsiderada = null);
        Mesa ObterPorId(int id);
        void Inserir(Mesa mesa);
        void Atualizar(Mesa mesa);
        void Deletar(int id);
    }
}
