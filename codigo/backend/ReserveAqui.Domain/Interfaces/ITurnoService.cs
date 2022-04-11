using ReserveAqui.Domain.DTO;
using System.Collections.Generic;

namespace ReserveAqui.Domain.Interfaces
{
    public interface ITurnoService
    {
        List<TurnoDto> Listar();
        List<TurnoDto> ListarPorIds(List<int> ids);
        void Inserir(TurnoDto turno);
        void Atualizar(TurnoDto turno);
        void Deletar(int id);
    }
}
