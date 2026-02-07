using RESTAPI_TMB2026.Models;

namespace RESTAPI_TMB2026
{
    public static class Orders
    {
        public static void OrdersRoutes(this WebApplication app)
        {
            app.MapGet("order", () => new OrderModel("Asminn"));
            
        }

    }
}
