using Microsoft.Win32;
using System.Windows;

namespace TZ
{
    class DefaultDialogService : IDialogService
    {
        /// <summary>
        /// диалоговое окно открытия файла 
        /// </summary>
        public string FilePath { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
        /// <summary>
        /// диалоговое окно сохранения файла
        /// </summary>
        /// <returns>путь к файлу</returns>
        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files | *.xml";
            saveFileDialog.DefaultExt = "xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }
        /// <summary>
        /// окно сообщений
        /// </summary>
        /// <param name="message">some string</param>
        /// <param name="type">4 - MessageBoxResult.YesNo, !=4 MessageBoxButton.OK</param>
        /// <returns>0 - MessageBoxResult.Yes</returns>
        public int ShowMessage(string message, int type)
        {
            MessageBoxButton t = MessageBoxButton.OK;
            if (type == 4) t = MessageBoxButton.YesNo;    
            return (MessageBox.Show(message, "", t) == MessageBoxResult.Yes) ? 0 : -1;
        }
    }
}
