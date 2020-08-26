using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Models.ModelsView
{
    public class Result<TPayload>
    {
        public int Status { get; set; } = 200;
        public TPayload Payload { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();

        public bool Succeeded => Status < 400 && Errors.Count == 0;

        public static implicit operator Result<TPayload>(TPayload payload) => Result.Ok(payload);

        public Result<TPayload> With(params Error[] errors)
        {
            Errors.AddRange(errors);
            return this;
        }
    }
}
