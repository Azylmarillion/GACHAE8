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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && SceneManager.GetActiveScene().name != "SceneAlice")
        {
            ManagePauseMenu();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Test");
    }

    public void ManagePauseMenu()
    {
        if (!m_OptionMenu.activeInHierarchy && !m_ControlsMenu.activeInHierarchy)
        {
            m_PauseMenu.SetActive(!m_PauseMenu.activeInHierarchy);
        }

    }

    public void Resume()
    {
        m_PauseMenu.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("SceneAlice");
    }

    public void OpenOptions()
    {
        m_OptionMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        m_OptionMenu.SetActive(false);
    }

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
