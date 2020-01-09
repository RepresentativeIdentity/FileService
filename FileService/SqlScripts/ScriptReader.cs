using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileService.SqlScripts
{
    public class ScriptReader
    {

        public string Read(string path)
        {
            FileInfo file = new FileInfo(path);

            return file.OpenText().ReadToEnd();
        }
    }
}
