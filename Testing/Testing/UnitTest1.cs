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
        private readonly Unity.IUnityContainer Container;

        public UnitTest1()
        {
            this.Container = new Unity.UnityContainer();
            Microsoft.Practices.Unity.Configuration.UnityConfigurationSection section = (Microsoft.Practices.Unity.Configuration.UnityConfigurationSection)System.Configuration.ConfigurationManager.GetSection("unity");
            section.Configure(Container);
        }
    }
}
