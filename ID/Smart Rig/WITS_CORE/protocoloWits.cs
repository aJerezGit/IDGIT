using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pyosoft
{
    public class protocoloWits
    {
        
        public string WITpaqueteNumero { get; set; }
        public string WITitem { get; set; }
        public string WITvalor { get; set; }
        public DateTime WITfecha { get; set; }

        public protocoloWits() { }

        public protocoloWits(string WITpaqueteNumero, string WITitem, string WITvalor, DateTime WITfecha)
        {
            this.WITpaqueteNumero = WITpaqueteNumero;
            this.WITitem = WITitem;
            this.WITvalor = WITvalor;
            this.WITfecha = WITfecha;
        }
    }
}
