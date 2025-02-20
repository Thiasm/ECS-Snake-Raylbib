public class SystemManager {
    private Dictionary<State, List<ISystem>> systemsByType = new Dictionary<State, List<ISystem>>();

    public void addSystem<T>(T system) where T : ISystem {
		if (!systemsByType.ContainsKey(system.systemType)) {
			systemsByType[system.systemType] = new List<ISystem>();
		}
		systemsByType[system.systemType].Add(system);
    }

	public void updateByType(EntityManager entityManager, float deltaTime) {
		State systemType = GameState.getInstance().gameState;

		if (systemsByType.ContainsKey(State.All))
			foreach (ISystem system in systemsByType[State.All])
				system.update(entityManager, deltaTime);

		if (systemsByType.ContainsKey(systemType)) {
			foreach (ISystem system in systemsByType[systemType]) {
				system.update(entityManager, deltaTime);
			}
		}
	}

	public void clearSystem() {
		systemsByType.Clear();
	}
}