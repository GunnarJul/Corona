using CoranaRegistration.Models;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoranaRegistration.Services
{
    public interface IPersonRegistrationService
    {
        Task AddItemAsync(PersonRegistration personRegistration);
        Task DeleteItemAsync(string id);
        Task<PersonRegistration> GetItemAsync(string id);
        Task<IEnumerable<PersonRegistration>> GetItemsAsync(string filter);
        Task UpdateItemAsync(string id, PersonRegistration person);
        Task<int> GetCovidNumberAsync();
        Task<int> GetCovidNumberRegistrationsAsync();
    }
}