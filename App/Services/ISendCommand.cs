using System;
using System.Threading.Tasks;
using App.Proxies;

namespace App.Services
{
    public interface ISendCommand
    {
        Task SendAsync(Guid toUserIdentifier, MessageProxy messageProxy);
    }
}