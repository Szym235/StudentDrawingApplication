using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using SystemOfDrawingStudentForAnswering.Models;

namespace SystemOfDrawingStudentForAnswering.ViewModels
{
    public partial class AllClasses : ObservableObject
    {

        public static AllClasses Instance { get; } = new AllClasses();
        [ObservableProperty]
        private string newStudentNameFromEntry;
        [ObservableProperty]
        private string newStudentSurnameFromEntry;
        [ObservableProperty]
        private Class classFromPicker;
        [ObservableProperty]
        private string newClassNameFromEntry;
        [ObservableProperty]
        private Student drawedStudent;
        [ObservableProperty]
        private Student studentSelectedForEdition;
        [ObservableProperty]
        private string editedStudentNameFromEntry;
        [ObservableProperty]
        private string editedStudentSurnameFromEntry;
        [ObservableProperty]
        private int luckyNumber;

        [ObservableProperty]
        private ObservableCollection<Class> classes = new ObservableCollection<Class>();

        private AllClasses()
        {
            luckyNumber = new Random().Next(1, 30);
        }
        [RelayCommand]
        private void AddStudent()
        {
            if ((NewStudentNameFromEntry != null && NewStudentNameFromEntry.Replace(" ", "") != "") &&
                (NewStudentSurnameFromEntry != null && NewStudentSurnameFromEntry.Replace(" ", "") != ""))
            {
                if (ClassFromPicker != null)
                {
                    ClassFromPicker.Students.Add(new Student(NewStudentNameFromEntry, NewStudentSurnameFromEntry));
                    NewStudentNameFromEntry = string.Empty;
                    NewStudentSurnameFromEntry = string.Empty;
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Błąd", "Nie wybrano klasy!", "OK");
                }
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Błąd", "Nie wpisano wszystkich danych ucznia!", "OK");
            }
        }

        [RelayCommand]
        private void AddClass()
        {
            if (NewClassNameFromEntry != null && NewClassNameFromEntry.Replace(" ", "") != "")
            {
                Class newClass = new Class(NewClassNameFromEntry);
                Classes.Add(newClass);
                ClassFromPicker = newClass;
                NewClassNameFromEntry = string.Empty;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Błąd", "Nie wpisano nazwy klasy!", "OK");
            }
        }

        [RelayCommand]

        private async Task SaveClass()
        {
            if (classFromPicker == null)
            {
                App.Current.MainPage.DisplayAlert("Błąd", "Nie wybrano klasy!", "OK");
                return;
            }
            StreamWriter streamWriter = new StreamWriter(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ClassFromPicker.Name + ".txt")
            );
            foreach (Student student in ClassFromPicker.Students)
            {
                streamWriter.WriteLine(student.Name + "," + student.Surname + "," + student.IsPresent + "," + student.DrawingProtection);
            }
            streamWriter.Close();
            App.Current.MainPage.DisplayAlert("Zapis", ClassFromPicker.Name + " została zapisana", "OK");
        }

        [RelayCommand]
        private async Task LoadClass()
        {
            FileResult file = await FilePicker.Default.PickAsync();
            if (file != null)
            {
                StreamReader streamReader = new StreamReader(await file.OpenReadAsync());
                string className = Path.GetFileNameWithoutExtension(file.FileName);
                List<string[]> students = new List<string[]>();
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    string[] student = line.Split(',');
                    students.Add(student);
                }
                streamReader.Close();
                Class loadedClass = new Class(className);
                try
                {
                    foreach (string[] student in students)
                    {
                        loadedClass.Students.Add(new Student(student[0], student[1], Boolean.Parse(student[2]), int.Parse(student[3])));
                    }
                }
                catch (Exception ex)
                {
                    App.Current.MainPage.DisplayAlert("Błąd", "Zły format pliku!", "OK");
                    return;
                }
                Classes.Add(loadedClass);
                ClassFromPicker = loadedClass;
            }
        }

        [RelayCommand]
        private void DeleteClass()
        {
            if (ClassFromPicker != null)
            {
                Classes.Remove(ClassFromPicker);
                ClassFromPicker = null;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Błąd", "Nie wybrano klasy!", "OK");
            }
        }

        [RelayCommand]
        private void DrawStudent()
        {
            List<Student> potentialWinners = new List<Student>();
            if(classFromPicker == null)
            {
                App.Current.MainPage.DisplayAlert("Błąd", "Nie wybrano klasy!", "OK");
                return;
            }
            for (int i = 0; i < ClassFromPicker.Students.Count; i++)
            {
                Student student = ClassFromPicker.Students[i];
                if (student.IsPresent && student.DrawingProtection == 0 && i + 1 != LuckyNumber)
                {
                    potentialWinners.Add(ClassFromPicker.Students[i]);
                }
            }
            foreach (Student student in ClassFromPicker.Students)
            {
                if (student.DrawingProtection > 0)
                {
                    student.DrawingProtection--;
                }
            }
            if (potentialWinners.Count == 0)
            {
                DrawedStudent = new Student("Brak uczniów możliwych do wylosowania!", "");
                return;
            }
            Random random = new Random();
            long number;
            do
            {
                number = random.NextInt64(potentialWinners.Count);
            } while (LuckyNumber == number + 1);
            Student winner = potentialWinners[((int)number)];
            winner.DrawingProtection = 3;
            DrawedStudent = potentialWinners[((int)number)];

        }

        [RelayCommand]
        private void RemoveStudent(Student student)
        {
            ClassFromPicker.Students.Remove(student);
            Debug.WriteLine("Student removed");
        }

        [RelayCommand]

        private void StartEditingStudent(Student student)
        {
            StudentSelectedForEdition = student;
            EditedStudentNameFromEntry = student.Name;
            EditedStudentSurnameFromEntry = student.Surname;
        }

        [RelayCommand]
        private void EditStudent()
        {
            if(studentSelectedForEdition == null)
            {
                App.Current.MainPage.DisplayAlert("Błąd", "Nie wybrano ucznia od edycji!", "OK");
                return;
            }
            StudentSelectedForEdition.Name = EditedStudentNameFromEntry;
            StudentSelectedForEdition.Surname = EditedStudentSurnameFromEntry;
            EditedStudentNameFromEntry = "";
            EditedStudentSurnameFromEntry = "";
            studentSelectedForEdition = null;
        }
    }
}
