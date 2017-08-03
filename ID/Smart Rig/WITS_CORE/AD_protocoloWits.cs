using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;

namespace pyosoft
{

    public partial class AD_protocoloWits
    {
        bool conexion = true;

        Thread procesoSecundarioSqlite;
        Thread procesoSecundarioMysql;
        int error = 0;

        public static int AgregarSQLite(List<protocoloWits> paquete, string cadenaConexion)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(cadenaConexion);
            SQLiteTransaction tr = null;

            m_dbConnection.Open();

            tr = m_dbConnection.BeginTransaction();

            int retorno = 0;
            //validaciones pendientes, el item no puede contener && o puntos o letras
            ////Fill WITS table
            foreach (protocoloWits item in paquete)
            {
                if (item.WITitem != "PA")
                {
                    string Ftable = (string.Format("INSERT INTO WitsRegistros (WIRpaquete, WIRitem, WIRvalor, WIRfechaRegistro) VALUES ('{0}', '{1}', '{2}', '{3}')",
                        item.WITpaqueteNumero, item.WITitem, item.WITvalor, item.WITfecha.ToString("yyyy-MM-dd H:mm:ss")));
                    SQLiteCommand llenadoConfiguracion = new SQLiteCommand(Ftable, m_dbConnection);
                    retorno = llenadoConfiguracion.ExecuteNonQuery();
                }
            }

            tr.Commit();

            m_dbConnection.Close();


            return retorno;
        }

        public static int AgregarMysql(List<protocoloWits> paquete, string cadenaConexion, int? error)
        {
            //una posible solucion es perder conexion y al momento de que este la encuentre darle un delay de 10 segundos para recuperar la conexion y ahi si intentar enviar datos nuevamente
            //retorno va a ser un valor de validacion de conexion
            int retorno = 1;
            //bool hayInternet = NetworkInterface.GetIsNetworkAvailable(); // AccesoInternet();
            //if (!hayInternet)
            //{
            //    retorno = 0;
            //}
            //if (hayInternet)
            //{
            //    //revalida
            //    if (retorno != 1)
            //    {
            //        Thread.Sleep(3000);
            //    }
                bool realmenteHayInternet = NetworkInterface.GetIsNetworkAvailable(); //AccesoInternet();
                if (realmenteHayInternet)
                {
                    MySqlConnection conn = new MySqlConnection(cadenaConexion);
                    MySqlTransaction MysqlTr;
                    if (error == 1)
                    {
                        conn.Close();
                        error = 0;
                    }
                    conn.Open();

                    MysqlTr = conn.BeginTransaction();


                    //validaciones pendientes, el item no puede contener && o puntos o letras
                    ////Fill WITS table
                    foreach (protocoloWits item in paquete)
                    {
                        if (item.WITitem != "PA")
                        {
                            string Ftable = (string.Format("INSERT INTO WitsRegistros (WIRpaquete, WIRitem, WIRvalor, WIRfechaRegistro) VALUES ('{0}', '{1}', '{2}', '{3}')",
                                item.WITpaqueteNumero, item.WITitem, item.WITvalor, item.WITfecha.ToString("yyyy-MM-dd H:mm:ss")));
                            MySqlCommand llenadoConfiguracion = new MySqlCommand(Ftable, conn);
                            retorno = llenadoConfiguracion.ExecuteNonQuery();
                        }
                    }

                    MysqlTr.Commit();

                    conn.Close();
                //}

            }

            return retorno;
        }

        public Tuple<List<protocoloWits>, int> FormateaWits(string cadenaConexion, string cadenaConexionMysql, string wits)
        {

            string[] stringSeparators = new string[] { "\r\n" };
            int resultado = 0;

            List<protocoloWits> listaWits = new List<protocoloWits>();
            if (wits.Length > 2)
            {
                string[] elementosWits = wits.Split(stringSeparators, StringSplitOptions.None);

                foreach (var dato in elementosWits)
                {
                    if (dato != "" && dato.Length > 4 && dato.Contains("?") == false && dato.Contains("&") == false && dato.Contains("EDR") == false)
                    {
                        protocoloWits elemento = new protocoloWits();
                        elemento.WITpaqueteNumero = dato.Substring(0, 2);
                        elemento.WITitem = dato.Substring(0, 4);
                        elemento.WITvalor = dato.Substring(4);
                        elemento.WITfecha = DateTime.Now;

                        listaWits.Add(elemento);
                    }
                }


                procesoSecundarioSqlite = new Thread(() =>  AD_protocoloWits.AgregarSQLite(listaWits, cadenaConexion));
                procesoSecundarioSqlite.Start();
                bool hayInternet = NetworkInterface.GetIsNetworkAvailable(); // AccesoInternet();
                bool rllyExists = false;
                if (!hayInternet)
                {
                    conexion = false;
                }
                if (hayInternet)
                {
                    if (!conexion)
                    {
                        Thread.Sleep(100);
                        rllyExists = NetworkInterface.GetIsNetworkAvailable();
                    }
                    if (rllyExists)
                    {
                        procesoSecundarioMysql = new Thread(() => AD_protocoloWits.AgregarMysql(listaWits, cadenaConexionMysql, error));
                        procesoSecundarioMysql.Start();
                    }
                }
                resultado = (elementosWits.Length * 100) / 38;
            }

            return Tuple.Create(listaWits, resultado);
        }

        private static bool AccesoInternet()
        {
            WebRequest Req = WebRequest.Create("http://www.google.com");
            System.Net.HttpWebResponse res = default(System.Net.HttpWebResponse);

            try
            {
                Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.google.com");


                res = (System.Net.HttpWebResponse)Req.GetResponse();

                Req.Abort();

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public int guardarConfiguracionWits(string nombreArchivo, List<string> valoresAguardar)
        {
            string path = @"c:\\pyosoft";
            string respuesta = string.Empty;

            crearFolder(path);

            if (Directory.Exists(path))
            {
                path += nombreArchivo;// "\\WitsConfiguracion.txt";
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("Wits1Configuracion");
                    foreach (string item in valoresAguardar)
                    {
                        sw.WriteLine(item);
                    }

                    sw.WriteLine("txtVariableN_0,92");
                    //variable inamovible, antes existia ahora es fija
                    sw.WriteLine("txtTorqueBroca_0,20");



                }
            }

            return 1;
        }

        public void crearFolder(string ruta)
        {
            // Specify the directory you want to manipulate.


            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(ruta))
                {
                    Console.WriteLine("That path exists already.");
                }
                else
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(ruta);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

    }
}
