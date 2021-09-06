using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileApi.ViewModels;

namespace ProfileApi.Repository
{
    public interface IProfileRepository
    {
        Task<List<Profile>> GetAll();
        Task AddProfile(Profile profile);
    }
}