using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChattingInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChattingService" in both code and config file together.
    [ServiceContract (CallbackContract =typeof(Iclient))]
    public interface IChattingService
    {
        [OperationContract]
         int Login(string UserName);

        [OperationContract]
        void logout();

        [OperationContract]
        void messageToAll(string message, string username);

        [OperationContract]
        List<string> getCurrentUsers();
    }
}
