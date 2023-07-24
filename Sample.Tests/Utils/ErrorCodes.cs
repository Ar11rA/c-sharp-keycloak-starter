using Sample.Tests.Utils.DTO;

namespace Sample.Tests.Utils
{
    public static class ErrorCodes
    {
        public static readonly Error UnreadableResponse = new(nameof(UnreadableResponse), "Unreadable response.");
    }
}