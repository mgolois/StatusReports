using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReports.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int RDAProjectId { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

    }
}
