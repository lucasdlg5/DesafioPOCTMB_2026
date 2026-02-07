namespace TMB_REST.Models
{
    public class OrderModel
    {
        public Guid Id { get; init; } //Init para funcionar somente quando chamar a classe e alterar apenas uma vez

        public string? Cliente { get; private set; }

        public string? Produto { get; private set; }

        public string? Valor { get; private set; }

        public string? Status { get; private set; }

        public string? Data_Criacao { get; private set; }

        public OrderModel(string cliente)
        {
            Cliente = cliente;
            Id = Guid.NewGuid();
        }
    }
}
