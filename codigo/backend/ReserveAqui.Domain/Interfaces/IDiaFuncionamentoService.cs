using ReserveAqui.Domain.DTO;
using System.Collections.Generic;

namespace ReserveAqui.Domain.Interfaces
{
    public interface IDiaFuncionamentoService
    {
        List<DiaFuncionamentoDto> Listar();
        void AtualizarDiasFuncionamento(List<DiaFuncionamentoDto> diasFuncionamento);
        bool ValidarFuncionamentoDia(int diaSemana);
    }
}
