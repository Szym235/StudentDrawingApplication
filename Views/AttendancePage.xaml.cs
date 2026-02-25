namespace SystemOfDrawingStudentForAnswering.Views;

using SystemOfDrawingStudentForAnswering.ViewModels;
public partial class AttendancePage : ContentPage
{
    public AttendancePage()
    {
        InitializeComponent();
        BindingContext = AllClasses.Instance;
    }
}