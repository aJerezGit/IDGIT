using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_Loss
{
    public partial class Form1 : Form
    {
        public double limiteInteriorLoss = -1;
        public double limiteExteriorLoss = 0;
        public double tiempo = 0;
        public double tasaPerdidaGanancia = 0;
        public double Wits0127 = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tiempo = tiempo + 1; // cada vez que el intervalo configurado en el timer desde el form aumenta un minuto cuando pasan 60000 ms
            tasaPerdidaGanancia = (Wits0127 / tiempo); // formula para determinar la tasa de perdida o ganancia

        }
    }
}
