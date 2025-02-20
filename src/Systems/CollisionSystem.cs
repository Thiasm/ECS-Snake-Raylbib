using System.Numerics;
using Raylib_cs;

public class CollisionSystem : ISystem {

    public CollisionSystem() {
        systemType = State.Gameplay;
    }

    public override void update(EntityManager entityManager, float deltaTime) {
        var snakeHead = entityManager.getEntitiesWithComponent<SnakeHead>();

        if (snakeHead.Count() == 0) {
            Console.Error.WriteLine("Error: Couldn't find the snake head.");
            return;
        }
    
        Position headPosition = entityManager.getComponent<Position>(snakeHead.First());

        checkFoodCollision(entityManager, headPosition);
        if (checkSnakeCollision(entityManager, headPosition) == true) {
            Console.WriteLine("Detected Collision");
            GameState.getInstance().setState(State.GameOver);
        }
    }

    private void checkFoodCollision(EntityManager entityManager, Position headPosition) {
        var foodEntities = entityManager.getEntitiesWithComponent<Food>();
        if (!foodEntities.Any())
            return;
        
        int foodEntity = foodEntities.First();
        Position foodPosition = entityManager.getComponent<Position>(foodEntity);
        if (foodPosition.position == headPosition.position) {
            entityManager.removeEntity(foodEntity);
            growSnake(entityManager);
            GameState.getInstance().addScore(10);
        }
    }

    private void growSnake(EntityManager entityManager) {
        var snakeSegments = entityManager.getEntitiesWithComponent<SnakeSegment>()
            .Select(entity => (entity, segment: entityManager.getComponent<SnakeSegment>(entity)))
            .OrderByDescending(x => x.segment.index)
            .ToList();

        var lastSegment = snakeSegments.First();
        var lastPosition = entityManager.getComponent<Position>(lastSegment.entity);

        int snakeSegment = entityManager.createEntity();
        entityManager.addComponent(snakeSegment, new SnakeSegment { index = lastSegment.segment.index + 1 });
        entityManager.addComponent(snakeSegment, new Position { position = lastPosition.position });
        entityManager.addComponent(snakeSegment, new Renderable { color = Color.Green });
        entityManager.addComponent(snakeSegment, new Size { scale = 1f });
    }

    private bool checkSnakeCollision(EntityManager entityManager, Position headPosition) {
        return entityManager.getEntitiesWithComponent<SnakeSegment>()
            .Any(entity => entityManager.getComponent<Position>(entity).position == headPosition.position);
    }
}