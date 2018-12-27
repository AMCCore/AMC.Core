using System;
using System.Linq;
using AMC.Core.Abstractions.DataProvider.QueryBuilder;
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
        public void MSSQLKataTesting()
        {
            var query = new Query("Users").Where("Id", 1).Where("Status", "Active");
            var storage = new MSSQLDataStoage();
            var somedata = storage.ExecuteQuery(query.GetQueryBuilder());
        }
    }
}
