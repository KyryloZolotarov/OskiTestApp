using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TestCatalog.Host.Data;
using TestCatalog.Host.Data.Entities;
using TestCatalog.Host.Repositories.Interfaces;

namespace TestCatalog.Host.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AnswerRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
    {
        _dbContext = dbContextWrapper.DbContext;
    }

    public async Task AddAnswerAsync(AnswerEntity answer)
    {
        await _dbContext.Answers.AddAsync(answer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAnswerAsync(AnswerEntity answer)
    {
        _dbContext.Answers.Remove(answer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<AnswerEntity> GetAnswerAsync(int answerId)
    {
        return await _dbContext.Answers.FirstOrDefaultAsync(h => h.Id == answerId);
    }

    public async Task<IEnumerable<AnswerEntity>> GetAnswersForTestAsync(int testId)
    {
        return await _dbContext.Answers.Where(c => c.Question.TestId == testId).ToListAsync();
    }

    public async Task UpdateAnswerAsync(AnswerEntity answer)
    {
        _dbContext.Answers.Update(answer);
        await _dbContext.SaveChangesAsync();
    }
}