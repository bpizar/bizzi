//// using JayGor.People.DataAccess.Factories.MySqlServer;
//// using Microsoft.Extensions.Configuration;
//// using System;

//using Microsoft.Extensions.Configuration;
//using System;
//using JayGor.People.DataAccess.Factories.MySqlServer;

//namespace JayGor.People.DataAccess
//{
//    public class FactoriesAD
//    {
//        public static IFactoriesAD GetFactory(string dataProvider)
//        {
//            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//            var configuration = builder.Build();
//            var connectionString = configuration.GetConnectionString(string.Format("{0}_ConnectionString", dataProvider));
           
//            switch (dataProvider)
//            {
//                case "EntityFramework.MySql":
//                    return new FactoryEFMySqlServer(connectionString);                    
//                //case "EntityFramework.SqlServer":
//                //    return new FactoryEFSqlServer(connectionString);
//                default:
//                    throw new Exception("Error with provider : " + dataProvider);
//            }
//        }
//    }
//}