using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OWASP10_2021.Areas.Admin.Models
{
    public class DirAndFiles
    {
        public List<string> Files { get; set; }
        public List<string> Directories { get; set; }

        public byte[] FileContents { get; set; }
    }
}
