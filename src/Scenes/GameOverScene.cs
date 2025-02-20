using System.Numerics;
using Raylib_cs;

public class GameOverScene : IScene {

    public override void update(float deltaTime) {
        if (Raylib.IsKeyPressed(KeyboardKey.Space)) {
            GameState.getInstance().resetGame();
            sceneManager.changeScene("snake");
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            sceneManager.exit();
    }

    public override void draw() {
        Vector2 windowSize = GameState.getInstance().windowSize;
        int score = GameState.getInstance().score;

        int centerX = (int)(windowSize.X / 2);
        int centerY = (int)(windowSize.Y / 2);

        Raylib.DrawText(
            "Game Over!",
            centerX - 100,
            centerY - 50,
            40,
            Color.Black
        );

        Raylib.DrawText(
            $"Score: {score}",
            centerX - 50,
            centerY,
            30,
            Color.Black
        );

        Raylib.DrawText(
            "Press SPACE to Restart\nPress ESC for Exit",
            centerX - 120,
            centerY + 50,
            20,
            Color.Gray
        );
    }

    public override void enter() {
    }

    public override void exit() {
    }
}