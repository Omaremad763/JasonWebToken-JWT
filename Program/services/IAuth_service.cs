using jwt__dev_Creed.model;

namespace jwt__dev_Creed.services
{
    public interface IAuth_service
    {

        Task<Auth_model> register_async (register_model model);

        Task<Auth_model> Gettoken_async(TokenRequest_model model);
    }
}
