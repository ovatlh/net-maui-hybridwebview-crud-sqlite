using SQLite;

namespace crudMauiApp.Data
{
    public class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }
}