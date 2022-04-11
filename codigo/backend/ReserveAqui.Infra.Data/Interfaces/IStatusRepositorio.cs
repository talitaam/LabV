using ReserveAqui.Infra.Data.Entities;
using System.Collections.Generic;

namespace ReserveAqui.Infra.Data.Interfaces
{
    public interface IStatusRepositorio
    {
        List<Status> Listar();
        Status ObterPorId(int id);
    }
}
