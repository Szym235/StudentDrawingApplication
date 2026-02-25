using SystemOfDrawingStudentForAnswering.ViewModels;

namespace SystemOfDrawingStudentForAnswering.Views;

public partial class DrawingPage : ContentPage
{
    public DrawingPage()
    {
        InitializeComponent();
        BindingContext = AllClasses.Instance;
    }
}