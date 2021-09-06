using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileApi.Repository;
using ProfileApi.ViewModels;

namespace ProfileApi.BusinessService
{
    public interface IProfileService
    {
        Task<List<Profile>> GetAll();
        Task AddProfile(Profile profile);
    }

    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<List<Profile>> GetAll()
        {
            return await _profileRepository.GetAll();
        }

        public async Task AddProfile(Profile profile)
        {
            await _profileRepository.AddProfile(profile);
        }
    }
}
