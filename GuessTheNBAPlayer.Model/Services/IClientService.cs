namespace GuessTheNBAPlayer.Model.Services
{
    public interface IClientService
    {
        Response SendRequest<Response>(string url) where Response : class;
    }
}