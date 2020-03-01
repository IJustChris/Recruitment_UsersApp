using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AppLogic.Services
{
    public class FileService : IFileService
    {
        private static readonly string _directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RecruitmentUsersApp";
        private static readonly string _filename = "Users.xml";
        private static readonly FileInfo _file = new FileInfo(_directory + $@"\{_filename}");

        public FileService()
        {
            ValidateAndFixPath();
        }
        public void SaveToFile(List<User> users)
        {
            ValidateAndFixPath();
            _file.Delete();
            using (Stream stream = new FileStream(_file.FullName, FileMode.OpenOrCreate,FileAccess.Write,FileShare.None))
            {
                var serializer = new XmlSerializer(typeof(List<User>));
                serializer.Serialize(stream, users);
            }
        }

        public IEnumerable<User> LoadAllUsers()
        {
            ValidateAndFixPath();

            if (!_file.Exists)
            {
                _file.Create();
                return new List<User>();
            }
            else
            {
                using (Stream stream = File.OpenRead(_file.FullName))
                {
                    var serializer = new XmlSerializer(typeof(List<User>));
                    return (List<User>)serializer.Deserialize(stream);
                }
            }
        }

        private void ValidateAndFixPath()
        {
            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);
 
        }
    }
}
