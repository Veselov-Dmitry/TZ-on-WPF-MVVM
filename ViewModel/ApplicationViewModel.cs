using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;
using System;

namespace TZ
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private StudetModel selectedStudent;

        IFileService fileService;
        IDialogService dialogService;

        public ObservableCollection<StudetModel> Students { get; set; }
        public StudetModel SelectedStudent
        {
            get
            {
                return selectedStudent;
            }
            set
            {
                selectedStudent = value;
                OnPropertyChanged("SelectedStudent");
            }
        }

        #region команда добавления нового объекта
        private CommandModel addCommand;
        public CommandModel AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new CommandModel(obj =>
                    {
                        StudetModel stud = new StudetModel();
                        Students.Insert(0, stud);
                        SelectedStudent = stud;
                    }));
            }
        }
        #endregion

        #region команда удаления объекта
        private CommandModel removeCommand;
        public CommandModel RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new CommandModel(obj =>
                    {
                        int count = 0;
                        foreach (StudetModel op in Students)
                            if (op.IsSelected) count++;
                        if (count == 0) return;
                        if (0 != dialogService.ShowMessage("Удалить выделенные (" + count + ") записи", 4))//4 - MessageBoxButton.YesNo return 0 - Yes
                            return;
                            count = Students.Count;
                            for (int i = 0; i < count; i++)
                            {
                                if (Students[i].IsSelected)
                                {
                                    Students.Remove(Students[i]);
                                    count = Students.Count;
                                    i--;
                                }
                            }
                    },
                    (obj) => Students.Count > 0));//условие для выполнения
            }
        }
        #endregion

        #region команда сохранения файла
        private CommandModel saveCommand;
        public CommandModel SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new CommandModel(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, Students.ToList());
                              //dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message,0);
                      }
                  }));
            }
        }
        #endregion

        #region команда открытия файла
        private CommandModel openCommand;
        public CommandModel OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new CommandModel(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var phones = fileService.Open(dialogService.FilePath);
                              Students.Clear();
                              foreach (var op in phones)
                                  Students.Add(op);
                              //dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message,0);
                      }
                  }));
            }
        }
        #endregion

        #region конструктор ApplicationViewModel
        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;

            Students = new ObservableCollection<StudetModel>();/*TEST
            {
                new StudetModel {Id=0, FirstName="Robert", Last="Jarman", Age=21, Gender=0 },
                new StudetModel {Id=1, FirstName="Leona", Last="Menders", Age =20, Gender=1 },
                new StudetModel {Id=2, FirstName="Helen", Last="Wilson", Age=21, Gender=1 },
                new StudetModel {Id=3, FirstName="John", Last="Smith", Age=22, Gender=0 }
            };/**/
        }
        #endregion+

        #region уведомление об изменении в списке объектов

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}


