using System.Numerics;
using Raylib_cs;

public class MenuScene : IScene {
    public override void update(float deltaTime) {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
            sceneManager.changeScene("snake");
        else if (Raylib.IsKeyPressed(KeyboardKey.Escape)) {
            sceneManager.changeScene("menu");
        }
    }

    public override void draw() {
        Vector2 windowSize = GameState.getInstance().windowSize;
        Vector2 cellSize = GameState.getInstance().cellSize;
        int centerX = (int)(windowSize.X / 2);
        int centerY = (int)(windowSize.Y / 2);

        Raylib.DrawText(
            "Snake",
            centerX - 150,
            centerY - 170,
            Math.Max((int)cellSize.Y, 100),
            Color.DarkGreen
        );

        Raylib.DrawText(
            "Play",
            centerX - 50,
            centerY,
            Math.Max((int)cellSize.Y, 50),
            Color.Gray
        );

        Raylib.DrawText(
            "Quit",
            centerX - 50,
            centerY + 100,
            Math.Max((int)cellSize.Y, 50),
            Color.Gray
        );
    }


    public override void exit() { }
}