using System;
using System.Threading.Tasks;
using Infra.Database.Model;

namespace Infra.Database
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Action<MsgContext> callback);
    }
}