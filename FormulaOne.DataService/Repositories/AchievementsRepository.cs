using System.Reflection.Metadata.Ecma335;
using FromulaOne.DataService.Data;
using FromulaOne.DataService.Repositories.Interface;
using FromulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FromulaOne.DataService.Repositories;

public class AchievementsRepository : GenericRepository<Achievement>, IAchievementsRepository
{
    public AchievementsRepository(ILogger logger, AppDbContext context) : base(logger, context)
    {
    }

    public async Task<Achievement?> GetDriverAchievementAsync(Guid driverId)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.DriverId == driverId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetDriverAchievementAsync Function Error",
                typeof(AchievementsRepository));
            throw;
        }
    }

    public override async Task<IEnumerable<Achievement>> All()
    {
        try
        {
            return await _dbSet.Where(x => x.Status == 1)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.AddedDate)
                .ToArrayAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Achievement All Error", typeof(AchievementsRepository));
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
            _logger.LogError(e, "{Repo} Delete Function Error", typeof(AchievementsRepository));
            throw;
        }
    }

    public override async Task<bool> Update(Achievement achievement)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == achievement.Id);
            if (result == null)
            {
                return false;
            }
            result.UpdatedDate = DateTime.UtcNow;
            result.FastestLap = achievement.FastestLap;
            result.PolePosition = achievement.PolePosition;
            result.RaceWins = achievement.RaceWins;
            result.WorldChampionship = achievement.WorldChampionship;
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Delete Function Error", typeof(AchievementsRepository));
            throw;
        }
    }
}
