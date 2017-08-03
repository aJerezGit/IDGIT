using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loss
{
    public partial class Form1 : Form
    {
        
        double Wits0127 = 27; //Correspondiente a canal 0127
        double tiempo = 0;

        public Form1()
        {
            InitializeComponent();
        }



        public void Form1_Load(object sender, EventArgs e)
        {
            timerGeneral.Enabled = true; // habilita el timer
            timerGeneral.Start(); // Inicia el timer
        }

        private void timerGeneral_Tick(object sender, EventArgs e)
        {

            tiempo = tiempo + 1; // cada vez que el intervalo configurado en el timer desde el form aumenta un minuto cuando pasan 60000 ms
            double tasaPerdidaGanancia = 0; // variable para determinar tasa de perdida o ganacia
            tasaPerdidaGanancia = (Wits0127 / tiempo); // formula para determinar la tasa de perdida o ganancia

            // PROTOCOLO DE ALARMA LOSS()
            if (tasaPerdidaGanancia <= -1) // Comparación para determinar situación de peligro
            {
                textBox1.Text = "Pérdida de lodo";
            }


        }
    }
}