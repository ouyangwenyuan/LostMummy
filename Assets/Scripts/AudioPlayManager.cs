using UnityEngine;
using System.Collections;

public class AudioPlayManager : MonoBehaviour
{
    static bool AudioBegin = false;
    private GameObject gamePlaySound;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        gamePlaySound = GameObject.FindGameObjectWithTag("GamePlayMusic");
        if (gamePlaySound == this.gameObject)
        {
            if (!AudioBegin)
            {
                DontDestroyOnLoad(this.gameObject);
                AudioBegin = true;
            }
        }
        else
        {
            Destroy(gamePlaySound);
        }
    }
}
