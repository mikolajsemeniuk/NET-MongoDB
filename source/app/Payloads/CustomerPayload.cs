using System;

namespace app.Payloads
{
    public class CustomerPayload
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}