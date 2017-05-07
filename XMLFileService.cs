using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TZ
{
    class XMLFileService : IFileService
    {
        /// <summary>
        /// открытие и сериализация
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<StudetModel> Open(string filename)
        {
            List<StudetModel> studs = new List<StudetModel>();
            var unSerializer = new XmlSerializer(typeof(List<StudetModel>), new XmlRootAttribute("Students"));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                studs = unSerializer.Deserialize(fs) as List<StudetModel>;
            }

            return studs;
        }

        /// <summary>
        /// сериализация и сохранение потока в файл
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="studsList"></param>
        public void Save(string filename, List<StudetModel> studsList)
        {
            var serializer = new XmlSerializer(typeof(List<StudetModel>), new XmlRootAttribute("Students"));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                serializer.Serialize(fs, studsList);
            }
        }
    }
}
