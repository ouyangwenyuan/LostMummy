using UnityEngine;
using System.Collections;

public class SmallMapScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        int currentLevel = Util.GetLevel() % 15;
        CheckAndMarkCurrentLevel(currentLevel);
    }

    public void CheckAndMarkCurrentLevel(int currentlevel)
    {
        var listRen = this.GetComponentsInChildren<SpriteRenderer>();
        for (int i = listRen.Length -1 ; i >= 0; i--)
        {
            if (i > currentlevel)
            {
                listRen[i].enabled = false;
            }
            else
            {
                listRen[i].enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
