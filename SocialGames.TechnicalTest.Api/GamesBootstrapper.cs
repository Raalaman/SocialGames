using LightInject;
using SocialGames.TechnicalTest.Api.Controllers;
using System.Reflection;

namespace SocialGames.TechnicalTest.Api
{
    public class GamesBootstrapper
    {
        IServiceContainer _serviceContainer;

        public GamesBootstrapper(IServiceContainer container)
        {
            _serviceContainer = container;
        }

        public void Run()
        {
            RegisterServices();
        }

        protected void RegisterServices()
        {
            var sampleAssembly = Assembly.GetAssembly(typeof(GamesControllers));
            _serviceContainer.RegisterAssembly(sampleAssembly);           
        }
    }
}
