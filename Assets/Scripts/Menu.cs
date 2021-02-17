using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public AudioSource clicksound;

    public Text creditText;
    private void Start()
    {
        Cursor.visible = true;
    }
    public void Play()
    {
        clicksound.Play();
        SceneManager.LoadScene("Scene1");
    }

    public void Exit()
    {
        clicksound.Play();
        Application.Quit();
    }

    public void Credits()
    {
        clicksound.Play();
        creditText.enabled = !creditText.enabled;
    }
}
