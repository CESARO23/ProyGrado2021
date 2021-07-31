//Libreria para acceso a Capa de Acceso a Datos
using CapaAD;
using System;
using System.Collections.Generic;
//Libreria para acceso a datos
using System.Data.Common;

namespace CapaRN
{
    public class xnumcor
    {
        #region Campos
        private int _cxncnumcor;
        private string _pxnctipcor;
        //Instancia para conexion a PostgreSQL 8.2
        private CLConexionMYSQL Conexion;
        #endregion

        #region Propiedades
        public int cxncnumcor
        {
            get { return this._cxncnumcor; }
            set { this._cxncnumcor = value; }
        }
        public string pxnctipcor
        {
            get { return this._pxnctipcor; }
            set { this._pxnctipcor = value; }
        }
        #endregion

        #region Constructor
        public xnumcor()
        {
            this._cxncnumcor = 0;
            this._pxnctipcor = "";
            this.Conexion = new CLConexionMYSQL();
        }
        #endregion

        #region Metodos
        public bool ObtenerDatos()
        {
            this.Conexion.Conectar();
            string sql = "select " +
                                 "cxncnumcor," +
                                 "pxnctipcor " +
                         "from xnumcor " +
                         "where " +
                                "pxnctipcor = @pxnctipcor";

            this.Conexion.PrepararComando(sql);

            this.Conexion.AsignarParametroCadena("@pxnctipcor", this._pxnctipcor);

            DbDataReader ResultadoConsulta = Conexion.EjecutarConsulta();



            if (ResultadoConsulta.Read())
            {
                this._cxncnumcor = Convert.ToInt32(ResultadoConsulta.GetString(0));
                this._pxnctipcor = ResultadoConsulta.GetString(1);
                return true;
                this.Conexion.Desconectar();
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
                                 "cxncnumcor," +
                                 "pxnctipcor " +
                         "from xnumcor " +
                         "where " +
                                "pxnctipcor = @pxnctipcor";

            this.Conexion.PrepararComando(sql);

            this.Conexion.AsignarParametroCadena("@pxnctipcor", this._pxnctipcor);

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
                string sql = "insert into xnumcor (" +
                                                   "cxncnumcor," +
                                                   "pxnctipcor" +
                                                   ") " +
                             "values (" +
                                      "@cxncnumcor," +
                                      "@pxnctipcor" +
                                                   ")";

                this.Conexion.PrepararComando(sql);

                this.Conexion.AsignarParametroEntero("@cxncnumcor", this._cxncnumcor);
                this.Conexion.AsignarParametroCadena("@pxnctipcor", this._pxnctipcor);

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
                string sql = "update xnumcor set " +
                                                 "cxncnumcor = @cxncnumcor" +
                             " where " +
                                    "pxnctipcor = @pxnctipcor";

                this.Conexion.PrepararComando(sql);

                this.Conexion.AsignarParametroEntero("@cxncnumcor", this._cxncnumcor);
                this.Conexion.AsignarParametroCadena("@pxnctipcor", this._pxnctipcor);

                this.Conexion.EjecutarTransaccion();
                this.Conexion.Desconectar();

                return true;
            }
        }
        public List<xnumcor> Lista(string where)
        {
            List<xnumcor> ListaResultado = new List<xnumcor>();
            this.Conexion.Conectar();
            string sql = "select " +
                                 "cxncnumcor," +
                                 "pxnctipcor " +
                         "from xnumcor ";

            if (where.Replace(" ", "") != "")
            {
                sql += "where " + where;
            }


            this.Conexion.PrepararComando(sql);
            DbDataReader ResultadoConsulta = Conexion.EjecutarConsulta();

            if (ResultadoConsulta != null)
            {
                while (ResultadoConsulta.Read())
                {
                    xnumcor Auxiliar = new xnumcor();
                    Auxiliar.cxncnumcor = ResultadoConsulta.GetInt32(0);
                    Auxiliar.pxnctipcor = ResultadoConsulta.GetString(1);
                    ListaResultado.Add(Auxiliar);
                }

            }
            this.Conexion.Desconectar();
            return ListaResultado;
        }
        public bool ObtenerSiguiente()
        {
            bool ban = false;
            if (this.ObtenerDatos())
            {
                this._cxncnumcor += 1;
                if (this.Modificar())
                {
                    ban = true;
                }
            }
            else
            {
                this._cxncnumcor = 1;
                if (this.Grabar())
                {
                    ban = true;
                }
            }
            return ban;
        }

        public string CorelativoCadena(int NumeroDigitos)
        {
            string aux = this._cxncnumcor.ToString();
            string aux2 = aux;

            if (aux.Length > NumeroDigitos)
            {
                return aux.Substring(0, NumeroDigitos);
            }
            else
            {
                for (int i = 1; i <= (NumeroDigitos - aux.Length); i++)
                {
                    aux2 = "0" + aux2;
                }
            }
            return aux2;

        }

        #endregion


        //no pasarse
    }
}