using System.Numerics;
using Raylib_cs;

public class GameState
{
    private static GameState instance = new GameState();
    public State gameState { get; private set; }
    public int score { get; private set; }

    public Vector2 windowSize { get; private set; }
    public Vector2 cellSize { get; private set; }
    public Vector2 mapSize { get; private set; } = new Vector2(10, 10);
    private const int TOTAL_PADDING = 4;
    
    public static GameState getInstance() {
        return instance;
    }

    public void addScore(int _score) {
        score += _score;
    }

    public void resetGame() {
        gameState = State.Gameplay;
        score = 0;
    }

    public void setState(State _gamestate) {
        gameState = _gamestate;
    }

    public void updateWindowSize() {
        windowSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        cellSize = new Vector2(
            windowSize.X / (mapSize.X + TOTAL_PADDING),
            windowSize.Y / (mapSize.Y + TOTAL_PADDING)
        );
    }
}