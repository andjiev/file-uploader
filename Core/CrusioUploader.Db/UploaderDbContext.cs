namespace CrusioUploader.Db
{
    public class UploaderDbContext : Base.BaseUploaderDbContext
    {
        public UploaderDbContext(string dbConnectionString) : base(dbConnectionString) { }
    }
}
