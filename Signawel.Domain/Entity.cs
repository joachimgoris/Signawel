using System;

namespace Signawel.Domain
{
    public class Entity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
