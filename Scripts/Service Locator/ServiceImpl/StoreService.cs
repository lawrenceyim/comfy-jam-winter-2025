using ServiceSystem;

public class StoreService : IService {
    private readonly PlayerDataRepository _playerDataRepository;

    public StoreService(PlayerDataRepository playerDataRepository) {
        _playerDataRepository = playerDataRepository;
    }
    
}