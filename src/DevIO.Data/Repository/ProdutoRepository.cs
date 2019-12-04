using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DevIOContext dbContext) : base(dbContext){}

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await _dbContext.Produtos.AsNoTracking().Include(p => p.Fornecedor)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await _dbContext.Produtos.AsNoTracking().Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}