namespace App
{
    public interface IResponseFactory
    {
        object Create(object data);
        object Create(string message);
    }
}