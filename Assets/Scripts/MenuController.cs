using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject NoSavePan = null;

    public void NewYesBtn()
    {
        SceneManager.LoadScene(_newGameLevel);
    }
    public void ConYesBtn()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            NoSavePan.SetActive(true);
        }


    }

    public void BExi()
    {
        Application.Quit();
    }

}
