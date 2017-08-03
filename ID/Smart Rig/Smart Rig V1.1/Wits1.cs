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
    public partial class Wits : MetroFramework.Forms.MetroForm
    {
        public Wits()
        {
            InitializeComponent();
        }

        bool seleccionado = false;
        string archivoConfigurador = "C:\\Pyosoft";
        string nombreConfigurador = "\\WitsConfiguracion.txt";

        private void btnTodo1_Click(object sender, EventArgs e)
        {
            SeleccionarControles();
        }

        private void SeleccionarControles()
        {
            if (seleccionado)
            {
                btnTodo1.Text = "Seleccionar todo";
                seleccionado = false;
            }
            else
            {
                btnTodo1.Text = "Deseleccionar todo";
                seleccionado = true;
            }
            foreach (Control item in pnlWits1.Controls)
            {
                if (item is CheckBox)
                {
                    ((CheckBox)item).Checked = seleccionado;
                }
            }

            foreach (Control item in pnlWits11.Controls)
            {
                if (item is CheckBox)
                {
                    ((CheckBox)item).Checked = seleccionado;
                }
            }

            foreach (Control item in pnlWits12.Controls)
            {
                if (item is CheckBox)
                {
                    ((CheckBox)item).Checked = seleccionado;
                }
            }

        }

        private void btnSiguiente1_Click(object sender, EventArgs e)
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
            foreach (Control item in pnlWits11.Controls)
            {
                if (item is CheckBox)
                {
                    if (((CheckBox)item).Checked)
                    {
                        valoresAguardar.Add(item.Name);
                    }
                }
            }
            foreach (Control item in pnlWits12.Controls)
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

            Wits2 ventanaWits2 = new Wits2();
            ventanaWits2.Show();
            this.Hide();
        }

        private void Wits_Load(object sender, EventArgs e)
        {
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

                        foreach (Control item in pnlWits11.Controls)
                        {
                            if (item.Name == check.Name)
                            {
                                ((CheckBox)item).Checked = true;
                            }
                        }

                        foreach (Control item in pnlWits12.Controls)
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
    }
}
