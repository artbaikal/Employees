using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Employees.Infrastructure.Commands;
using Employees.Models;
using Employees.Services.GRPC;
using Employees.ViewModels.Base;
using Employees.Views.Windows;

namespace Employees.ViewModels

{
    internal class MainWindowViewModel : ViewModel
    {



        #region Список сотрудников ListEmployees
        private ObservableCollection<NotifyEmployee> _ListEmployees = new ObservableCollection<NotifyEmployee>();

        public ObservableCollection<NotifyEmployee> ListEmployees { get => _ListEmployees; set => Set(ref _ListEmployees, value); }



        #endregion

        #region SelectedEmployee : Employee - Выбранный сотрудник
        /// <summary>Выбранный сотрудник</summary>
        private NotifyEmployee _SelectedEmployee;

        /// <summary>Выбранный сотрудник</summary>
        public NotifyEmployee SelectedEmployee { get => _SelectedEmployee; set => Set(ref _SelectedEmployee, value); }

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


        #region Команды 



        #region команда Получения списка ListEmployees



        private ICommand _ListEmployeesCommand;

        public ICommand ListEmployeesCommand => _ListEmployeesCommand ??= new LambdaCommand(OnListEmployeesCommandExecuted, CanListEmployeesCommandExecute);

        private static bool CanListEmployeesCommandExecute(object p) => true;

        async private void OnListEmployeesCommandExecuted(object p)
        {

            var empls = new List<Employee>();

            try
            {
                await grpcClient.GetEmployees(empls);
                ListEmployees.Clear();

                foreach(var tmp in empls)
                {
                    var empl = new NotifyEmployee(tmp);
                    
                    ListEmployees.Add(empl);
                }

                if(ListEmployees.Count>0)
                {
                    SelectedEmployee = ListEmployees[0];
                }
                

            }
            catch
            {
                //if (!(p is Int32))
                //{
                    MessageBox.Show("Ошибка при получении списка с сервера");
                //}


            }


        }

        

        #endregion



        #region AddEmployeeCommand - Команда добавления сотрудника

        private ICommand _AddEmployeeCommand;

        public ICommand AddEmployeeCommand => _AddEmployeeCommand ??= new LambdaCommand(OnAddEmployeeCommandExecuted, CanAddEmployeeCommandExecute);

        private static bool CanAddEmployeeCommandExecute(object p) => true;

        private void OnAddEmployeeCommandExecuted(object p)
        {
            var empl = new NotifyEmployee();

            var dlg = new EditWindow
            {

      
                Birthday = Convert.ToDateTime("01.01.2000"),
 


            };

            if (dlg.ShowDialog() == true)
            {

                empl.Surname = dlg.Surname;
                empl.Name = dlg.EName;
                empl.Patronymic = dlg.Patronymic;
                empl.Birthday = dlg.Birthday;
                empl.Sex = dlg.Sex;
                empl.HasChild = dlg.HasChild;

                var res = grpcClient.AddNewEmployeeRequest((Employee)empl);
                if (res == -1)
                {
                    MessageBox.Show("Ошибка при отправке запроса на сервер. запись не добавлена");
                }
                else
                {

                    empl.Id = res;
                    ListEmployees.Add(empl);

                }



            }

        }

        #endregion

        #region EditEmployeeCommand - Команда редактирования сотрудника

        private ICommand _EditEmployeeCommand;


        public ICommand EditEmployeeCommand => _EditEmployeeCommand ??= new LambdaCommand(OnEditEmployeeCommandExecuted, CanEditEmployeeCommandExecute);

        private static bool CanEditEmployeeCommandExecute(object p) => p is NotifyEmployee;

        private void OnEditEmployeeCommandExecuted(object p)
        {
            var empl = (NotifyEmployee)p;

            var dlg = new EditWindow
            {

                Surname = empl.Surname,
                EName = empl.Name,
                Patronymic = empl.Patronymic,
                Birthday = empl.Birthday,
                Sex = empl.Sex,
                HasChild = empl.HasChild,
           


            };

            if (dlg.ShowDialog() == true)
            {
                bool messageAccepted = true;
                if(messageAccepted)
                {
                    empl._surname = dlg.Surname;
                    empl._name = dlg.EName;
                    empl._patronymic = dlg.Patronymic;
                    empl._birthday = dlg.Birthday;
                    empl._sex = dlg.Sex;
                    empl._hasChild = dlg.HasChild;
                }

            }
            //if (dlg.ShowDialog() == true)
            //                MessageBox.Show("Пользователь выполнил редактирование");
            //          else
            //MessageBox.Show("Пользователь отказался");
        }

        #endregion

        #region DelEmployeeCommand - Команда удаления сотрудника

        private ICommand _DelEmployeeCommand;


        public ICommand DelEmployeeCommand => _DelEmployeeCommand ??= new LambdaCommand(OnDelEmployeeCommandExecuted, CanDelEmployeeCommandExecute);

        private static bool CanDelEmployeeCommandExecute(object p) => p is NotifyEmployee;

        private void OnDelEmployeeCommandExecuted(object p)
        {

            if (!(p is NotifyEmployee )) return;
            var empl = (NotifyEmployee)p;
            var eml_index = ListEmployees.IndexOf(empl);

            string messageBoxText = "Удалить выбранную запись сотрудника? ";
            string caption = "Удаленее сотрудника";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

            if(result== MessageBoxResult.Yes)
            {
                try
                {
                    ListEmployees.Remove(empl);
                    if (eml_index < ListEmployees.Count)
                        SelectedEmployee = ListEmployees[eml_index];
                    else
                    {
                        SelectedEmployee = ListEmployees[eml_index-1];
                    }
                }
                catch
                {
                }
            }

        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {





            //var tmp = new NotifyEmployee();
            ////tmp.Id = 1;
            //tmp.Id = 1;
            //tmp.Name = "Иван";
            //tmp.Surname = "Иванов";
            //tmp.Patronymic = "Иванович";
            //tmp.Birthday = DateTime.Now;
            //tmp.Sex = "М";
            //tmp.HasChild = true;

            //ListEmployees.Add(tmp);

            //SelectedEmployee = tmp;

            //tmp = new NotifyEmployee();
            //tmp.Id = 2;
            //tmp.Name = "Сергей";
            //tmp.Surname = "Токарев";
            //tmp.Patronymic = "Петрович";
            //tmp.Birthday = DateTime.Now;
            //tmp.Sex = "М";
            //tmp.HasChild = false;

            //ListEmployees.Add(tmp);

            OnListEmployeesCommandExecuted(1);

        }
    }

}
