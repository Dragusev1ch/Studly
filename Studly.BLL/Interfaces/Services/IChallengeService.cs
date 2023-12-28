using Studly.BLL.DTO.Challenge;

namespace Studly.BLL.Interfaces.Services;

public interface IChallengeService
{
    public void CreateChallenge(ChallengeRegistrationDto challengeDto,string email);
    public ChallengeDto? GetChallenge(string title);
    public ChallengeDto GetChallengeById(int id);
    public ChallengeDto GetCurrentChallenge(string title);
    public IQueryable<ChallengeDto> List();
    public ChallengeDto Update(ChallengeUpdateDto newChallenge, string title);
    public bool Delete(int id);
    void Dispose();
}