﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Optimizer.Services.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Optimizer.Api.Config
{
    public static class StartUpConfiguration
    {
        //public static IServiceCollection AddAppsettingBinding(this IServiceCollection service, IConfiguration configuration)
        //{
        //    service.Configure<ApiUrls>(opts => configuration.GetSection("ApiUrls").Bind(opts));
        //    return service;
        //}

        public static IServiceCollection AddProxiesRegistration(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddHttpContextAccessor();

            service.AddHttpClient<IOptimizerLogic, OptimizerLogic>();

            return service;
        }
    }
}
