using ReserveAqui.Domain.DTO.Autenticacao;

namespace ReserveAqui.AppService.Interfaces
{
    public interface IAutenticarUsuarioPorLoginSenhaAppService
    {
        UsuarioAutenticadoDto Autenticar(string login, string senha);
    }
}
