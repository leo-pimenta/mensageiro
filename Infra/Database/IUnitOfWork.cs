using System;
using System.Threading.Tasks;
using Infra.Database.Model;

namespace Infra.Database
{
    public interface IUnitOfWork
    {
        Task<TResult> ExecuteAsync<TResult>(Func<MsgContext, Task<TResult>> callback);
        Task ExecuteAsync(Func<MsgContext, Task> callback);
        TResult Execute<TResult>(Func<MsgContext, TResult> callback);
    }
}