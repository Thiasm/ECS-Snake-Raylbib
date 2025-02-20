using System.Numerics;
using Raylib_cs;

public class PauseInputSystem : ISystem {

    public PauseInputSystem() {
        systemType = State.Pause;
    }

    public override void update(EntityManager entityManager, float deltaTime) {
        if (Raylib.IsKeyPressed(KeyboardKey.P))
            GameState.getInstance().setState(State.Gameplay);
    }
}
