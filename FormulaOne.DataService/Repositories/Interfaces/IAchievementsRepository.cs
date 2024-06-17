using FromulaOne.Entities.DbSet;

namespace FromulaOne.DataService.Repositories.Interface;

public interface IAchievementsRepository : IGenericRepository<Achievement>
{
    Task<Achievement?> GetDriverAchievementAsync(Guid driverId);
}
