using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Repositories.Interfaces;

namespace TestCatalog.Host.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task AddQuestionAsync(QuestionEntity question)
        {
            await _dbContext.Questions.AddAsync(question);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(QuestionEntity question)
        {
            _dbContext.Questions.Remove(question);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<QuestionEntity> GetQuestionAsync(int questionId)
        {
            return await _dbContext.Questions.FirstOrDefaultAsync(h => h.Id == questionId);
        }

        public async Task UpdateQuestionAsync(QuestionEntity question)
        {
            _dbContext.Questions.Update(question);
            await _dbContext.SaveChangesAsync();
        }
    }
}
