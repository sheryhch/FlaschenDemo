using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlaschenDemo.Core.Interfaces;
using FlaschenDemo.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlaschenDemo.IOC
{
    public static class Register
    {
        public static void Add(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IBeerService, BeerService>();

            #endregion

           


        }
    }
}
