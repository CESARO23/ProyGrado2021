using System;
using System.Collections.Generic;
using System.Text;
//Libreria para acceso a datos
using System.Data.Common; 
//Libreria para acceso a Capa de Acceso a Datos
using CapaAD;

namespace CapaRN
{
	public class aperson {

		#region Campos
            private string _papsidpers;
            private string _capsnomper;
            private string _capsapepat;
            private string _capsapemat;
            private string _capscipers;
            private string _capscelula;
            private string _capstelefo;
            private string _capsdirecc;
            //Instancia para conexion a Mysql
            private CLConexionMYSQL Conexion;
		#endregion 

		#region Propiedades
		    public string papsidpers
            { 
                get{ return this._papsidpers;}
                set{ this._papsidpers = value;}
            } 
		    public string capsnomper
            { 
                get{ return this._capsnomper;}
                set{ this._capsnomper = value;}
            } 
		    public string capsapepat
            { 
                get{ return this._capsapepat;}
                set{ this._capsapepat = value;}
            } 
		    public string capsapemat
            { 
                get{ return this._capsapemat;}
                set{ this._capsapemat = value;}
            } 
		    public string capscipers
            { 
                get{ return this._capscipers;}
                set{ this._capscipers = value;}
            } 
		    public string capscelula
            { 
                get{ return this._capscelula;}
                set{ this._capscelula = value;}
            } 
		    public string capstelefo
            { 
                get{ return this._capstelefo;}
                set{ this._capstelefo = value;}
            } 
		    public string capsdirecc
            { 
                get{ return this._capsdirecc;}
                set{ this._capsdirecc = value;}
            } 
        #endregion

        #region Constructor
            public aperson()
            { 
		        this._papsidpers = "";
		        this._capsnomper = "";
		        this._capsapepat = "";
		        this._capsapemat = "";
		        this._capscipers = "";
		        this._capscelula = "";
		        this._capstelefo = "";
		        this._capsdirecc = "";
                this.Conexion = new CLConexionMYSQL();            } 
        #endregion

