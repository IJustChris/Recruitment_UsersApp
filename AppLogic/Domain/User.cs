using AppLogic.Extensions;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    [Serializable()]
    public class User : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        private Dictionary<string, string> _errors = new Dictionary<string, string>();
        public bool HasErrors => HasAnyErrors();

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName{ get; set; }
        [Required]
        public string StreetName{ get; set; }
        [Required]
        public string HouseNumber{ get; set; }
        public string ApartmentNumber{ get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DayofBirth { get; set; }


        public User()
        {
            DayofBirth = DateTime.Now.AddYears(-18);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(FirstName), FirstName);
            info.AddValue(nameof(LastName), LastName);
            info.AddValue(nameof(StreetName), StreetName);
            info.AddValue(nameof(HouseNumber), HouseNumber);
            info.AddValue(nameof(ApartmentNumber), ApartmentNumber);
            info.AddValue(nameof(PostalCode), PostalCode);
            info.AddValue(nameof(PhoneNumber), PhoneNumber);
            info.AddValue(nameof(DayofBirth), DayofBirth);
        }

        public User(SerializationInfo info, StreamingContext context)
        {
            FirstName = (string)info.GetValue(nameof(FirstName), typeof(string));
            LastName = (string)info.GetValue(nameof(LastName), typeof(string));
            StreetName = (string)info.GetValue(nameof(StreetName), typeof(string));
            HouseNumber = (string)info.GetValue(nameof(HouseNumber), typeof(string));
            ApartmentNumber = (string)info.GetValue(nameof(ApartmentNumber), typeof(string));
            PostalCode = (string)info.GetValue(nameof(PostalCode), typeof(string));
            PhoneNumber = (string)info.GetValue(nameof(PhoneNumber), typeof(string));
            DayofBirth = (DateTime)info.GetValue(nameof(DayofBirth), typeof(DateTime));
        }

        public string Error => null;
        public string this[string propertyName]
        {
            get
            {
                CheckForErrors();
                Trace.WriteLine($"Errors: {_errors.Count}");
                if (_errors.ContainsKey(propertyName))
                    return _errors[propertyName];
                else
                    return string.Empty;
            }
        }

        private bool HasAnyErrors()
        {
            CheckForErrors();
            return _errors.Any();
        }

        private void CheckForErrors()
        {
            _errors.Clear();

            var properties = GetType()
                .GetProperties()
                .Where(x => x.IsDefined(typeof(RequiredAttribute),true));

            foreach (var prop in properties)
            {
                switch (prop.Name)
                {
                    case nameof(DayofBirth):

                        if (DayofBirth > DateTime.Now)
                            _errors.Add(prop.Name, "Provide valid day of birth");
                        break;

                    default:
                        if (prop.GetValue(this) == null || prop.GetValue(this).ToString().Empty())
                            _errors.Add(prop.Name, "Field cannot be empty");
                        break;
                }
            }
        }
#pragma warning disable CS0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning enable CS0067
    }
}
