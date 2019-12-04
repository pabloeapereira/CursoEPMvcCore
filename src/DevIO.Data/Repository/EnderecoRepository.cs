using System;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(DevIOContext dbContext) : base(dbContext)
        {
        }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await _dbSet.AsNoTracking()
                .Include(e => e.Fornecedor)
                .FirstOrDefaultAsync(e => e.FornecedorId == fornecedorId);
        }
    }
}