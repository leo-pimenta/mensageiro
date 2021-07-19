using System;
using System.Threading.Tasks;
using Infra.Cryptography;
using Infra.Database.Model;
using Microsoft.Extensions.Configuration;

namespace Infra.Database
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration Configuration;
        private readonly IEncryptor Encryptor;

        public EntityFrameworkUnitOfWork(IConfiguration configuration, IEncryptor encryptor)
        {
            this.Configuration = configuration;
            this.Encryptor = encryptor;
        }

        public async Task ExecuteAsync(Action<MsgContext> callback)
        {
            using (var context = CreateContext())
            {
                callback(context);
                await context.SaveChangesAsync();
            }
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<MsgContext, Task<TResult>> callback)
        {
            using (var context = CreateContext())
            {
                TResult result = await callback(context);
                await context.SaveChangesAsync();
                return result;
            }
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<MsgContext, TResult> callback)
        {
            using (var context = CreateContext())
            {
                TResult result = callback(context);
                await context.SaveChangesAsync();
                return result;
            }
        }

        private MsgContext CreateContext() => new MsgContext(this.Configuration, this.Encryptor);
    }
}