using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    class FTPChecker
    {
        public string Version { get; set; }
        public string UpdateToken { get; set; }
        public string ZipToUpdate { get; set; }
        public string UpdateUrl { get; set; }
    }
}
