using SystemOfDrawingStudentForAnswering.ViewModels;

namespace SystemOfDrawingStudentForAnswering.Views;

public partial class ManageStudentsPage : ContentPage
{
    public ManageStudentsPage()
    {
        InitializeComponent();
        BindingContext = AllClasses.Instance;
    }
}