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
        public string _surname
        {
            get { return Surname; }
            set { Surname = value; OnPropertyChanged(); }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
