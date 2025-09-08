# PeopleSolution

Projeto para **cadastro e gerenciamento de pessoas**, com interface desktop (WPF) conectada a uma API REST hospedada no Azure.

---

## Estrutura da Solução

📁 FinishedExecutable: Instalador do aplicativo WPF (conectado à nuvem)

📁 People.Api: Código-fonte da API ASP.NET publicada no Azure

📁 People.Wpf: Código-fonte da aplicação desktop (WPF)

📄 PeopleSolution.sln


---

## API hospedada na nuvem Azure

A API está disponível publicamente com Swagger:

[`https://peoplesolution-cve3e3hwc6dfbnbx.brazilsouth-01.azurewebsites.net/swagger/index.html`](https://peoplesolution-cve3e3hwc6dfbnbx.brazilsouth-01.azurewebsites.net/swagger/index.html)

---

## Funcionalidades do sistema

A aplicação permite:

- Listar pessoas cadastradas
- Adicionar novos registros
- Editar dados de pessoas existentes
- Excluir registros
- Atualizar a tabela de pessoas

---

## Tecnologias Utilizadas

| Camada       | Tecnologia                           |
|--------------|---------------------------------------|
| Interface    | WPF (.NET)                            |
| Backend      | ASP.NET Core Web API                  |
| Hospedagem   | Azure App Service                     |
| Linguagem    | C#                                    |
| IDE          | Visual Studio 2022                    |
| Versão .NET  | .NET 8                               |

---

## Como Usar o Sistema

### 1. Instalar o aplicativo

1. Acesse a pasta `FinishedExecutable`
2. Execute o instalador `.exe`
3. O aplicativo será instalado e aberto automaticamente

> A conexão com a API já está configurada para funcionar com o endereço da nuvem.

---

### 2. Usar o sistema

- Preencha os campos: `Nome`, `Sobrenome`, `Telefone`
- Clique em **Salvar** para cadastrar
- Clique em **Novo** para limpar o formulário
- Selecione um registro da tabela para:
  - Mude os campos e clique em Salvar
  - Ou botão Excluir

---

## Tela da Aplicação
<img width="1365" height="714" alt="telaWpf" src="https://github.com/user-attachments/assets/461c380e-a3dc-49ae-b11c-15395e70fa66" />

