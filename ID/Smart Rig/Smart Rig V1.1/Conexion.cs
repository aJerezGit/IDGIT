using PYOLOGGER;
using Radar;
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
using WindowsFormsApplication1;

namespace Smart_Rig_V1._1
{
    public partial class frmConexion : MetroFramework.Forms.MetroForm
    {
        public frmConexion()
        {
            InitializeComponent();
            this.StyleManager = EstiloForms;
        }

        public PuertoSerial puertoSeleccionado { get; set; }
        string path = @"C:\\Pyosoft\\Data.txt";
        string configurador1 = @"C:\\Pyosoft\\WitsConfiguracion.txt";
        string configurador2 = @"C:\\Pyosoft\\WitsConfiguracion2.txt";
        string configurador3  = @"C:\\Pyosoft\\WitsConfiguracion3.txt";
        string configurador4 = @"C:\\Pyosoft\\WitsConfiguracion4.txt";
        string ConfiguradorRadar1 = "\\RadarConfiguracion1.txt";
        string ConfiguradorRadar2 = "\\RadarConfiguracion2.txt";


        private void frmConexion_Load(object sender, EventArgs e)
        {
            //TODO recordar quitar esta trampa
            frmConfigurarRadar2 radar2 = new frmConfigurarRadar2();
            //radar2.Show();

            if (File.Exists(path))
            {
                if (File.Exists(configurador1))
                {
                    if (File.Exists(configurador2))
                    {
                        if (File.Exists(configurador3))
                        {
                            if (File.Exists(configurador4))
                            {
                                if (File.Exists(ConfiguradorRadar1))
                                {
                                    //frmConfigurarRadar2 radar2 = new frmConfigurarRadar2();
                                    radar2.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    if (File.Exists(ConfiguradorRadar2))
                                    {
                                        ConfigurarRadar1 radar1 = new ConfigurarRadar1();
                                        radar1.Show();
                                        this.Hide();
                                    }
                                }


                                Tiempo_Real tiempoReal = new Tiempo_Real();
                                tiempoReal.Show();
                                this.Hide();
                            }
                            else
                            {
                                Wits4 ventanaWits4 = new Wits4();
                                ventanaWits4.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            Wits3 ventanaWits3 = new Wits3();
                            ventanaWits3.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        Wits2 ventanaWits2 = new Wits2();
                        ventanaWits2.Show();
                        this.Hide();
                    }
                }
                else
                {
                    Wits ventanaWits = new Wits();
                    ventanaWits.Show();
                    this.Hide();
                }
            }
            else if (File.Exists(path) && (File.ReadLines(path).Skip(2).Take(1).First() == null || File.ReadLines(path).Skip(2).Take(1).First() != ""))
            {
                MessageBox.Show("Falta parte de la configuracion del puerto a conectar, reingrese la informacion de este");
            }
                foreach (Control item in pnlBD.Controls)
                {
                    item.Enabled = false;
                }

                cargarPuertosCom();
                cargarVelocidades();
                cbVelocidad.SelectedIndex = 0;
            
        }

        private void btnConexion_Click(object sender, EventArgs e)
        {
            puertoSeleccionado = seleccionaPuertoSerial();

            //recordar cambiar este iff es diferente no igual
            if (puertoSeleccionado.puerto != null && puertoSeleccionado.baudios != null)
            {

                foreach (Control item in pnlConexion.Controls)
                {
                    item.Enabled = false;
                }

                foreach (Control item in pnlBD.Controls)
                {
                    item.Enabled = true;
                }
            }
        }

        public void cargarPuertosCom()
        {
            foreach (string puertosDisponibles in System.IO.Ports.SerialPort.GetPortNames())
            {
                cbPuertos.Items.Add(puertosDisponibles);
            }
        }

        public void cargarVelocidades()
        {
            cbVelocidad.Items.Add("9600");
            cbVelocidad.Items.Add("56200");
            cbVelocidad.Items.Add("115200");
        }

        public PuertoSerial seleccionaPuertoSerial()
        {
            PuertoSerial puerto = new PuertoSerial();
            if (cbPuertos.SelectedItem != null)
            {
                puerto.puerto = cbPuertos.SelectedItem.ToString();
                puerto.baudios = cbVelocidad.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Debe seleccionar el puerto a comunicar.");
            }
            return puerto;
        }

        private void btnBD_Click(object sender, EventArgs e)
        {
            if (txtNombreBD.Text != null && txtNombreBD.Text != "")
            {
                string baseDatos = "C:\\Pyosoft\\";
                baseDatos += txtNombreBD.Text + ".sqlite";

                if (!File.Exists(baseDatos))
                {
                    MessageBox.Show("Creando la base de datos");
                    int conexion = new sqlConexion().createDatabase(txtNombreBD.Text, baseDatos);
                    if (conexion == 1)
                        MessageBox.Show("Base de datos creada exitosamente");

                    //this.Close();
                }
                else
                {
                    //int configurado = new sqlConexion().necesitaConfiguracion();
                    //if (configurado != 1)
                    //{
                        MessageBox.Show("Base de datos ya creada, Configure la información correspondiente");

                        //dvModBus.DataSource = new sqlConexion().modBusData();
                        //dvModBus.Columns[0].ReadOnly = true;
                        //dvModBus.Columns[0].DefaultCellStyle.ForeColor = System.Drawing.Color.Gray;
                    //}
                }

                GuardarPuerto();

                Wits ventanaWits = new Wits();
                ventanaWits.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Debe asignar un nombre a la base de datos a crear.");
            }
            
        }

        public void GuardarPuerto()
        {            
            if (File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(puertoSeleccionado.puerto);
                    sw.WriteLine(puertoSeleccionado.baudios);
                }
            }
        }
    }
}
