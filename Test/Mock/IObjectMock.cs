namespace Test.Mock
{
    public interface IObjectMock<TObject>
    {
        TObject Create();
    }
}