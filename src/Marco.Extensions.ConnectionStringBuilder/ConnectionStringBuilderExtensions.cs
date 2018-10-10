using System;
using System.Data;
using System.Data.SqlClient;

namespace Marco.Extensions.ConnectionStringBuilder
{
    public static class ConnectionStringBuilderExtensions
    {
        /// <summary>
        ///  Builder a new DbConnection for alternate database catalogs
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="catalogName"></param>
        /// <returns></returns>
        public static IDbConnection BuildDbConnection(this DefaultConnectionString defaultConnectionString, string catalogAlternate = null)
        {
            if (defaultConnectionString == null)
                throw new ArgumentNullException(nameof(defaultConnectionString));

            return new SqlConnection(BuildConnectionString(defaultConnectionString, catalogAlternate));
        }

        /// <summary>
        ///  Builder a new Connection String for alternate database catalogs
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="catalogName"></param>
        /// <returns></returns>
        public static string BuildConnectionString(this DefaultConnectionString defaultConnectionString, string catalogAlternate = null)
        {
            if (defaultConnectionString == null)
                throw new ArgumentNullException(nameof(defaultConnectionString));

            return Builder(defaultConnectionString, catalogAlternate).ConnectionString;
        }


        private static SqlConnectionStringBuilder Builder(DefaultConnectionString defaultConnectionString, string catalogAlternate) =>
            new SqlConnectionStringBuilder
            {
                ConnectionString = defaultConnectionString.ConnectionString,
                InitialCatalog = catalogAlternate ?? defaultConnectionString.Catalog
            };
    }
}
