using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStudy.Data
{
    public class UserSettings
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public HubClient HubClient { get; set; }
    }
}
