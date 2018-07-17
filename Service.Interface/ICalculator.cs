using System.ServiceModel;
namespace Artech.ConcurrentServiceInvocation.Service.Interface
{
    [ServiceContract(Namespace = "http://www.artech.com/", SessionMode = SessionMode.Allowed)]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double x, double y);
    }
}
