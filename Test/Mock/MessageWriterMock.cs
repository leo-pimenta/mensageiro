using App.Services;
using Moq;

namespace Test.Mock
{
    public class MessageWriterMock : IObjectMock<IMessageWriter>
    {
        public IMessageWriter Create()
        {
            return new Mock<IMessageWriter>().Object;
        }
    }
}