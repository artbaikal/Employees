using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Employees.Infrastructure.Commands;
using Employees.Models;
using Employees.ViewModels.Base;


namespace Employees.ViewModels

{
    internal class MainWindowViewModel : ViewModel
    {

        #region Список сотрудников ListEmployees
        private ObservableCollection<Employee> _ListEmployees = new ObservableCollection<Employee>();

        public ObservableCollection<Employee> ListEmployees { get => _ListEmployees; set => Set(ref _ListEmployees, value); }

        //ObservableCollection
        //private ObservableCollection<Employee> _ListEmployees;
        //public ObservableCollection<Employee> ListEmployees
        //{
        //    get { return _ListEmployees; }
        //    set { _ListEmployees = value;}
        //}

        #endregion

        #region SelectedEmployee : Employee - Выбранный сотрудник
        /// <summary>Выбранный студент</summary>
        private Employee _SelectedEmployee;

        /// <summary>Выбранный студент</summary>
        public Employee SelectedEmployee { get => _SelectedEmployee; set => Set(ref _SelectedEmployee, value); }

        #endregion

        #region Заголовок окна

        private string _Title = "Управление работниками";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            //set
            //{
            //    //if (Equals(_Title, value)) return;
            //    //_Title = value;
            //    //OnPropertyChanged();

            //    Set(ref _Title, value);
            //}
            set => Set(ref _Title, value);
        }

        #endregion


        #region команда CloseApplicationCommand2

        public ICommand CloseApplicationCommand2 { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            //Application.Current.Shutdown();
            //Title = DateTime.Now.ToString();

            
            var tmp = new Employee();
            tmp.Id = 3;
            tmp.Name = "Сергей";
            tmp.Surname = "Ульянов";
            tmp.Patronymic = "Петрович";
            tmp.Birthday = DateTime.Now;
            tmp.Sex = "М";
            tmp.HasChild = false;

            //ListEmployees.Add(tmp);

            //OnPropertyChanged(nameof(ListEmployees));

            //SelectedEmployee = ListEmployees[2];
            //var ListEmployees2 = ListEmployees;
            //ListEmployees = null;
            //ListEmployees = ListEmployees2;

            //SelectedEmployee = tmp;

            ListEmployees[0].Name = "gga "+DateTime.Now.ToString();
            OnPropertyChanged(nameof(ListEmployees));
        }

        #endregion

        public MainWindowViewModel()
        {

            //ListEmployees = new ObservableCollection<Employee>();
            #region команды
            CloseApplicationCommand2 = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            #endregion

            var tmp = new Employee();
            tmp.Id = 1;
            tmp.Name = "Иван";
            tmp.Surname = "Иванов";
            tmp.Patronymic = "Иванович";
            tmp.Birthday = DateTime.Now;
            tmp.Sex = "М";
            tmp.HasChild = true;

            ListEmployees.Add(tmp);

            SelectedEmployee = tmp;

            tmp = new Employee();
            tmp.Id = 2;
            tmp.Name = "Сергей";
            tmp.Surname = "Токарев";
            tmp.Patronymic = "Петрович";
            tmp.Birthday = DateTime.Now;
            tmp.Sex = "М";
            tmp.HasChild = false;

            ListEmployees.Add(tmp);

        }
    }

}
