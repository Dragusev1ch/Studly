using Studly.BLL.DTO.Challenge;

namespace Studly.BLL.Interfaces.Services;

public interface IChallengeService
{
    public ChallengeDto Create(ChallengeRegistrationDto challengeDto,string userEmail);
    public IEnumerable<ChallengeDto>? GetUserList(string userEmail);

    public ChallengeDto GetById(int id);
    public IEnumerable<ChallengeDto> GetList();
    public ChallengeDto Update(ChallengeUpdateDto newChallenge, string title);
    public bool Delete(int id);
    void Dispose();
}