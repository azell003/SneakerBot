using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerBot.Framework
{
    [Serializable]
    class Profile
    {
        public string firstName;
        public string lastName;
        public string email;
        public string phoneNo;
        public string billingAddress;
        public string city;
        public string zipcode;
        public string shoeSize;
        public string ccNumber;
        public string ccExpDate;
        public string ccCvc;
        public string siteUrl;
    }
}
