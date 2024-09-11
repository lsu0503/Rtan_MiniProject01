using UnityEngine.SceneManagement;

public class LevelPreSelector
{
    private static LevelPreSelector instance;
    int preSelectedLevel = 0;
    public int PreSelectedLevel
    {
        get { return preSelectedLevel; }
        set { preSelectedLevel = value; }
    }

    public static LevelPreSelector GetInstance()
    {
        if(instance == null)
        {
            instance = new LevelPreSelector();
        }
        return instance;
    }

    public void LoadMainSceneWithLevel(int level)
    {
        preSelectedLevel = level;
        SceneManager.LoadScene("MainScene");
    }
}
