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

        private void btnSiguienteAFE_Click(object sender, EventArgs e)
        {
            foreach (Control item in pnlDHT.Controls)
            {
                if (item is Panel)
                {
                    foreach (Control itemSubPanel in item.Controls)
                    {
                        if (itemSubPanel is MetroTextBox)
                        {
                            if (((MetroTextBox)itemSubPanel).Text == "")
                            {
                                MessageBox.Show("Se debe configurar todas las variables del panel AFE");
                                return;
                            }
                        }
                    }
                }
            }

            List<string> valoresAguardar = new List<string>();

            foreach (Control item in pnlDHT.Controls)
            {
                if (item is Panel)
                {
                    foreach (Control itemSubPanel in item.Controls)
                    {
                        if (itemSubPanel is MetroTextBox)
                        {
                            if (((MetroTextBox)itemSubPanel).Text != "")
                            {
                                valoresAguardar.Add(itemSubPanel.Name + "_" + itemSubPanel.Text);
                            }
                        }
                    }
                }
            }

            int guardar = new AD_protocoloWits().guardarConfiguracionWits(nombreConfigurador, valoresAguardar);


            this.Hide();
        }
    }
}
