using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.Services
{
    public interface ILocaleStorageService : IFileService, ICreateDirectory { }
    public interface ICreateDirectory
    {
        Task CreateDirectoryAsync(string folderPath);
    }
}
