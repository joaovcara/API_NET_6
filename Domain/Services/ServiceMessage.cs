using Domain.Interfaces.InterfaceServices;
using Domain.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _IMessage;

        public ServiceMessage(IMessage message) 
        { 
            _IMessage = message;
        }
    }
}
