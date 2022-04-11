using ReserveAqui.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IAtualizarClienteAppService
    {
        void Atualizar(ClienteDto cliente);
    }
}
