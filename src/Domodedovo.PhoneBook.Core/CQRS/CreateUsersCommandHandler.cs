using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domodedovo.PhoneBook.Core.Services;
using Domodedovo.PhoneBook.Data;
using Domodedovo.PhoneBook.Data.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domodedovo.PhoneBook.Core.CQRS
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand>
    {
        private readonly IMapper _mapper;
        private readonly UsersDbContext _usersDbContext;
        private readonly ILogger<CreateUsersCommandHandler> _logger;
        private readonly IBinaryLoadingService _binaryLoadingService;
        private readonly IFileStorageService _fileStorageService;

        public CreateUsersCommandHandler(UsersDbContext usersDbContext, IMapper mapper,
            ILogger<CreateUsersCommandHandler> logger, IBinaryLoadingService binaryLoadingService,
            IFileStorageService fileStorageService)
        {
            _usersDbContext = usersDbContext;
            _mapper = mapper;
            _logger = logger;
            _binaryLoadingService = binaryLoadingService;
            _fileStorageService = fileStorageService;
        }

        public async Task<Unit> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Adding {request.Users.Count} Users...");

            var users = _mapper.Map<ICollection<User>>(request.Users);

            using (var transaction = await _usersDbContext.Database.BeginTransactionAsync(cancellationToken))
            {
                _usersDbContext.AddRange(users);

                await _usersDbContext.SaveChangesAsync(cancellationToken);

                if (request.PictureLoadingOptions != null
                    && (request.PictureLoadingOptions.StoreInDatabaseEnabled
                        || request.PictureLoadingOptions.StoreInFileSystemEnabled))
                {
                    foreach (var user in users)
                    {
                        if (!user.Pictures.Any())
                            continue;

                        foreach (var picture in user.Pictures.Select(e => e.Picture))
                        {
                            if (picture.Url == null)
                            {
                                _logger.LogWarning($"Picture {picture.Id} loading skipped. Url not defined.");
                                continue;
                            }

                            var image = await _binaryLoadingService.LoadAsync(picture.Url,
                                request.PictureLoadingOptions.ThrowOnExceptions);

                            if (image == null)
                                continue;

                            if (request.PictureLoadingOptions.StoreInDatabaseEnabled)
                                picture.Image = image;

                            if (!request.PictureLoadingOptions.StoreInFileSystemEnabled)
                                continue;

                            var imagePath = await _fileStorageService.SaveAsync(
                                $"users\\{user.Id}\\pictures\\{picture.Id}.jpg", image,
                                request.PictureLoadingOptions.ThrowOnExceptions);

                            picture.Path = imagePath;
                        }
                    }

                    _usersDbContext.UpdateRange(users);
                }

                await transaction.CommitAsync(cancellationToken);
            }

            await _usersDbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"{users.Count} Users added.");

            return Unit.Value;
        }
    }
}