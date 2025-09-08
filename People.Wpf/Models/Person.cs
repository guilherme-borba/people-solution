using System;

namespace People.Wpf.Models
{
    // Modelo de dados recebido/enviado pela API
    internal class Person
    {
        // ID da pessoa
        public string Id { get; set; }

        // Nome da pessoa
        public string Nome { get; set; }

        // Sobrenome da pessoa
        public string Sobrenome { get; set; }

        // Telefone da pessoa
        public string Telefone { get; set; }
    }
}
