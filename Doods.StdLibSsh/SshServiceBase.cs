﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Doods.StdLibSsh.Interfaces;
using Renci.SshNet;

namespace Doods.StdLibSsh
{
    public class SshServiceBase : IDisposable, IClientSsh
    {
        protected const int TimeoutInSecond = 60;
        private SshClient _client;

        protected  string HostName;
        protected  string UserName;
        protected  string Password;

        public SshClient Client => _client;
        //private ConnectionInfo _connectionInfo;
        protected SshServiceBase()
        {

        }

        protected virtual SshClient GetSshClient()
        {
            return _client ?? (_client = new SshClient(HostName, UserName, Password));
        }


        public Task<string> RunCommandAsync(string cmdStr, CancellationToken token)
        {
            if (_client == null) return null;

            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(token))
            {

                var request = _client.CreateCommand(cmdStr);

                var res =  Task.Factory.FromAsync((callback, stateObject) => request.BeginExecute(callback, stateObject), request.EndExecute, null);
                return res;
            }

            //return sshCommand.Result;
        }

        public SshCommand RunQuerry(string cmd)
        {
            return _client.RunCommand(cmd);
        }


        public async Task ConnectAsync()
        {
            await Task.Factory.StartNew(Connect);   
        }

        public void Connect()
        {
            if (_client == null)
            {
                GetSshClient();
            }
            _client.Connect();
        }

        public bool IsConnected()
        {
            return IsAuthenticated();

        }

        public bool IsAuthenticated()
        {
            if (_client == null) return false;
            return _client.IsConnected;
        }

        public void Dispose()
        {
            
            _client?.Dispose();
            _client = null;

        }
    }
}