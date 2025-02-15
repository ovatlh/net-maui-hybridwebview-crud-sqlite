using crudMauiApp.Data;
using crudMauiApp.Data.Entities;

namespace crudMauiApp
{
    public class GlobalApp
    {
        public DBContext DBContext;
        public BaseRepository<Item> ItemRepository;

        public GlobalApp()
        {
            DBContext = new DBContext();
        }

        public BaseRepository<Item> RefItemRepository()
        {
            ItemRepository ??= new BaseRepository<Item>();
            return ItemRepository;
        }
    }
}