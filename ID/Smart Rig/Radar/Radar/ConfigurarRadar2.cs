using MetroFramework.Controls;
using pyosoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Radar
{
    public partial class frmConfigurarRadar2 : MetroFramework.Forms.MetroForm
    {
        public frmConfigurarRadar2()
        {
            InitializeComponent();
        }


        private void frmConfigurarRadar2_Load(object sender, EventArgs e)
        {

        }

        string nombreConfigurador = "\\RadarConfiguracion2.txt";

        private void btnAtrasAFE_Click(object sender, EventArgs e)
        {
            ConfigurarRadar1 configuraRadar1 = new ConfigurarRadar1();
            configuraRadar1.Show();
            this.Hide();
        }

        private void btnSiguienteDHT_Click(object sender, EventArgs e)
        {
            foreach (Control item in pnlDHT.Controls)
            {
                if (item is MetroTextBox)
                {
                    if (((MetroTextBox)item).Text == "")
                    {
                        MessageBox.Show("Debe ingresar todos los valores del cuadrante.");
                        return;
                    }
                }
            }

            List<string> valoresAguardar = new List<string>();

            foreach (Control item in pnlDHT.Controls)
            {
                if (item is MetroTextBox)
                {
                    if (((MetroTextBox)item).Text != "")
                    {
                        valoresAguardar.Add(item.Name + "_" + item.Text);
                    }
                }
            }

            int guardar = new AD_protocoloWits().guardarConfiguracionWits(nombreConfigurador, valoresAguardar);

            this.Hide();
        }
    }
}
