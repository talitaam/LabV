using ReserveAqui.Infra.Data.Entities;

namespace ReserveAqui.Infra.Data.Interfaces
{
    public interface IServicoAutenticacaoRepository
    {
        DadosAutenticacao AutenticarPorIdSessao(string idSecao);
    }
}
