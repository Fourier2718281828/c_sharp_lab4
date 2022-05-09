using Lab2.Navigation;
using Lab2.Models;
using Lab2.Tools;
using System;
using System.Threading.Tasks;
using System.Windows;
using Lab3.Exceptions;

namespace Lab2.ViewModels
{
    internal class AddPersonWindowViewModel : INavigatable<MainNavigationTypes>
    {
        #region Fields
        private Person _person;
        private RelayCommand<object> _backCommand;
        private RelayCommand<object> _addCommand;
        private readonly Action _exitNavigation;
        #endregion

        #region Constructors
        public AddPersonWindowViewModel (Action exitNavigation, ref Person person)
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
            set => _person.Name = value;
        }

        public string Surname
        {
            get => _person.Surname;
            set => _person.Surname = value;
        }

        public string Email
        {
            get => _person.Email;
            set => _person.Email = value;
        }

        public DateTime? DateOfBirth
        {
            get => _person.DateOfBirth;
            set => _person.DateOfBirth = value;
        }

        public RelayCommand<object> AddCommand
        {
            get => _addCommand ??= new RelayCommand<object>(_ => add(), _ => allFieldsFilled());
        }
        public RelayCommand<object> BackCommand
        {
            get => _backCommand ??= new RelayCommand<object>(_ => back());
        }

        public MainNavigationTypes ViewType => MainNavigationTypes.AddPerson;

        #endregion

        #region Methods
        private void back()
        {
            Name = Surname = Email =  null;
            DateOfBirth = null;
            _exitNavigation.Invoke();
        }

        private bool allFieldsFilled()
        {
            return (!String.IsNullOrEmpty(Name)      &&
                    !String.IsNullOrEmpty(Surname)   &&
                    !String.IsNullOrEmpty(Email)     &&
                    DateOfBirth != null);
        }

        private async void add()
        {
            try 
            {
                _person.checkTheEmail();

                Task task1 = Task.Run(() => _person.computeAge());
                Task task2 = Task.Run(() => _person.computeIsAdult());
                Task task3 = Task.Run(() => _person.computeSunSign());
                Task task4 = Task.Run(() => _person.computeChineseZodiacSign());
                Task task5 = Task.Run(() => _person.computeHasBirthday());

                await task1;
                await task2;
                await task3;
                await task4;
                await task5;

                _person.checkTheAge();
            }
            catch(BadEmailException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FutureDateException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(PastDateException ex)
            {
                MessageBox.Show(ex.Message);
            }

            Name = Surname = Email = null;
            DateOfBirth = null;
            _exitNavigation.Invoke();
            return;
        }
        #endregion
    }
}