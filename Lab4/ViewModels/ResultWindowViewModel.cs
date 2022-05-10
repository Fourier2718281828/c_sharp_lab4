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
        private RelayCommand<object> _sortNames;
        private RelayCommand<object> _sortSurnames;
        private RelayCommand<object> _sortEmails;
        private RelayCommand<object> _sortDates;
        private RelayCommand<object> _sortIsAdult;
        private RelayCommand<object> _sortSunSigns;
        private RelayCommand<object> _sortChinese;
        private RelayCommand<object> _sortBirthday;
        private RelayCommand<object> _deleteAll;

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
                OnPropertyChanged(nameof(CollectionOfPeople));
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

        public RelayCommand<object> DeleteAllCommand
        {
            get => _deleteAll ??= new RelayCommand<object>(_ => deleteAll());
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
            get => _toAddPersonCommand ??= new RelayCommand<object>(_ => add());
        }

        public RelayCommand<object> ToEditPersonCommand
        {
            get => _toEditPersonCommand ??= new RelayCommand<object>(_ => edit());
        }

        public RelayCommand<object> SortNamesCommand
        {
            get => _sortNames ??= new RelayCommand<object>(_ => sortByNames());
        }

        public RelayCommand<object> SortSurnamesCommand
        {
            get => _sortSurnames ??= new RelayCommand<object>(_ => sortBySurnames());
        }

        public RelayCommand<object> SortEmailsCommand
        {
            get => _sortEmails ??= new RelayCommand<object>(_ => sortByEmails());
        }

        public RelayCommand<object> SortDateCommand
        {
            get => _sortDates ??= new RelayCommand<object>(_ => sortByDates());
        }

        public RelayCommand<object> SortIsAdultCommand
        {
            get => _sortIsAdult ??= new RelayCommand<object>(_ => sortByIsAdult());
        }

        public RelayCommand<object> SortSunSignsCommand
        {
            get => _sortSurnames ??= new RelayCommand<object>(_ => sortBySunSign());
        }

        public RelayCommand<object> SortChineseCommand
        {
            get => _sortNames ??= new RelayCommand<object>(_ => sortByChinese());
        }

        public RelayCommand<object> SortBirthdaysCommand
        {
            get => _sortNames ??= new RelayCommand<object>(_ => sortByBirthday());
        }

        public MainNavigationTypes ViewType => MainNavigationTypes.Result;
        #endregion

        #region Methods
        private void deleteAll()
        {
            foreach(Person p in _collectionOfPeople)
            {
                _repo.Erase(p);
            }
            _collectionOfPeople.Clear();
            return;
        }
        private void add()
        {
            ChosenPerson = new();
            _addNavigation.Invoke();
        }
        private void edit()
        {
            if (!isFullyAPerson(ChosenPerson))
            {
                MessageBox.Show("You haven't chosen a person to edit.");
            }
            else
            {
                _editNavigation.Invoke();
            }
        }
        private async void erase()
        {
            if (!isFullyAPerson(ChosenPerson))
            {
                MessageBox.Show("You haven't chosen a person to erase.");
            }
            else
            {
                await _repo.Erase(ChosenPerson);
                _collectionOfPeople.Remove(find(ChosenPerson));
                ChosenPerson = new();
            }
        }

        private bool isFullyAPerson(Person p)
        {
            if (p == null) return false;
            return p.Name != null && p.Surname != null && p.DateOfBirth != null && p.Email != null;
        }

        private Person find(Person p)
        {
            for (int i = 0; i < _collectionOfPeople.Count; ++i)
            {
                if (_collectionOfPeople[i].equals(p))
                    return _collectionOfPeople[i];
            }

            return p;
        }

        private void sortByNames()
        {
            _collectionOfPeople = new ObservableCollection<Person>(_collectionOfPeople.OrderBy(person => person.Name).ToList());
            OnPropertyChanged(nameof(CollectionOfPeople));
        }

        private void sortBySurnames()
        {
            _collectionOfPeople = new(_collectionOfPeople.OrderBy(person => person.Surname).ToList());
            OnPropertyChanged(nameof(CollectionOfPeople));
        }

        private void sortByEmails()
        {
            _collectionOfPeople = new(_collectionOfPeople.OrderBy(person => person.Email).ToList());
            OnPropertyChanged(nameof(CollectionOfPeople));
        }

        private void sortByDates()
        {
            _collectionOfPeople = new(_collectionOfPeople.OrderBy(person => person.DateOfBirth).ToList());
            OnPropertyChanged(nameof(CollectionOfPeople));
        }

        private void sortByIsAdult()
        {
            _collectionOfPeople = new(_collectionOfPeople.OrderBy(person => person.IsAdult).ToList());
            OnPropertyChanged(nameof(CollectionOfPeople));
        }

        private void sortBySunSign()
        {
            _collectionOfPeople = new(_collectionOfPeople.OrderBy(person => person.SunSign).ToList());
            OnPropertyChanged(nameof(CollectionOfPeople));
        }

        private void sortByChinese()
        {
            _collectionOfPeople = new(_collectionOfPeople.OrderBy(person => person.ChineseSign).ToList());
            OnPropertyChanged(nameof(CollectionOfPeople));
        }

        private void sortByBirthday()
        {
            _collectionOfPeople = new(_collectionOfPeople.OrderBy(person => person.IsBirthday).ToList());
            OnPropertyChanged(nameof(CollectionOfPeople));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
