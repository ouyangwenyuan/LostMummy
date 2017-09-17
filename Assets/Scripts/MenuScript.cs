using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class MenuScript : MonoBehaviour
{
    public AudioClip clicksound;
    private static int countPlay = 0;

    // Use this for initialization
    void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("1074921");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(clicksound, transform.position, 1.0f);
        Application.LoadLevel("GamePlay");
    }

    public void QuitGame()
    {
        AudioSource.PlayClipAtPoint(clicksound, transform.position, 1.0f);
        Application.Quit();
    }

    public void RateGame()
    {
        AudioSource.PlayClipAtPoint(clicksound, transform.position, 1.0f);
        Application.OpenURL("market://details?id=com.xacuop.aicap2016");
    }


}
