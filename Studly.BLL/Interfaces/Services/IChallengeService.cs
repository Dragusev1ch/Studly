using Studly.BLL.DTO;
using Studly.BLL.DTO.Challenge;
using Studly.DAL.Enums;

namespace Studly.BLL.Interfaces.Services;

public interface IChallengeService
{
    public ChallengeDto Create(ChallengeRegistrationDto challengeDto, string userEmail);
    public IEnumerable<ChallengeDto>? GetUserList(string userEmail);

    public IEnumerable<ChallengeDto>? GetUserList(string userEmail, int offset, int count, bool? completedVisible,
        bool? sortByPriority,
        DateVariants? date, ChallengeStatus? sortByStatus);

    public ChallengeDto GetById(int id);
    public IEnumerable<ChallengeDto> GetList();
    public ChallengeDto Update(ChallengeUpdateDto newChallenge, int id);
    public bool Delete(int id);

    void Dispose();
}