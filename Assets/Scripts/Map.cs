using UnityEngine;
using System.Linq;
using System.Collections;

public class Map : MonoBehaviour
{
    public GameObject prefab_Player;
    public GameObject prefab_Mummy_white;
    public GameObject prefab_Mummy_red;

    public GameObject prefab_ngang_duoi;
    public GameObject prefab_ngang_tren;

    public GameObject prefab_doc_trai;
    public GameObject prefab_doc_phai;

    public GameObject prefab_wall2traitren;
    public GameObject prefab_wall2traiduoi;
    public GameObject prefab_wall2phaitren;
    public GameObject prefab_wall2phaiduoi;
    public GameObject prefab_wall3gocduoi;


    public GameObject prefab_goal_down;
    public GameObject prefab_goal_up;
    public GameObject prefab_goal_left;
    public GameObject prefab_goal_right;
    public GameObject prefab_Border;

    public GameObject prefab_Trap;


    public GameObject prefab_Key;
    public GameObject prefab_Gate;

    public AudioClip clicksound;

    public Transform StartPointDrawMap;
    private int orderlayoutwall = 0;

    private int MAX_LINE = 8;
    private int MAX_COLUMN = 8;

    private int soundState;
    private UISprite spr;

    public int[][] MapMatrix;

    void Start()
    {
        spr = GameObject.FindGameObjectWithTag("buttonSound").GetComponent<UISprite>();
        soundState = Util.GetSoundState();
        if (soundState == 1)
        {
            spr.spriteName = "sound_off";
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
            spr.spriteName = "sound-on";
        }

        orderlayoutwall = 0;
        if (LevelMap.currentLevel <= LevelMap.allMAP.Count)
        {
            LevelMap.flagWin = false;
            DRAW_MAP(LevelMap.allMAP[LevelMap.currentLevel - 1]);
        }
        else
            LevelMap.flagWin = true;
    }

    void Update()
    {
      
    }

    /// <summary>
    /// DRAW ALL OBJECT TO MAP
    /// </summary>
    /// <param name="map"></param>
    private void DRAW_MAP(int[,] map)
    {
        for (int i = 0; i < MAX_COLUMN; i++)
        {
            for (int j = 0; j < MAX_LINE; j++)
            {
                if (map[i, j] != 0)
                {
                    //init object inside map
                    DRAW_OBJECT(map[i, j], CalculatePostion(i, j));
                }
            }
        }
        //Ve Player
        if (map[8, 0] != 0 && map[8, 1] != 0)
        {
            prefab_Player.transform.position = CalculatePostion(map[8, 0], map[8, 1]);
            Animator anim = prefab_Player.GetComponent<Animator>();
            anim.SetInteger("state", 3);
        }
        //Ve mummy white
        if (map[8, 2] != 0 && map[8, 3] != 0)
        {
            prefab_Mummy_white.SetActive(true);
            prefab_Mummy_white.transform.position = CalculatePostion(map[8, 2], map[8, 3]);
            Animator anim = prefab_Mummy_white.GetComponent<Animator>();
            anim.SetInteger("mummystate", 3);
        }
        else
            prefab_Mummy_white.SetActive(false);
        //Ve mummy red
        if (map[8, 4] != 0 && map[8, 5] != 0)
        {
            prefab_Mummy_red.SetActive(true);
            prefab_Mummy_red.transform.position = CalculatePostion(map[8, 4], map[8, 5]);
            Animator anim = prefab_Mummy_red.GetComponent<Animator>();
            anim.SetInteger("mummyredstate", 3);
        }
        else
            prefab_Mummy_red.SetActive(false);
    }


