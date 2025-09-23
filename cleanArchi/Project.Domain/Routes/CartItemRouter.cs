namespace Project.Domain.Routes.BaseRouter
{
    public partial class Router
    {
        public class CartItemRouter : Router
        {
            private const string Prefix = Rule + "CartItems";
            public const string Add = Prefix + "/";
            public const string Delete = Prefix + "/{id}";
            public const string Update = Prefix + "/{id}";
            public const string GetAll = Prefix + "/";
            public const string GetById = Prefix + "/{id}";
        }
    }
}