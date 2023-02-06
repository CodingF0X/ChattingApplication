using ChattingInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChattingServer
{
    [ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Multiple,InstanceContextMode =InstanceContextMode.Single)]

    public class ChattingService : IChattingService
    {
        public ConcurrentDictionary<string, ConnectedClient> _connectedClient = new ConcurrentDictionary<string, ConnectedClient>();

        public List<string> getCurrentUsers()
        {
            List<string> listOfUsers = new List<string>();
            foreach(var client in _connectedClient)
            {
                listOfUsers.Add(client.Value.UserName);
            }
            return listOfUsers;
        }



        public int Login(string UserName)
        {

            foreach(var client in _connectedClient)
            {
                if (client.Key.ToLower() == UserName.ToLower())
                    return 1;
            }    

            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<Iclient>();

            ConnectedClient newClient = new ConnectedClient();
            newClient.connection = establishedUserConnection;
            newClient.UserName = UserName;

            _connectedClient.TryAdd(UserName, newClient);


            updateHelper(0, UserName);


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("client logged In  : {0} at {1}", newClient.UserName, System.DateTime.Now);
            Console.ResetColor();

            return 0;
        }

        public void logout()
        {
            ConnectedClient client = myClient();
            if(client !=null)
            {
                ConnectedClient removedClient;
                _connectedClient.TryRemove(client.UserName, out removedClient);

                updateHelper(1, removedClient.UserName);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("client logged off  : {0} at {1}",removedClient.UserName,System.DateTime.Now);
                Console.ResetColor();
            }
        }

        public void messageToAll(string message, string username)
        {

            foreach(var client in _connectedClient)
            {
                if (client.Key.ToLower() != username.ToLower())
                    client.Value.connection.getMessage(message,username);
                
            }
        }

        public ConnectedClient myClient()
        {
            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<Iclient>();
            
            foreach(var client in _connectedClient)
            {
                if (client.Value.connection == establishedUserConnection)
                    return client.Value;
            }

            return null;
        }

        public void updateHelper(int value, string userName)
        {
            foreach(var client in _connectedClient)
            {
                if(client.Value.UserName.ToLower() != userName.ToLower())
                {
                    client.Value.connection.getUpdate(value, userName);

                }
            }
        }
    }
}
