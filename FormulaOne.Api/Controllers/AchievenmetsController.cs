using AutoMapper;
using FromulaOne.DataService.Repositories.Interface;
using FromulaOne.Entities.DbSet;
using FromulaOne.Entities.Dtos.Requests;
using FromulaOne.Entities.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace FromulaOne.Api.Controllers;

public class AchievenmetsController : BaseController
{
    public AchievenmetsController(
        IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }


    [HttpGet]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> GetDriverAchiebements(Guid driverId)
    {
        var driverAchievements = await _unitOfWork.Achievements.GetDriverAchievementAsync(driverId);

        if (driverAchievements == null)
        {
            return NotFound("Achievements not found");
        }

        var result = _mapper.Map<DriverAchievementsResponse>(driverAchievements);
        return Ok(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddAchievement([FromBody] CreateDriverAchievementRequest achievement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = _mapper.Map<Achievement>(achievement);

        await _unitOfWork.Achievements.Add(result);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetDriverAchiebements), new { driverId = result.DriverId }, result);
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateAchievements([FromBody] UpdateDriverAchievementRequest achievement)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = _mapper.Map<Achievement>(achievement);

        await _unitOfWork.Achievements.Update(result);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
