namespace SystemOfDrawingStudentForAnswering.ViewsPartials;

public partial class NavigationBar : ContentView
{
    public NavigationBar()
    {
        InitializeComponent();
    }

    private async void AttendanceButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AttendancePage");
    }

    private async void DrawingButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//DrawingPage");
    }

    private async void ManageStudentsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ManageStudentsPage");
    }
}