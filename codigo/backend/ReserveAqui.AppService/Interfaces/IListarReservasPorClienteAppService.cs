﻿using ReserveAqui.Domain.DTO;
using System.Collections.Generic;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IListarReservasPorClienteAppService
    {
        List<ReservaDto> Listar(string login);
    }
}
