using System.Collections.Generic;
using System.Threading.Tasks;
using CosmosDBTutorial.Models;

namespace CosmosDBTutorial.Services
{
    public interface ICosmosDBService
    {
        Task<IEnumerable<Employee>> GetItemsAsync(string query);
        Task<Employee> GetItemAsync(string id);
        Task AddItemAsync(Employee employee);
        Task UpdateItemAsync(string id, Employee employee);
        Task DeleteItemAsync(string id);
    }
}
