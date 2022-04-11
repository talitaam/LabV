using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Shared.Validacao;
using System.Collections.Generic;

namespace ReserveAqui.Domain.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepositorio statusRepositorio;

        public StatusService(IStatusRepositorio statusRepositorio)
        {
            this.statusRepositorio = statusRepositorio;
        }

        public List<StatusDto> Listar()
        {
            return statusRepositorio.Listar().MapearMultiplosStatus();
        }

        public StatusDto ObterPorId(int id)
        {
            Validate.That(id).IsGreaterThan(0);

            return statusRepositorio.ObterPorId(id).MapearStatus();
        }
    }
}
