using GetDadosPatrimoniais.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetDadosPatrimoniais.Services;

public interface IFornecedorService
{
    Task<List<ContentFornecedor>> BuscarFornecedoresAsync();
}
