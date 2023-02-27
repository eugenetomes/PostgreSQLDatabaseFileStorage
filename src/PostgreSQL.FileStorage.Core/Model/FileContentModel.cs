using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.FileStorage.Core.Model
{
    public class FileContentModel
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
