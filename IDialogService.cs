namespace TZ
{
    interface IDialogService
    {
        int ShowMessage(string message, int type);   // показ сообщения
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenFileDialog();  // открытие файла
        bool SaveFileDialog();  // сохранение файла
    }
}
