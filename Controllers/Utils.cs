using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Permutante
{
    class Utils
    {
        public byte[]  SerializePDF()
        {
            string path = @"C:\CurriculumPT.pdf";
            var bytesData = File.ReadAllBytes(path);

            return bytesData;


        }
    }
}
