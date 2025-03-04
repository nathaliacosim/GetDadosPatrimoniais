using GetDadosPatrimoniais.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetDadosPatrimoniais.Repositories;

public interface IFornecedorRepository
{
    Task SalvarFornecedoresAsync(List<ContentFornecedor> fornecedores);
}