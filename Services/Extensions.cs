﻿using Microsoft.Extensions.Configuration;

namespace EmailAPI.Services
{
        public static class EmailExtension
        {
            public static string GetEmailConfiguration(this IConfiguration config, string name)
            {
                return config.GetSection("EmailConfiguration")[name];
            }
        }
    
}
