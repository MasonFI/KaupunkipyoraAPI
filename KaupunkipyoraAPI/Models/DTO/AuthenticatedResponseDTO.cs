using KaupunkipyoraAPI.Contracts;

namespace KaupunkipyoraAPI.Models.DTO
{
    public class AuthenticatedResponseDTO: IAuthenticatedResponseDTO
    {
        public string Token { get; set; } = String.Empty;
    }
}
