using TMB_REST.Models;

namespace TMB_REST
{
    public static class Orders
    {


        public static void OrdersRoutes(this WebApplication app)
        {

            // Sua massa de dados pré-definida
            var massaDeDados = new List<OrderModel>
            {
                new OrderModel(1, "Asminn", "Teclado Mecânico", 250.00, 1, "2026-05-05"),
                new OrderModel(2, "Beatriz Silva", "Monitor 24pol", 890.50, 2, "2026-05-05"),
                new OrderModel(3, "Carlos Oliveira", "Mouse Gamer", 120.00, 1, "2026-05-02"),
                new OrderModel(4, "Asminn", "Teclado Mecânico", 250.00, 1, "2026-02-01"),
                new OrderModel(5, "Beatriz Silva", "Monitor 24pol", 890.50, 2, "2026-02-02"),
                new OrderModel(6, "Carlos Oliveira", "Mouse Gamer", 120.00, 1, "2026-02-03"),
                new OrderModel(7, "Daniela Costa", "Webcam HD", 350.00, 3, "2026-02-04"),
                new OrderModel(8, "Eduardo Santos", "Headset Bluetooth", 450.00, 1, "2026-02-05"),
                new OrderModel(9, "Fernanda Lima", "Suporte Articulado", 180.00, 2, "2026-02-06"),
                new OrderModel(10, "Gabriel Souza", "Cadeira Ergonômica", 1200.00, 1, "2026-02-07"),
                new OrderModel(11, "Helena Rocha", "SSD 1TB", 550.00, 4, "2025-12-25"),
                new OrderModel(12, "Igor Mendes", "Memória RAM 16GB", 400.00, 1, "2026-01-15"),
                new OrderModel(13, "Julia Paiva", "Cabo HDMI 2.1", 45.00, 2, "2026-02-07"),
                new OrderModel(14, "Julia Paiva", "Cabo HDMI 2.1", 45.00, 2, "2026-02-07"),
                new OrderModel(15, "Julia Paiva", "Cabo HDMI 2.1", 45.00, 2, "2026-02-07"),
                new OrderModel(16, "Julia Paiva", "Cabo HDMI 2.1", 45.00, 2, "2026-02-07")
            };

            //app.MapGet("order", () =>
            //{
            //    var random = new Random();
            //    int indice = random.Next(massaDeDados.Count);
            //    return massaDeDados[indice];
            //});

            app.MapGet("orders", () =>
            {
                return massaDeDados;
            });

            app.MapGet("orders/{id}", (int id) =>
            {
                var order = massaDeDados.FirstOrDefault(o => o.Id == id);
                return order is not null ? Results.Ok(order) : Results.NotFound("Not Found");
            });

            app.MapPost("orders", (OrderModel newOrder) =>
            {
                if (newOrder is null)
                {
                    return Results.BadRequest("Invalid order data.");
                }
                // Simula a criação de um novo pedido, atribuindo um ID único
                int newId = massaDeDados.Max(o => o.Id) + 1;
                // Usa o operador ?? para garantir valores não nulos para os parâmetros obrigatórios
                var createdOrder = new OrderModel(
                    newId,
                    newOrder.Cliente,
                    newOrder.Produto,
                    newOrder.Valor ?? 0.0,
                    newOrder.Status ?? 0,
                    newOrder.Data_Criacao
                );
                massaDeDados.Add(createdOrder);
                return Results.Created($"/orders/{createdOrder.Id}", createdOrder);
            });
        }
    }
}
