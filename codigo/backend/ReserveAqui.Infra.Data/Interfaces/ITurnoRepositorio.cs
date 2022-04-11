using ReserveAqui.Infra.Data.Entities;
using System.Collections.Generic;

namespace ReserveAqui.Infra.Data.Interfaces
{
    public interface ITurnoRepositorio
    {
        IEnumerable<Turno> Listar();
        IEnumerable<Turno> ListarPorIds(List<int> idsTurnos);
        Turno ObterPorId(int id);
        void Inserir(Turno turno);
        void Atualizar(Turno turno);
        void Deletar(int id);
    }
}
