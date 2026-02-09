namespace TMB_REST.Models
{
    public class OrderModel
    {
        // Tornado set para permitir materialização pelo EF
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string Produto { get; set; }
        public double Valor { get; set; }
        public int Status { get; set; }
        public string Data_Criacao { get; set; }

        // Construtor sem parâmetros necessário para o EF Core
        public OrderModel() { }

        public OrderModel(int id, string cliente, string produto, double valor, int status, string data_criacao)
        {
            Id = id;
            Cliente = cliente;
            Produto = produto;
            Valor = valor;
            Status = status;
            Data_Criacao = data_criacao;
        }
    }
}
