using System.Numerics;
using Raylib_cs;

public class SceneManager : ISceneManager {
    public float deltaTime { get; private set; }
    private IScene? currentScene;
    private IScene? nextScene;
    private Dictionary<string, IScene> scenes = new();
    private bool isRunning = true;
    
    public SceneManager() {}

    public SceneManager(Vector2 windowSize) {
        Image icon = Raylib.LoadImage("assets/icon.png");
        
        Raylib.InitWindow((int)windowSize.X, (int)windowSize.Y, "Snake");
        GameState.getInstance().updateWindowSize();
        Raylib.SetTargetFPS(60);
        Raylib.SetWindowIcon(icon);
    }

    public void registerScene(string name, IScene scene) {
        scenes[name] = scene;
        scene.initialize(this);
    }

    public void changeScene(string sceneName) {
        if (scenes.TryGetValue(sceneName, out var scene))
            nextScene = scene;
    }

    public void run(string sceneName) {
        if (scenes.TryGetValue(sceneName, out var scene))
            currentScene = scene;
        else {
            Console.WriteLine("Error: Can't find the scene you try to run.");
            return;
        }

        while (!Raylib.WindowShouldClose() && isRunning) {
            deltaTime = Raylib.GetFrameTime();
            if (Raylib.IsWindowResized())
                GameState.getInstance().updateWindowSize();

            if (nextScene != null && nextScene != currentScene) {
                currentScene?.exit();
                currentScene = nextScene;
                currentScene?.enter();
                nextScene = null;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            currentScene?.update(deltaTime);
            currentScene?.draw();

            Raylib.EndDrawing();
        }
        currentScene?.exit();
        Raylib.CloseWindow();
    }

    public void exit() {
        isRunning = false;
    } 
}