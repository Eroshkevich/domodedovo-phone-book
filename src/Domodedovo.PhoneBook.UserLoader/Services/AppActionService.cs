using System;
using Domodedovo.PhoneBook.Core.Options;
using Domodedovo.PhoneBook.UserLoader.Actions;
using Domodedovo.PhoneBook.UserLoader.Exceptions;
using Domodedovo.PhoneBook.UserLoader.Options;
using Microsoft.Extensions.Options;

namespace Domodedovo.PhoneBook.UserLoader.Services
{
    public class AppActionService : IAppActionService
    {
        private const ushort MaxUsersCount = 5000;

        private readonly IAppActionFactory _appActionFactory;
        private readonly ConsoleCommandOptions _consoleCommandOptions;

        public AppActionService(IAppActionFactory appActionFactory,
            IOptions<ConsoleCommandOptions> consoleCommandOptions)
        {
            _appActionFactory = appActionFactory;
            _consoleCommandOptions = consoleCommandOptions.Value;
        }

        public IAppAction GetAppCommand()
        {
            if (_consoleCommandOptions.UsersCount > MaxUsersCount)
                throw new AppCommandException($"UsersCount out of range. Maximum Users count = {MaxUsersCount}.");

            var command = _consoleCommandOptions.Command;

            if (!Enum.TryParse<AppCommand>(command, true, out var appCommand))
                throw new AppCommandException($"Unknown command \"{command}\".");

            switch (appCommand)
            {
                case AppCommand.Fetch:
                {
                    var pictureLoadingOptionsString = _consoleCommandOptions.PictureLoading;

                    var pictureLoadingOptions = new PictureLoadingOptions();
                    if (pictureLoadingOptionsString.Contains("_db"))
                        pictureLoadingOptions.StoreInDatabaseEnabled = true;
                    if (pictureLoadingOptionsString.Contains("_fs"))
                        pictureLoadingOptions.StoreInFileSystemEnabled = true;
                    if (pictureLoadingOptionsString.Contains("_silent"))
                        pictureLoadingOptions.ThrowOnExceptions = false;

                    return GetFetchUsersAction(pictureLoadingOptions);
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IAppAction GetFetchUsersAction(PictureLoadingOptions pictureLoadingOptions)
        {
            return _appActionFactory.CreateFetchUsersAction(_consoleCommandOptions.UsersCount, pictureLoadingOptions);
        }
    }
}