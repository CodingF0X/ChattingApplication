using ChattingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChattingClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientCallback : Iclient
    {
        public void getMessage(string message, string userName)
        {
                ((MainWindow)Application.Current.MainWindow).takeMessage(message, userName);

            
        }

      

        public void getUpdate(int value, string userName)
        {
            switch(value)
            {
                case 0:
                    {
                        ((MainWindow)Application.Current.MainWindow).addUser(userName);
                        break;
                    }

                case 1:
                    {
                        ((MainWindow)Application.Current.MainWindow).removeUser(userName);
                        break;
                    }
            }
        }
    }
}
