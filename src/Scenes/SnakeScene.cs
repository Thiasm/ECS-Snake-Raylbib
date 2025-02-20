using System.Numerics;
using Raylib_cs;

public class SnakeScene : IScene {
    public override void initialize(ISceneManager SceneManager) {
        sceneManager = SceneManager; // Stocké dans Iscene permet d'avoir accès aux fonctions de SceneManager
    }

    public override void update(float deltaTime) {
        systemManager.updateByType(entityManager, deltaTime);

        if (GameState.getInstance().gameState == State.GameOver)
            sceneManager.changeScene("gameOver");
    }

    public override void enter() {
        GameState.getInstance().setState(State.Gameplay);
        createSnake();
        setupSystems();
    }

    public override void exit() {
        entityManager.clearEntities();
        systemManager.clearSystem();
    }

    private void setupSystems() {
        systemManager.addSystem<GameplayInputSystem>(new GameplayInputSystem());
        systemManager.addSystem<GameplayRenderSystem>(new GameplayRenderSystem());
        systemManager.addSystem<PauseInputSystem>(new PauseInputSystem());
        systemManager.addSystem<PauseRenderSystem>(new PauseRenderSystem());
        systemManager.addSystem<MovementSystem>(new MovementSystem());
        systemManager.addSystem<CollisionSystem>(new CollisionSystem());
        systemManager.addSystem<FoodSystem>(new FoodSystem());
    }

    private void createSnake() {
        Vector2 startPosition = GameState.getInstance().mapSize / 2;
        int snake = entityManager.createEntity();
        entityManager.addComponent(snake, new SnakeHead());
        entityManager.addComponent(snake, new Direction { direction = Vector2.UnitX });
        entityManager.addComponent(snake, new Position { position = startPosition });
        entityManager.addComponent(snake, new Renderable { color = Color.DarkGreen });
        entityManager.addComponent(snake, new Size { scale = 1f });

        for (int i = 0; i < 4; i += 1) {
            int snakeSegment = entityManager.createEntity();
            entityManager.addComponent(snakeSegment, new SnakeSegment { index = i });
            entityManager.addComponent(snakeSegment, new Position { position = new Vector2(startPosition.X - (i + 1), startPosition.Y) });
            entityManager.addComponent(snakeSegment, new Renderable { color = Color.Green });
            entityManager.addComponent(snakeSegment, new Size { scale = 1f });
        }
    }

}