using Lab2.Navigation;
using Lab2.Tools;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;

namespace Lab2.ViewModels
{
    internal class ResultWindowViewModel : INavigatable<MainNavigationTypes>, INotifyPropertyChanged
    {
        #region Fields
        private FileRepositoryOfPeople _repo;
        private ObservableCollection<Person> _collectionOfPeople;
        private readonly Action _addNavigation;
        private readonly Action _editNavigation;
        private RelayCommand<object> _toAddPersonCommand;
        private RelayCommand<object> _toEditPersonCommand;
        private RelayCommand<object> _erasePersonCommand;
        private RelayCommand<object> _exitCommand;
        private Person _chosenPerson;

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Constructors
        public ResultWindowViewModel
            (
            Action addNavigation, 
            Action editNavigation, 
            ref FileRepositoryOfPeople repo, 
            ref ObservableCollection<Person> col,
            ref Person chosen
            )
        {
            _addNavigation = addNavigation;
            _editNavigation = editNavigation;
            _repo = repo;
            _collectionOfPeople = col;
            _chosenPerson = chosen;

            return;
        }
        #endregion

        #region Properties
        public ObservableCollection<Person> CollectionOfPeople
        {
            get => _collectionOfPeople;
            set
            {
                _collectionOfPeople = value;
                //OnPropertyChanged(nameof(CollectionOfPeople));
            }
        }

        public Person ChosenPerson
        {
            get => _chosenPerson; 
            set
            {
                _chosenPerson.Name = value?.Name;
                _chosenPerson.Surname = value?.Surname;
                _chosenPerson.DateOfBirth = value?.DateOfBirth;
                _chosenPerson.Email = value?.Email;
            }
        }
        public RelayCommand<object> ExitCommand
        {
            get => _exitCommand ??= new RelayCommand<object>(_ => Environment.Exit(0));
        }

        public RelayCommand<object> EraseCommand
        {
            get => _erasePersonCommand ??= new RelayCommand<object>(_ => erase());
        }

        public RelayCommand<object> ToAddPersonCommand
        {
            get => _toAddPersonCommand ??= new RelayCommand<object>(_ => _addNavigation.Invoke());
        }

        public RelayCommand<object> ToEditPersonCommand
        {
            get => _toEditPersonCommand ??= new RelayCommand<object>(_ => edit());
        }

        public MainNavigationTypes ViewType => MainNavigationTypes.Result;
        #endregion

        #region Methods
        private void edit()
        {
            _editNavigation.Invoke();
        }
        private async void erase()
        {
            if (ChosenPerson == null)
            {
                MessageBox.Show("You haven't chosen a person to erase.");
            }
            else
            {
                await _repo.Erase(ChosenPerson);
                _collectionOfPeople.Remove(find(ChosenPerson));
            }
        }

        private Person find(Person p)
        {
            for (int i = 0; i < _collectionOfPeople.Count; ++i)
            {
                if (_collectionOfPeople[i].Equals(p))
                    return _collectionOfPeople[i];
            }

            return p;
        }

        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
