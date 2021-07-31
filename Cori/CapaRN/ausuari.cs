using System;
using System.Collections.Generic;
using System.Text;
//Libreria para acceso a datos
using System.Data.Common; 
//Libreria para acceso a Capa de Acceso a Datos
using CapaAD;
using System.Windows.Forms;

namespace CapaRN
{
	public class ausuari : aperson{

		#region Campos
            private bool _cauaestusu;
            private string _cauacorele;
            private string _cauafotusu;
            private string _pauaidusua;
            private string _cauacontra;
            private string _fauaidtius;
            private string _cauausuari;
            //Instancia para conexion a Mysql
            private CLConexionMYSQL Conexion;
		#endregion 

		#region Propiedades
		    public bool cauaestusu
            { 
                get{ return this._cauaestusu;}
                set{ this._cauaestusu = value;}
            } 
		    public string cauacorele
            { 
                get{ return this._cauacorele;}
                set{ this._cauacorele = value;}
            } 
		    public string cauafotusu
            { 
                get{ return this._cauafotusu;}
                set{ this._cauafotusu = value;}
            } 
		    public string pauaidusua
            { 
                get{ return this._pauaidusua;}
                set{ this._pauaidusua = value;}
            } 
		    public string cauacontra
            { 
                get{ return this._cauacontra;}
                set{ this._cauacontra = value;}
            } 
		    public string fauaidtius
            { 
                get{ return this._fauaidtius;}
                set{ this._fauaidtius = value;}
            } 
		    public string cauausuari
            { 
                get{ return this._cauausuari;}
                set{ this._cauausuari = value;}
            }
        public System.Drawing.Image cauafotogr
        {
            get
            {
                imagenes a = new imagenes();
                if (this._cauafotusu != "")
                {

                    return a.ConvertBase64StringToImage(this._cauafotusu);
                }
                else
                {
                    return null;
                }
            }

            set
            {
                imagenes a = new imagenes();
                this._cauafotusu = a.ConvertImageToBase64String(a.Resize(value, 400, 400, Application.StartupPath + "\\temp.jpg"));
            }
        }
        #endregion

        #region Constructor
        public ausuari()
            { 
		        this._cauaestusu = true;
		        this._cauacorele = "";
		        this._cauafotusu = "";
		        this._pauaidusua = "";
		        this._cauacontra = "";
		        this._fauaidtius = "";
		        this._cauausuari = "";
                this.Conexion = new CLConexionMYSQL();            } 
        #endregion

