using app.Models;
using MongoDB.Driver;

namespace app.Data
{
    public class DataContext
    {
        public readonly IMongoCollection<Customer> Customers;

        public DataContext(IMongoDatabase database) =>
            Customers = database.GetCollection<Customer>("customers");
    }
}