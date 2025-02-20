using System.Numerics;

// Permet aux scenes de pouvoir changer de scene et de quitter
public interface ISceneManager {
    float deltaTime { get; }

    void changeScene(string sceneName);
    void exit();
}