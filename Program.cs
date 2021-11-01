using LearningWordsApp.Repositories;
using LearningWordsApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace LearningWordsApp
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();

            scope.ServiceProvider.GetRequiredService<BusinessLogic>().Start();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IDeckService, DeckService>();
            services.AddSingleton<IDeckRepository, DeckRepository>();
            services.AddSingleton<ICardService, CardService>();
            services.AddSingleton<ICardRepository, CardRepository>();
            services.AddSingleton<BusinessLogic>();
            _serviceProvider = services.BuildServiceProvider(true);
        }
    }
}


