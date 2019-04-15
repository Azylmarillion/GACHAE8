using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject m_OptionMenu;
    [SerializeField] private GameObject m_ControlsMenu;
    [SerializeField] private GameObject m_PauseMenu;
    [SerializeField] private GameObject m_Credits;
    [SerializeField] private AudioSource m_audioSourceScene;


    private void Update()
    {

    }

    private void Awake()
    {
        XboxControllerInputManagerWindows.OnStartDownInputPress += ManagePauseMenu;
    }

    public void Play()
    {
        SceneManager.LoadScene("Test");
    }

    public void ManagePauseMenu(bool _doIt)
    {
        if (!_doIt) return;

        if (SceneManager.GetActiveScene().name != "SceneAlice")
        {
            if (!m_OptionMenu.activeInHierarchy && !m_ControlsMenu.activeInHierarchy)
            {
                if(m_PauseMenu.activeInHierarchy)
                {
                    Time.timeScale = 1;
                    m_PauseMenu.SetActive(false);
                }
                else
                {
                    Time.timeScale = 0;
                    m_PauseMenu.SetActive(true);
                }
            }
        }        
    }

    public void Resume()
    {
        m_PauseMenu.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("SceneAlice");
    }

    #region OPTIONS
    public void OpenOptions()
    {
        m_OptionMenu.SetActive(true);
       //FindObjectOfType<>
    }

    public void SetVolume(float _volume)
    {
        m_audioSourceScene.volume = _volume;
    }

    public void SetQuality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }

    public void ScreenManager(bool _wantFullScreen)
    {
        if (_wantFullScreen && !Screen.fullScreen)
        {
            Screen.fullScreen = true;
        }
        else if (!_wantFullScreen && Screen.fullScreen)
        {
            Screen.fullScreen = false;
        }

    }

    public void CloseOptions()
    {
        m_OptionMenu.SetActive(false);
    }
    #endregion

    public void OpenControls()
    {
        m_OptionMenu.SetActive(false);
        m_ControlsMenu.SetActive(true);
    }

    public void CloseControls()
    {
        m_ControlsMenu.SetActive(false);
        m_OptionMenu.SetActive(true);
    }

    public void OpenCredits()
    {
        m_Credits.SetActive(true);
    }

    public void CloseCredits()
    {
        m_Credits.SetActive(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}