using LabWebAPI.Contracts.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWebAPI.Contracts.Services
{
    public interface IFileService
    {
        Task<string> AddFileAsync(Stream stream, string folderPath, string fileName);
        Task<DownloadFile> GetFileAsync(string path);
        Task DeleteFileAsync(string path);
    }
}
