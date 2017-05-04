using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : SingletonUnity<GameManager>
{
    private string[] scenes;
    public int currentScene = 1;
    public string DirScene;

    public bool IsGameRunning = true;

    void Start()
    {
        scenes = Constant.Scenes;
        DontDestroyOnLoad (this.gameObject);
    }

    public void loadScene(SceneType scene)
    {
        DirScene = scenes [(int)scene];
        SceneManager.LoadScene (Constant.Scenes[(int)SceneType.LoadingScene]);
    }

    public void RestartScene()
    {
        loadScene ((SceneType)currentScene);
    }



}

public enum SceneType
{
    SplashScene,
    Scene1,
    Scene2,
    Scene3,
    LoadingScene
}

