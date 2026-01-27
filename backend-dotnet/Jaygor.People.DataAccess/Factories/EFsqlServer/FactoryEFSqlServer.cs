//using JayGor.Calendar.DataAccess.Factories.EFsqlServer;
//using System;

//namespace JayGor.Calendar.DataAccess.EFsqlServer
//{
//    public class FactoryEFSqlServer : IFactoriesAD
//    {

//        private string connectionString;

//        public FactoryEFSqlServer(string conexionString)
//        {
//            this.connectionString = conexionString;
//        }

//        public IAApp App
//        {
//            get
//            {
//                //throw new NotImplementedException();
//                return new SqlServerAppDa(this.connectionString);
//            }
//        }
//    }
//}
