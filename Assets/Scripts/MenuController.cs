using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text VolumeSliderValue = null;
    [SerializeField] private Slider VolumeSlider = null;

    [Header("Confirmation")]
    [SerializeField] private GameObject ConfirmationPrompt = null;


    [Header("Main menu")]
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

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        VolumeSliderValue.text = volume.ToString("000");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("MasterVolume", AudioListener.volume);
        //StartCoroutine(ConfimationBox());
    }

    public IEnumerable ConfirmationBox()
    {
        ConfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        ConfirmationPrompt.SetActive(false);
    }
}
