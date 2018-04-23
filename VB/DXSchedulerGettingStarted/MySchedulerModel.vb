Imports System.Linq
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System

Namespace DXSchedulerGettingStarted

    #Region "#myschedulermodel"
    Public Class MySchedulerModel
        Inherits DbContext

        ' Your context has been configured to use a 'MySchedulerModel' connection string from your application's 
        ' configuration file (App.config or Web.config). By default, this connection string targets the 
        ' 'DXSchedulerGettingStarted.MySchedulerModel' database on your LocalDb instance. 
        ' 
        ' If you wish to target a different database and/or database provider, modify the 'MySchedulerModel' 
        ' connection string in the application configuration file.
        Public Sub New()
            MyBase.New("name=MySchedulerModel")
            Database.SetInitializer(Of MySchedulerModel)(New SchedulerDBInitializer())
        End Sub

        ' Add a DbSet for each entity type that you want to include in your model. For more information 
        ' on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        Public Overridable Property MyAppointments() As DbSet(Of EFAppointment)
        Public Overridable Property MyResources() As DbSet(Of EFResource)
    End Class
    #End Region ' #myschedulermodel
    #Region "#model"
    Public Class EFAppointment
        <Key()> _
        Public Property UniqueID() As Integer
        <Required> _
        Public Property Type() As Integer
        <Required> _
        Public Property StartDate() As Date
        <Required> _
        Public Property EndDate() As Date
        Public Property AllDay() As Boolean
        Public Property Subject() As String
        Public Property Location() As String
        Public Property Description() As String
        Public Property Status() As Integer
        Public Property Label() As Integer
        Public Property ResourceIDs() As String
        Public Property ReminderInfo() As String
        Public Property RecurrenceInfo() As String
    End Class

    Public Class EFResource
        <Key()> _
        Public Property UniqueID() As Integer
        Public Property ResourceID() As Integer
        Public Property ResourceName() As String
        Public Property Color() As Integer
    End Class
    #End Region ' #model
    #Region "#SchedulerDBInitializer"
    Public Class SchedulerDBInitializer
        Inherits CreateDatabaseIfNotExists(Of MySchedulerModel)

        Protected Overrides Sub Seed(ByVal context As MySchedulerModel)
            Dim defaultResources As IList(Of EFResource) = New List(Of EFResource)()

            defaultResources.Add(New EFResource() With {.ResourceID = 1, .ResourceName = "Resource 1"})
            defaultResources.Add(New EFResource() With {.ResourceID = 2, .ResourceName = "Resource 2"})

            For Each res As EFResource In defaultResources
                context.MyResources.Add(res)
            Next res
            MyBase.Seed(context)
        End Sub
    End Class
        #End Region ' #SchedulerDBInitializer
End Namespace