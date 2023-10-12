namespace GuessTheNBAPlayer.Model.Entities.Internal
{
    public class ServerResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ServerResult<T> : ServerResult
    {
        public T Data { get; set; }
    }
}