namespace FromulaOne.DataService.Repositories.Interface;

public interface IUnitOfWork
{
    IDriverRepository Drivers { get; }
    IAchievementsRepository Achievements { get; }

    Task<bool> CompleteAsync();
}
