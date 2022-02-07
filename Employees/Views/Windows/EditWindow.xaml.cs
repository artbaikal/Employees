using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Employees.Views.Windows
{


    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        #region LastName : string - Фамилия

        /// <summary>Фамилия</summary>
        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register(
                nameof(LastName),
                typeof(string),
                typeof(EditWindow),
                new PropertyMetadata(default(string)));

        /// <summary>Фамилия</summary>
        //[Category("")]
        [Description("Фамилия")]
        public string LastName { get => (string)GetValue(LastNameProperty); set => SetValue(LastNameProperty, value); }

        #endregion





        public EditWindow()
        {
            InitializeComponent();
        }
    }
}
