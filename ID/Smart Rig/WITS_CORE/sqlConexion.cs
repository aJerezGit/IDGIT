using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace PYOLOGGER
{
    public class sqlConexion
    {
        SQLiteConnection m_dbConnection = new SQLiteConnection();//"Data Source=C:\\pyosoft\\WITSdataBase.sqlite;Version=3;");

        const string connStrMysql = "Server=pyosoftprb.cfsh1lwjn4sj.us-west-2.rds.amazonaws.com;Port=3306;user=pyosoft;password=PY050FTDB;";
        // old string "Server=pyosoftprb.cfsh1lwjn4sj.us-west-2.rds.amazonaws.com;Port=3306;user=pyosoft;password=PY050FTDB;";

        public int createDatabase(string baseDatosNombre, string rutaBaseDatos)
        {
            try
            {
                int resultadoSqlite = SqliteProcedure(baseDatosNombre, rutaBaseDatos);

                int resultadoMysql = MysqlProcedure(baseDatosNombre, rutaBaseDatos);

                if (resultadoMysql == 1 && resultadoSqlite == 1)
                    return 1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            
        }

        public int SqliteProcedure(string baseDatosNombre, string rutaBaseDatos)
        {
            string cadenaConexion = crearTxtDataBase(rutaBaseDatos, baseDatosNombre);
            SQLiteConnection.CreateFile(rutaBaseDatos);
            m_dbConnection = new SQLiteConnection(cadenaConexion);

            m_dbConnection.Open();

            //CONFIGURACION
            string CTableConfiguracion = "CREATE TABLE IF NOT EXISTS configuracion (CONid int, CONaccion varchar(50), CONvalorNumerico INT, CONvalorTexto VARCHAR(50))";
            SQLiteCommand creacionConfiguracion = new SQLiteCommand(CTableConfiguracion, m_dbConnection);
            creacionConfiguracion.ExecuteNonQuery();

            ////Fill configuration table
            //string Ftable = "INSERT INTO configuracion (CONid, CONaccion, CONvalorNumerico, CONvalorTexto) " +
            //                                           " SELECT 1, 'MOD_CONFIGURADO', 0, null " +
            //                                           " UNION ALL " +
            //                                           " SELECT  2, 'MOD_PERMITE_CAMBIOS', 1, null " + 
            //                                           " UNION ALL " + 
            //                                           " SELECT 3, 'MOD_INICIO_AUTOMATICO', 0, null";
            //SQLiteCommand llenadoConfiguracion = new SQLiteCommand(Ftable, m_dbConnection);
            //llenadoConfiguracion.ExecuteNonQuery();

            ////tabla de identificacion WITS
            string CtablewITs = "CREATE TABLE IF NOT EXISTS Wits (WITid INTEGER  PRIMARY KEY AUTOINCREMENT, WITitemCod VARCHAR(50), WITdescripcion INT, WITfechaRegistro SMALLDATETIME)";
            SQLiteCommand creacionWits = new SQLiteCommand(CtablewITs, m_dbConnection);
            creacionWits.ExecuteNonQuery();

            ////Fill WITS table
            string Ftable = "INSERT INTO configuracion (CONid, CONaccion, CONvalorNumerico, CONvalorTexto) " +
                                                       " SELECT 1, 'MOD_CONFIGURADO', 0, null " +
                                                       " UNION ALL " +
                                                       " SELECT  2, 'MOD_PERMITE_CAMBIOS', 1, null " +
                                                       " UNION ALL " +
                                                       " SELECT 3, 'MOD_INICIO_AUTOMATICO', 0, null";
            SQLiteCommand llenadoConfiguracion = new SQLiteCommand(Ftable, m_dbConnection);
            llenadoConfiguracion.ExecuteNonQuery();

            ////tabla de registros WITS
            string CtableWITRegistros = "CREATE TABLE IF NOT EXISTS WitsRegistros (WIRid INTEGER  PRIMARY KEY AUTOINCREMENT, WIRpaquete INT, WIRitem VARCHAR(50), WIRvalor INT, WIRprofundidad INT, WIRfechaRegistro SMALLDATETIME)";
            SQLiteCommand creacionWitsRegistros = new SQLiteCommand(CtableWITRegistros, m_dbConnection);
            creacionWitsRegistros.ExecuteNonQuery();

            ////tabla modBus abreviada
            //string CtableMODBUSabreviado = "CREATE TABLE modbusAbreviado (MOAid INTEGER  PRIMARY KEY AUTOINCREMENT, MOAaddress INT, MOAinicioAddress INT, MOAnumeroPuntos INT)";
            //SQLiteCommand creacionModAbreviado = new SQLiteCommand(CtableMODBUSabreviado, m_dbConnection);
            //creacionModAbreviado.ExecuteNonQuery();

            ////tabla Historial Registros
            //string CtableHistorialMODBUS = "CREATE TABLE modbusHistorial (MOHid INTEGER  PRIMARY KEY AUTOINCREMENT, MOHvariable, MOHesclavo, MOHvalor, MOHfecha)";
            //SQLiteCommand creacionModHistorial = new SQLiteCommand(CtableHistorialMODBUS, m_dbConnection);
            //creacionModHistorial.ExecuteNonQuery();

            m_dbConnection.Close();

            return 1;
        }

        public int MysqlProcedure(string baseDatosNombre, string rutaBaseDatos)
        {
            MySqlConnection conn = new MySqlConnection(connStrMysql);
            MySqlCommand cmd;
            string baseDatos;

            conn.Open();
            baseDatos = "CREATE DATABASE IF NOT EXISTS `" + baseDatosNombre + "`;";
            cmd = new MySqlCommand(baseDatos, conn);
            cmd.ExecuteNonQuery();                

            //creating tables
            string CtablewITs = "USE " + baseDatosNombre + "; CREATE TABLE IF NOT EXISTS Wits (WITid INTEGER  PRIMARY KEY auto_increment, WITitemCod VARCHAR(50), WITdescripcion INT, WITfechaRegistro datetime)";
            cmd = new MySqlCommand(CtablewITs, conn);
            cmd.ExecuteNonQuery();

            string CtableWITRegistros = "USE " + baseDatosNombre + "; CREATE TABLE IF NOT EXISTS WitsRegistros (WIRid INTEGER  PRIMARY KEY auto_increment, WIRpaquete INT, WIRitem VARCHAR(50), WIRvalor DECIMAL(9,4), WIRprofundidad INT, WIRfechaRegistro datetime)";
            cmd = new MySqlCommand(CtableWITRegistros, conn);
            cmd.ExecuteNonQuery();

            conn.Close();

            return 1;
        }

        private string crearTxtDataBase(string rutaBaseDatos, string baseDatosNombre)
        {
            string path = @"c:\\pyosoft";
            string respuesta = string.Empty;

            crearFolder(path);

            if (Directory.Exists(path))
            {
                rutaBaseDatos = "Data Source=" + rutaBaseDatos;
                rutaBaseDatos += "; Version = 3;";
                path += "\\Data.txt";

                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(rutaBaseDatos);
                    sw.WriteLine(baseDatosNombre);
                }
                respuesta = rutaBaseDatos;
            }

            return respuesta;
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



        public int necesitaConfiguracion()
        {
            try
            {
                m_dbConnection.Open();

                //necesita configuracion?
                string configuracion = "SELECT CONvalorNumerico FROM configuracion " +
                                        " WHERE CONaccion = 'MOD_CONFIGURADO' ";
                var sqliteAdapter = new SQLiteDataAdapter(configuracion, m_dbConnection);
                DataTable resultado = new DataTable();
                var cmdBuilder = new SQLiteCommandBuilder(sqliteAdapter);
                sqliteAdapter.Fill(resultado);
                m_dbConnection.Close();

                if (resultado.Rows.Count > 0)
                    return int.Parse(resultado.Rows[0][0].ToString());
                else
                    return 0;
                
            
            }
            catch (Exception ex)
            {                
                throw new ArgumentException(ex.Message, ex);
            }
           
        }

        public DataTable modBusData()
        {
            m_dbConnection.Open();

            DataTable modBus = new DataTable();

            string consulta = "SELECT MODid, MODnombre, MODidEsclavo, MODaddress FROM modBus";
            var sqliteAdapter = new SQLiteDataAdapter(consulta, m_dbConnection);
            var cmdBuilder = new SQLiteCommandBuilder(sqliteAdapter);
            sqliteAdapter.Fill(modBus);
            m_dbConnection.Close();

            return modBus;
        }

        public int guardadoConfiguracion(string MODnombre, int MODidEsclavo, int MODaddress)
        {
            m_dbConnection.Open();

            string guardado = "INSERT INTO modBus (MODnombre, MODidEsclavo, MODaddress) " + 
                               " SELECT  @MODnombre, @MODidEsclavo, @MODaddress";
            SQLiteCommand agregar = new SQLiteCommand(guardado, m_dbConnection);

            try
            {
                agregar.Parameters.Clear();
                agregar.Parameters.AddWithValue("@MODnombre", Convert.ToString(MODnombre));
                agregar.Parameters.AddWithValue("@MODidEsclavo", Convert.ToString(MODidEsclavo));
                agregar.Parameters.AddWithValue("@MODaddress", Convert.ToString(MODaddress));

                agregar.ExecuteNonQuery();
                m_dbConnection.Close();

                return 1;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }            
        }

        public DataTable modBusHistorial()
        {
            m_dbConnection.Open();
            DataTable modBusHistorial = new DataTable();

            string datos = "SELECT MOHid , MOHesclavo, MOHvariable, MOHvalor, MOHfecha  FROM modbusHistorial";
            var sqliteAdapter = new SQLiteDataAdapter(datos, m_dbConnection);
            var cmdBuilder = new SQLiteCommandBuilder(sqliteAdapter);
            sqliteAdapter.Fill(modBusHistorial);
            m_dbConnection.Close();

            return modBusHistorial;
        }

        public void guardadoHistorial(int idPaquete, int idItem, string valor, string profundidad)
        {
            m_dbConnection.Open();

            string guardado = "INSERT INTO WitsRegistros ((WIRpaquete, WIRitem , WIRvalor, WIRprofundidad, WIRfechaRegistro)" +
                               " SELECT  @WIRpaquete, @WIRitem, @WIRvalor, @WIRprofundidad, @WIRfechaRegistro";
            SQLiteCommand agregar = new SQLiteCommand(guardado, m_dbConnection);

            try
            {
                agregar.Parameters.Clear();
                agregar.Parameters.AddWithValue("@WIRpaquete", Convert.ToString(idPaquete));
                agregar.Parameters.AddWithValue("@WIRitem", Convert.ToString(idItem));
                agregar.Parameters.AddWithValue("@WIRvalor", valor);
                agregar.Parameters.AddWithValue("@WIRprofundidad", profundidad);
                agregar.Parameters.AddWithValue("@WIRfechaRegistro", DateTime.Now.ToString());

                agregar.ExecuteNonQuery();
                m_dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }            
        }
    }
}
