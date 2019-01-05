namespace CrusioUploader.Db.Base
{
    using System.Data;

    public abstract class BaseUploaderDbContext
    {
        private readonly string dbConnectionString;

        protected BaseUploaderDbContext(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                return new System.Data.SqlClient.SqlConnection(dbConnectionString);
            }
        }
    }
}
