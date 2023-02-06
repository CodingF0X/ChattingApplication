using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChattingInterfaces
{
    
   public interface Iclient
    {
        [OperationContract]
        void getMessage(string message, string userName);

        [OperationContract]
        void getUpdate(int value,string userName);
    }
}
