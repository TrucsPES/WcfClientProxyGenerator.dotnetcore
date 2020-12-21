using System.ServiceModel;

namespace WcfClientProxyGenerator.Tests.Infrastructure
{
    [ServiceContract]
    public interface IChildService : ITestService
    {
        [OperationContract]
        string ChildMethod(string input);
    }
}
