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
    public partial class ConfigurarRadar1 : MetroFramework.Forms.MetroForm
    {
        public ConfigurarRadar1()
        {
            InitializeComponent();
        }

        string nombreConfigurador = "\\RadarConfiguracion1.txt";

        private void frmConfigurarRadar1_Load(object sender, EventArgs e)
        {
            UsoControles("pnlHMSE2", false);
            UsoControles("pnlManiobras", false);
        }

        private void UsoControles(string nombreControl, bool habilita)
        {
            var panel = (Panel)this.Controls.Find(nombreControl, true).FirstOrDefault();

            foreach (Control item in panel.Controls)
            {
                item.Enabled = habilita;
            }
        }
        //TODO: putear a omar por que creo nuevos botones y ni siquiera los asigno a las funciones que tenian antes

        private void btnSiguienteBroca_Click(object sender, EventArgs e)
        {
            foreach (Control item in pnlHMSE1.Controls)
            {
                if (item is MetroTextBox)
                {
                    if (((MetroTextBox)item).Text == "" )
                    {
                        MessageBox.Show("Debe ingresar todos los valores del cuadrante.");
                        return;
                    }
                }
            }

            UsoControles("pnlHMSE1", false);
            UsoControles("pnlHMSE2", true);
            UsoControles("pnlManiobras", false);
        }

        private void btnAtrasHMSE_Click(object sender, EventArgs e)
        {    
            UsoControles("pnlManiobras", false);
            UsoControles("pnlHMSE1", true);
            UsoControles("pnlHMSE2", false);
        }

        private void btnSiguienteHMSE_Click(object sender, EventArgs e)
        {
            foreach (Control item in pnlHMSE2.Controls)
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

            UsoControles("pnlManiobras", true);
            UsoControles("pnlHMSE1", false);
            UsoControles("pnlHMSE2", false);
        }

        private void btnAtrasPipeMove_Click(object sender, EventArgs e)
        {
            UsoControles("pnlManiobras", false);
            UsoControles("pnlHMSE1", false);
            UsoControles("pnlHMSE2", true);
        }

        private void btnSiguientePipeMove_Click(object sender, EventArgs e)
        {
            foreach (Control item in pnlManiobras.Controls)
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

            foreach (Control item in pnlHMSE1.Controls)
            {
                if (item is MetroTextBox)
                {
                    if (((MetroTextBox)item).Text != "")
                    {
                        valoresAguardar.Add(item.Name + "_" + item.Text);
                    }
                }
            }
            foreach (Control item in pnlHMSE2.Controls)
            {
                if (item is MetroTextBox)
                {
                    if (((MetroTextBox)item).Text != "")
                    {
                        valoresAguardar.Add(item.Name + "_" + item.Text);
                    }
                }
            }
            foreach (Control item in pnlManiobras.Controls)
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


            frmConfigurarRadar2 configuraRadar2 = new frmConfigurarRadar2();
            configuraRadar2.Show();
            this.Hide();
        }
    }
}
