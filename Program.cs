using System;
using System.IO;
using System.Threading.Tasks;
using GetDadosPatrimoniais.Services;
using Microsoft.Extensions.Configuration;

namespace GetDadosPatrimoniais
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var config = LoadConfiguration();
            string tokenConversao = config["TokenConversao"];

            string postgresConnectionString = ConfigurePostgres(config);
            if (string.IsNullOrEmpty(postgresConnectionString))
            {
                Console.WriteLine("❌ A string de conexão do PostgreSQL está vazia ou nula.");
                return;
            }

            Console.WriteLine($"📡 Conexão com PostgreSQL estabelecida com sucesso!");
            
            Console.WriteLine("🔄 Buscando fornecedores da API...");
            var fornecedorService = new FornecedorService(tokenConversao);
            var fornecedorRepository = new FornecedorRepository(postgresConnectionString);

            // 🔹 Buscar fornecedores da API
            var fornecedores = await fornecedorService.BuscarFornecedoresAsync();
            if (fornecedores.Count > 0)
            {
                Console.WriteLine($"📦 {fornecedores.Count} fornecedores encontrados. Salvando no banco...");

                // 🔹 Salvar no banco de dados
                await fornecedorRepository.SalvarFornecedoresAsync(fornecedores);
                Console.WriteLine("✅ Dados salvos com sucesso! Tudo concluído!");
            }
            else
            {
                Console.WriteLine("⚠️ Nenhum fornecedor encontrado para salvar.");
            }

            Console.WriteLine("🏁 Processo finalizado com êxito!");
        }

        private static IConfiguration LoadConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Console.WriteLine($"🌍 Host: {config["Postgres:Host"]}");
            Console.WriteLine($"📍 Porta: {config["Postgres:Port"]}");
            Console.WriteLine($"📚 Banco de Dados: {config["Postgres:Database"]}");
            Console.WriteLine($"🔑 Usuário: {config["Postgres:Username"]}");

            return config;
        }

        private static string ConfigurePostgres(IConfiguration config)
        {
            string host = config["Postgres:Host"];
            int port = int.Parse(config["Postgres:Port"]);
            string database = config["Postgres:Database"];
            string username = config["Postgres:Username"];
            string password = config["Postgres:Password"];

            var connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";
            Console.WriteLine($"🔌 Connection string do PostgreSQL: {connectionString}\n");

            return connectionString;
        }
    }
}
