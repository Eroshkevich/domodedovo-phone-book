using System;
using System.Threading.Tasks;
using Domodedovo.PhoneBook.Integrations.RandomUser.Exceptions;
using Domodedovo.PhoneBook.UserLoader.Exceptions;
using Domodedovo.PhoneBook.UserLoader.Services;
using Microsoft.Extensions.Logging;

namespace Domodedovo.PhoneBook.UserLoader
{
    public class AppHost
    {
        private readonly IAppActionService _appActionService;
        private readonly ILogger<AppHost> _logger;

        public AppHost(IAppActionService appActionService, ILogger<AppHost> logger)
        {
            _appActionService = appActionService;
            _logger = logger;
        }

        public async Task<ExitCode> Run()
        {
            _logger.LogInformation("Start");

            try
            {
                var appCommand = _appActionService.GetAppCommand();

                await appCommand.Execute();

                return ExitCode.Success;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                //TODO: Do it in Exception Handler
                switch (e)
                {
                    case AppCommandException _:
                        return ExitCode.UnknownCommand;
                    case RandomUserApiException _:
                        return ExitCode.RandomUserApiError;
                    default:
                        throw;
                }
            }
        }
    }
}