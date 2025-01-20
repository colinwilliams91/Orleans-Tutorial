using OrleansURLShortener.Interfaces;

namespace OrleansURLShortener.Router
{
    /// <summary>
    /// TODO: Once extension methods on builder handle GET, POST, PUT, DELETE; "Router" should call all 4 in one method
    /// So that Program.cs/Startup.cs can call 1 single method instead of 4, DI them in here and invoke in 1 method?
    /// </summary>
    public class Router
    {
        //public IGetEndpointMapper GetEndpointMapper { get; set; }
        //public void Router(IGetEndpointMapper getEndpointMapper)
        //{
        //    this.GetEndpointMapper = getEndpointMapper;
        //}
        public required IEndpointRouteBuilder App {  get; set; }

        public Router(IEndpointRouteBuilder app)
        {
            this.App = app;
        }

        //public static void MapGetHome(this IEndpointRouteBuilder builder)
        //{

        //}
    }
}
