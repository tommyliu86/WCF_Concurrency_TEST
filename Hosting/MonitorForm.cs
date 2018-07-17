using System;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using Artech.ConcurrentServiceInvocation.Service;
using Artech.ConcurrentServiceInvocation.Service.Interface;
namespace Artech.ConcurrentServiceInvocation.Hosting
{
    public partial class MonitorForm : Form
    {
        private SynchronizationContext _syncContext;
        private ServiceHost _serviceHost;

        public MonitorForm()
        {
            InitializeComponent();
        }

        private void MonitorForm_Load(object sender, EventArgs e)
        {
            string header = string.Format("{0, -13}{1, -22}{2,-20}{3,-20}", "Client", "Time", "Thread", "Event");
            this.listBoxExecutionProgress.Items.Add(header);
            _syncContext = SynchronizationContext.Current;
            EventMonitor.MonitoringNotificationSended += ReceiveMonitoringNotification;
            this.Disposed += delegate
            {
                EventMonitor.MonitoringNotificationSended -= ReceiveMonitoringNotification;
                _serviceHost.Close();
            };
            _serviceHost = new ServiceHost(typeof(CalculatorService));
            _serviceHost.Open();
        }

        public void ReceiveMonitoringNotification(object sender, MonitorEventArgs args)
        {
            string message = string.Format("{0, -15}{1, -20}{2,-24}{3,-20}", args.ClientId, args.EventTime.ToLongTimeString(), args.ThreadId, args.EventType);
            _syncContext.Post(state => this.listBoxExecutionProgress.Items.Add(message), null);
        }
    }
}




 


