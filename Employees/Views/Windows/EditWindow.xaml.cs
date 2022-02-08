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
        #region Surname : string - Фамилия

        /// <summary>Фамилия</summary>
        public static readonly DependencyProperty SurnameProperty =
            DependencyProperty.Register(
                nameof(Surname),
                typeof(string),
                typeof(EditWindow),
                new PropertyMetadata(default(string)));

        /// <summary>Фамилия</summary>
        //[Category("")]
        [Description("Фамилия")]
        public string Surname { get => (string)GetValue(SurnameProperty); set => SetValue(SurnameProperty, value); }

        #endregion


        #region EName : string - Имя

        /// <summary>Имя</summary>
        public static readonly DependencyProperty ENameProperty =
            DependencyProperty.Register(
                nameof(EName),
                typeof(string),
                typeof(EditWindow),
                new PropertyMetadata(default(string)));

        /// <summary>Имя</summary>
        //[Category("")]
        [Description("Имя")]
        public string EName { get => (string)GetValue(ENameProperty); set => SetValue(ENameProperty, value); }

        #endregion

        #region Patronymic

        /// <summary>Patronymic</summary>
        public static readonly DependencyProperty PatronymicProperty =
            DependencyProperty.Register(
                nameof(Patronymic),
                typeof(string),
                typeof(EditWindow),
                new PropertyMetadata(default(string)));


        public string Patronymic { get => (string)GetValue(PatronymicProperty); set => SetValue(PatronymicProperty, value); }

        #endregion


        #region Birthday

        /// <summary>Birthday</summary>
        public static readonly DependencyProperty BirthdayProperty =
            DependencyProperty.Register(
                nameof(Birthday),
                typeof(DateTime),
                typeof(EditWindow),
                new PropertyMetadata(default(DateTime)));


        public DateTime Birthday { get => (DateTime)GetValue(BirthdayProperty); set => SetValue(BirthdayProperty, value); }

        #endregion

        #region Sex

        /// <summary>Sex</summary>
        public static readonly DependencyProperty SexProperty =
            DependencyProperty.Register(
                nameof(Sex),
                typeof(string),
                typeof(EditWindow),
                new PropertyMetadata(default(string)));


        public string Sex { get => (string)GetValue(SexProperty); set => SetValue(SexProperty, value); }


        //sexMale sexFemale
        public static readonly DependencyProperty SexPropertyMale =
            DependencyProperty.Register(
                nameof(sexMale),
                typeof(bool),
                typeof(EditWindow),
                new PropertyMetadata(default(bool)));


        public bool sexMale { get => (bool)GetValue(SexPropertyMale); set => SetValue(SexPropertyMale, value); }

        public static readonly DependencyProperty SexPropertyFemale =
               DependencyProperty.Register(
                   nameof(sexFemale),
                   typeof(bool),
                   typeof(EditWindow),
                   new PropertyMetadata(default(bool)));


        public bool sexFemale { get => (bool)GetValue(SexPropertyFemale); set => SetValue(SexPropertyFemale, value); }

        #endregion


        #region HasChild

        /// <summary>Sex</summary>
        public static readonly DependencyProperty HasChildProperty =
            DependencyProperty.Register(
                nameof(HasChild),
                typeof(bool),
                typeof(EditWindow),
                new PropertyMetadata(default(bool)));


        public bool HasChild { get => (bool)GetValue(HasChildProperty); set => SetValue(HasChildProperty, value); }

        #endregion

        public EditWindow()
        {
            InitializeComponent();
        }
    }
}
