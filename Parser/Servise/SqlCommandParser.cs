using DaoModels.DAO;

namespace Parser.DAO
{
    public class SqlCommandParser
    {
        private Context context = null;
        public SqlCommandParser()
        {
            context = new Context();
        }
    }
}
