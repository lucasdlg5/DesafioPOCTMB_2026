using TMB_REST.Models;

namespace TMB_REST
{
    public static class Orders
    {
        public static void OrdersRoutes(this WebApplication app)
        {
            app.MapGet("order", () => new OrderModel("Asminn"));

        }
    }
}
