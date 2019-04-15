
namespace DaoModels.DAO.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string KeyAuthorized { get; set; }
    }
}