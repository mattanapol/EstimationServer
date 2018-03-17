using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Estimation.Interface.Repositories;

namespace Estimation.DataAccess.Repositories
{
    public class LocalStorageRepository : ILocalStorageRepository
    {
        public DirectoryInfo GetCurrentDirectoryInfo()
        {
            throw new NotImplementedException();
        }
    }
}
