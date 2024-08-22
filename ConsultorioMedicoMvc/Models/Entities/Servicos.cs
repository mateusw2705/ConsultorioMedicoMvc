namespace ConsultorioMedicoMvc.Models.Entities
{
    public class Servicos
    {
        //entidade de servico
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public decimal Valor { get; set; }
    }
}
