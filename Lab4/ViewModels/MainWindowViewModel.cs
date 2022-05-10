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
            _repo = new(people);
            _collectionOfPeople = new(_repo.GetAll());
            _chosenPerson = new();
            Navigate(MainNavigationTypes.Result);
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

        private readonly Person[] people =
        {
            new Person("Oleg",      "Olegovych",        "ol1@ol.ol"   ,new DateTime(1995, 1, 1)),
            new Person("Ruslan",    "Ruslanovych",      "ru1@ru.ru"   ,new DateTime(1967, 2, 1)),
            new Person("Nikita",    "Nikitovych",       "bi1@ni.ni"   ,new DateTime(1999, 1, 2)),
            new Person("George",    "Georgiyovych",     "ge1@ge.ge"   ,new DateTime(1999, 1, 1)),
            new Person("Gavrylo",   "Gavrylovych",      "ga1@ga.ga"   ,new DateTime(1967, 7,1)),
            new Person("Max",       "Maximovych",       "ma1@ma.ma"   ,new DateTime(1985, 1, 1)),
            new Person("Ihor",      "Ihorovych",        "ih1@ih.ih"   ,new DateTime(1985, 1, 2)),
            new Person("Artem",     "Artemovych",       "ar1@ar.ar"   ,new DateTime(1985, 1, 3)),
            new Person("Olexiy",    "Olexiyovych",      "ol1@ol.ol"   ,new DateTime(2005, 1, 1)),
            new Person("Stas",      "Stasovych",        "st1@st.st"   ,new DateTime(2010, 1, 1)),

            new Person("Olegk",      "Olegovych",        "ol2@ol.ol"   ,new DateTime(1995, 1, 1)),
            new Person("Ruslank",    "Ruslanovych",      "ru2@ru.ru"   ,new DateTime(1967, 2, 1)),
            new Person("Nikitak",    "Nikitovych",       "bi2@ni.ni"   ,new DateTime(1999, 1, 2)),
            new Person("Georgek",    "Georgiyovych",     "ge2@ge.ge"   ,new DateTime(1999, 7, 1)),
            new Person("Gavrylok",   "Gavrylovych",      "ga2@ga.ga"   ,new DateTime(1967, 1,1)),
            new Person("Maxk",       "Maximovych",       "ma2@ma.ma"   ,new DateTime(1985, 1, 1)),
            new Person("Ihork",      "Ihorovych",        "ih2@ih.ih"   ,new DateTime(1985, 4, 2)),
            new Person("Artemk",     "Artemovych",       "ar2@ar.ar"   ,new DateTime(1985, 1, 3)),
            new Person("Olexiyk",    "Olexiyovych",      "ol2@ol.ol"   ,new DateTime(2005, 1, 4)),
            new Person("Stask",      "Stasovych",        "st2@st.st"   ,new DateTime(2010, 1, 1)),

            new Person("Olegj",      "Olegovych",        "ol3@ol.ol"   ,new DateTime(1995, 1, 1)),
            new Person("Ruslanj",    "Ruslanovych",      "ru3@ru.ru"   ,new DateTime(1967, 2, 1)),
            new Person("Nikitaj",    "Nikitovych",       "bi3@ni.ni"   ,new DateTime(1999, 2, 2)),
            new Person("Georgje",    "Georgiyovych",     "ge3@ge.ge"   ,new DateTime(1999, 1, 1)),
            new Person("Gavryloj",   "Gavrylovych",      "ga3@ga.ga"   ,new DateTime(1967, 3,1)),
            new Person("Maxj",       "Maximovych",       "ma3@ma.ma"   ,new DateTime(1985, 1, 1)),
            new Person("Ihorj",      "Ihorovych",        "ih3@ih.ih"   ,new DateTime(1985, 1, 2)),
            new Person("Artemj",     "Artemovych",       "ar3@ar.ar"   ,new DateTime(1985, 10, 3)),
            new Person("Olexiyj",    "Olexiyovych",      "ol3@ol.ol"   ,new DateTime(2005, 1, 1)),
            new Person("Stasj",      "Stasovych",        "st3@st.st"   ,new DateTime(2010, 1, 1)),

            new Person("Olegg",      "Olegovych",        "ol4@ol.ol"   ,new DateTime(1995, 1, 1)),
            new Person("Ruslang",    "Ruslanovych",      "ru4@ru.ru"   ,new DateTime(1967, 2, 1)),
            new Person("Nikitag",    "Nikitovych",       "bi4@ni.ni"   ,new DateTime(1999, 11, 2)),
            new Person("Georgeg",    "Georgiyovych",     "ge4@ge.ge"   ,new DateTime(1999, 1, 1)),
            new Person("Gavrylog",   "Gavrylovych",      "ga4@ga.ga"   ,new DateTime(1967, 1,1)),
            new Person("Maxg",       "Maximovych",       "ma4@ma.ma"   ,new DateTime(1985, 11, 1)),
            new Person("Ihorg",      "Ihorovych",        "ih4@ih.ih"   ,new DateTime(1985, 1, 2)),
            new Person("Artemg",     "Artemovych",       "ar4@ar.ar"   ,new DateTime(1985, 1, 3)),
            new Person("Olexiyg",    "Olexiyovych",      "ol4@ol.ol"   ,new DateTime(2005, 1, 1)),
            new Person("Stasg",      "Stasovych",        "st4@st.st"   ,new DateTime(2010, 1, 1)),

            new Person("Olegb",      "Olegovych",        "ol5@ol.ol"   ,new DateTime(1995, 1, 1)),
            new Person("Ruslanb",    "Ruslanovych",      "ru5@ru.ru"   ,new DateTime(1967, 2, 1)),
            new Person("Nikitab",    "Nikitovych",       "bi5@ni.ni"   ,new DateTime(1999, 1, 2)),
            new Person("Georgeb",    "Georgiyovych",     "ge5@ge.ge"   ,new DateTime(1999, 7, 1)),
            new Person("Gavrylob",   "Gavrylovych",      "ga5@ga.ga"   ,new DateTime(1967, 1,1)),
            new Person("Maxb",       "Maximovych",       "ma5@ma.ma"   ,new DateTime(1985, 1, 1)),
            new Person("Ihorb",      "Ihorovych",        "ih5@ih.ih"   ,new DateTime(1985, 1, 2)),
            new Person("Artemb",     "Artemovych",       "ar5@ar.ar"   ,new DateTime(1985, 5, 3)),
            new Person("Olexiyb",    "Olexiyovych",      "ol5@ol.ol"   ,new DateTime(2005, 1, 1)),
            new Person("Stasb",      "Stasovych",        "st5@st.st"   ,new DateTime(2010, 1, 1)),
        };
    }
}
