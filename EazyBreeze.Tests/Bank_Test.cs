using EazyBreeze.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EazyBreeze.Tests
{
    [TestClass]
    public class Bank_Test
    {
        [TestMethod]
        public void Bank_Work_Test()
        {
            var bank = new Bank(2);

            var clients = new[] { 4, 10, 5 };

            foreach(var client in clients)
                bank.AddClient(client);

            var duration = bank.Work();

            Assert.AreEqual(duration, 10);
        }
    }
}
