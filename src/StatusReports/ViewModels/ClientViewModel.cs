using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatusReports.Models;

namespace StatusReports.ViewModels
{
    public class ClientViewModel
    {
        public ClientViewModel() { }

        public ClientViewModel(Client aClient, LookupModel lookupData)
        {
            client = aClient;
            LookupData = lookupData;
        }
        public Client client { get; set; }
        public LookupModel LookupData { get; set; }
    }
}
