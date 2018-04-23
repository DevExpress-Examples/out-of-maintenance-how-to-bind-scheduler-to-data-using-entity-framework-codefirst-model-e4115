Imports Microsoft.VisualBasic
#Region "#mainwindow"
Imports System
Imports System.Data.Entity
Imports System.Windows
Imports DevExpress.XtraScheduler

Namespace DXSchedulerEFTest
	Partial Public Class MainWindow
		Inherits Window
		Private context As New SchedulerContext()

		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Database.SetInitializer(New SchedulerContextSeedInilializer())
			context.Database.Initialize(False)
			context.EFAppointments.Load()
			context.EFResources.Load()

			Me.schedulerControl1.Storage.AppointmentStorage.DataContext = context.EFAppointments.Local
			Me.schedulerControl1.Storage.ResourceStorage.DataContext = context.EFResources.Local

			AddHandler schedulerControl1.Storage.AppointmentsInserted, AddressOf Storage_AppointmentsModified
			AddHandler schedulerControl1.Storage.AppointmentsChanged, AddressOf Storage_AppointmentsModified
			AddHandler schedulerControl1.Storage.AppointmentsDeleted, AddressOf Storage_AppointmentsModified

			schedulerControl1.Storage.ResourceStorage.ColorSaving = ColorSavingType.ArgbColor
			schedulerControl1.Storage.AppointmentStorage.ResourceSharing = True
			schedulerControl1.GroupType = SchedulerGroupType.Resource
			schedulerControl1.Start = DateTime.Now.Date
		End Sub

		Private Sub Storage_AppointmentsModified(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
			context.SaveChanges()
		End Sub

		Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
			MyBase.OnClosing(e)
			Me.context.Dispose()
		End Sub
	End Class
End Namespace
#End Region ' #mainwindow