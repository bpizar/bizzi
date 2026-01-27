// -----------------------------------------------------------------------
// <copyright file="MySqlDatabaseService.cs" company="facilSFE">
//     Company copyright tag.
// </copyright>
// -----------------------------------------------------------------------
namespace JayGor.People.DataAccess.Factories.MySqlServer
{ 
    using JayGor.People.DataAccess;
    using JayGor.People.DataAccess.MySql;
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;


    /// <summary>
    /// My sql database service.
    /// </summary>
    public partial class MySqlDatabaseService : IDatabaseService// IDisposable
    {
        /// <summary>
        /// The context.
        /// </summary>
        private MySqlContextDB context;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:FacilSFE.AccesoDatos.Fabricas.MySqlServer.MySqlDatabaseService"/> class.
        /// </summary>
        /// <param name="context">The Context.</param>
        public MySqlDatabaseService(MySqlContextDB context)
        {
            this.context = context;
        }

       
    }
}