        #region Metodos
            public bool ObtenerDatos() 
            { 
                this.Conexion.Conectar();
			    string sql = "select " +
                                     "papsidpers," +
                                     "capsnomper," +
                                     "capsapepat," +
                                     "capsapemat," +
                                     "capscipers," +
                                     "capscelula," +
                                     "capstelefo," +
                                     "capsdirecc " + 
                             "from aperson " +
                             "where "+
                                    "papsidpers = @papsidpers or capscipers = @capscipers"; //añadi la busqueda por carnet

                this.Conexion.PrepararComando(sql);

                this.Conexion.AsignarParametroCadena("@papsidpers",this._papsidpers);
                this.Conexion.AsignarParametroCadena("@capscipers", this._capscipers);

                DbDataReader ResultadoConsulta = Conexion.EjecutarConsulta();

                if (ResultadoConsulta.Read())
                {
                    this._papsidpers=ResultadoConsulta.GetString(0);
                    this._capsnomper=ResultadoConsulta.GetString(1);
                    this._capsapepat=ResultadoConsulta.GetString(2);
                    this._capsapemat=ResultadoConsulta.GetString(3);
                    this._capscipers=ResultadoConsulta.GetString(4);
                    this._capscelula=ResultadoConsulta.GetString(5);
                    this._capstelefo=ResultadoConsulta.GetString(6);
                    this._capsdirecc=ResultadoConsulta.GetString(7);
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
                                     "papsidpers," +
                                     "capsnomper," +
                                     "capsapepat," +
                                     "capsapemat," +
                                     "capscipers," +
                                     "capscelula," +
                                     "capstelefo," +
                                     "capsdirecc " + 
                             "from aperson " +
                             "where " +
                                    "papsidpers = @papsidpers or capscipers = @capscipers";
 
                this.Conexion.PrepararComando(sql); 

                this.Conexion.AsignarParametroCadena("@papsidpers",this._papsidpers);
                this.Conexion.AsignarParametroCadena("@capscipers", this._capscipers);

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
			        string sql = "insert into aperson (" +
                                                       "papsidpers," +
                                                       "capsnomper," +
                                                       "capsapepat," +
                                                       "capsapemat," +
                                                       "capscipers," +
                                                       "capscelula," +
                                                       "capstelefo," +
                                                       "capsdirecc" +
                                                       ") " +
	                             "values (" + 
                                          "@papsidpers," +
                                          "@capsnomper," +
                                          "@capsapepat," +
                                          "@capsapemat," +
                                          "@capscipers," +
                                          "@capscelula," +
                                          "@capstelefo," +
                                          "@capsdirecc" +
                                                       ")";

                    this.Conexion.PrepararComando(sql);

                    this.Conexion.AsignarParametroCadena("@papsidpers",this._papsidpers);
                    this.Conexion.AsignarParametroCadena("@capsnomper",this._capsnomper);
                    this.Conexion.AsignarParametroCadena("@capsapepat",this._capsapepat);
                    this.Conexion.AsignarParametroCadena("@capsapemat",this._capsapemat);
                    this.Conexion.AsignarParametroCadena("@capscipers",this._capscipers);
                    this.Conexion.AsignarParametroCadena("@capscelula",this._capscelula);
                    this.Conexion.AsignarParametroCadena("@capstelefo",this._capstelefo);
                    this.Conexion.AsignarParametroCadena("@capsdirecc",this._capsdirecc);

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
			        string sql = "update aperson set " +
                                                     "capsnomper = @capsnomper, " +
                                                     "capsapepat = @capsapepat, " +
                                                     "capsapemat = @capsapemat, " +
                                                     "capscipers = @capscipers, " +
                                                     "capscelula = @capscelula, " +
                                                     "capstelefo = @capstelefo, " +
                                                     "capsdirecc = @capsdirecc" +
                                 " where " +
                                        "papsidpers = @papsidpers";
 
                this.Conexion.PrepararComando(sql); 

                    this.Conexion.AsignarParametroCadena("@papsidpers",this._papsidpers);
                    this.Conexion.AsignarParametroCadena("@capsnomper",this._capsnomper);
                    this.Conexion.AsignarParametroCadena("@capsapepat",this._capsapepat);
                    this.Conexion.AsignarParametroCadena("@capsapemat",this._capsapemat);
                    this.Conexion.AsignarParametroCadena("@capscipers",this._capscipers);
                    this.Conexion.AsignarParametroCadena("@capscelula",this._capscelula);
                    this.Conexion.AsignarParametroCadena("@capstelefo",this._capstelefo);
                    this.Conexion.AsignarParametroCadena("@capsdirecc",this._capsdirecc);

                    this.Conexion.EjecutarTransaccion();
                    this.Conexion.Desconectar();

                    return true;
                }
            }
            public List<aperson> Lista(string where)
            { 
                List<aperson> ListaResultado = new List<aperson>();
                this.Conexion.Conectar(); 
			    string sql = "select " + 
                                     "papsidpers," +
                                     "capsnomper," +
                                     "capsapepat," +
                                     "capsapemat," +
                                     "capscipers," +
                                     "capscelula," +
                                     "capstelefo," +
                                     "capsdirecc " + 
                             "from aperson " ;
 
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
                          aperson Auxiliar = new aperson();
                          Auxiliar.papsidpers = ResultadoConsulta.GetString(0);
                          Auxiliar.capsnomper = ResultadoConsulta.GetString(1);
                          Auxiliar.capsapepat = ResultadoConsulta.GetString(2);
                          Auxiliar.capsapemat = ResultadoConsulta.GetString(3);
                          Auxiliar.capscipers = ResultadoConsulta.GetString(4);
                          Auxiliar.capscelula = ResultadoConsulta.GetString(5);
                          Auxiliar.capstelefo = ResultadoConsulta.GetString(6);
                          Auxiliar.capsdirecc = ResultadoConsulta.GetString(7);
                          ListaResultado.Add(Auxiliar);
                    }

                }
                this.Conexion.Desconectar();
                return ListaResultado;
            } 
        #endregion 

	}
}

