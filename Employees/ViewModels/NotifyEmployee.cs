using Employees.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Employees.ViewModels
{
    internal class NotifyEmployee : Employee, INotifyPropertyChanged
    {

        public int _id
        {
            get { return Id; }
            set { Id = value; OnPropertyChanged(); }

        }
        public string _name
        {
            get { return Name; }
            set { Name = value; OnPropertyChanged(); }

        }

        public string _surname
        {
            get { return Surname; }
            set { Surname = value; OnPropertyChanged(); }

        }
        public string _patronymic
        {
            get { return Patronymic; }
            set { Patronymic = value; OnPropertyChanged(); }

        }

        public DateTime _birthday
        {
            get { return Birthday; }
            set { Birthday = value; OnPropertyChanged(); }

        }
        public string _sex
        {
            get { return Sex; }
            set { Sex = value; OnPropertyChanged(); }

        }
        public bool _hasChild
        {
            get { return HasChild; }
            set { HasChild = value; OnPropertyChanged(); }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public NotifyEmployee()
        { 
        }
            public NotifyEmployee(Employee tmp)
        {
            Id = tmp.Id;
            Name = tmp.Name;
            Surname = tmp.Surname;
            Patronymic=tmp.Patronymic;
            Birthday = tmp.Birthday;
            Sex=tmp.Sex;
            HasChild=tmp.HasChild;

        }
    }
}
