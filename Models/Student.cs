using CommunityToolkit.Mvvm.ComponentModel;

namespace SystemOfDrawingStudentForAnswering.Models
{
    public partial class Student : ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string surname;
        [ObservableProperty]
        Boolean isPresent;
        [ObservableProperty]
        int drawingProtection;

        public Student(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
            isPresent = true;
            drawingProtection = 0;
        }

        public Student(string name, string surname, Boolean isPresent, int drawingProtection)
        {
            this.name = name;
            this.surname = surname;
            this.isPresent = isPresent;
            this.drawingProtection = drawingProtection;
        }
    }
}
