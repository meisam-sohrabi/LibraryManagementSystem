namespace LibrarySys.Application.Common.Exceptions
{
    public class GlobalException : Exception
    {
        public GlobalException() : base("An error occurred while processing your request")
        {

        }
    }
}
