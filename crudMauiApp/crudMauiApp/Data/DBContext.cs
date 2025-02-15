using crudMauiApp.Data.Entities;
using SQLite;

namespace crudMauiApp.Data
{
    public class DBContext
    {
        public string dbName;
        public string dbPath;
        public readonly SQLiteAsyncConnection dbConnection;

        public DBContext()
        {
            dbName = $@"db.{AppInfo.Current.VersionString}.db3";
            dbPath = Path.Combine(FileSystem.AppDataDirectory, dbName);
            dbConnection = new SQLiteAsyncConnection(dbPath);

            CreateTables();
        }

        public async Task CreateTables()
        {
            CreateTableResult resultItem = await dbConnection.CreateTableAsync<Item>();
            Console.WriteLine(resultItem);
        }
    }
}