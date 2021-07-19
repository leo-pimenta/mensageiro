using System;
using System.Threading.Tasks;
using Infra.Database.Model;

namespace Infra.Database
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Action<MsgContext> callback);
        Task<TResult> ExecuteAsync<TResult>(Func<MsgContext, Task<TResult>> callback);
        Task<TResult> ExecuteAsync<TResult>(Func<MsgContext, TResult> callback);
    }
}