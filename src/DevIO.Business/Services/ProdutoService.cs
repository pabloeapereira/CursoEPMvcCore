using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class ProdutoService : BaseService, IFornecedorService
    {
        public async Task Adicionar(Fornecedor fornecedor)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            throw new NotImplementedException();
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public async Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
