using System;
using ServiceSystem;

public class PlayerDataService : IService {
	public event Action MoneyUpdated;

	private readonly PlayerDataRepository _playerDataRepository;

	public PlayerDataService(PlayerDataRepository playerDataRepository) {
		_playerDataRepository = playerDataRepository;
	}

	public int GetMoney() {
		return _playerDataRepository.Money;
	}

	public void SetMoney(int money) {
		_playerDataRepository.Money = money;
		MoneyUpdated?.Invoke();
	}

	public int GetDay() {
		return _playerDataRepository.Day;
	}

	public void SetDay(int day) {
		_playerDataRepository.Day = day;
	}
}
