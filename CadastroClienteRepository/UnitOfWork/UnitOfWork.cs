using CadastroCliente.Data;
using CadastroClienteRepository.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroClienteRepository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private IDbContextTransaction _dbTransaction;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task BeginTrasaction()
        {
            _dbTransaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _dbTransaction.CommitAsync();
        }

        public async void Dispose()
        {
            _dbTransaction?.Dispose();
        }

        public async Task Rollback()
        {
            await _dbTransaction.RollbackAsync();
        }
    }
}
