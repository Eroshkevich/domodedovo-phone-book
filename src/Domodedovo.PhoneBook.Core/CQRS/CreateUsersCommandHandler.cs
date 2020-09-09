using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domodedovo.PhoneBook.Core.Services;
using Domodedovo.PhoneBook.Data;
using Domodedovo.PhoneBook.Data.Models.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domodedovo.PhoneBook.Core.CQRS
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand>
    {
        private readonly IMapper _mapper;
        private readonly UsersDbContext _usersDbContext;
        private readonly ILogger<CreateUsersCommandHandler> _logger;
        private readonly IImageLoadService _imageLoadService;

        public CreateUsersCommandHandler(UsersDbContext usersDbContext, IMapper mapper,
            ILogger<CreateUsersCommandHandler> logger, IImageLoadService imageLoadService)
        {
            _usersDbContext = usersDbContext;
            _mapper = mapper;
            _logger = logger;
            _imageLoadService = imageLoadService;
        }

        public async Task<Unit> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Adding {request.Users.Count} Users...");

            var users = _mapper.Map<ICollection<User>>(request.Users);

            foreach (var user in users)
            {
                if (request.PictureLoadingOptions != null &&
                    user.Pictures.Any()
                    && (request.PictureLoadingOptions.StoreInDatabaseEnabled
                        || request.PictureLoadingOptions.StoreInFileSystemEnabled))
                {
                    foreach (var userPicture in user.Pictures)
                    {
                        if (userPicture.Picture.Url == null)
                        {
                            _logger.LogWarning($"Picture {userPicture.Picture.Id} loading skipped. Url not defined.");
                        }

                        var image = await _imageLoadService.LoadAsync(userPicture.Picture.Url);

                        if (image != null && request.PictureLoadingOptions.StoreInDatabaseEnabled)
                            userPicture.Picture.Image = image;
                    }
                }

                _usersDbContext.Add(user);
            }

            await _usersDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"{users.Count} Users added.");

            return Unit.Value;
        }
    }
}