using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelButton : MonoBehaviour
{
    public void SelectLevel()
    {
        SceneManager.LoadScene("MainScene");
    }
}
