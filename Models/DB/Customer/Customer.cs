using System;

namespace Webapi.DB.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Nullable<DateTime> Created { get; set; }
        public Nullable<DateTime> Updated { get; set; }
    }
}