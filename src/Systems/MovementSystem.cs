using System.Numerics;

public class MovementSystem : ISystem 
{
    private readonly Vector2 playAreaMin;
    private readonly Vector2 playAreaMax;
    private float moveTimer;

    public MovementSystem()  {
        Vector2 mapSize = GameState.getInstance().mapSize;

        systemType = State.Gameplay;
        moveTimer = 0f;
        playAreaMin = new Vector2(0, 0);
        playAreaMax = mapSize - new Vector2(1, 1);
    }

    public override void update(EntityManager entityManager, float deltaTime)  {
        moveTimer += deltaTime;
        if (moveTimer < 0.15f)
            return;
        moveTimer = 0f;

        var snakeHead = entityManager.getEntitiesWithComponent<SnakeHead>().First();
        var oldHeadPosition = entityManager.getComponent<Position>(snakeHead);

        moveSnakeHead(entityManager, snakeHead, oldHeadPosition);
        moveSnakeBody(entityManager, oldHeadPosition);
    }

    private void moveSnakeHead(EntityManager entityManager, int snakeHead, Position oldPosition) {
        var headPosition = oldPosition;
        var direction = entityManager.getComponent<Direction>(snakeHead);
        
        headPosition.position += direction.direction;

        if (!isPositionValid(headPosition.position))
            return;

        entityManager.setComponent(snakeHead, headPosition);
    }

    private void moveSnakeBody(EntityManager entityManager, Position oldHeadPosition) {
        var entities = entityManager.getEntitiesWithComponent<SnakeSegment>()
            .Select(id => (id, segment: entityManager.getComponent<SnakeSegment>(id)))
            .OrderBy(x => x.segment.index)
            .ToList();

        Position previousPosition = oldHeadPosition;
        foreach (var entity in entities) {
            var currentPosition = entityManager.getComponent<Position>(entity.id);
            entityManager.setComponent(entity.id, previousPosition);
            previousPosition = currentPosition;
        }
    }

    private bool isPositionValid(Vector2 position) {
        return position.X >= playAreaMin.X && 
               position.X <= playAreaMax.X && 
               position.Y >= playAreaMin.Y && 
               position.Y <= playAreaMax.Y;
    }
}