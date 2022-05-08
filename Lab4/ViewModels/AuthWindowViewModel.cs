using Lab2.Navigation;
using Lab2.Models;
using Lab2.Tools;
using System;
using System.Threading.Tasks;
using System.Windows;
using Lab3.Exceptions;

namespace Lab2.ViewModels
{
    internal class AuthWindowViewModel : INavigatable<MainNavigationTypes>
    {
        #region Fields
        private Person _person;
        private RelayCommand<object> _exitCommand;
        private RelayCommand<object> _proceedCommand;
        private readonly Action _exitNavigation;
        #endregion

        #region Constructors
        public AuthWindowViewModel (Action exitNavigation, ref Person person)
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

        public RelayCommand<object> ProceedCommand
        {
            get => _proceedCommand ??= new RelayCommand<object>(_ => proceed(), _ => allFieldsFilled());
        }
        public RelayCommand<object> ExitCommand
        {
            get => _exitCommand ??= new RelayCommand<object>(_ => Environment.Exit(0));
        }

        public MainNavigationTypes ViewType => MainNavigationTypes.Auth;

        #endregion

        #region Methods
        private bool allFieldsFilled()
        {
            return (!String.IsNullOrEmpty(Name)      &&
                    !String.IsNullOrEmpty(Surname)   &&
                    !String.IsNullOrEmpty(Email)     &&
                    DateOfBirth != null);
        }

        private async void proceed()
        {
            try 
            {
                _person.checkTheEmail();

                await Task.Run(() => _person.computeAge());
                await Task.Run(() => _person.computeIsAdult());
                await Task.Run(() => _person.computeSunSign());
                await Task.Run(() => _person.computeChineseZodiacSign());
                await Task.Run(() => _person.computeHasBirthday());

                _person.checkTheAge();
                _exitNavigation.Invoke();
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

            return;
        }
        #endregion
    }
}