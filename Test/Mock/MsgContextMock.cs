using System.Threading;
using Infra.Database.Model;
using Moq;

namespace Test.Mock
{
    public class MsgContextMock : IObjectMock<MsgContext>
    {
        public MsgContext Create()
        {
            var mock = new Mock<MsgContext>();

            mock
                .Setup(context => context.SaveChangesAsync(default(CancellationToken)))
                .Callback<CancellationToken>(_ => {});
            
            return mock.Object;
        }
    }
}