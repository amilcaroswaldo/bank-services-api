using creditcard.Infraestructure.DbContext.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace creditcard.Infraestructure.DbContext
{
    [ExcludeFromCodeCoverage]
    public class AppDbContext : IAppDbContext
    {
        #region parameters
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;
        #endregion
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetDbConnection()
        {
            var connectionString = _configuration.GetConnectionString("devConnection");
            _connection = new SqlConnection(connectionString);
            return _connection;
        }
    }
}
