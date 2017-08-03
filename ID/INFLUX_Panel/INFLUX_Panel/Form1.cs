using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INFLUX_Panel
{
    public partial class Form1 : Form
    {
        private int tiempo; //Declaración de la variable tiempo
        private double Wits0127 = 0; //Correspondiente a canal 0127
        private double Wits0127Anterior = 0; //Canal 0127 anterior
        private double Wits0128 = 0; //Correspondiente a canal 0128
        private double Wits0128Anterior = 0; //Canal 0127 anterior
        private double Wits0121 = 0; //Correspondiente a canal 0121
        private double Wits0121Anterior = 0;//Canal 0121 anterior
        private double Wits0130 = 0; //Correspondiente a canal 0130
        private double Wits0130Anterior = 0;//Canal 0130 anterior
        private double tasaPerdidaGanancia = 0;// variable para determinar tasa de perdida o ganacia

        public Form1()
        {
            InitializeComponent();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            timerGeneral.Enabled = true; //Habilita el timer
            timerGeneral.Start(); // Inicia el timer a contar
        }
        private void timerGeneral_Tick(object sender, EventArgs e)
        {
            tiempo = tiempo + 1; // cada vez que el intervalo configurado en el timer desde el form aumenta un minuto cuando pasan 60000 ms
            tasaPerdidaGanancia = (Wits0127 / tiempo); // formula para determinar la tasa de perdida o ganancia
            // Primera situación disminución de presión.
            if (Wits0121Anterior > Wits0121)
            {   //Segunda condición aumentos
                if (Wits0127Anterior > Wits0127)
                {
                    if (Wits0128Anterior > Wits0128)
                    {
                        if (Wits0130Anterior == Wits0130)
                        {
                            if (tasaPerdidaGanancia > 1)
                            {
                                textBox1.Text = "Condición de influjo";
                            }
                        }
                    }
                }
            }
            Wits0121 = Wits0121Anterior;
            Wits0127 = Wits0127Anterior;
            Wits0128 = Wits0128Anterior;
            Wits0130 = Wits0130Anterior;
        }


    }
}
