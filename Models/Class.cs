using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SystemOfDrawingStudentForAnswering.Models
{
    public partial class Class : ObservableObject
    {
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private ObservableCollection<Student> students = new ObservableCollection<Student>();

        public Class(string name)
        {
            this.name = name;
        }
    }
}
