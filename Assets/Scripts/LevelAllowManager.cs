using UnityEngine;

public class LevelAllowManager : MonoBehaviour
{
    public GameObject HardModeButton;
    public GameObject HardModeButtonBlocker;

    // Start is called before the first frame update
    void Start()
    {
        string allowLevelKey = "AllowLevel";
        int allowLevel = 0;
        if(PlayerPrefs.HasKey(allowLevelKey))
        {
            allowLevel = PlayerPrefs.GetInt(allowLevelKey);
        }

        if(allowLevel == 0)
        {
            HardModeButton.SetActive(false);
            HardModeButtonBlocker.SetActive(true);
        }
    }
}
