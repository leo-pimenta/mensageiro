namespace App
{
    public class ResponseFactory : IResponseFactory
    {
        public object Create(object data) => new { data };
        public object Create(string message) => new { message };
    }
}