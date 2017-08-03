using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPT_Panel
{
    public partial class Form1 : Form
    {
        double tiempoEstimadoPerforacion = 0; //Valor ingresado por textbox NPT AFE
        double tiempoEjecucionPerforacion = 0; // Valor real de ejecucion
        double tiempoEstimadoMove = 0; //Valor ingresado por textbox NPT AFE
        double tiempoEjecucionMove = 0; // Valor real de ejecucion
        double tiempoNPT = 0;// Valor del tiempo de demora operacion no planeada
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timerGeneral.Enabled = true; // Se habilita el timer
            timerGeneral.Start(); // Se inicia el conteo del timer
        }
        private void timerGeneral_Tick(object sender, EventArgs e)
        {
            tiempoEjecucionPerforacion = tiempoEjecucionPerforacion + 1; //Cada evento, cada minuto el tiempo de ejecucion aumenta
            if (tiempoEjecucionPerforacion > tiempoEstimadoPerforacion)// Se valida si el tiempo esta dentro de lo acordado
            {
                tiempoNPT = tiempoNPT++;//Se aumenta el tiempo no planeado
            }
           
        }
        private void timer1_Tick(object sender, EventArgs e) //TICK timer move
        {
            tiempoEjecucionMove = tiempoEjecucionMove + 1; //Cada evento, cada minuto el tiempo de ejecucion aumenta
            if (tiempoEjecucionMove > tiempoEstimadoMove)// Se valida si el tiempo esta dentro de lo acordado
            {
                tiempoNPT = tiempoNPT++;//Se aumenta el tiempo no planeado
            }

        }


    }
}
