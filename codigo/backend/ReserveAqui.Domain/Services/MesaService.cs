using ReserveAqui.Domain.DTO;
using ReserveAqui.Domain.Interfaces;
using ReserveAqui.Infra.Data.Entities;
using ReserveAqui.Infra.Data.Interfaces;
using ReserveAqui.Shared;
using ReserveAqui.Shared.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReserveAqui.Domain.Services
{
    public class MesaService : IMesaService
    {
        private readonly IMesaRepositorio mesaRepositorio;

        public MesaService(IMesaRepositorio mesaRepositorio)
        {
            this.mesaRepositorio = mesaRepositorio;
        }

        public MesaDto ObterPorId(int id)
        {
            Validate.That(id).IsGreaterThan(0);

            return mesaRepositorio.ObterPorId(id).MapearMesa();
        }
        public List<MesaDto> Listar()
        {
            return mesaRepositorio.Listar().MapearMesas();
        }

        public List<MesaDto> ListarPorIds(List<int> idsMesas)
        {
            Validate.That(idsMesas).IsNotNullOrEmpty();

            return mesaRepositorio.ListarPorIds(idsMesas).MapearMesas();
        }

        public List<MesaDto> ListarMesasDisponiveisPorTurnos(DateTime dataReserva, List<int> idsTurnos, int? idReservaDesconsiderada = null)
        {
            Validate.That(dataReserva.Date).IsGreaterThanOrEqualTo(DateTime.Now.Date);
            Validate.That(idsTurnos).IsNotNullOrEmpty();

            List<List<MesaDto>> agrupamentoMesasLivresPorTurnos = new List<List<MesaDto>>();
            List<MesaDto> mesasLivresPorTurnos = new List<MesaDto>();
            List<List<int>> idsMesasLivresPorTurnos = new List<List<int>>();

            foreach (var idTurno in idsTurnos)
            {
                var mesas = mesaRepositorio.ListarMesasDisponiveisPorTurno(dataReserva, idTurno, idReservaDesconsiderada).MapearMesas();
    
                if(mesas != null && mesas.Any())
                {
                    agrupamentoMesasLivresPorTurnos.Add(mesas);
                    mesasLivresPorTurnos.AddRange(mesas);
                }
            }

            if (!agrupamentoMesasLivresPorTurnos.Any())
                return new List<MesaDto>();

            foreach (var item in agrupamentoMesasLivresPorTurnos)
            {
                idsMesasLivresPorTurnos.Add(item.Select(i => i.Id).ToList());
            }

            var idsMesasLivres = idsMesasLivresPorTurnos
                .Skip(1)
                .Aggregate(
                    new HashSet<int>(idsMesasLivresPorTurnos.First()),
                    (h, e) => { h.IntersectWith(e); return h; }
                );
            
            return mesasLivresPorTurnos.Where(x => idsMesasLivres.Contains(x.Id)).GroupBy(x => x.Id)
                                  .Select(g => g.First()).ToList();
        }

        public void Inserir(MesaDto mesa)
        {
            Validate.That(mesa).IsNotNull();
            Validate.That(mesa.QuantCadeiras).IsNotNull();
            Validate.That(mesa.NomeMesa).IsNotNullOrWhiteSpace();
            

            mesaRepositorio.Inserir(ConverterParaEntidade(mesa));
        }

        public void Atualizar(MesaDto mesa)
        {
            Validate.That(mesa).IsNotNull();
            Validate.That(mesa.QuantCadeiras).IsNotNull();
            Validate.That(mesa.NomeMesa).IsNotNullOrWhiteSpace();


            var turnoObtido = ObterPorId(mesa.Id);
            Validate.That(turnoObtido).IsNotNull(Mensagens.ERRO_MESA_NAO_ENCONTRADA);

            mesaRepositorio.Atualizar(ConverterParaEntidade(mesa));
        }

        public void Deletar(int id)
        {
            var turnoObtido = ObterPorId(id);
            Validate.That(turnoObtido).IsNotNull(Mensagens.ERRO_MESA_NAO_ENCONTRADA);

            mesaRepositorio.Deletar(id);
        }

        private Mesa ConverterParaEntidade(MesaDto mesa)
        {
            return new Mesa()
            {
                Id = mesa.Id,
                QuantCadeiras = mesa.QuantCadeiras,
                NomeMesa = mesa.NomeMesa
            };
        }
    }
}
