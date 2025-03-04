# **Get Dados Patrimoniais** 🚀

O **Get Dados Patrimoniais** tem como objetivo buscar todos os cadastros do sistema **Cloud Patrimônio** da Betha Sistemas. A aplicação interage com a API do sistema, acessando todas as rotas disponíveis para **coletar dados** de bens, fornecedores, responsáveis, baixas, transferências, comissões, inventários, entre outros. Esses dados são então **persistidos** em um banco de dados PostgreSQL.

## 🚧 Objetivos e Funcionalidades

- **Acesso às rotas da API:** Conectar-se com a API do Cloud Patrimônio para buscar dados de diversas entidades do sistema.

- **Extração de dados:** Coletar informações sobre fornecedores, bens, transferências, responsáveis e muito mais.

- **Persistência no banco de dados:** Salvar as informações extraídas em uma base de dados **PostgreSQL**.

- **Processamento e carga de dados em lote:** Utiliza paginação para garantir que todos os dados sejam coletados sem sobrecarregar a API.

## 📦 Funcionalidades

- **Integração com API REST:** A aplicação consome endpoints da API REST do Cloud Patrimônio da Betha Sistemas.

- **Conexão com PostgreSQL:** Armazena dados de fornecedores, bens e outros em um banco de dados PostgreSQL.

- **Utilização de Dapper:** A persistência de dados no banco é realizada com a biblioteca Dapper, que proporciona uma maneira eficiente e simples de executar comandos SQL.

- **Exibição de progresso:** Durante o processo de busca, a aplicação exibe um indicador de progresso para o usuário, com frames de carregamento.

## 💻 Tecnologias Utilizadas

- **C#:** Linguagem de programação principal.

- **.NET Framework:** Framework utilizado para o desenvolvimento da aplicação.

- **Dapper:** ORM para acesso rápido e eficiente ao banco de dados.

- **PostgreSQL:** Banco de dados utilizado para persistir as informações.

- **API REST:** Acesso à API do Cloud Patrimônio para buscar os dados.

- **JSON:** Formato utilizado para manipulação das respostas da API.

## ⚙️ Como Funciona

- **Configuração Inicial:** O sistema é configurado a partir de um arquivo appsettings.json que contém informações sobre o token de acesso à API e os detalhes de conexão com o banco PostgreSQL.

- **Conexão com a API:** O sistema faz requisições para as rotas da API do Cloud Patrimônio, buscando os dados de fornecedores, bens e outros.

- **Armazenamento no Banco de Dados:** As informações são persistidas no banco PostgreSQL utilizando **Dapper** para facilitar a inserção e manipulação dos dados.

- **Exibição de Progresso:** A cada nova página de dados recebida da API, um indicador de progresso é exibido para o usuário.

## 🔧 Configuração e Instalação

### 1. **Clonando o Repositório**

Para começar, clone o repositório para sua máquina local:

```bash
git clone https://github.com/seu-usuario/GetDadosPatrimoniais.git
cd GetDadosPatrimoniais
```

### 2. **Instalar Dependências**

Antes de rodar o projeto, você precisa instalar as dependências do .NET e as bibliotecas necessárias. Para isso, execute:

```bash
dotnet restore
```

### 3. **Configurar o Banco de Dados**

No arquivo `appsettings.json`, insira as credenciais de acesso ao seu banco de dados PostgreSQL e a configuração da API do **Cloud Patrimônio**.

Exemplo:

```json
{
  "TokenConversao": "seu-token-aqui",
  "Postgres": {
    "Host": "localhost",
    "Port": "5432",
    "Database": "seu-banco",
    "Username": "postgres",
    "Password": "sua-senha"
  }
}
```

### 4. **Executar a Aplicação**

Após as configurações, rode o projeto com o comando:

```bash
dotnet run
```

🔄 **Fluxo de Execução**

- O sistema buscará os **fornecedores**, **bens**, **transferências** e outros dados da API do **Cloud Patrimônio**.
- A cada página de dados recebida, o progresso será mostrado no console.
- Os dados serão persistidos no banco de dados **PostgreSQL** para posterior análise e uso.

### 5. **Mensagens no Console**

Durante a execução, você verá mensagens detalhadas no console, como:

```bash
🔄 Buscando fornecedores da API...
📦 3983 fornecedores encontrados. Salvando no banco...
✅ Dados salvos com sucesso! Tudo concluído!
🏁 Processo finalizado com êxito!
```

### 6. **Verificando os Dados no Banco**

Após a execução, você pode acessar seu banco de dados PostgreSQL para verificar os dados persistidos.

---

## 📜 Licença

Este projeto está licenciado sob a [MIT License](LICENSE) - veja o arquivo LICENSE para mais detalhes.

---

## 📣 Contribuição

Se desejar contribuir, faça um **fork** deste repositório, adicione suas melhorias e envie um **pull request**. Todas as contribuições são bem-vindas! 🙌

---
