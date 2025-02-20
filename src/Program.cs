using System.Numerics;

class Program {
    static void Main() {
        Vector2 windowSize = new Vector2(800, 800);
        SceneManager sceneManager = new SceneManager(windowSize);        
        sceneManager.registerScene("menu", new MenuScene());
        sceneManager.registerScene("snake", new SnakeScene());
        sceneManager.registerScene("gameOver", new GameOverScene());
        
        sceneManager.run("menu");
    }
}