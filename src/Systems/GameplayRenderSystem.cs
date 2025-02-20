using System.Numerics;
using Raylib_cs;

public class GameplayRenderSystem : ISystem {

    public GameplayRenderSystem() {
        systemType = State.All;
    }

    public override void update(EntityManager entityManager, float deltaTime) {
        Vector2 mapSize = GameState.getInstance().mapSize;
        Vector2 cellSize = GameState.getInstance().cellSize;

        drawEntities(entityManager, cellSize);
        drawMapGrid(mapSize, cellSize);
        drawMapBorder(mapSize, cellSize);
        drawScore(cellSize);
    }

    private void drawEntities(EntityManager entityManager, Vector2 cellSize) {
        var entities = entityManager.getEntitiesWithComponent<Renderable>();

        foreach (int entity in entities) {
            Position position = entityManager.getComponent<Position>(entity);
            Renderable renderable = entityManager.getComponent<Renderable>(entity);
            Size size = entityManager.getComponent<Size>(entity);

            Vector2 drawSize = cellSize * new Vector2(size.scale, size.scale);
            Vector2 padding = (cellSize - drawSize) / 2f;
            Vector2 drawPosition = ((position.position + new Vector2(2, 2)) * cellSize) + padding;

            Raylib.DrawRectangleV(drawPosition, drawSize, renderable.color);
        }
    }

    private void drawMapBorder(Vector2 mapSize, Vector2 cellSize) {
        float borderWidth = cellSize.X * (mapSize.X + 2);
        float borderHeight = cellSize.Y * (mapSize.Y + 2);
        Color borderColor = Color.Gray;

        Raylib.DrawRectangleV(
            new Vector2(cellSize.X, cellSize.Y),
            new Vector2(borderWidth, cellSize.Y),
            borderColor
        );
        Raylib.DrawRectangleV(
            new Vector2(cellSize.X, borderHeight),
            new Vector2(borderWidth, cellSize.Y),
            borderColor
        );
        Raylib.DrawRectangleV(
            new Vector2(cellSize.X, cellSize.Y),
            new Vector2(cellSize.X, borderHeight),
            borderColor
        );
        Raylib.DrawRectangleV(
            new Vector2(borderWidth, cellSize.Y),
            new Vector2(cellSize.X, borderHeight),
            borderColor
        );
    }

    private void drawMapGrid(Vector2 mapSize, Vector2 cellSize) {
        for (float x = 1; x <= mapSize.X + 3; x += 1) {
            Raylib.DrawLineV(
                new Vector2(x * cellSize.X , cellSize.Y),
                new Vector2(x * cellSize.X, cellSize.Y * (mapSize.Y + 3)),
                Color.Black
            );
            Raylib.DrawLineV(
                new Vector2(cellSize.X, x * cellSize.Y),
                new Vector2(cellSize.X * (mapSize.X + 3), x * cellSize.Y),
                Color.Black
            );
        }
    }

    private void drawScore(Vector2 cellSize) {
        int score = GameState.getInstance().score;

        Raylib.DrawText(
            $"Score: {score}",
            (int)cellSize.X,
            20,
            Math.Min((int)cellSize.Y, (int)cellSize.X) - 20,
            Color.Black
        );
    }
}