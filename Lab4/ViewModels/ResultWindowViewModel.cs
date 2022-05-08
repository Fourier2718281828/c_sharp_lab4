using Lab2.Navigation;
using Lab2.Tools;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.ViewModels
{
    internal class ResultWindowViewModel : INavigatable<MainNavigationTypes>
    {
        #region Fields
        private readonly Person _person;
        private readonly Action _exitNavigation;
        private RelayCommand<object> _exitCommand;
        #endregion

        #region Constructors
        public ResultWindowViewModel(Action exitNavigation, ref Person person)
        {
            _exitNavigation = exitNavigation;
            _person = person;
            return;
        }
        #endregion

        #region Properties
        public string Name
        {
            get => _person.Name;
        }

        public string Surname
        {
            get => _person.Surname;
        }

        public string Email
        {
            get => _person.Email;
        }

        public string DateOfBirth
        {
            get => _person.DateOfBirth.Value.ToShortDateString();
        }

        public string IsAdult
        {
            get => _person.IsAdult ? "yes" : "no";
        }

        public string SunSign
        {
            get => _person.SunSign.ToString();
        }

        public string ChineseSign
        { 
            get => _person.ChineseSign.ToString();
        }

        public string HasBirthday
        {
            get => _person.IsBirthday ? "Happy Birthday!" :"No";
        }
        public RelayCommand<object> ExitCommand
        {
            get => _exitCommand ??= new RelayCommand<object>(_ => Environment.Exit(0));
        }

        public MainNavigationTypes ViewType => MainNavigationTypes.Result;
        #endregion
    }
}
