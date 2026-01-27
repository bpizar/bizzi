//using JayGor.Calendar.DataAccess.MySql;
//using JayGor.Calendar.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using JayGor.Calendar.Entities.CustomEntities;
//using JayGor.Calendar.Entities.Responses;
//using JayGor.Calendar.Entities.Entities;
//using JayGor.Calendar.Entities.Enumerators;

//namespace JayGor.Calendar.DataAccess.Factories.MySqlServer
//{
//    public partial class MySqlServerAppDa : IAApp
//    {
//        public IEnumerable<Staff_Pay_Settings> GetStaffPaySettingsByIdfStaff(long id)
//        {
//            var context = MySqlServerFactoryDB.Create();
//            var transaction = context.Database.BeginTransaction();

//            try
//            {                
//                var staffpaysettings = context.Staff_Pay_Settings.Where(c => c.IdfStaff == id).ToList();
//                transaction.Commit();
//                return staffpaysettings;
//            }
//            catch (Exception ex)
//            {
//                transaction.Rollback();
//                throw ex;
//            }
//        }
//    }
//}
