using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIGHT_Panel
{
    public partial class Form1 : Form
    {
        private double Wits0130 = 0;//Canal correspondiente al 0130
        private double Wits0130Anterior = 0;//Canal correspondiente al 0110
        public double Wits0121 = 0;//Canal correspondiente al 0121
        public double Wits0118 = 0;//Canal correspondiente al 0118
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TimerGeneral.Enabled = true; //Habilita el timer
            TimerGeneral.Start(); // Inicia el timer a contar
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double Wits0121Limite = Wits0121 + 50;
            double Wits0118Limite = Wits0118 + 1000;

            if (Wits0130 == Wits0130Anterior)
            {
                if (Wits0121 >= Wits0121Limite)
                {
                    textBox1.Text = "Aumento de presión";
                }
            }

            if (Wits0118 > Wits0118Limite)
            {
                textBox1.Text = "Aumento de torque";
            }
        }
    }
}
