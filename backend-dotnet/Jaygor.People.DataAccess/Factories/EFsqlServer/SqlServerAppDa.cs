//using JayGor.Calendar.DataAccess.EFsqlServer;
//using JayGor.Calendar.DataAccess.SqlServer;
//using JayGor.Calendar.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;


//namespace JayGor.Calendar.DataAccess.Factories.EFsqlServer
//{
//    public partial class SqlServerAppDa : IAApp
//    {

//        public SqlServerAppDa(string conexion)
//        {
//            SqlServerFactoryDB.EstablecerConfiguracion(conexion);
//        }

//        public IEnumerable<tabla1> ObtenerTabla1()
//        {
//            using (var context = SqlServerFactoryDB.Create())
//            {
//                var salida = new List<tabla1>();
//                salida.AddRange(context.tabla1);

//                return salida;
//            }
//        }

//        public string Ping()
//        {
//            return string.Format("Pong - {0}", DateTime.Now);
//        }       
//    }
//}