using System;

namespace Accounting.WS.Models.ConfigModel
{
    public class Authentication
    {
        public string Domain { get; set; }
        public string SecretKey { get; set; }
        public int ExpiredDate { get; set; }
    }
}