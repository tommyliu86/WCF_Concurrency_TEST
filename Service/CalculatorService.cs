using System.ServiceModel;
using System.Threading;
using Artech.ConcurrentServiceInvocation.Service.Interface;
namespace Artech.ConcurrentServiceInvocation.Service
{
[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,ConcurrencyMode =ConcurrencyMode.Multiple)]
public class CalculatorService : ICalculator
{
    //省略成员
    public double Add(double x, double y)
    {
        EventMonitor.Send(EventType.StartExecute);
        Thread.Sleep(5000);
        double result = x + y;
        EventMonitor.Send(EventType.EndExecute);
        return result;
    }
}
}