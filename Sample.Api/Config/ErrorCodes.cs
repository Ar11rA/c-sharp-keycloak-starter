using System;
using Sample.Api.DTO;

namespace Sample.Api.Config
{
    public static class ErrorCodes
    {
        public static readonly Error UnreadableResponse = new(nameof(UnreadableResponse), "Unreadable response.");
    }
}

