using Studly.BLL.DTO.Challenge;

namespace Studly.BLL.Interfaces.Services;

public interface IChallengeService
{
    public void Create(ChallengeRegistrationDto challengeDto,string email);
    public ChallengeDto? Get(string title);
    public ChallengeDto GetById(int id);
    public IQueryable<ChallengeDto> List();
    public ChallengeDto Update(ChallengeUpdateDto newChallenge, string title);
    public bool Delete(int id);
    void Dispose();
}