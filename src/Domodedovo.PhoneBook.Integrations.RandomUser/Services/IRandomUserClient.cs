﻿using System.Threading.Tasks;
using Domodedovo.PhoneBook.Integrations.RandomUser.DTO;

namespace Domodedovo.PhoneBook.Integrations.RandomUser.Services
{
    public interface IRandomUserClient
    {
        Task<ResponseDTO> GetUsersAsync(ushort? count = null);
    }
}