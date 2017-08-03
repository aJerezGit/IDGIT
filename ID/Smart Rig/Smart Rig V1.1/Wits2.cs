using pyosoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart_Rig_V1._1
{
    public partial class Wits2 : MetroFramework.Forms.MetroForm
    {
        public Wits2()
        {
            InitializeComponent();
        }

        bool seleccionado = false;
        string archivoConfigurador = "C:\\Pyosoft";
        string nombreConfigurador = "\\WitsConfiguracion2.txt";


        private void Wits2_Load(object sender, EventArgs e)
        {
            UsoControles("pnlWits2", false);
            UsoControles("pnlWits3", false);

            string archivoALeer = archivoConfigurador + nombreConfigurador;
            if (File.Exists(archivoALeer))
            {
                string[] items = File.ReadAllLines(archivoALeer);

                foreach (string itemAseleccionar in items)
                {
                    CheckBox check = (CheckBox)this.Controls.Find(itemAseleccionar, true).FirstOrDefault();

                    if (check != null)
                    {
                        foreach (Control item in pnlWits1.Controls)
                        {
                            if (item is CheckBox)
                            {
                                if (item.Name == check.Name)
                                {
                                    ((CheckBox)item).Checked = true;
                                }
                            }
                        }

                        foreach (Control item in pnlWits2.Controls)
                        {
                            if (item.Name == check.Name)
                            {
                                ((CheckBox)item).Checked = true;
                            }
                        }

                        foreach (Control item in pnlWits3.Controls)
                        {
                            if (item.Name == check.Name)
                            {
                                ((CheckBox)item).Checked = true;
                            }
                        }
                    }
                }
            }

        }
        //TO DO: hay una refactorizacion importante a tener en cuenta de la busqueda de controles para hacerlo una clase generica y utilizarla sobre toda la aplicacion.
        private void UsoControles(string nombreControl, bool habilita)
        {
            var panel = (Panel)this.Controls.Find(nombreControl, true).FirstOrDefault();

            foreach (Control item in panel.Controls)
            {
                item.Enabled = habilita;
            }
        }

        private void btnAtras1_Click(object sender, EventArgs e)
        {
            Wits ventanaWits1 = new Wits();
            ventanaWits1.Show();
            this.Hide();
        }

        private void btnTodo2_Click(object sender, EventArgs e)
        {
            SeleccionarControles("btnTodo1","pnlWits1");
        }

        private void btnTodo2_Click_1(object sender, EventArgs e)
        {
            SeleccionarControles("btnTodo2", "pnlWits2");
        }

        private void btnTodo3_Click(object sender, EventArgs e)
        {
            SeleccionarControles("btnTodo3", "pnlWits3");
        }

        private void SeleccionarControles(string nombreBoton, string nombrePanel)
        {
            Button boton = (Button)this.Controls.Find(nombreBoton, true).FirstOrDefault();

            if (seleccionado)
            {
                boton.Text = "Seleccionar todo";
                seleccionado = false;
            }
            else
            {
                boton.Text = "Deseleccionar todo";
                seleccionado = true;
            }
            Panel panel = (Panel)this.Controls.Find(nombrePanel, true).FirstOrDefault();

            foreach (Control item in panel.Controls)
            {
                if (item is CheckBox)
                {
                    ((CheckBox)item).Checked = seleccionado;
                }
            }
        }

        private void btnSiguiente2_Click(object sender, EventArgs e)
        {
            UsoControles("pnlWits1", false);
            UsoControles("pnlWits2", true);
            UsoControles("pnlWits3", false);
        }

        private void btnSiguiente7_Click(object sender, EventArgs e)
        {
            UsoControles("pnlWits1", false);
            UsoControles("pnlWits2", false);
            UsoControles("pnlWits3", true);

        }

        private void btnSiguiente8_Click(object sender, EventArgs e)
        {
            List<string> valoresAguardar = new List<string>();

            foreach (Control item in pnlWits1.Controls)
            {
                if (item is CheckBox)
                {
                    if (((CheckBox)item).Checked)
                    {
                        valoresAguardar.Add(item.Name);
                    }
                }
            }
            foreach (Control item in pnlWits2.Controls)
            {
                if (item is CheckBox)
                {
                    if (((CheckBox)item).Checked)
                    {
                        valoresAguardar.Add(item.Name);
                    }
                }
            }
            foreach (Control item in pnlWits3.Controls)
            {
                if (item is CheckBox)
                {
                    if (((CheckBox)item).Checked)
                    {
                        valoresAguardar.Add(item.Name);
                    }
                }
            }

            int guardar = new AD_protocoloWits().guardarConfiguracionWits(nombreConfigurador, valoresAguardar);

            Wits3 ventanaWits3 = new Wits3();
            ventanaWits3.Show();
            this.Hide();
        }

        private void btnAtras2_Click(object sender, EventArgs e)
        {
            UsoControles("pnlWits1", true);
            UsoControles("pnlWits2", false);
            UsoControles("pnlWits3", false);
        }

        private void btnAtras3_Click(object sender, EventArgs e)
        {
            UsoControles("pnlWits1", false);
            UsoControles("pnlWits2", true);
            UsoControles("pnlWits3", false);
        }
    }
    
}
