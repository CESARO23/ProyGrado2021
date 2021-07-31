//LIbrerias para conexion a PosgreSQL 8.2
using MySql.Data.MySqlClient;
using System;
//Libreria para leer de un Archivo de Configuraci&oacute;n
using System.Configuration;
//Libreria de Datos
using System.Data;
using System.Data.Common;
//Libreria para escribir y leer de un archivo de texto
using System.IO;

namespace CapaAD
{
    public class CLConexionMYSQL
    {
        private string CadenaDeConexion;
        private MySqlConnection Conexion;
        private MySqlCommand Comando;

        /// <summary>
        /// Crea una instancia de la clase CLConexionPGSQL con parametros de conexi&oacute;n
        /// </summary>
        /// <param name="SV">Nombre del Servidor</param>
        /// <param name="PT">Puerto de Conexi&oacute;n</param>
        /// <param name="US">Usuario de la Base de Datos</param>
        /// <param name="PW">Password del Usuario de la Base de Datos</param>
        /// <param name="BD">Nombre de la Base de Datos</param>
        public CLConexionMYSQL(string SV, string PT, string US, string PW, string BD)
        {
            CadenaDeConexion = "server=" + SV +
                                ";port=" + PT +
                                ";user=" + US +
                                ";password=" + PW +
                                ";database=" + BD +
                                ";";
            Conexion = new MySqlConnection();
        }

        /// <summary>
        /// Crea una instancia de la clase CLConexionPGSQL tomando los par&aacute;metros
        /// de un archivo de configuraci&oacute;n (App.config)
        /// </summary>
        public CLConexionMYSQL()
        {
            try
            {
                this.CadenaDeConexion = "server=" + ConfigurationManager.AppSettings.Get("SV") +
                                        ";port=" + ConfigurationManager.AppSettings.Get("PT") +
                                        ";user=" + ConfigurationManager.AppSettings.Get("US") +
                                        ";password=" + ConfigurationManager.AppSettings.Get("PW") +
                                        ";database=" + ConfigurationManager.AppSettings.Get("BD") +
                                        ";";

            }
            catch (Exception Ex)
            {
                Log(Ex.Message);
            }
        }

        /// <summary>
        /// Crea una instancia de la clase CLConexionPGSQLCrearBD con parametros de conexi&oacute;n
        /// </summary>
        /// <param name="SV">Nombre del Servidor</param>
        /// <param name="PT">Puerto de Conexi&oacute;n</param>
        /// <param name="US">Usuario de la Base de Datos</param>
        /// <param name="PW">Password del Usuario de la Base de Datos</param>
        /// <param name="BD">Nombre de la Base de Datos</param>
        public CLConexionMYSQL(string SV, string PT, string US, string PW)
        {
            CadenaDeConexion = "server=" + SV +
                                ";port=" + PT +
                                ";user=" + US +
                                ";password=" + PW +
                                ";";
            Conexion = new MySqlConnection();
        }

        /// <summary>
        /// Permite Conectar a la Base de Datos
        /// </summary>
        public bool Conectar()
        {
            this.Conexion = new MySqlConnection(this.CadenaDeConexion);
            try
            {
                Conexion.Open();
                return true;
            }
            catch (Exception Ex)
            {
                Log(Ex.Message);
                return false;
            }
        }

