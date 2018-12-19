using System;
using System.Linq;
using AMC.Core.Abstractions.QuantumBasis.QuantumTypes;
using AMC.Core.Abstractions.QuantumBasis.QuantumUsers;
using AMC.Core.DataStorages.MSSQLDataProvider;
using AMC.Core.DataStorages.MSSQLDataProvider.SQLKataQueryBuilderExtention;
using AMC.Core.Logic.QuantumDataProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlKata;
using Unity;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        private readonly Unity.IUnityContainer Container;

        public UnitTest1()
        {
            this.Container = new Unity.UnityContainer();
            Microsoft.Practices.Unity.Configuration.UnityConfigurationSection section = (Microsoft.Practices.Unity.Configuration.UnityConfigurationSection)System.Configuration.ConfigurationManager.GetSection("unity");
            section.Configure(Container);
        }

        [TestMethod]
        public void TestMethod1()
        {
            //add some new data
            var _loggerFactory = Container.ResolveAll<AMC.Core.Abstractions.Logger.ILoggerFactory>().First();
            var _logger = _loggerFactory.Create(typeof(UnitTest1));

            _logger.Log(new AMC.Core.Abstractions.Logger.LogEntry(AMC.Core.Abstractions.Logger.LoggingEventType.Error, "Hellow WindsorDI"));
        }

        [TestMethod]
        public void MSSQLKataTesting()
        {
            var query = new Query("Users").Where("Id", 1).Where("Status", "Active");
            var storage = new MSSQLDataStoage();
            var somedata = storage.ExecuteQuery(query.GetQueryBuilder());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void QuantumStorageTest()
        {
            var storage = new MSSQLDataStoage();
            QuantumStorageFactory f = new QuantumStorageFactory();
            var repo = f.GetQuantumStorage<QuantumUser>(storage);
            var u1 = repo.Load(1);
            var u2 = repo.Load(() => { return new ulong[] { (2 + 2), 4, 100500 }; });
        }
    }
}
