public abstract class IScene {
    protected readonly EntityManager entityManager;
    protected SystemManager systemManager;
    protected ISceneManager sceneManager;

    protected IScene() {
        entityManager = new EntityManager();
        systemManager = new SystemManager();
        sceneManager = new SceneManager();
    }

    public virtual void initialize(ISceneManager manager) {
        sceneManager = manager;
    }

    public virtual void update(float deltaTime) { }
    public virtual void draw() { }
    public virtual void enter() { }
    public virtual void exit() { }
}