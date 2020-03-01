using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Services
{
    public interface IFileService: IService
    {
        IEnumerable<User> LoadAllUsers();
        void SaveToFile(List<User> users);
    }
}
