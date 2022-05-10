using Lab2.Navigation;
using Lab2.Models;
using Lab2.Tools;
using System;
using System.Threading.Tasks;
using System.Windows;
using Lab3.Exceptions;
using Lab4.Repositories;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Lab2.ViewModels
{
    internal class EditPersonWindowViewModel : INavigatable<MainNavigationTypes>
    {
        #region Fields
        private FileRepositoryOfPeople _repo;
        private ObservableCollection<Person> _collectionOfPeople;
        private RelayCommand<object> _backCommand;
        private RelayCommand<object> _editCommand;
        private readonly Action _exitNavigation;
        private Person _chosenPerson;
        private Person _newPerson = new();

        #endregion

        #region Constructors
        public EditPersonWindowViewModel
            (
            Action exitNavigation, 
            ref FileRepositoryOfPeople repo,
            ref ObservableCollection<Person> col,
            ref Person chosenPerson
            )
        {
            _repo = repo;
            _collectionOfPeople = col;
            _exitNavigation = exitNavigation;
            _chosenPerson = chosenPerson;

            /*_collectionOfPeople.Remove(_chosenPerson);*/

            
            return;
        }
        #endregion

        #region Properties
        public string Name
        {
            get => _newPerson.Name ??= _chosenPerson.Name; 
            set
            {
                _newPerson.Name = value;
            }
        }

        public string Surname
        {
            get => _newPerson.Surname ??= _chosenPerson.Surname;
            set
            {
                _newPerson.Surname = value;
            }
        }

        public string Email
        {
            get => _newPerson.Email ??= _chosenPerson.Email;
            set
            {
                _newPerson.Email = value;
            }
        }

        public DateTime? DateOfBirth
        {
            get => _newPerson.DateOfBirth ??= _chosenPerson.DateOfBirth;
            set
            {
                _newPerson.DateOfBirth = value;
            }
        }

        public RelayCommand<object> EditCommand
        {
            get => _editCommand ??= new RelayCommand<object>(_ => edit(), _ => allFieldsFilled());
        }
        public RelayCommand<object> BackCommand
        {
            get => _backCommand ??= new RelayCommand<object>(_ => back());
        }

        public MainNavigationTypes ViewType => MainNavigationTypes.Edit;

        #endregion

        #region Methods
        private void back()
        {
            _chosenPerson = new();
            _exitNavigation.Invoke();
        }

        private bool allFieldsFilled()
        {
            return (!String.IsNullOrEmpty(Name) &&
                    !String.IsNullOrEmpty(Surname) &&
                    !String.IsNullOrEmpty(Email) &&
                    DateOfBirth != null);
        }

        private async void edit()
        {
            Person person = new Person(Name, Surname, Email, DateOfBirth);
            try
            {
                person.checkTheEmail();

                Task task1 = Task.Run(() => person.computeAge());
                Task task2 = Task.Run(() => person.computeIsAdult());
                Task task3 = Task.Run(() => person.computeSunSign());
                Task task4 = Task.Run(() => person.computeChineseZodiacSign());
                Task task5 = Task.Run(() => person.computeHasBirthday());

                await task1;
                await task2;
                await task3;
                await task4;
                await task5;

                person.checkTheAge();

                Task task6 = Task.Run(() => _repo.AddOrUpdateAsync(person));
                await task6;

                await eraseChosen();
                _collectionOfPeople.Add(person);

                _newPerson = new();

                _exitNavigation.Invoke();
            }
            catch (BadEmailException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FutureDateException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (PastDateException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private async Task eraseChosen()
        {
            Task<Person?> task = Task.Run(() => find(_chosenPerson));
            await task;
            if (task.Result == null) return;
            _collectionOfPeople.Remove((Person) task.Result);
        }

        private Person? find(Person p)
        {
            for(int i = 0; i < _collectionOfPeople.Count; ++i)
            {
                if(_collectionOfPeople[i].Equals(p))
                    return _collectionOfPeople[i]; 
            }

            return null;
        }

        #endregion
    }
}
