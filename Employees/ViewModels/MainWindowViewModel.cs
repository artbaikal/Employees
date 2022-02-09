using System;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Employees.Infrastructure.Commands;
using Model.Models;
using Employees.Services.GRPC;
using Employees.ViewModels.Base;
using Employees.Views.Windows;

namespace Employees.ViewModels

{
    internal class MainWindowViewModel : ViewModel
    {



        #region Список сотрудников ListEmployees
        private ObservableCollection<Employee> _ListEmployees = new ObservableCollection<Employee>();

        public ObservableCollection<Employee> ListEmployees { get => _ListEmployees; set => Set(ref _ListEmployees, value); }



        #endregion

        #region SelectedEmployee : Employee - Выбранный сотрудник
        /// <summary>Выбранный сотрудник</summary>
        private Employee _SelectedEmployee;

        /// <summary>Выбранный сотрудник</summary>
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
                    if (p is Int32)
                    {
                        if ((int)p == tmp.Id)
                        {
                            SelectedEmployee = tmp;
                        }
                    }

                        ListEmployees.Add(tmp);
                }

                bool firstrun = ((p is Int32)) && (int)p == -1;

                if (!(p is Int32)||firstrun)

                {

                    if (ListEmployees.Count > 0)
                    {
                        SelectedEmployee = ListEmployees[0];
                    }
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
            var empl = new Employee();

            var dlg = new EditWindow
            {
                Surname = "",
                EName = "",
                Patronymic = "",
                
                HasChild = false,



                Birthday = Convert.ToDateTime("01.01.2000"),
                sexMale=true,
 


            };

            if (dlg.ShowDialog() == true)
            {

                empl.Surname = dlg.Surname;
                empl.Name = dlg.EName;
                empl.Patronymic = dlg.Patronymic;
                empl.Birthday = dlg.Birthday;
                empl.Sex = dlg.sexFemale == true ? "Ж" : "М";
                empl.HasChild = dlg.HasChild;

                var res = grpcClient.AddNewEmployeeRequest((Employee)empl);
                if (res == -1)
                {
                    MessageBox.Show("Ошибка при отправке запроса на сервер. запись не добавлена");
                }
                else
                {

                    empl.Id = res;
                    //ListEmployees.Add(empl);
                    OnListEmployeesCommandExecuted(empl.Id);

                }



            }

        }

        #endregion

        #region EditEmployeeCommand - Команда редактирования сотрудника

        private ICommand _EditEmployeeCommand;


        public ICommand EditEmployeeCommand => _EditEmployeeCommand ??= new LambdaCommand(OnEditEmployeeCommandExecuted, CanEditEmployeeCommandExecute);

        private static bool CanEditEmployeeCommandExecute(object p) => p is Employee;

        private void OnEditEmployeeCommandExecuted(object p)
        {
            var empl = (Employee)p;



            var dlg = new EditWindow
            {

                Surname = empl.Surname,
                EName = empl.Name,
                Patronymic = empl.Patronymic,
                Birthday = empl.Birthday,
                Sex = empl.Sex,
                sexFemale = empl.Sex == "Ж" ? true : false,
                sexMale = empl.Sex == "М" ? true : false,
                HasChild = empl.HasChild,
           


            };

            if (dlg.ShowDialog() == true)
            {


                empl.Surname = dlg.Surname;
                empl.Name = dlg.EName;
                empl.Patronymic = dlg.Patronymic;
                empl.Birthday = dlg.Birthday;
                empl.Sex = dlg.sexFemale == true ? "Ж" : "М";
                empl.HasChild = dlg.HasChild;

                var res = grpcClient.EditEmployeeRequest((Employee)empl);
                if (res == -1)
                {
                    MessageBox.Show("Ошибка при отправке запроса на сервер. запись не обновлена");
                }
                
                else 
                {
      
                    
                    OnListEmployeesCommandExecuted(empl.Id);


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

        private static bool CanDelEmployeeCommandExecute(object p) => p is Employee;

        private void OnDelEmployeeCommandExecuted(object p)
        {


            if (!(p is Employee )) return;
            var empl = (Employee)p;
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
                    
                    var res = grpcClient.DelEmployeeRequest((Employee)empl);
                    if (res == -1)
                    {
                        MessageBox.Show("Ошибка при отправке запроса на сервер. запись не удалена");
                    }

                    else
                    {
                        var tmplist= ListEmployees;
                        tmplist.Remove(empl);

                        int tmp;
                        if (eml_index < tmplist.Count)
                            tmp = tmplist[eml_index].Id;
                        else
                        {
                            tmp= tmplist[eml_index - 1].Id;
                        }

                        OnListEmployeesCommandExecuted(tmp);

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

            OnListEmployeesCommandExecuted(-1);

        }
    }

}
