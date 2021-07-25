using System.Threading.Tasks;

namespace App.Proxies
{
    public interface IProxy<TObject>
    {
        TObject Request();
        Task<TObject> RequestAsync();
    }
}