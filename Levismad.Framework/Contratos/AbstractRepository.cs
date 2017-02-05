using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Oracle;

namespace Levismad.Framework.Contratos
{
    public abstract class AbstractRepository
    {
        public IDbConnection Db { get; set; }
        public string ConnectionString { get; set; }
        public string HostName { get; set; }

        protected AbstractRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["conexao_desenvolvimento"].ConnectionString;
            Db = new OracleConnection(ConnectionString);
        }
        public IDbConnection Open(int commandTimeOut = 120)
        {
            if (Db.State == ConnectionState.Broken)
            {
                Db.Close();
            }
            if (Db.State == ConnectionState.Closed)
            {
                Db.Open();
            }

            OrmLiteConfig.DialectProvider = OracleOrmLiteDialectProvider.Instance;
            OrmLiteConfig.ClearCache();
            OrmLiteConfig.DialectProvider.NamingStrategy = new OrmLiteNamingStrategyBase();
            OrmLiteConfig.CommandTimeout = commandTimeOut;

            return Db;
        }
        public void OpenConnection()
        {

            OrmLiteConfig.DialectProvider = OracleOrmLiteDialectProvider.Instance;
            OrmLiteConfig.ClearCache();
            OrmLiteConfig.DialectProvider.NamingStrategy = new OrmLiteNamingStrategyBase();
            OrmLiteConfig.CommandTimeout = 120;

            if (Db.State == ConnectionState.Broken)
            {
                Db.Close();
            }
            if (Db.State == ConnectionState.Closed)
            {
                Db.Open();
            }



        }
        public IDbTransaction CreateTransaction()
        {
            if (Db.State == ConnectionState.Broken)
            {
                Db.Close();
            }
            if (Db.State == ConnectionState.Closed)
            {
                Db.Open();
            }
            var transaction = Db.OpenTransaction();
            return transaction;
        }

    }
}