        public bool Conectar_Inicio()
        {
            this.Conexion = new MySqlConnection(this.CadenaDeConexion);
            try
            {
                Conexion.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Permite Desconectar de la Base de Datos
        /// </summary>
        public void Desconectar()
        {
            this.Conexion.Close();
        }

        /// <summary>
        /// Escribe los errores producdos en tiempo de ejecuci&oacute;n en un archivo de nombre
        /// "ErrorLog.log" en el directorio raiz de la aplicaci&oacute;n
        /// </summary>
        /// <param name="msg_error">Mensaje de error a escribir en el archivo "ErrorLog.log" </param>
        private void Log(string msg_error)
        {
            string path = "ErrorLog.log";
            StreamWriter sw = File.AppendText(path);
            try
            {
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "|" + this.Comando.CommandText + "|" + msg_error);
            }
            catch (Exception ex)
            {
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "|" + ex.Message + "|" + msg_error);
            }
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Prepara la ejecuci&oacute;n de un comando SQL        
        /// </summary>
        /// <param name="CadenaSQL">Comando SQL" </param>
        public void PrepararComando(string CadenaSQL)
        {
            this.Comando = new MySqlCommand();
            this.Comando.Connection = this.Conexion;
            this.Comando.CommandType = CommandType.Text;
            this.Comando.CommandText = CadenaSQL;
        }

        /// <summary>
        /// Ejecuta un transacci&oacute;n SQL sin retornar resultado alguno.
        /// Ej: insert, update, delete.
        /// </summary>
        public void EjecutarTransaccion()
        {
            try
            {
                this.Comando.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Log(Ex.Message);
            }
        }

        /// <summary>
        /// Ejecuta un consulta SQL y retorna un objeto del tipo NpgsqlDataReader.
        /// Ej: select.
        /// </summary>
        public DbDataReader EjecutarConsulta()
        {
            try
            {
                return this.Comando.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Log(Ex.Message);
                return null;
            }
        }

        public DataTable EjecutarConsulta(string Tabla)
        {
            try
            {
                MySqlDataAdapter Adaptador = new MySqlDataAdapter();
                Adaptador.SelectCommand = this.Comando;
                DataTable table = new DataTable(Tabla);
                Adaptador.Fill(table);
                return table;
            }
            catch (MySqlException ex)
            {
                Log(ex.Message);
                throw new Exception("Error al ejecutar el comando." + ex.Message);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                throw new Exception("Error en el acceso a la BD. " + ex.Message);
            }
        }

        /// <summary>
        /// Setea un par&aacute;metro como nulo del comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro cuyo valor ser&aacute; nulo.</param>
        public void AsignarParametroNulo(string nombre)
        {
            AsignarParametro(nombre, "", "NULL");
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo bool al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroLogico(string nombre, bool valor)
        {
            AsignarParametro(nombre, "", valor.ToString());
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo cadena al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroCadena(string nombre, string valor)
        {
            AsignarParametro(nombre, "'", valor);
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo cadena al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroCadena(string nombre, char valor)
        {
            AsignarParametro(nombre, "'", valor.ToString());
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo entero al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroEntero(string nombre, int valor)
        {
            AsignarParametro(nombre, "", valor.ToString());
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo entero en cadena al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro en cadena de un entero.</param>
        public void AsignarParametroEntero(string nombre, string valor)
        {
            AsignarParametro(nombre, "", valor);
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo entero largo al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroEntero(string nombre, long valor)
        {
            AsignarParametro(nombre, "", valor.ToString());
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo double al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroDouble(string nombre, double valor)
        {
            AsignarParametro(nombre, "", valor.ToString());
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo decimal al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroDecimal(string nombre, decimal valor)
        {
            AsignarParametro(nombre, "", valor.ToString().Replace(',', '.'));
        }

        /// <summary>
        /// Asigna un par&aacute;metro al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="separador">El separador que ser&aacute; agregado al valor del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        private void AsignarParametro(string nombre, string separador, string valor)
        {
            int indice = this.Comando.CommandText.IndexOf(nombre);
            string prefijo = this.Comando.CommandText.Substring(0, indice);
            string sufijo = this.Comando.CommandText.Substring(indice + nombre.Length);
            this.Comando.CommandText = prefijo + separador + valor + separador + sufijo;

        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo fecha al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroFecha(string nombre, DateTime valor)
        {
            //AsignarParametro(nombre, "'", valor.ToString("MM/dd/yy HH:mm:ss"));
            AsignarParametro(nombre, "'", valor.ToString("yyyy/MM/dd"));
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo fecha - mes al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroFechaMes(string nombre, DateTime valor)
        {
            //AsignarParametro(nombre, "'", valor.ToString("MM/dd/yy HH:mm:ss"));
            AsignarParametro(nombre, "'", valor.ToString("yyyy/MM/") + "01");
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo fecha y hora al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroFechaHora(string nombre, DateTime valor)
        {
            //AsignarParametro(nombre, "'", valor.Day.ToString() + "/" + valor.Month.ToString() + "/" + valor.Year.ToString() + " " + valor.ToLongTimeString());
            //CultureInfo bo = new CultureInfo("es-BO");
            AsignarParametro(nombre, "'", valor.ToString("yyyy/MM/dd HH:mm:ss"));
        }

        /// <summary>
        /// Asigna un par&aacute;metro de tipo hora al comando creado.
        /// </summary>
        /// <param name="nombre">El nombre del par&aacute;metro.</param>
        /// <param name="valor">El valor del par&aacute;metro.</param>
        public void AsignarParametroHora(string nombre, DateTime valor)
        {
            //AsignarParametro(nombre, "'", valor.Day.ToString() + "/" + valor.Month.ToString() + "/" + valor.Year.ToString() + " " + valor.ToLongTimeString());
            //CultureInfo bo = new CultureInfo("es-BO");
            AsignarParametro(nombre, "'", valor.ToString("HH:mm:ss"));
        }

        /// <summary>
        /// Encripta una cadena empleando algoritmos basados en Base64.
        /// </summary>
        /// <param name="Clave">Cadena a Encriptar</param>
        public static string Encriptar(string Clave)
        {
            byte[] bytes = new byte[Clave.Length];
            for (int i = 0; i < Clave.Length; i++)
                bytes[i] = Convert.ToByte(Clave[i]);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Desencripta una cadena empleando algoritmos basados en Base64.
        /// </summary>
        /// <param name="Clave">Cadena a Desencriptar</param>
        public static string Desencriptar(string Clave)
        {
            byte[] bytes = Convert.FromBase64String(Clave);
            string res = "";
            foreach (byte b in bytes)
                res += (char)b;
            return res;
        }
    }
}