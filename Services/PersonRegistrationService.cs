using CoranaRegistration.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoranaRegistration.Services
{
    public class PersonRegistrationService : IPersonRegistrationService
    {
        private Container _container;
        public PersonRegistrationService(CosmosClient databaseClient, string databaseName, string containerName)
        {
            _container = databaseClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(PersonRegistration personRegistration)
        {
            await _container.CreateItemAsync<PersonRegistration>(personRegistration, new PartitionKey(personRegistration.id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<PersonRegistration>(id, new PartitionKey(id));
        }

        public async Task UpdateItemAsync(string id, PersonRegistration person)
        {
            await _container.UpsertItemAsync<PersonRegistration>(person, new PartitionKey(id));
        }
        public async Task<PersonRegistration> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<PersonRegistration> response = await _container.ReadItemAsync<PersonRegistration>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ce) when (ce.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PersonRegistration>> GetItemsAsync(string filter)
        {
            var query = _container.GetItemQueryIterator<PersonRegistration>(new QueryDefinition(filter));
            var result = new List<PersonRegistration>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response.ToList());
            }
            return result;
        }

        public async Task<int> GetCovidNumberAsync()
        {
            var filter ="SELECT VALUE COUNT(1) FROM c where c.testresult = true";
            var query = _container.GetItemQueryIterator<int>(new QueryDefinition(filter));
            var result = new List<int>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response.ToList());
            }
            return result.Any() ? result.First() : 0;
        }

        public async Task<int> GetCovidNumberRegistrationsAsync()
        {
            var filter = "SELECT VALUE COUNT(1) FROM c";
            var query = _container.GetItemQueryIterator<int>(new QueryDefinition(filter));
            var result = new List<int>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response.ToList());
            }
            return result.Any() ? result.First() : 0;
        }
    }
}
