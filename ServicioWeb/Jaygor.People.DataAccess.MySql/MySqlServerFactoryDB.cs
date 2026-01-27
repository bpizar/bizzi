//using Microsoft.EntityFrameworkCore;

//namespace JayGor.People.DataAccess.MySql
//{
//	public static class MySqlServerFactoryDB
//	{
//		static string connectionString = string.Empty;

//		public static void SetConfiguration(string conexion)
//		{
//			connectionString = conexion;
//		}

//		public static MySqlContextDB Create()
//		{
//			//return null;

//			//// EstablecerConfiguracion();
//			var optionsBuilder = new DbContextOptionsBuilder<MySqlContextDB>();
//            optionsBuilder.UseMySql(connectionString);
//			var context = new MySqlContextDB(optionsBuilder.Options);
//			context.Database.EnsureCreated();

//            // optionsBuilder.use .UseLazyLoadingProxies();

//            return context;
//		}
//	}
//}