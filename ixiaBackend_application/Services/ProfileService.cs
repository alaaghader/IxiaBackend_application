using AutoMapper;
using AutoMapper.QueryableExtensions;
using ixiaBackend_application.Models;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IxiaContext _context;
        private readonly IMapper _mapper;

        public ProfileService(IxiaContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<UserView>> GetUserAsync(string id)
        {
            var result = await _context.Users.Where(x => x.Id == id)
                .ProjectTo<UserView>(_mapper.ConfigurationProvider)
                .FirstAsync();

            if (result == null)
            {
                return Result.NotFound<UserView>();
            }

            return result;
        }

        public async Task<Result<UserView>> UpdateProfileAsync(string userId, ProfileInput profileInput)
        {
            var user = await _context.Users.SingleOrDefaultAsync(e => e.Id == userId);
            _mapper.Map(profileInput, user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserView>(user);
        }
    }
}
