using pyosoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smart_Rig_V1._1
{
    public partial class Tiempo_Real : MetroFramework.Forms.MetroForm
    {
        public bool leer = false;
        string dataConexion = "C:\\Pyosoft\\Data.txt";
        string cadenaConexion = string.Empty;
        static string cadenaConexionMySql = "Server=pyosoftprb.cfsh1lwjn4sj.us-west-2.rds.amazonaws.com;Port=3306;Database=";
        string userNpass = "user=pyosoft;password=PY050FTDB;";
        string MysqlConexion = string.Empty;

        public bool guarda = false;
        bool mensajeMostrar = true;

        Thread tr;

        //variables de conexion
        string puertoGeneral = string.Empty;
        int baudGeneral = 0;

        public Tiempo_Real()
        {
            InitializeComponent();
        }

        //codigo para cerrar messagebox despues de tiempo
        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }

            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }

            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        private void bntIniciar_Click(object sender, EventArgs e)
        {
            if (!leer)
            {
                if (File.Exists(dataConexion))
                {
                    puertoGeneral = File.ReadLines(dataConexion).Skip(2).Take(1).First();
                    baudGeneral = int.Parse(File.ReadLines(dataConexion).Skip(3).Take(1).First());
                    leer = true;
                    bntIniciar.Text = "Parar";
                    IniciarProceso(puertoGeneral, baudGeneral);
                }
            }
            else
            {
                bntIniciar.Text = "Iniciar";
                leer = false;
            }
        }

        public void IniciarProceso(string puertoGeneral, int baudGeneral)
        {
            if (File.Exists(dataConexion))
            {
                string dataBaseName = File.ReadLines(dataConexion).Skip(1).Take(1).First();
               // lblBaseDatos.Text = dataBaseName;
                cadenaConexion = File.ReadLines(dataConexion).First();
                MysqlConexion = cadenaConexionMySql + dataBaseName + ";" + userNpass;
            }

            try
            {
                PuertoSerial.PortName = puertoGeneral; //puertoSerial.puertoSeleccionado.puerto;
                PuertoSerial.BaudRate = baudGeneral; //int.Parse(puertoSerial.puertoSeleccionado.baudios);
                PuertoSerial.Open();

                while (leer)
                {

                    string[] separadorItems = { "&&", "!!" };
                    string[] stringSeparators = new string[] { "\r\n" };

                    int contador = 0;
                    Thread.Sleep(100);
                    string datosSerial = "";
                    mensajeMostrar = true;
                    //Thread.Sleep(250);
                    datosSerial = PuertoSerial.ReadExisting();
                    lblSerialStatus.Text = "Connected";

                    string[] valor1 = datosSerial.Split(separadorItems, StringSplitOptions.None);
                    foreach (string l in valor1)
                    {
                        if (l != "" && l.Length > 5)
                        {
                            Tuple<List<protocoloWits>, int> resultado = new AD_protocoloWits().FormateaWits(cadenaConexion, MysqlConexion, l);
                            pintarElemento(resultado.Item1);
                            if (resultado.Item2 != 0)
                            {
                                //if (resultado.Item2 < 33)
                                //{ lblTotalDatos.ForeColor = System.Drawing.Color.Red; }
                                //else if (resultado.Item2 > 34 && resultado.Item2 < 66)
                                //{ lblTotalDatos.ForeColor = System.Drawing.Color.Yellow; }
                                //else
                                //{ lblTotalDatos.ForeColor = System.Drawing.Color.Green; }
                                //lblTotalDatos.Text = resultado.Item2.ToString() + "%";

                                mensajeMostrar = true;
                                lblInternetStatus.Text = "Conencted";

                            }
                            else {
                                lblInternetStatus.Text = "Disconnected";
                            }
                        }

                        Application.DoEvents();
                    }

                    contador += 1;
                    if (contador == 2)
                    {
                        PuertoSerial.DiscardInBuffer();
                        contador = 0;
                    }

                    datosSerial = "";

                }

                //codigo de prueba 
                //prueba
                //string prueba = "&&\r\nEDR PASON\r\n01083016.00\r\n01103014.00\r\n01126000.00\r\n01130.22\r\n0115674\r\n011730.17\r\n0119173\r\n0120211\r\n01219.43\r\n0122448\r\n01234\r\n01248\r\n01257\r\n012646\r\n0127124\r\n012887\r\n0130769\r\n013730.22\r\n0139167\r\n0140288\r\n01419.45\r\n0142976\r\n11083015.00\r\n12121\r\n12134\r\n12146\r\n12150\r\n111539\r\n11168\r\n111765\r\n111879\r\n111948\r\n112011\r\n112152\r\n111151\r\n!!\r\n&&\r\nEDR PASON\r\n01083024.00\r\n01103016.00\r\n01126000.00\r\n01130.32\r\n0115534\r\n011730.25\r\n0119302\r\n0120255\r\n01219.46\r\n012220\r\n01233\r\n01248\r\n01252\r\n01268\r\n0127124\r\n012813\r\n0130443\r\n013730.30\r\n013924\r\n0140321\r\n01419.47\r\n0142871\r\n11083017.00\r\n12123\r\n12132\r\n12143\r\n12153\r\n111546\r\n11168\r\n111733\r\n11181\r\n111931\r\n112031\r\n112161\r\n111155\r\n!!\r\n&&\r\nEDR PASON\r\n01083032.00\r\n01103018.00\r\n01126000.00\r\n01130.43\r\n0115196\r\n011730.33\r\n0119246\r\n0120223\r\n01219.48\r\n012296\r\n01233\r\n01240\r\n01253\r\n012638\r\n0127128\r\n0128140\r\n0130262\r\n013730.38\r\n0139157\r\n014057\r\n01419.50\r\n0142693\r\n11083019.00\r\n12125\r\n12130\r\n12140\r\n12150\r\n111569\r\n111617\r\n111725\r\n111831\r\n111939\r\n112045\r\n112140\r\n111156\r\n!!\r\n&&\r\nEDR PASON\r\n01083040.00\r\n01103020.00\r\n01126000.00\r\n01130.52\r\n0115467\r\n011730.41\r\n0119175\r\n012025\r\n01219.51\r\n0122387\r\n01233\r\n01248\r\n01258\r\n012612\r\n012793\r\n012860\r\n0130365\r\n013730.46\r\n0139222\r\n0140235\r\n01419.52\r\n0142618\r\n11083021.00\r\n12120\r\n12135\r\n12140\r\n12152\r\n11155\r\n111616\r\n111753\r\n11181\r\n111955\r\n112068\r\n112127\r\n1111260\r\n!!\r\n&&\r\nEDR PASON\r\n01083048.00\r\n01103022.00\r\n01126000.00\r\n01130.63\r\n0115961\r\n011730.49\r\n0119107\r\n012079\r\n01219.53\r\n0122658\r\n01231\r\n01245\r\n01254\r\n012610\r\n012785\r\n012833\r\n0130790\r\n013730.54\r\n01394\r\n0140228\r\n01419.55\r\n0142461\r\n11083023.00\r\n12122\r\n12137\r\n12141\r\n12156\r\n11150\r\n111665\r\n111740\r\n111850\r\n11193\r\n112036\r\n112122\r\n111193\r\n!!\r\n&&\r\nEDR PASON\r\n01083056.00\r\n01103024.00\r\n01126000.00\r\n01130.73\r\n0115194\r\n011730.57\r\n0119304\r\n0120133\r\n01219.56\r\n0122148\r\n01232\r\n01241\r\n01253\r\n012628\r\n0127171\r\n0128185\r\n0130632\r\n013730.62\r\n013943\r\n014068\r\n01419.57\r\n0142170\r\n11083025.00\r\n12121\r\n12131\r\n12140\r\n12151\r\n111548\r\n111635\r\n111741\r\n111844\r\n111925\r\n112060\r\n112131\r\n1111432\r\n!!\r\n&&\r\nEDR PASON\r\n01083064.00\r\n01103026.00\r\n01126000.00\r\n01130.83\r\n0115404\r\n011730.65\r\n0119288\r\n0120157\r\n01219.58\r\n0122730\r\n01232\r\n01243\r\n01252\r\n012644\r\n012798\r\n012816\r\n0130774\r\n013730.70\r\n0139326\r\n0140154\r\n01419.60\r\n0142517\r\n11083027.00\r\n12125\r\n12132\r\n12145\r\n12154\r\n111514\r\n111615\r\n111761\r\n111844\r\n111933\r\n112065\r\n11217\r\n1111281\r\n!!\r\n&&\r\nEDR PASON\r\n01083072.00\r\n01103028.00\r\n01126000.00\r\n01130.93\r\n0115432\r\n011730.73\r\n0119173\r\n0120278\r\n01219.61\r\n0122626\r\n01234\r\n01248\r\n01256\r\n01260\r\n0127119\r\n01287\r\n0130422\r\n013730.78\r\n0139270\r\n014058\r\n01419.62\r\n0142697\r\n11083029.00\r\n12122\r\n12132\r\n12144\r\n12152\r\n111556\r\n111653\r\n111720\r\n111849\r\n111943\r\n11202\r\n112152\r\n1111395\r\n!!\r\n&&\r\nEDR PASON\r\n01083080.00\r\n01103030.00\r\n01126000.00\r\n01131.02\r\n0115727\r\n011730.81\r\n0119195\r\n0120212\r\n01219.63\r\n0122412\r\n01235\r\n01244\r\n01258\r\n012648\r\n012727\r\n0128170\r\n0130270\r\n013730.86\r\n0139189\r\n0140355\r\n01419.65\r\n0142635\r\n11083031.00\r\n12120\r\n12134\r\n12145\r\n12150\r\n111514\r\n111656\r\n111735\r\n11180\r\n111964\r\n112049\r\n112157\r\n1111450\r\n!!\r\n&&\r\nEDR PASON\r\n01083088.00\r\n01103032.00\r\n01126000.00\r\n01131.12\r\n0115514\r\n011730.89\r\n0119229\r\n0120109\r\n01219.66\r\n0122483\r\n01233\r\n01244\r\n01252\r\n012624\r\n0127143\r\n0128113\r\n0130138\r\n013730.94\r\n0139347\r\n0140236\r\n01419.67\r\n0142853\r\n11083033.00\r\n12120\r\n12134\r\n12145\r\n12156\r\n11156\r\n11160\r\n111750\r\n111846\r\n11191\r\n112010\r\n112167\r\n1111134\r\n!!\r\n&&\r\nEDR PASON\r\n01083096.00\r\n01103034.00\r\n01126000.00\r\n01131.23\r\n0115256\r\n011730.97\r\n0119186\r\n0120145\r\n01219.68\r\n0122798\r\n01233\r\n01242\r\n01252\r\n012642\r\n0127163\r\n012829\r\n0130849\r\n013731.02\r\n013987\r\n0140229\r\n01419.70\r\n0142824\r\n11083035.00\r\n12122\r\n12135\r\n12147\r\n12157\r\n111530\r\n111628\r\n111761\r\n111830\r\n111949\r\n112042\r\n112129\r\n1111403\r\n!!\r\n&&\r\nEDR PASON\r\n01083104.00\r\n01103036.00\r\n01126000.00\r\n01131.33\r\n0115976\r\n011731.05\r\n0119106\r\n012044\r\n01219.71\r\n0122187\r\n01232\r\n01247\r\n01254\r\n012630\r\n0127173\r\n012849\r\n0130149\r\n013731.10\r\n0139222\r\n0140205\r\n01419.72\r\n014237\r\n11083037.00\r\n12127\r\n12131\r\n12147\r\n12152\r\n111534\r\n111666\r\n111729\r\n11189\r\n111927\r\n112023\r\n112110\r\n1111395\r\n!!\r\n&&\r\nEDR PASON\r\n01083112.00\r\n01103038.00\r\n01126000.00\r\n01131.42\r\n0115782\r\n011731.13\r\n0119111\r\n0120191\r\n01219.73\r\n0122382\r\n01231\r\n01246\r\n01253\r\n012643\r\n012719\r\n012839\r\n0130134\r\n013731.18\r\n0139207\r\n014060\r\n01419.75\r\n0142900\r\n11083039.00\r\n12120\r\n12137\r\n12146\r\n12152\r\n111536\r\n111650\r\n111744\r\n111822\r\n111941\r\n112060\r\n11215\r\n1111130\r\n!!\r\n&&\r\nEDR PASON\r\n01083120.00\r\n01103040.00\r\n01126000.00\r\n01131.52\r\n0115803\r\n011731.21\r\n0119303\r\n0120219\r\n01219.76\r\n0122846\r\n01234\r\n01244\r\n01257\r\n01267\r\n0127128\r\n0128184\r\n013060\r\n013731.26\r\n013968\r\n014077\r\n01419.77\r\n0142424\r\n11083041.00\r\n12120\r\n12130\r\n12146\r\n12152\r\n111549\r\n111666\r\n111739\r\n11182\r\n111952\r\n112069\r\n112138\r\n1111237\r\n!!\r\n&&\r\nEDR PASON\r\n01083128.00\r\n01103042.00\r\n01126000.00\r\n01131.63\r\n0115654\r\n011731.29\r\n0119138\r\n0120168\r\n01219.78\r\n0122298\r\n01237\r\n01245\r\n01257\r\n012638\r\n012751\r\n012867\r\n0130844\r\n013731.34\r\n0139325\r\n0140161\r\n01419.80\r\n0142127\r\n11083043.00\r\n12120\r\n12133\r\n12141\r\n12151\r\n111550\r\n111656\r\n111744\r\n111870\r\n111960\r\n112050\r\n112165\r\n1111329\r\n!!\r\n&&\r\nEDR PASON\r\n01083136.00\r\n01103044.00\r\n01126000.00\r\n01131.73\r\n011575\r\n011731.37\r\n011972\r\n0120214\r\n01219.81\r\n0122962\r\n01238\r\n012\r\n";



            }
            catch (Exception ex)
            {
                Application.DoEvents();
                PuertoSerial.Close();
                if (mensajeMostrar)
                {
                    AutoClosingMessageBox.Show("Error al conectar el puerto serial. " + ex.Message + "Intentando Conectar nuevamente...", "Desconexión", 5000);
                    mensajeMostrar = false;
                    lblSerialStatus.Text = "Disconnected";
                }
                IniciarProceso(puertoGeneral, baudGeneral);

            }

        }

        public void pintarElemento(List<protocoloWits> elementos)
        {

            foreach (protocoloWits elemento in elementos)
            {
                //grupo 1
                if (elemento.WITitem == "0108")
                {
                    txtResultado0108.Text = elemento.WITvalor;
                    lbl0108.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0110")
                {
                    txtResultado0110.Text = elemento.WITvalor; //txtHoleDepth2.Text = elemento.WITvalor;
                    lbl0110.ForeColor = System.Drawing.Color.Black;// lblHoleDepth2.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0112")
                {
                    txtResultado0112.Text = elemento.WITvalor;
                    lbl0112.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0113")
                {
                    txtResultado0113.Text = elemento.WITvalor;// txtROP2.Text = elemento.WITvalor;
                    lbl0113.ForeColor = System.Drawing.Color.Black; //lblROP2.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0115")
                {
                    txtResultado0115.Text = elemento.WITvalor;
                    lbl0115.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0117")
                {
                    txtResultado0117.Text = elemento.WITvalor; //txtWeightOnBit2.Text = elemento.WITvalor;
                    lbl0117.ForeColor = System.Drawing.Color.Black;// lblWeightOnBit2.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0119")
                {
                    txtResultado0119.Text = elemento.WITvalor; //txtWeightOnBit2.Text = elemento.WITvalor;
                    lbl0117.ForeColor = System.Drawing.Color.Black;// lblWeightOnBit2.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0120")
                {
                    txtResultado0120.Text = elemento.WITvalor;
                    lbl0120.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0121")
                {
                    txtResultado0121.Text = elemento.WITvalor;
                    lbl0121.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0122")
                {
                    txtResultado0122.Text = elemento.WITvalor;
                    lbl0122.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0123")
                {
                    txtResultado0123.Text = elemento.WITvalor;
                    lbl0123.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0124")
                {
                    txtResultado0124.Text = elemento.WITvalor;
                    lbl0124.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0125")
                {
                    txtResultado0125.Text = elemento.WITvalor;
                    lbl0125.ForeColor = System.Drawing.Color.Black;
                }

                //grupo 2
                if (elemento.WITitem == "0126")
                {
                    txtResultado0126.Text = elemento.WITvalor;
                    lbl0126.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0127")
                {
                    txtResultado0127.Text = elemento.WITvalor;
                    lbl0127.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0128")
                {
                    txtResultado0128.Text = elemento.WITvalor;
                    lbl0128.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0130")
                {
                    txtResultado0130.Text = elemento.WITvalor;
                    lbl0130.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0137")
                {
                    txtResultado0137.Text = elemento.WITvalor;
                    lbl0137.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0139")
                {
                    txtResultado0139.Text = elemento.WITvalor;
                    lbl0139.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0140")
                {
                    txtResultado0140.Text = elemento.WITvalor;
                    lbl0140.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0141")
                {
                    txtResultado0141.Text = elemento.WITvalor;
                    lbl0141.ForeColor = System.Drawing.Color.Black;
                }
                if (elemento.WITitem == "0142")
                {
                    txtResultado0142.Text = elemento.WITvalor;
                    lbl0142.ForeColor = System.Drawing.Color.Black;
                }

                ////grupo 3
                //if (elemento.WITitem == "1212")
                //{ txtC1.Text = elemento.WITvalor; lblC1.ForeColor = System.Drawing.Color.Black; }
                //if (elemento.WITitem == "1213")
                //{ txtC2.Text = elemento.WITvalor; lblC2.ForeColor = System.Drawing.Color.Black; }
                //if (elemento.WITitem == "1214")
                //{ txtC3.Text = elemento.WITvalor; lblC3.ForeColor = System.Drawing.Color.Black; }
                //if (elemento.WITitem == "1215")
                //{ txtIC4.Text = elemento.WITvalor; lblIC4.ForeColor = System.Drawing.Color.Black; }
                if (elemento.WITitem == "1115")
                { txtResultado1115.Text = elemento.WITvalor; lbl1115.ForeColor = System.Drawing.Color.Black; }
                if (elemento.WITitem == "1116")
                { txtResultado1116.Text = elemento.WITvalor; lbl1116.ForeColor = System.Drawing.Color.Black; }
                if (elemento.WITitem == "1117")
                { txtResultado1117.Text = elemento.WITvalor; lbl1117.ForeColor = System.Drawing.Color.Black; }
                if (elemento.WITitem == "1118")
                { txtResultado1118.Text = elemento.WITvalor; lbl1118.ForeColor = System.Drawing.Color.Black; }
                if (elemento.WITitem == "1119")
                { txtResultado1119.Text = elemento.WITvalor; lbl1119.ForeColor = System.Drawing.Color.Black; }
                if (elemento.WITitem == "1120")
                { txtResultado1120.Text = elemento.WITvalor; lbl1120.ForeColor = System.Drawing.Color.Black; }
                if (elemento.WITitem == "1121")
                { txtResultado1121.Text = elemento.WITvalor; lbl1121.ForeColor = System.Drawing.Color.Black; }
                if (elemento.WITitem == "1111")
                { txtResultado1111.Text = elemento.WITvalor; lbl1111.ForeColor = System.Drawing.Color.Black; }
            }
        }

        private void Tiempo_Real_Load(object sender, EventArgs e)
        {
            lbl0108.BackColor = Color.Red;
        }

        private void btnTiempoReal2_Click(object sender, EventArgs e)
        {
            Real_Time2 tiempoReal2 = new Real_Time2();
            tiempoReal2.Show();
        }

        private void btnRadar_Click(object sender, EventArgs e)
        {
            Radar radar = new Radar();
            radar.Show();
        }
    }
}
