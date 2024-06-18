using Proteus.Models;
using System.Collections.Generic;

namespace Proteus.ServicesMoria
{
    public interface IUploadService
    {
        void Create(UploadsViewModel data, int egykliosId, string Afm);
        string Delete(int uploadId);
        void Destroy(UploadsViewModel data);
        void DestroyFile(UploadsFilesViewModel data);
        IEnumerable<UploadsFilesViewModel> GetFiles(int uploadId);
        IEnumerable<UploadsViewModel> Read(int egykliosId, int schoolId);
        IEnumerable<UploadsViewModel> Read(int egykliosId, string Afm);
        UploadsViewModel Refresh(int entityId);
        void Update(UploadsViewModel data, int egykliosId, string Afm);
    }
}