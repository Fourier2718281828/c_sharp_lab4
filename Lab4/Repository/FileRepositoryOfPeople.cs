using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Lab2.Models;
using Lab3.Exceptions;

namespace Lab4.Repositories
{
    class FileRepositoryOfPeople
    {
        private static readonly string BaseFolder = 
            Path.Combine
            (
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), 
            "C#Lab4Storage", 
            "People"
            );

        public FileRepositoryOfPeople()
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);

            if (Directory.GetFiles(BaseFolder).Length == 0)
            {
                addPeople();
            }

        }

        public async Task AddOrUpdateAsync(Person per)
        {
            var stringObj = JsonSerializer.Serialize(per);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, per.Email), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }

        public void AddOrUpdate(Person per)
        {
            var stringObj = JsonSerializer.Serialize(per);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, per.Email), false))
            {
                sw.WriteAsync(stringObj);
            }
        }

        public async Task<Person> GetAsync(string login)
        {
            string stringObj = null;
            string filePath = Path.Combine(BaseFolder, login);

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sw = new StreamReader(filePath))
            {
                stringObj = await sw.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<Person>(stringObj);
        }

        public async Task<List<Person>> GetAllAsync()
        {
            var res = new List<Person>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = await sw.ReadToEndAsync();
                }

                res.Add(JsonSerializer.Deserialize<Person>(stringObj));
            }

            return res;
        }

        public List<Person> GetAll()
        {
            var res = new List<Person>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = sw.ReadToEnd();
                }

                res.Add(JsonSerializer.Deserialize<Person>(stringObj));
            }

            return res;
        }

        public async Task Erase(Person p)
        {
            string dest = Path.Combine(BaseFolder, p.Email);
            Task t = Task.Run(() => File.Delete(dest));
            await t;
        }

        public async void addPeople()
        {
            for (int i = 0; i < people.Length; ++i)
            {
                
                Person person = people[i];
                
                person.checkTheEmail();

                person.computeAge();
                person.computeIsAdult();
                person.computeSunSign();
                person.computeChineseZodiacSign();
                person.computeHasBirthday();

                person.checkTheAge();

                AddOrUpdate(person);
                    
            }
        }

        private static readonly Person[] people =
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