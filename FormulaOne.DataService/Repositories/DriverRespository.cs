using FromulaOne.DataService.Data;
using FromulaOne.DataService.Repositories.Interface;
using FromulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FromulaOne.DataService.Repositories;

public class DriverRespository : GenericRepository<Driver>, IDriverRepository
{
    public DriverRespository(ILogger logger, AppDbContext context) : base(logger, context)
    {
    }

    public override async Task<IEnumerable<Driver>> All()
    {
        try
        {
            return await _dbSet.Where(x => x.Status == 1)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.AddedDate)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} All Function Error", typeof(DriverRespository));
            throw;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return false;
            }
            result.Status = 0;
            result.UpdatedDate = DateTime.UtcNow;
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Delete Function Error", typeof(DriverRespository));
            throw;
        }
    }

    public override async Task<bool> Update(Driver driver)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == driver.Id);
            if (result == null)
            {
                return false;
            }
            result.DateOfBirth = driver.DateOfBirth;
            result.DriverNumber = driver.DriverNumber;
            result.FirstName = driver.FirstName;
            result.LastName = driver.LastName;
            result.UpdatedDate = DateTime.UtcNow;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Update Function Error", typeof(DriverRespository));
            throw;
        }
    }
}
