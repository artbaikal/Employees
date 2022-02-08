using Employees.Infrastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Employees.Infrastructure.Commands
{
    class CloseWindowCommand : Command
    {
        public override bool CanExecute(object parameter) => parameter is Window;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;

            var window = (Window)parameter;
            window.Close();
        }
    }

    class CloseDialogCommand : Command
    {
        public bool? DialogResult { get; set; }

        public override bool CanExecute(object parameter) => parameter is Window;

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;


            var window = (Window)parameter;


            if (window.DataContext is Employees.Views.Windows.EditWindow && DialogResult==true)
            {
                Employees.Views.Windows.EditWindow tmp = (Employees.Views.Windows.EditWindow)window;
                //if (tmp.Sex!="М"&& tmp.Sex != "Ж")
                //{
                //    MessageBox.Show("Неверно заполнено поле пол - значение может быть М или Ж");
                //    return;
                //}
                if (tmp.EName==""||tmp.Surname=="")
                {
                    MessageBox.Show("Имя или Фамилия не могут быть пустыми");
                    return;
                }
            }





            window.DialogResult = DialogResult;
            window.Close();
        }
    }
}
