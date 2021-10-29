using System.Threading.Tasks;
using Infra.Database.Model;

namespace App.Services
{
    public abstract class DbService
    {
        private readonly MsgContext Context;

        protected DbService(MsgContext context) => this.Context = context;

        protected async Task SaveDbChangesAsync() => await this.Context.SaveChangesAsync();
    }
}