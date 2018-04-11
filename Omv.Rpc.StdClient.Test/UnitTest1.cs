using Doods.StdLibSsh;
using Doods.StdLibSsh.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Omv.Rpc.StdClient.Clients;
using Omv.Rpc.StdClient.Services;
using System;
using System.IO;

namespace Omv.Rpc.StdClient.Test
{
    internal class SshServiceTest : SshServiceBase
    {
        public SshServiceTest()
        {
            HostName = "XXX";
            UserName = "xxx";
            Password = "xxx";
            Toto();
        }

        private void Toto()
        {
            var path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            path = Path.Combine(path, @"UnitTest1Folder\StdClientTest.txt");
            var testData = System.IO.File.ReadAllText(path);

            var tab = testData.Split('@');
            HostName = tab[0];
            UserName = tab[1];
            Password = tab[2];
        }
    }


    [TestClass]
    public class UnitTest1
    {
        private static IClientSsh _sshService = new SshServiceTest();



        [TestMethod]
        public void GetStdClientTestFile()
        {
            var path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            path = Path.Combine(path, @"UnitTest1Folder\StdClientTest.txt");
            var testData = System.IO.File.ReadAllText(path);
            Assert.IsNotNull(testData);
        }


        [TestMethod]
        public void ReadStdClientTestFile()
        {
            var path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            path = Path.Combine(path, @"UnitTest1Folder\StdClientTest.txt");
            var testData = System.IO.File.ReadAllText(path);

            var tab = testData.Split('@');
            Assert.AreEqual(tab.Length,3);
        }

        [TestMethod]
        public void CanConnect()
        {
            var res = _sshService.CanConnect();
            Assert.IsTrue(res, $"Can't connect");
        }

        [TestMethod]
        public void Connect()
        {
            var res = _sshService.Connect();
            Assert.IsTrue(res, $"IS not connected");
        }

        [TestMethod]
        public void EnumerateShareCommand()
        {
            var cmd = ShareMgmtService.CreateEnumerateShareCommand();
            var query = new OmvRpcQuery<JObject>(_sshService, cmd);
            var ers = query.Run();
            Assert.IsNotNull(ers);
        }

        [TestMethod]
        public void StatusServicesCommand()
        {
            var cmd = SystemService.CreateStatusServicesCommand();
            var query = new OmvRpcQuery<JObject>(_sshService, cmd);
            var ers = query.Run();
            Assert.IsNotNull(ers);
        }
    }
}