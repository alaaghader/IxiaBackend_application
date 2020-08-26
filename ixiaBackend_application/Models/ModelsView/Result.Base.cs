using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Models.ModelsView
{
    public static class Result
    {
        public static Result<T> Ok<T>(T value) => new Result<T>
        {
            Status = 200,
            Payload = value
        };
        public static Result<T> NotFound<T>(T value = default) => new Result<T>
        {
            Status = 404,
            Payload = value
        };
        public static Result<T> Conflict<T>(T value = default) => new Result<T>
        {
            Status = 409,
            Payload = value
        };
        public static Result<T> BadRequest<T>(T value = default) => new Result<T>
        {
            Status = 400,
            Payload = value
        };
        public static Result<T> Unauthorized<T>(T value = default) => new Result<T>
        {
            Status = 401,
            Payload = value
        };
        public static Result<T> Forbidden<T>(T value = default) => new Result<T>
        {
            Status = 403,
            Payload = value
        };

        public static Result<TPayload> From<T, TPayload>(Result<T> result, TPayload payload = default) => new Result<TPayload>
        {
            Errors = result.Errors,
            Payload = payload,
            Status = result.Status
        };

        internal static Result<T> Ok<T>(Func<T> p)
        {
            throw new NotImplementedException();
        }
    }
}
