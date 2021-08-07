using System;
using System.Threading.Tasks;
using App.Proxies;

namespace App.Services
{
    public interface IMessageReader
    {
        void Subscribe(string userIndentifier);
        void Unsubscribe(string userIndentifier);
        void Start();
        void Stop();
    }
}