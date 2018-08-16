using System;
using System.Collections.Generic;
using System.Linq;
using Communication.Client;
using Communication.Server;
using Communication.Shared;
using Hik.Collections;

namespace servertcp
{
    public sealed class DataSingleton
    {
        private static readonly Lazy<DataSingleton> lazy =
            new Lazy<DataSingleton>(() => new DataSingleton());

        public static DataSingleton Instance => lazy.Value;
        
        private DataSingleton()
        {
            
        }
        
        public ThreadSafeSortedList<long, Room> Rooms { get; } = new ThreadSafeSortedList<long, Room>();

        public readonly ThreadSafeSortedList<long, ServerClient> ServerClients = new ThreadSafeSortedList<long, ServerClient>();
        public List<UserClient> UserClients => (from client in ServerClients.GetAllItems() select client.ToUserClient()).ToList();
    }
}