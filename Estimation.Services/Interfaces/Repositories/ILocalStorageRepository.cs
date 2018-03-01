using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Estimation.Services.Interfaces.Repositories
{
    public interface ILocalStorageRepository
    {
        DirectoryInfo GetCurrentDirectoryInfo();
    }
}
