namespace TMB_REST.Models
{
    public class OrderModel
    {
        // Tornado set para permitir materialização pelo EF
        public int Id { get; set; } 

        public string Cliente { get; private set; }

        public string Produto { get; private set; }

        public double Valor { get; private set; }

        public int Status { get; private set; }

        public string Data_Criacao { get; private set; }

        // Construtor sem parâmetros necessário para o EF Core
        protected OrderModel() { }

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