    public void ReplayMap()
    {
        AudioSource.PlayClipAtPoint(clicksound, prefab_Player.transform.position, 1.0f);
        if (LevelMap.flagWin == true)
        {
            LevelMap.flagWin = false;
            Util.SetLevelToStorage(1);
            Application.LoadLevel(1);
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public void GotoMenu()
    {
        AudioSource.PlayClipAtPoint(clicksound, prefab_Player.transform.position, 1.0f);
        Application.LoadLevel("GameMenu");
    }


    public void OnOffSound()
    {
        soundState = Util.GetSoundState();
        if (soundState == 1) //đang bật âm thanh
        {
            Debug.Log("Off Sound");
            AudioListener.pause = true;
            Util.SetSoundState(0);
            spr.spriteName = "sound-on";

        }
        else
        {
            Debug.Log("On Sound");
            Util.SetSoundState(1);
            AudioListener.pause = false;
            spr.spriteName = "sound_off";

        }
    }

    /// <summary>
    /// CALCULATE POSITION ON OBJECT
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <returns></returns>
    private Vector3 CalculatePostion(int i, int j)
    {
        Vector3 POS = new Vector3(0, 0, 0);
        POS.x = StartPointDrawMap.position.x + (j - 1) * 2.5f;
        POS.y = StartPointDrawMap.position.y - (i - 1) * 2.5f;
        POS.z = i / 10.0f;
        return POS;
    }

    private void DRAW_OBJECT(int type, Vector3 pos)
    {
        if (type == LevelMap.xxxx) //Draw boder
        {
            Instantiate(prefab_Border, pos, prefab_Border.transform.rotation);
        }
        else if (type == LevelMap.S_TRAP)
        {
            Debug.Log("Ve trap" + pos.x + " " + pos.y);
            Instantiate(prefab_Trap, pos, prefab_Trap.transform.rotation);
        }
        else if (type == LevelMap.S_DICH1) //Draw horital goaldown
        {
            pos.y = pos.y + 0.5f;
            Instantiate(prefab_goal_down, pos, prefab_goal_down.transform.rotation);
        }
        else if (type == LevelMap.S_DICH2) //Draw horital goalup
        {
            pos.y = pos.y + 0.1f;
            Instantiate(prefab_goal_up, pos, prefab_goal_up.transform.rotation);
        }
        else if (type == LevelMap.S_DICH3) //Draw horital goal left
        {
            pos.y = pos.y + 0.2f;
            Instantiate(prefab_goal_left, pos, prefab_goal_left.transform.rotation);
        }
        else if (type == LevelMap.S_DICH4) //Draw horital goal right
        {
            pos.y = pos.y + 0.2f;
            Instantiate(prefab_goal_right, pos, prefab_goal_right.transform.rotation);
        }

        else if (type == LevelMap.W_NGANG)
        {
            GameObject g = Instantiate(prefab_ngang_duoi, pos, prefab_ngang_duoi.transform.rotation) as GameObject;
            g.GetComponentInChildren<SpriteRenderer>().sortingOrder = orderlayoutwall;
            orderlayoutwall++;
        }
        else if (type == LevelMap.W_NGANG_T)
        {
            GameObject g = Instantiate(prefab_ngang_tren, pos, prefab_ngang_tren.transform.rotation) as GameObject;
            g.GetComponentInChildren<SpriteRenderer>().sortingOrder = orderlayoutwall;
            orderlayoutwall++;
        }
        else if (type == LevelMap.W_DOCT)
        {
            GameObject g = Instantiate(prefab_doc_trai, pos, prefab_doc_trai.transform.rotation) as GameObject;
            g.GetComponentInChildren<SpriteRenderer>().sortingOrder = orderlayoutwall;
            orderlayoutwall++;
        }
        else if (type == LevelMap.W_DOCP)
        {
            GameObject g = Instantiate(prefab_doc_phai, pos, prefab_doc_phai.transform.rotation) as GameObject;
            g.GetComponentInChildren<SpriteRenderer>().sortingOrder = orderlayoutwall;
            orderlayoutwall++;
        }
        else if (type == LevelMap.W_TRAI_D)
        {
            GameObject g = Instantiate(prefab_wall2traiduoi, pos, prefab_wall2traiduoi.transform.rotation) as GameObject;
            var allsprite = g.GetComponentsInChildren<SpriteRenderer>();
            foreach (var m in allsprite)
            {
                m.sortingOrder = orderlayoutwall;
                orderlayoutwall++;
            }
        }
        else if (type == LevelMap.W_TRAI_U)
        {
            GameObject g = Instantiate(prefab_wall2traitren, pos, prefab_wall2traitren.transform.rotation) as GameObject;
            var allsprite = g.GetComponentsInChildren<SpriteRenderer>();
            foreach (var m in allsprite)
            {
                m.sortingOrder = orderlayoutwall;
                orderlayoutwall++;
            }
        }
        else if (type == LevelMap.W_PHAI_D)
        {
            GameObject g = Instantiate(prefab_wall2phaiduoi, pos, prefab_wall2phaiduoi.transform.rotation) as GameObject;
            var allsprite = g.GetComponentsInChildren<SpriteRenderer>();
            foreach (var m in allsprite)
            {
                m.sortingOrder = orderlayoutwall;
                orderlayoutwall++;
            }
        }
        else if (type == LevelMap.W_PHAI_U)
        {
            GameObject g = Instantiate(prefab_wall2phaitren, pos, prefab_wall2phaitren.transform.rotation) as GameObject;
            var allsprite = g.GetComponentsInChildren<SpriteRenderer>();
            foreach (var m in allsprite)
            {
                m.sortingOrder = orderlayoutwall;
                orderlayoutwall++;
            }
        }
        else if (type == LevelMap.W_ALL_D)
        {
            GameObject g = Instantiate(prefab_wall3gocduoi, pos, prefab_wall3gocduoi.transform.rotation) as GameObject;
            var allsprite = g.GetComponentsInChildren<SpriteRenderer>();
            foreach (var m in allsprite)
            {
                m.sortingOrder = orderlayoutwall;
                orderlayoutwall++;
            }
        }
        else if (type == LevelMap.S_KEY)
        {
            Instantiate(prefab_Key, pos, prefab_Key.transform.rotation);
        }
        else if (type == LevelMap.S_GATE)
        {
            prefab_Gate.transform.position = pos;
        }

    }
}
