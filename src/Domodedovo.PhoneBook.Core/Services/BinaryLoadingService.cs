﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domodedovo.PhoneBook.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Domodedovo.PhoneBook.Core.Services
{
    public class BinaryLoadingService : IBinaryLoadingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BinaryLoadingService> _logger;

        public BinaryLoadingService(HttpClient httpClient, ILogger<BinaryLoadingService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<byte[]> LoadAsync(Uri url, bool throwOnExceptions = true)
        {
            _logger.LogTrace($"Loading image from url {url} ...");

            var response = await _httpClient.GetAsync(url);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                if (throwOnExceptions)
                    throw new ImageLoadException(e);

                _logger.LogError($"Image loading from url {url} Error.", e);
                return null;
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}