using Lab2.Navigation;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.ViewModels;
using Lab4.Repositories;
using System.Collections.ObjectModel;

namespace Lab2.ViewModels
{
    internal class MainWindowViewModel : BaseNavigatableViewModel<MainNavigationTypes>
    {
        private FileRepositoryOfPeople          _repo;
        private ObservableCollection<Person>    _collectionOfPeople;
        private Person                          _chosenPerson;
        public MainWindowViewModel()
        {
            _repo = new();
            _collectionOfPeople = new ObservableCollection<Person>(_repo.GetAll());
            _chosenPerson = new();
            Navigate(MainNavigationTypes.Result);

            //Task<List<Person>> people = _repo.GetAllAsync();
            //_collectionOfPeople = new ObservableCollection<Person>(people.Result);
        }

        protected override INavigatable<MainNavigationTypes> CreateViewModel(MainNavigationTypes type)
        {
            switch (type)
            {
                case MainNavigationTypes.AddPerson:
                    return new AddPersonWindowViewModel
                        (
                        () => Navigate(MainNavigationTypes.Result), 
                        ref _repo,
                        ref _collectionOfPeople
                        );
                case MainNavigationTypes.Result:
                    return new ResultWindowViewModel
                        (
                        () => Navigate(MainNavigationTypes.AddPerson),
                        () => Navigate(MainNavigationTypes.Edit),
                        ref _repo,
                        ref _collectionOfPeople,
                        ref _chosenPerson
                        );
                case MainNavigationTypes.Edit:
                    return new EditPersonWindowViewModel
                        (
                        () => Navigate(MainNavigationTypes.Result), 
                        ref _repo,
                        ref _collectionOfPeople,
                        ref _chosenPerson
                        );
                default:
                    return null;
            }
        }
    }
}
