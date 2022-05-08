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
        private RelayCommand<object> _toAddPersonCommand;

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
        public RelayCommand<object> ExitCommand
        {
            get => _exitCommand ??= new RelayCommand<object>(_ => Environment.Exit(0));
        }

        public RelayCommand<object> ToAddPersonCommand
        {
            get => _toAddPersonCommand ??= new RelayCommand<object>(_ => _exitNavigation.Invoke());
        }

        public MainNavigationTypes ViewType => MainNavigationTypes.Result;
        #endregion
    }
}
