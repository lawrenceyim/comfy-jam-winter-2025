using ServiceSystem;

public class PlayerDataService : IService {
    private readonly PlayerDataRepository _playerDataRepository;

    public PlayerDataService(PlayerDataRepository playerDataRepository) {
        _playerDataRepository = playerDataRepository;
    }
}