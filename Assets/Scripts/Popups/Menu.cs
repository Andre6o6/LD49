using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private PortraitManager _portraits;

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        _portraits.CachePortraits();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
