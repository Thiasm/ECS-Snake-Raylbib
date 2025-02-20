using System.Numerics;
using Raylib_cs;

public class FoodSystem : ISystem {
    private readonly Random random = new();

    public FoodSystem() {
        systemType = State.Gameplay;
    }

    public override void update(EntityManager entityManager, float deltaTime) {
        if (!entityManager.getEntitiesWithComponent<Food>().Any())
            spawnFood(entityManager);
    }

    private void spawnFood(EntityManager entityManager) {
        Vector2 mapSize = GameState.getInstance().mapSize;
        Vector2 position;
        do {
            position = new Vector2(
                random.Next(0, (int)mapSize.X), // >= Min && < Max
                random.Next(0, (int)mapSize.Y)
            );
        } while (isPositionOccupied(entityManager, position) == true);

        int food = entityManager.createEntity();
        entityManager.addComponent(food, new Food());
        entityManager.addComponent(food, new Position { position = position });
        entityManager.addComponent(food, new Renderable { color = Color.Red });
        entityManager.addComponent(food, new Size { scale = 0.5f });
    }

    private bool isPositionOccupied(EntityManager entityManager, Vector2 foodPosition) {
        var snakeHead = entityManager.getEntitiesWithComponent<SnakeHead>().First();
        Position headPosition = entityManager.getComponent<Position>(snakeHead);

        if (headPosition.position == foodPosition)
            return true;

        var snakeBody = entityManager.getEntitiesWithComponent<SnakeSegment>()
            .Select(entity => entityManager.getComponent<Position>(entity).position)
            .ToHashSet();

        return snakeBody.Contains(foodPosition);
    }
}