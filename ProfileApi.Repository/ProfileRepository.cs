using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProfileApi.ViewModels;

namespace ProfileApi.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly string _dbName = "profile.db";
        public async Task<List<Profile>> GetAll()
        {
            var profiles = new List<Profile>();
            if (!System.IO.File.Exists(_dbName)) return profiles;
            var records = await System.IO.File.ReadAllTextAsync(_dbName);
            profiles = JsonConvert.DeserializeObject<List<Profile>>(records);
            return profiles;
        }

        public async Task AddProfile(Profile profile)
        {
            if (!System.IO.File.Exists(_dbName))
            {
                await System.IO.File.AppendAllTextAsync(_dbName, JsonConvert.SerializeObject(new List<Profile> { profile }));
            }
            else
            {
                var records = await System.IO.File.ReadAllTextAsync(_dbName);
                var profiles = JsonConvert.DeserializeObject<List<Profile>>(records);
                profiles.Add(profile);
                await System.IO.File.WriteAllTextAsync(_dbName, JsonConvert.SerializeObject(profiles));
            }
        }
    }
}
