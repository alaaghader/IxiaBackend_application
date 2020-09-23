using AutoMapper;
using AutoMapper.QueryableExtensions;
using ixiaBackend_application.Models;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IxiaContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProfileService(IxiaContext context,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _mapper = mapper;

            this.webHostEnvironment = hostEnvironment;
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

        public async  Task<Result<UserView>> UpdateProfilePicture(string userId, ProfileImageInput profileImageInput)
        {
            var user = await _context.Users.SingleOrDefaultAsync(e => e.Id == userId);
            user.ProfilePicture = UploadedFile(profileImageInput.Img);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserView>(user);
        }

        private string UploadedFile(IFormFile model)
        {
            try
            {
                if (model.Length > 0)
                {
                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = File.Create(webHostEnvironment.WebRootPath + "\\Upload\\" + model.FileName))
                    {
                        model.CopyTo(fileStream);
                        fileStream.Flush();
                        return model.FileName;
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
    }
}
