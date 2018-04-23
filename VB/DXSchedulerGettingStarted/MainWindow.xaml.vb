#Region "#usings"
Imports System.Windows
Imports System.Data.Entity
Imports DevExpress.XtraScheduler
Imports DevExpress.Xpf.Ribbon
#End Region ' #usings
Namespace DXSchedulerGettingStarted
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits DXRibbonWindow

        Public Sub New()
            InitializeComponent()
        End Sub
        #Region "#window_loaded"
        Private context As New MySchedulerModel()
        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            context.MyAppointments.Load()
            context.MyResources.Load()

            Me.scheduler.Storage.AppointmentStorage.DataContext = context.MyAppointments.Local
            Me.scheduler.Storage.ResourceStorage.DataContext = context.MyResources.Local


            AddHandler scheduler.Storage.AppointmentsInserted, AddressOf Storage_AppointmentsModified
            AddHandler scheduler.Storage.AppointmentsChanged, AddressOf Storage_AppointmentsModified
            AddHandler scheduler.Storage.AppointmentsDeleted, AddressOf Storage_AppointmentsModified
        End Sub
        #End Region ' #window_loaded
        #Region "#storage_appointmentsmodified"
        Private Sub Storage_AppointmentsModified(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
            context.SaveChanges()
        End Sub
        #End Region ' #storage_appointmentsmodified
        #Region "#onclosing"
        Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
            MyBase.OnClosing(e)
            Me.context.Dispose()
        End Sub
        #End Region ' #onclosing
    End Class
End Namespace
