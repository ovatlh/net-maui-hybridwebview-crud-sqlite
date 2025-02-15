using crudMauiApp.Data;

namespace crudMauiApp
{
    public class GlobalApp
    {
        public DBContext DBContext;

        public GlobalApp()
        {
            DBContext = new DBContext();
        }
    }
}