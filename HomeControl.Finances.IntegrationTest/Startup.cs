using AutoMapper;
using HomeControl.Finances.IntegrationTest.SeedWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomeControl.Finances.IntegrationTest
{
    [TestClass]
    public class Startup
    {

        public TestContext TestContext { get; set; }


        [AssemblyInitialize]
        public static void Configure(TestContext testContext)
        {
            ///Utilze este método para configurar dados iniciais do projeto
            ///MapperProfile.Initialize();
            //Empty
        }

        [TestMethod]
        public void VerificarMapper()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
