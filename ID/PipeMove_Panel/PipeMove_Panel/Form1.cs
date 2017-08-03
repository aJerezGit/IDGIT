using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PipeMove_Panel
{
    public partial class Form1 : Form
    {
        private double Wits0110 = 0;//Canal correspondiente al 0110
        private double Wits0113 = 0;//Canal correspondiente al 0113
        private double Wits0117 = 0;//Canal correspondiente al 0113
        private double Wits0108 = 0;//Canal correspondiente al 0108
        private double Wits4781 = 0;//Canal correspondiente al Trip Speed
        private double Wits4782 = 0;//Canal correspondiente al Overpull
        private double VelocidadLimiteBajando = 0;//asignado al textbox del configurador
        private double VelocidadLimiteSubiendo = 0;//asignado al textbox del configurador
        private double viaje = 0;
        private double Wits0108Anterior = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TimerGeneral.Enabled = true; //Habilita el timer
            TimerGeneral.Start(); // Inicia el timer a contar
        }

        private void TimerGeneral_Tick(object sender, EventArgs e)
        {   //Comparadores para identificar que el viaje se encuentra bajando
            if (Wits0110>Wits0108)
            {   if (Wits0113==0)
                {
                    if (Wits0108 > Wits0108Anterior)
                    {
                        viaje = 1;
                    }
                    if (Wits0108Anterior > Wits0108)
                    {
                        viaje = 2;
                    }
                }
            }
           
            if (viaje==1)
            {
                if (Wits4781>VelocidadLimiteSubiendo)
                {
                    textBox1.Text = "Supera limite del viaje subiendo";
                }
                if (Wits0117 > 1)
                {
                    textBox1.Text = "Punto apretado";
                }
            }
            if (viaje==2)
            {
                if (Wits4781 > VelocidadLimiteBajando)
                {
                    textBox1.Text = "Supera limite del viaje bajando";
                }
            }
            Wits0108Anterior = Wits0108;

        }
    }
}
