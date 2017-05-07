using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace TZ
{
    [Serializable]
    [XmlType("Student")]
    public class StudetModel : INotifyPropertyChanged , IDataErrorInfo
    {
        #region переменые

        private int id;
        private string firstName;
        private string last;
        private int age;
        private string suffix;
        private int gender;
        private bool isSelected;

        #endregion

        #region свойства

        [XmlAttribute("Id")]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        
        [XmlElement("FirstName")]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        
        [XmlElement("Last")]
        public string Last
        {
            get
            {
                return last;
            }
            set
            {
                last = value;
                OnPropertyChanged( "Last" );
            }
        }
        
        [XmlElement("Age")]
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                int temp = age % 100;
                if (temp > 19) temp %= 10;
                switch (temp)
                {
                    case 1: { suffix = " год"; break; }
                    case 2: { suffix = " года"; break; }
                    case 3: { suffix = " года"; break; }
                    case 4: { suffix = " года"; break; }
                    default: { suffix = " лет"; break; }
                }
                OnPropertyChanged("Age");
                OnPropertyChanged("Suffix");
            }
        }
        
        [XmlElement("Gender")]

        public int Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                OnPropertyChanged( "Gender" );
            }
        }
        [XmlIgnore]

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        [XmlIgnore]
        public string Suffix
        {
            get
            {
                return suffix;
            }
        }

        #endregion

        #region уведомление щб изменении свойств INotifyPropertyChanged (для MVVM не обязательно)
        public event PropertyChangedEventHandler PropertyChanged;

        //нужен 4.5 т.к. [CallerMemberName] You need to install KB2468871 on top of .NET Framework 4 to be able to use caller info attributes
        public void OnPropertyChanged( [CallerMemberName] string prop = "" )
        {
            if( PropertyChanged != null )
                PropertyChanged( this , new PropertyChangedEventArgs( prop ) );
        }
        #endregion

        #region Обработка ошибок IDataErrorInfo
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Age":
                        {
                            //Обработка ошибок для свойства Age
                            if ((Age < 16) || (Age > 100))
                            {
                                error = "Возраст должен быть больше 16 и меньше 100";
                            }
                            break;
                        }
                    case "FirstName":
                        {
                            //Обработка ошибок для свойства FirstName
                            if (String.IsNullOrEmpty( FirstName ))
                            {
                                error = "Имя не может быть пустым";
                            }
                            break;
                        }
                    case "Last":
                        {
                            //Обработка ошибок для свойства Length
                            if (String.IsNullOrEmpty(Last)) { 
                                error = "Фамилия не может быть пустой";
                            }
                            break;
                        }
                }
                return error;
            }
        }
        #endregion

    }
}
