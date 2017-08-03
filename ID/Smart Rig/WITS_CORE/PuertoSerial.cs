using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class PuertoSerial
    {
        public String puerto { get; set; }
        public String baudios { get; set; }
        public String bitParidad { get; set; }

        public PuertoSerial() { }

        public PuertoSerial(String puerto, String baudios, String bitParidad)
        {
            this.puerto = puerto;
            this.baudios = baudios;
            this.bitParidad = bitParidad;
        }
    }
}
