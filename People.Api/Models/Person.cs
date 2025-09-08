namespace People.Api.Models
{
    // Essa classe representa uma pessoa
    // Base para criar a tabela "People"
    public class Person
    {
        // ID único
        public Guid Id { get; set; } = Guid.NewGuid();

        // Nome da pessoa (obrigatório)
        public string Nome { get; set; } = string.Empty;

        // Sobrenome da pessoa (obrigatório)
        public string Sobrenome { get; set; } = string.Empty;

        // Telefone da pessoa (obrigatório)
        public string Telefone { get; set; } = string.Empty;
    }
}
