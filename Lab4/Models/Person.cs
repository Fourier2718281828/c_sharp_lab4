using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Lab3.Exceptions;

namespace Lab2.Models
{
    internal class Person
    {
        #region Fields
        private String          _name;
        private String          _surname;
        private String          _email;
        private DateTime?       _dateOfBirth;
        private bool            _isAdult;
        private WesternZodiac   _sunSign;
        private ChineseZodiac   _chineseSign;
        private bool            _hasBirthday;
        public short?           _age;
        #endregion

        #region Constants
        const short ADULT_AGE = 18;
        #endregion

        #region ZodiacSigns
        public enum WesternZodiac
        {
            Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn, Aquarius, Pisces
        }
        public enum ChineseZodiac
        {
            Rat, Ox, Tiger, Rabbit, Dragon, Snake, Horse, Goat, Monkey, Rooster, Dog, Pig
        }

        private enum Month
        {
            Jan = 1, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec
        }
        #endregion

        #region Constructors
        public Person(string name, string surname, string email, DateTime? dateOfBirth)
        {
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;

            return;
        }

        public Person(string surname, string email) 
            : this(null, surname, email, null) 
        { 
            return; 
        }

        public Person(string name, string surname, DateTime? dateOfBirth)
            : this(name, surname, null, dateOfBirth)
        {
            return;
        }

        public Person()
            : this(null, null, null, null)
        {
            return;
        }
        #endregion

        #region GeneralProperies
        public String Name
        {
            get => _name;
            set => _name = value;
        }

        public String Surname
        {
            get => _surname;
            set => _surname = value;
        }

        public String Email
        {
            get => _email; 
            set => _email = value;
        }

        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value;
                
            
        }
        #endregion

        #region ComputedProperties
        public bool IsAdult                => _isAdult;
        public WesternZodiac SunSign       => _sunSign;
        public ChineseZodiac ChineseSign   => _chineseSign;
        public bool IsBirthday             => _hasBirthday;
        public short? Age                   => _age;
        #endregion

        #region Methods
        public void computeIsAdult()
        {
            _isAdult = _age >= ADULT_AGE;
        } 
        public void computeAge()
        {
            if (DateOfBirth == null) _age = null;

            _age = (short)(DateTime.Now.Year - _dateOfBirth.Value.Year + 
                   ((
                     DateTime.Now.Month  >= _dateOfBirth.Value.Month &&
                     DateTime.Now.Day    >= _dateOfBirth.Value.Day)  ||
                     DateTime.Now.Year   == _dateOfBirth.Value.Year  ? 0 : -1
                   ));
        }

        public void computeHasBirthday()
        {
            _hasBirthday = (DateTime.Now.Day == _dateOfBirth.Value.Day) && (DateTime.Now.Month == _dateOfBirth.Value.Month);
        }

        public void computeSunSign() => _sunSign = getWesternZodiacSign();     

        public void computeChineseZodiacSign()
        {
            _chineseSign = (ChineseZodiac)((_dateOfBirth.Value.Year - 4) % 12);
        }
        private WesternZodiac getWesternZodiacSign()
        {
            if (isIn(Month.Mar, 21, Month.Apr, 19)) return WesternZodiac.Aries;
            if (isIn(Month.Apr, 20, Month.May, 20)) return WesternZodiac.Taurus;
            if (isIn(Month.May, 21, Month.Jun, 21)) return WesternZodiac.Gemini;
            if (isIn(Month.Jun, 22, Month.Jul, 22)) return WesternZodiac.Cancer;
            if (isIn(Month.Jul, 23, Month.Aug, 22)) return WesternZodiac.Leo;
            if (isIn(Month.Aug, 23, Month.Sep, 22)) return WesternZodiac.Virgo;
            if (isIn(Month.Sep, 23, Month.Oct, 22)) return WesternZodiac.Libra;
            if (isIn(Month.Oct, 23, Month.Nov, 22)) return WesternZodiac.Scorpio;
            if (isIn(Month.Nov, 23, Month.Dec, 21)) return WesternZodiac.Sagittarius;
            //if (isIn(Month.Dec, 22, Month.Jan, 19)) return WesternZodiac.Capricorn;
            if (isIn(Month.Jan, 20, Month.Feb, 18)) return WesternZodiac.Aquarius;
            if (isIn(Month.Feb, 19, Month.Mar, 20)) return WesternZodiac.Pisces;

            return WesternZodiac.Capricorn;
        }
        private bool isIn(Month m1, int d1, Month m2, int d2)
        {
            return _dateOfBirth >= new DateTime(_dateOfBirth.Value.Year, (int)m1, d1)
                && _dateOfBirth <= new DateTime(_dateOfBirth.Value.Year, (int)m2, d2);
        }

        public void checkTheAge()
        {
            if (Age == null) return;

            if (DateOfBirth.Value.Date > DateTime.Now)
                throw new FutureDateException($"The person {Name ?? ""} hasn't been born yet");
            if (Age >= 135)
                throw new PastDateException($"The person {Name ?? ""} is too old. Age: {Age}");
        }

        public void checkTheEmail()
        {
            if (Email == null) return;

            Regex email = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (!email.IsMatch(Email))
                throw new BadEmailException($"Illigal email format.");
        }
        #endregion
    }
}
