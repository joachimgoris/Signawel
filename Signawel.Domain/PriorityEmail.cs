using System;

namespace Signawel.Domain
{
    public class PriorityEmail
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string EmailSuffix { get; set; }
    }
}
