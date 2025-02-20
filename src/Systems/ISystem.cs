public enum State {
    All,
    Gameplay,
    Pause,
    GameOver
}

public abstract class ISystem {
    public abstract void update(EntityManager entityManager, float deltaTime);
    public State systemType;
}