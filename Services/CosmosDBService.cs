using CosmosDBTutorial.Models;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBTutorial.Services
{
    public class CosmosDbService : ICosmosDBService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }
        
        public async Task AddItemAsync(Employee employee)
        {
            await this._container.CreateItemAsync<Employee>(employee, new PartitionKey(employee.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Employee>(id, new PartitionKey(id));
        }

        public async Task<Employee> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Employee> response = await this._container.ReadItemAsync<Employee>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch(CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            { 
                return null;
            }

        }

        public async Task<IEnumerable<Employee>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Employee>(new QueryDefinition(queryString));
            List<Employee> results = new List<Employee>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Employee employee)
        {
            await this._container.UpsertItemAsync<Employee>(employee, new PartitionKey(id));
        }
    }
}
