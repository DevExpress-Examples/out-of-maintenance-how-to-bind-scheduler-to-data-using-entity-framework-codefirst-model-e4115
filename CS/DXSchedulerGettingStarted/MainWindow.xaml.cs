#region #usings
using System.Windows;
using System.Data.Entity;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Ribbon;
#endregion #usings
namespace DXSchedulerGettingStarted
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXRibbonWindow
    {   
        public MainWindow()
        {
            InitializeComponent();
        }
        #region #window_loaded
        MySchedulerModel context = new MySchedulerModel();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.MyAppointments.Load();
            context.MyResources.Load();

            this.scheduler.Storage.AppointmentStorage.DataContext = context.MyAppointments.Local;
            this.scheduler.Storage.ResourceStorage.DataContext = context.MyResources.Local;


            this.scheduler.Storage.AppointmentsInserted +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);
            this.scheduler.Storage.AppointmentsChanged +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);
            this.scheduler.Storage.AppointmentsDeleted +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);
        }
        #endregion #window_loaded
        #region #storage_appointmentsmodified
        void Storage_AppointmentsModified(object sender, PersistentObjectsEventArgs e)
        {
            context.SaveChanges();
        }
        #endregion #storage_appointmentsmodified
        #region #onclosing
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            this.context.Dispose();
        }
        #endregion #onclosing
    }
}
