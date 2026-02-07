namespace TMB_REST.Models
{
    public class OrderModel
    {
        //public Guid Id { get; init; } //Init para funcionar somente quando chamar a classe e alterar apenas uma vez
        public int Id { get; init; } //int para facilitar os testes iniciais de ID pequeno

        public string? Cliente { get; private set; }

        public string? Produto { get; private set; }

        public double? Valor { get; private set; }

        public int? Status { get; private set; }

        public string? Data_Criacao { get; private set; }

        public OrderModel(int id, string cliente, string produto, double valor, int status, string data_criacao)
        {
            //Id = Guid.NewGuid();
            Id = id;
            Cliente = cliente;
            Produto = produto;
            Valor = valor;
            Status = status;
            Data_Criacao = data_criacao;
        }
    }
}
