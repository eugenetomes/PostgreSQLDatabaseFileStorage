using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreSQL.FileStorage.Core.Model
{
    public class FileContentBasicModel
    {
        public Guid Id { get; set; }
        public string Entity { get; set; }
        public string PrimaryKey { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string Filename { get; set; }
        public string? FileType { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
    }
}
