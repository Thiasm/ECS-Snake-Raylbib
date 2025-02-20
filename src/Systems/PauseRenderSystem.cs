using System.Numerics;
using Raylib_cs;

public class PauseRenderSystem : ISystem {

    public PauseRenderSystem() {
        systemType = State.Pause;
    }

    public override void update(EntityManager entityManager, float deltaTime) {
        Vector2 windowSize = GameState.getInstance().windowSize;
        Vector2 cellSize = GameState.getInstance().cellSize;
        int centerX = (int)(windowSize.X / 2);
        int centerY = (int)(windowSize.Y / 2);

        Raylib.DrawRectangle(0, 0, (int)windowSize.X, (int)windowSize.Y, new Color(0, 0, 0, 100));

        Raylib.DrawText(
            "GAME PAUSED",
            centerX - 200,
            centerY - 50,
            60,
            Color.Black
        );
    }
}
