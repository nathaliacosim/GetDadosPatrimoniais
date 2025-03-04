using GetDadosPatrimoniais.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace GetDadosPatrimoniais.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;
        private const string apiUrl = "https://patrimonio.betha.cloud/patrimonio-services/api/fornecedores";
        private const int limit = 50;
        private static readonly string[] loadingFrames = { "|", "/", "-", "\\" };

        public FornecedorService(string token)
        {
            _httpClient = new HttpClient();
            _token = token;
        }

        public async Task<List<ContentFornecedor>> BuscarFornecedoresAsync()
        {
            List<ContentFornecedor> fornecedores = new();
            int offset = 0;
            int paginaAtual = 1;
            bool hasNext = true;

            try
            {
                Console.Write("Buscando fornecedores ");

                while (hasNext)
                {
                    string url = $"{apiUrl}?offset={offset}&limit={limit}";
                    HttpRequestMessage request = new(HttpMethod.Get, url);
                    request.Headers.Add("Authorization", $"Bearer {_token}");

                    HttpResponseMessage response = await _httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var resultado = JsonSerializer.Deserialize<Fornecedor>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (resultado?.content != null)
                    {
                        fornecedores.AddRange(resultado.content);
                    }

                    MostrarLoading(paginaAtual);

                    hasNext = resultado?.hasNext ?? false;
                    offset += limit;
                    paginaAtual++;
                }

                Console.WriteLine("\n✅ Busca concluída!\n");
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"\n❌ Erro de requisição HTTP: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"\n❌ Erro ao desserializar JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Erro inesperado: {ex.Message}");
            }

            return fornecedores;
        }

        private void MostrarLoading(int pagina)
        {
            int frameIndex = pagina % loadingFrames.Length;
            Console.Write($"\rCarregando página {pagina} {loadingFrames[frameIndex]}  ");
            Thread.Sleep(200);
        }
    }
}
