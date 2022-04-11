using ReserveAqui.Domain.DTO;
using System;
using System.Collections.Generic;

namespace ReserveAqui.Domain.Interfaces
{
    public interface IMesaService
    {
        List<MesaDto> Listar();
        List<MesaDto> ListarPorIds(List<int> idsMesas);
        List<MesaDto> ListarMesasDisponiveisPorTurnos(DateTime dataReserva, List<int> idsTurnos, int? idReservaDesconsiderada = null);
        void Inserir(MesaDto mesa);
        void Atualizar(MesaDto mesa);
        void Deletar(int id);
    }
}
