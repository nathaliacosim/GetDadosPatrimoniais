using Dapper;
using GetDadosPatrimoniais.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FornecedorRepository
{
    private readonly string _connectionString;

    public FornecedorRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task SalvarFornecedoresAsync(List<ContentFornecedor> fornecedores)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var fornecedor in fornecedores)
                    {
                        Console.WriteLine("🔹 Inserindo fornecedor...");

                        var fornecedorQuery = @"
                            INSERT INTO fornecedores (nome, cpf_cnpj, tipo_valor, tipo_descricao, 
                                                       situacao_valor, situacao_descricao, data_inclusao, 
                                                       optante_simples)
                            VALUES (@Nome, @CpfCnpj, @TipoValor, @TipoDescricao, @SituacaoValor, 
                                    @SituacaoDescricao, @DataInclusao, @OptanteSimples) 
                            RETURNING id;";

                        var fornecedorId = await connection.ExecuteScalarAsync<int>(fornecedorQuery, new
                        {
                            Nome = fornecedor.nome,
                            CpfCnpj = fornecedor.cpfCnpj,
                            TipoValor = fornecedor.tipo.valor,
                            TipoDescricao = fornecedor.tipo.descricao,
                            SituacaoValor = fornecedor.situacao.valor,
                            SituacaoDescricao = fornecedor.situacao.descricao,
                            DataInclusao = DateTime.Parse(fornecedor.dataInclusao),
                            OptanteSimples = fornecedor.optanteSimples
                        }, transaction);

                        Console.WriteLine("✅ Fornecedor inserido com sucesso! (ID: " + fornecedorId + ")");

                        var telefoneQuery = @"
                            INSERT INTO telefones (fornecedor_id, numero, observacao, tipo, ordem)
                            VALUES (@FornecedorId, @Numero, @Observacao, @Tipo, @Ordem);";

                        foreach (var telefone in fornecedor.telefones)
                        {
                            await connection.ExecuteAsync(telefoneQuery, new
                            {
                                FornecedorId = fornecedorId,
                                Numero = telefone.numero,
                                Observacao = telefone.observacao,
                                Tipo = telefone.tipo,
                                Ordem = telefone.ordem
                            }, transaction);
                        }

                        Console.WriteLine("✅ Telefones inseridos com sucesso!");

                        var linkQuery = @"
                            INSERT INTO links (fornecedor_id, rel, href)
                            VALUES (@FornecedorId, @Rel, @Href);";

                        foreach (var link in fornecedor.links)
                        {
                            await connection.ExecuteAsync(linkQuery, new
                            {
                                FornecedorId = fornecedorId,
                                Rel = link.rel,
                                Href = link.href
                            }, transaction);
                        }

                        Console.WriteLine("✅ Links inseridos com sucesso!");
                    }

                    transaction.Commit();
                    Console.WriteLine("✅ Transação concluída com sucesso!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("❌ Erro ao salvar os dados do fornecedor: " + ex.Message);
                    throw new Exception("Erro ao salvar os dados do fornecedor: " + ex.Message);
                }
            }
        }
    }
}
