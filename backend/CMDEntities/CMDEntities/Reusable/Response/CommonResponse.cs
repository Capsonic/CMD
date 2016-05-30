using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDEntities.Reusable.Response
{
    class CommonResponse
    {
        public bool ErrorThrown { get; set; }
        public string ResponseDescription { get; set; }
        public object Result { get; set; }
    }

    public class ValidationResult
    {
        public long EntityId { get; set; }
        public string EntityKind { get; set; }
        public string FriendlyIdentifier { get; set; }
        public string Description { get; set; }
    }
}
