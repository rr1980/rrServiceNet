using System;

namespace rrServiceNet.BaseClient
{
    public class CallPackage
    {
        public string Command { get; set; }
        public string Data { get; set; }
        public Guid Guid { get; private set; }

        public CallPackage()
        {
            Guid = System.Guid.NewGuid();
        }
    }
}
