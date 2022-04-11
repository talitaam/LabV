using ReserveAqui.Infra.Data.Entities;
using System.Collections.Generic;

namespace ReserveAqui.Infra.Data.Interfaces
{
    public interface IDiaFuncionamentoRepositorio
    {
        IEnumerable<DiaFuncionamento> ListarDiasFuncionamento();
        void AtualizarDiaFuncionamento(int idDia, bool ativo);
        DiaFuncionamento ObterDiaFuncionamento(int diaSemana);
    }
}
