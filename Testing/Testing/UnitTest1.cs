using System;
using System.Linq;
using System.Reflection;
using AMC.Core.Abstractions.DataProvider.QueryBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlKata;
using Unity;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        private static void SetEntryAssembly()
        {
            SetEntryAssembly(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        private static void SetEntryAssembly(Assembly assembly)
        {
            AppDomainManager manager = new AppDomainManager();
            FieldInfo entryAssemblyfield = manager.GetType().GetField("m_entryAssembly", BindingFlags.Instance | BindingFlags.NonPublic);
            entryAssemblyfield.SetValue(manager, assembly);

            AppDomain domain = AppDomain.CurrentDomain;
            FieldInfo domainManagerField = domain.GetType().GetField("_domainManager", BindingFlags.Instance | BindingFlags.NonPublic);
            domainManagerField.SetValue(domain, manager);
        }

        private readonly Unity.IUnityContainer Container;

        public UnitTest1()
        {
            this.Container = new Unity.UnityContainer();
            Microsoft.Practices.Unity.Configuration.UnityConfigurationSection section = (Microsoft.Practices.Unity.Configuration.UnityConfigurationSection)System.Configuration.ConfigurationManager.GetSection("unity");
            section.Configure(Container);
        }

        [TestMethod]
        public void LoggerTest()
        {
            //add some new data
            var _loggerFactory = Container.ResolveAll<AMC.Core.Abstractions.Logger.ILoggerFactory>().First();
            var _logger = _loggerFactory.Create(typeof(UnitTest1));

            _logger.Log(new AMC.Core.Abstractions.Logger.LogEntry(AMC.Core.Abstractions.Logger.LoggingEventType.Error, "Hellow WindsorDI"));
        }

        [TestMethod]
        public void Base()
        {
            //add some new data
        }
    }
}
