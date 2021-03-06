﻿using Buildersoft.Andy.X.Storage.IO.Storage.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buildersoft.Andy.X.Storage.Services.Handlers
{
    public class StorageEventHandler
    {
        private readonly NodeDataStorageService _service;
        public StorageEventHandler(NodeDataStorageService service)
        {
            _service = service;
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            _service.DataStorageConnected += DataStorageService_DataStorageConnected;
            _service.DataStorageDisconnected += DataStorageService_DataStorageDisconnected;
        }

        private void DataStorageService_DataStorageDisconnected(Data.Model.Events.DataStorageDisconnectedArgs obj)
        {
            Console.WriteLine("Client Disconnected :" + obj.Id);
        }

        private async void DataStorageService_DataStorageConnected(Data.Model.Events.DataStorageConnectedArgs obj)
        {
            Console.WriteLine("Client Connected :" + obj.Id);
            // Sendback to Andy X, Tenants with their own properties
           await _service.SendStorageCurrentState(TenantConfigFile.GetTenants());
        }
    }
}
