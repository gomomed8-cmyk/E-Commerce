using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public sealed record Error(string Code, string Description,ErrorType ErrorType=ErrorType.Failure)
    {
        public static Error Failure(string Code = "General.Failure", string description = "General Failure has Occurred")
        => new(Code, description, ErrorType.Failure);
        public static Error Validation(string Code = "General.Validation", string description = "General Validation Error has Occurred")
        => new(Code, description, ErrorType.Validation);
        public static Error NotFound(string Code = "General.NotFound", string description = "Resource NotFound")
        => new(Code, description, ErrorType.NotFound);
        public static Error Conflict(string Code = "General.Conflict", string description = "General Conflict has Occurred")
        => new(Code, description, ErrorType.Conflict);
        public static Error Unauthorized(string Code = "General.Unauthorized", string description = "Accuss Is Denied Due To Bad Authorization")
        => new(Code, description, ErrorType.Unauthorized);
        public static Error Forbidden(string Code = "General.Forbidden", string description = "This Operation Is Forbidden")
        => new(Code, description, ErrorType.Forbidden);
        public static Error InvalidCredentials(string Code = "General.InvalidCredentials", string description = "Provided Credentials Are Invaild")
        => new(Code, description, ErrorType.InvalidCredentials);

    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ErrorType
    {
        Failure=0,
        Validation=1,
        NotFound=2,
        Conflict=3,
        Unauthorized=4,
        Forbidden=5,
        InvalidCredentials=6
    }
}
