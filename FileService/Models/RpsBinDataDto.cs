using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileService.Models
{
    public class RpsBinDataDto
    {
        public int BinDataID { get; set; }

        public Byte[] Data { get; set; }

        public string FileName { get; set; }

    }
}
