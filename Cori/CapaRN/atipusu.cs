using System;
using System.Collections.Generic;
using System.Text;
//Libreria para acceso a datos
using System.Data.Common; 
//Libreria para acceso a Capa de Acceso a Datos
using CapaAD;

namespace CapaRN
{
	public class atipusu {

		#region Campos
            private string _patuidtius;
            private string _catucarusu;
            //Instancia para conexion a PostgreSQL 8.2
            private CLConexionMYSQL Conexion;
		#endregion 

		#region Propiedades
		    public string patuidtius
            { 
                get{ return this._patuidtius;}
                set{ this._patuidtius = value;}
            } 
		    public string catucarusu
            { 
                get{ return this._catucarusu;}
                set{ this._catucarusu = value;}
            } 
        #endregion

        #region Constructor
            public atipusu()
            { 
		        this._patuidtius = "";
		        this._catucarusu = "";
                this.Conexion = new CLConexionMYSQL();            } 
        #endregion

        #region Metodos
            public bool ObtenerDatos() 
            { 
                this.Conexion.Conectar();
			    string sql = "select " +
                                     "patuidtius," +
                                     "catucarusu " + 
                             "from atipusu " +
                             "where "+
                                    "patuidtius = @patuidtius";

                this.Conexion.PrepararComando(sql);

                this.Conexion.AsignarParametroCadena("@patuidtius",this._patuidtius);

                DbDataReader ResultadoConsulta = Conexion.EjecutarConsulta();

                if (ResultadoConsulta.Read())
                {
                    this._patuidtius=ResultadoConsulta.GetString(0);
                    this._catucarusu=ResultadoConsulta.GetString(1);
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
                                     "patuidtius," +
                                     "catucarusu " + 
                             "from atipusu " +
                             "where " +
                                    "patuidtius = @patuidtius";
 
                this.Conexion.PrepararComando(sql); 

                this.Conexion.AsignarParametroCadena("@patuidtius",this._patuidtius);

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
			        string sql = "insert into atipusu (" +
                                                       "patuidtius," +
                                                       "catucarusu" +
                                                       ") " +
	                             "values (" + 
                                          "@patuidtius," +
                                          "@catucarusu" +
                                                       ")";

                    this.Conexion.PrepararComando(sql);

                    this.Conexion.AsignarParametroCadena("@patuidtius",this._patuidtius);
                    this.Conexion.AsignarParametroCadena("@catucarusu",this._catucarusu);

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
			        string sql = "update atipusu set " +
                                                     "catucarusu = @catucarusu" +
                                 " where " +
                                        "patuidtius = @patuidtius";
 
                this.Conexion.PrepararComando(sql); 

                    this.Conexion.AsignarParametroCadena("@patuidtius",this._patuidtius);
                    this.Conexion.AsignarParametroCadena("@catucarusu",this._catucarusu);

                    this.Conexion.EjecutarTransaccion();
                    this.Conexion.Desconectar();

                    return true;
                }
            }
            public List<atipusu> Lista(string where)
            { 
                List<atipusu> ListaResultado = new List<atipusu>();
                this.Conexion.Conectar(); 
			    string sql = "select " + 
                                     "patuidtius," +
                                     "catucarusu " + 
                             "from atipusu " ;
 
                if (where.Replace(" ", "") != "")
                {
                    sql+= "where " + where;
                }

 
                this.Conexion.PrepararComando(sql); 
                DbDataReader ResultadoConsulta = Conexion.EjecutarConsulta();

                if (ResultadoConsulta!=null)
                {
                    while (ResultadoConsulta.Read())
                    {
                          atipusu Auxiliar = new atipusu();
                          Auxiliar.patuidtius = ResultadoConsulta.GetString(0);
                          Auxiliar.catucarusu = ResultadoConsulta.GetString(1);
                          ListaResultado.Add(Auxiliar);
                    }

                }
                this.Conexion.Desconectar();
                return ListaResultado;
            } 
        #endregion 

	}
}

