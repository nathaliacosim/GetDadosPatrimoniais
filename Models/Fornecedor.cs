using System.Collections.Generic;

namespace GetDadosPatrimoniais.Models;

public class ContentFornecedor
{
    public int id { get; set; }
    public List<LinkFornecedor> links { get; set; }
    public string nome { get; set; }
    public string cpfCnpj { get; set; }
    public TipoFornecedor tipo { get; set; }
    public SituacaoFornecedor situacao { get; set; }
    public string dataInclusao { get; set; }
    public bool optanteSimples { get; set; }
    public List<object> contasBancarias { get; set; }
    public List<object> emails { get; set; }
    public List<TelefoneFornecedor> telefones { get; set; }
}

public class LinkFornecedor
{
    public string rel { get; set; }
    public string href { get; set; }
}

public class Fornecedor
{
    public int offset { get; set; }
    public int limit { get; set; }
    public bool hasNext { get; set; }
    public List<ContentFornecedor> content { get; set; }
    public int total { get; set; }
    public object valor { get; set; }
    public object soma { get; set; }
    public object dados { get; set; }
}

public class SituacaoFornecedor
{
    public string valor { get; set; }
    public string descricao { get; set; }
}

public class TelefoneFornecedor
{
    public int id { get; set; }
    public string numero { get; set; }
    public string observacao { get; set; }
    public string tipo { get; set; }
    public int ordem { get; set; }
}

public class TipoFornecedor
{
    public string valor { get; set; }
    public string descricao { get; set; }
}