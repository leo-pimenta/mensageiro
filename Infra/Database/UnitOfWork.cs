using System;
using System.Threading.Tasks;
using Infra.Cryptography;
using Infra.Database.Model;
using Microsoft.Extensions.Configuration;

namespace Infra.Database
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IConfiguration Configuration;
        private readonly IEncryptor Encryptor;
        private readonly MsgContext Context;

        public EntityFrameworkUnitOfWork(IConfiguration configuration, IEncryptor encryptor)
        {
            this.Configuration = configuration;
            this.Encryptor = encryptor;
            this.Context = CreateContext();
        }        

        public TResult Execute<TResult>(Func<MsgContext, TResult> callback)
        {
            TResult result = callback(this.Context);
            this.Context.SaveChanges();
            return result;
        }

        public async Task ExecuteAsync(Func<MsgContext, Task> callback)
        {
            await callback(this.Context);
            await this.Context.SaveChangesAsync();
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<MsgContext, Task<TResult>> callback)
        {
            TResult result = await callback(this.Context);
            await this.Context.SaveChangesAsync();
            return result;
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        private MsgContext CreateContext() => new MsgContext(this.Configuration, this.Encryptor);
    }
}