#region #mainwindow
using System;
using System.Data.Entity;
using System.Windows;
using DevExpress.XtraScheduler;

namespace DXSchedulerEFTest
{   
    public partial class MainWindow : Window
    {
        private SchedulerContext context = new SchedulerContext();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Database.SetInitializer(new SchedulerContextSeedInilializer());
            context.Database.Initialize(false);
            context.EFAppointments.Load();
            context.EFResources.Load();

            this.schedulerControl1.Storage.AppointmentStorage.DataContext = context.EFAppointments.Local;
            this.schedulerControl1.Storage.ResourceStorage.DataContext = context.EFResources.Local;

            this.schedulerControl1.Storage.AppointmentsInserted +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);
            this.schedulerControl1.Storage.AppointmentsChanged +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);
            this.schedulerControl1.Storage.AppointmentsDeleted +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);

            schedulerControl1.Storage.ResourceStorage.ColorSaving = ColorSavingType.ArgbColor;
            schedulerControl1.Storage.AppointmentStorage.ResourceSharing = true;
            schedulerControl1.GroupType = SchedulerGroupType.Resource;
            schedulerControl1.Start = DateTime.Now.Date;
        }

        void Storage_AppointmentsModified(object sender, PersistentObjectsEventArgs e)
        {
            context.SaveChanges();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            this.context.Dispose();
        }
    }
}
#endregion #mainwindow