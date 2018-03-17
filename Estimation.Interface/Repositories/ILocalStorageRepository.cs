using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Estimation.Interface.Repositories
{
    public interface ILocalStorageRepository
    {
        DirectoryInfo GetCurrentDirectoryInfo();
    }
}