        #region Metodos
            public bool ObtenerDatos() 
            { 
                this.Conexion.Conectar();
			    string sql = "select " +
                                     "cauaestusu," +
                                     "cauacorele," +
                                     "cauafotusu," +
                                     "pauaidusua," +
                                     "cauacontra," +
                                     "fauaidtius," +
                                     "cauausuari " + 
                             "from ausuari " +
                             "where "+
                                    "pauaidusua = @pauaidusua";

                this.Conexion.PrepararComando(sql);

                this.Conexion.AsignarParametroCadena("@pauaidusua",this._pauaidusua);

                DbDataReader ResultadoConsulta = Conexion.EjecutarConsulta();

                if (ResultadoConsulta.Read())
                {
                    this._cauaestusu=ResultadoConsulta.GetBoolean(0);
                    this._cauacorele=ResultadoConsulta.GetString(1);
                    this._cauafotusu=ResultadoConsulta.GetString(2);
                    this._pauaidusua=ResultadoConsulta.GetString(3);
                    this._cauacontra=ResultadoConsulta.GetString(4);
                    this._fauaidtius=ResultadoConsulta.GetString(5);
                    this._cauausuari=ResultadoConsulta.GetString(6);
                    this.Conexion.Desconectar();

                    return true;
                }
                else
                {
                    this.Conexion.Desconectar();
                    return false;
                }
            }
            public bool VerificarExistencia()
            { 
                this.Conexion.Conectar(); 
			    string sql = "select " + 
                                     "cauaestusu," +
                                     "cauacorele," +
                                     "cauafotusu," +
                                     "pauaidusua," +
                                     "cauacontra," +
                                     "fauaidtius," +
                                     "cauausuari " + 
                             "from ausuari " +
                             "where " +
                                    "pauaidusua = @pauaidusua or cauausuari=@cauausuari";
 
                this.Conexion.PrepararComando(sql); 

                this.Conexion.AsignarParametroCadena("@pauaidusua",this._pauaidusua);
                this.Conexion.AsignarParametroCadena("@cauausuari", this._cauausuari);

                DbDataReader ResultadoConsulta = Conexion.EjecutarConsulta();

                if (ResultadoConsulta.HasRows)
                {
                this.Conexion.Desconectar();

                    return true;
                }
                else 
                { 

                this.Conexion.Desconectar();
                    return false;
                } 
            } 
            public bool Grabar()
            { 
                if (this.VerificarExistencia())
                {
                    return false;
                }
                else 
                { 
                    this.Conexion.Conectar();
			        string sql = "insert into ausuari (" +
                                                       "cauaestusu," +
                                                       "cauacorele," +
                                                       "cauafotusu," +
                                                       "pauaidusua," +
                                                       "cauacontra," +
                                                       "fauaidtius," +
                                                       "cauausuari" +
                                                       ") " +
	                             "values (" + 
                                          "@cauaestusu," +
                                          "@cauacorele," +
                                          "@cauafotusu," +
                                          "@pauaidusua," +
                                          "@cauacontra," +
                                          "@fauaidtius," +
                                          "@cauausuari" +
                                                       ")";

                    this.Conexion.PrepararComando(sql);

                    this.Conexion.AsignarParametroLogico("@cauaestusu",this._cauaestusu);
                    this.Conexion.AsignarParametroCadena("@cauacorele",this._cauacorele);
                    this.Conexion.AsignarParametroCadena("@cauafotusu",this._cauafotusu);
                    this.Conexion.AsignarParametroCadena("@pauaidusua",this._pauaidusua);
                    this.Conexion.AsignarParametroCadena("@cauacontra",this._cauacontra);
                    this.Conexion.AsignarParametroCadena("@fauaidtius",this._fauaidtius);
                    this.Conexion.AsignarParametroCadena("@cauausuari",this._cauausuari);

                    this.Conexion.EjecutarTransaccion();
                    this.Conexion.Desconectar();

                    return true;
                } 
            } 
            public bool Modificar()
            { 
                if (!this.VerificarExistencia())
                {
                    return false;
                }
                else 
                { 
                    this.Conexion.Conectar();
			        string sql = "update ausuari set " +
                                                     "cauaestusu = @cauaestusu, " +
                                                     "cauacorele = @cauacorele, " +
                                                     "cauafotusu = @cauafotusu, " +
                                                     "cauacontra = @cauacontra, " +
                                                     "fauaidtius = @fauaidtius, " +
                                                     "cauausuari = @cauausuari" +
                                 " where " +
                                        "pauaidusua = @pauaidusua";
 
                this.Conexion.PrepararComando(sql); 

                    this.Conexion.AsignarParametroLogico("@cauaestusu",this._cauaestusu);
                    this.Conexion.AsignarParametroCadena("@cauacorele",this._cauacorele);
                    this.Conexion.AsignarParametroCadena("@cauafotusu",this._cauafotusu);
                    this.Conexion.AsignarParametroCadena("@pauaidusua",this._pauaidusua);
                    this.Conexion.AsignarParametroCadena("@cauacontra",this._cauacontra);
                    this.Conexion.AsignarParametroCadena("@fauaidtius",this._fauaidtius);
                    this.Conexion.AsignarParametroCadena("@cauausuari",this._cauausuari);

                    this.Conexion.EjecutarTransaccion();
                    this.Conexion.Desconectar();

                    return true;
                }
            }
            public List<ausuari> Lista(string where)
            { 
                List<ausuari> ListaResultado = new List<ausuari>();
                this.Conexion.Conectar(); 
			    string sql = "select " +
                                     "papsidpers," +
                                     "capsnomper," +
                                     "capsapepat," +
                                     "capsapemat," +
                                     "capscipers," +
                                     "capscelula," +
                                     "capstelefo," +
                                     "capsdirecc," +

                                     "cauaestusu," +
                                     "cauacorele," +
                                     "cauafotusu," +
                                     "pauaidusua," +
                                     "cauacontra," +
                                     "fauaidtius," +
                                     "cauausuari " + 
                             "from ausuari, aperson " +
                             "where pauaidusua = papsidpers";
 
                if (where.Replace(" ", "") != "")
                {
                    sql+= "and " + where;
                }

 
                this.Conexion.PrepararComando(sql); 
                DbDataReader ResultadoConsulta = Conexion.EjecutarConsulta();

                if (ResultadoConsulta!=null)
                {
                    while (ResultadoConsulta.Read())
                    {
                        ausuari Auxiliar = new ausuari();
                        Auxiliar.papsidpers = ResultadoConsulta.GetString(0);
                        Auxiliar.capsnomper = ResultadoConsulta.GetString(1);
                        Auxiliar.capsapepat = ResultadoConsulta.GetString(2);
                        Auxiliar.capsapemat = ResultadoConsulta.GetString(3);
                        Auxiliar.capscipers = ResultadoConsulta.GetString(4);
                        Auxiliar.capscelula = ResultadoConsulta.GetString(5);
                        Auxiliar.capstelefo = ResultadoConsulta.GetString(6);
                        Auxiliar.capsdirecc = ResultadoConsulta.GetString(7);

                        Auxiliar.cauaestusu = ResultadoConsulta.GetBoolean(8);
                        Auxiliar.cauacorele = ResultadoConsulta.GetString(9);
                        Auxiliar.cauafotusu = ResultadoConsulta.GetString(10);
                        Auxiliar.pauaidusua = ResultadoConsulta.GetString(11);
                        Auxiliar.cauacontra = ResultadoConsulta.GetString(12);
                        Auxiliar.fauaidtius = ResultadoConsulta.GetString(13);
                        Auxiliar.cauausuari = ResultadoConsulta.GetString(14);
                        ListaResultado.Add(Auxiliar);
                    }

                }
                this.Conexion.Desconectar();
                return ListaResultado;
            } 
        #endregion 

	}
}

