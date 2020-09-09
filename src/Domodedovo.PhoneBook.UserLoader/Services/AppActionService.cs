using System;
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

            return appCommand switch
            {
                AppCommand.Fetch => GetFetchUsersAction(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IAppAction GetFetchUsersAction()
        {
            return _appActionFactory.CreateFetchUsersAction(_consoleCommandOptions.UsersCount);
        }
    }
}