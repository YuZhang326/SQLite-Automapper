namespace FromulaOne.Entities.Dtos.Response;

public class DriverAchievementsResponse
{
    public Guid DriverId { get; set; }
    public int WorldChampionship { get; set; }
    public int FastestLap { get; set; }
    public int PolePosition { get; set; }
    public int Wins { get; set; }
}
