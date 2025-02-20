using System.Numerics;
using Raylib_cs;

public class GameplayInputSystem : ISystem {
    private float moveTimer;
    private Vector2? bufferedDirection;
    private readonly Dictionary<KeyboardKey, Vector2> directionMap = new() {
        { KeyboardKey.Up, -Vector2.UnitY },
        { KeyboardKey.Down, Vector2.UnitY },
        { KeyboardKey.Left, -Vector2.UnitX },
        { KeyboardKey.Right, Vector2.UnitX }
    };

    public GameplayInputSystem() {
        systemType = State.Gameplay;
        moveTimer = 0f;
        bufferedDirection = null;
    }

    public override void update(EntityManager entityManager, float deltaTime) {
        var snakeHead = entityManager.getEntitiesWithComponent<SnakeHead>().First();
        var currentDirection = entityManager.getComponent<Direction>(snakeHead);

        if (Raylib.IsKeyPressed(KeyboardKey.P))
            GameState.getInstance().setState(State.Pause);

        moveTimer += deltaTime;
        handleGameInput(entityManager, snakeHead, currentDirection);
    }

    private void handleGameInput(EntityManager entityManager, int snakeHead, Direction currentDirection) {
        foreach (var (key, newDirection) in directionMap) {
            if (Raylib.IsKeyPressed(key) && !isOppositeDirection(currentDirection.direction, newDirection)) {
                bufferedDirection = newDirection;
                break;
            }

            if (moveTimer >= 0.15f && bufferedDirection.HasValue) {
                currentDirection.direction = bufferedDirection.Value;
                entityManager.setComponent(snakeHead, currentDirection);
                bufferedDirection = null;
                moveTimer = 0f;
            }
        }
    }

    private bool isOppositeDirection(Vector2 current, Vector2 newDir) {
        if (current + newDir == Vector2.Zero)
            return true;
        return false;
    }
}
