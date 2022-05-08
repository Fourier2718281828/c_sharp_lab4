using Lab2.Navigation;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.ViewModels
{
    internal class MainWindowViewModel : BaseNavigatableViewModel<MainNavigationTypes>
    {
        private Person _person = new();
        public MainWindowViewModel()
        {
            Navigate(MainNavigationTypes.Result);
        }

        protected override INavigatable<MainNavigationTypes> CreateViewModel(MainNavigationTypes type)
        {
            switch (type)
            {
                case MainNavigationTypes.Auth:
                    return new AuthWindowViewModel(() => Navigate(MainNavigationTypes.Result), ref _person);
                case MainNavigationTypes.Result:
                    return new ResultWindowViewModel(() => Navigate(MainNavigationTypes.Auth), ref _person);
                default:
                    return null;
            }
        }
    }
}
