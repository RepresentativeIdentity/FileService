using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileService.Models
{
    public class DSUserDto
    {
        public int SIDUser { get; set; }

        public string LoginName { get; set; }

        public string NTDomainName { get; set; }

        public string NTSID { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Active { get; set; }

        public string UserDesc { get; set; }

        public DateTime CreateDate { get; set; }

        public int LogonCount { get; set; }

        public int ConfigObjectDataID { get; set; }

        public string XML { get; set; }

        public Guid GUID { get; set; }

        public string Email { get; set; }

        public string Settings { get; set; }

        public int SystemInd { get; set; }

        public int IsView2012Ind { get; set; }

        public string FullName { get; set; }

        public string TelefonBroj { get; set; }

        public int DProtocolIDImenik { get; set; }


    }
}
