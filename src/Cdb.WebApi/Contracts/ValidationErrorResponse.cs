using System.Collections.Generic;

namespace Cdb.WebApi.Contracts
{
    public class ValidationErrorResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
