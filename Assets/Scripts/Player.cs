using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Player : MonoBehaviour
{
    public Transform sightUp;
    public Transform sightDown;
    public Transform sightLeft;
    public Transform sightRight;

    private bool flagMove = false;
    private bool flagTurnOfPlayer = true;

    private Vector2 nextPoint = new Vector2(0, 0);
    private Animator anim;
    private float step = 2.5f;

    private GameObject mummywhite;
    private GameObject mummyred;
    private GameObject dieObject;
    private GameObject dieObjectRed;
    private GameObject PopupPanel;
    private GameObject buttonNextLevel;
    private GameObject buttonReplayGame;
    private GameObject gate;
    public GameObject BlockTrap;
    private UILabel lblTitlePopup;
    private UILabel lblLevel;
    private UILabel lblGate;


    public AudioClip clicksound;
    public AudioClip walk;
    public AudioClip gameover;
    public AudioClip openGate;
    public AudioClip blocktrap;
    private bool isGameOver = false;
    // Use this for initialization
    void Start()
    {
        //Util.SetLevelToStorage(10);
        //Init map
        LevelMap.flagPassedLevel = false;
        LevelMap.InitALLMAP();
        LevelMap.currentLevel = Util.GetLevel();

        flagTurnOfPlayer = true;
        flagMove = false;
        anim = this.gameObject.GetComponent<Animator>();
        gate = GameObject.FindGameObjectWithTag("gate");

        dieObject = GameObject.FindGameObjectWithTag("dieobject");
        dieObjectRed = GameObject.FindGameObjectWithTag("dieobjectred");

        PopupPanel = GameObject.FindGameObjectWithTag("popup");
        buttonNextLevel = GameObject.FindGameObjectWithTag("p_btn_next");
        buttonReplayGame = GameObject.FindGameObjectWithTag("p_btn_replay");

        lblTitlePopup = GameObject.FindGameObjectWithTag("lblTitle").GetComponent<UILabel>();
        lblLevel = GameObject.FindGameObjectWithTag("lblLevel").GetComponent<UILabel>();
        lblGate = GameObject.FindGameObjectWithTag("lblGate").GetComponent<UILabel>();
        lblLevel.text = Util.GetLevel() + "";
        if (Util.GetLevel() > 15)
        {
            lblGate.text = "2";
        }
        else
        {
            lblGate.text = "1";
        }

        mummywhite = GameObject.FindGameObjectWithTag("mummy");
        mummyred = GameObject.FindGameObjectWithTag("mummyred");

        buttonNextLevel.SetActive(false);
        buttonReplayGame.SetActive(false);
        dieObject.SetActive(false);
        dieObjectRed.SetActive(false);
        BlockTrap.SetActive(false);

        if (LevelMap.currentLevel <= LevelMap.allMAP.Count)
        {
            LevelMap.flagWin = false;
            PopupPanel.SetActive(false);
        }
        else
        {
            LevelMap.flagWin = true;
            PopupPanel.SetActive(true);
            lblTitlePopup.text = "YOU WIN !";
            buttonReplayGame.SetActive(true);
            if (Advertisement.isSupported && Advertisement.IsReady())
            {
                Advertisement.Show("rewardedVideo", new ShowOptions { resultCallback = result => { } });
            }
        }
    }

    /// <summary>
    /// Check around player have object 
    /// </summary>
    /// <param name="vectorCheck"></param>
    /// <returns></returns>
    private bool isCanMove(Transform vectorCheck)
    {
        RaycastHit2D b = Physics2D.Raycast(vectorCheck.position, Vector3.left, 0);
        if (b.collider != null)
        {
            if (b.collider.gameObject.tag == "wall")
            {
                Debug.Log("Khong di chuyen duoc ");
                return false;
            }
            else if (b.collider.gameObject.tag == "gate")
            {
                Animator a = b.collider.gameObject.GetComponent<Animator>();
                return a.GetBool("flagCloseGate");
            }
        }
        return true;
    }

    public bool GetTurnPlayerMove()
    {
        return flagTurnOfPlayer;
    }

    public void SetTurnPlayerMove(bool MovePlayerFlag)
    {
        this.flagTurnOfPlayer = MovePlayerFlag;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isGameOver || LevelMap.flagPassedLevel) return;
        if (col.gameObject.tag == "finishpoint")
        {
            LevelMap.flagPassedLevel = true;
            //Hide sprite player and mummy
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false; //hide sprite player
            if (mummywhite != null)
            {
                mummywhite.GetComponent<SpriteRenderer>().enabled = false; //hide sprite mummy white
                mummywhite.GetComponent<CircleCollider2D>().enabled = false;
            }
            if (mummyred != null)
            {
                mummyred.GetComponent<SpriteRenderer>().enabled = false; //hide sprite mummy red 
                mummyred.GetComponent<CircleCollider2D>().enabled = false;
            }

            AudioSource.PlayClipAtPoint(openGate, transform.position, 1.0f);
            PopupPanel.SetActive(true);//Display popup
            Util.SetLevelToStorage(LevelMap.currentLevel + 1);

            //Set next level
            if ((LevelMap.currentLevel + 1) <= LevelMap.allMAP.Count)
            {
                lblTitlePopup.text = "COMPLETED !";
                buttonNextLevel.SetActive(true);
            }
            //Set Win
            else
            {
                LevelMap.flagWin = true;
                lblTitlePopup.text = "YOU WIN !";
                buttonReplayGame.SetActive(true);
                if (Advertisement.isSupported && Advertisement.IsReady())
                {
                    Advertisement.Show("rewardedVideo", new ShowOptions { resultCallback = result => { } });
                }
            }

        }
        else if (col.gameObject.tag == "mummy") // GAME OVER
        {
            isGameOver = true;
            AudioSource.PlayClipAtPoint(gameover, transform.position, 1.0f);

            dieObject.transform.position = this.transform.position;
            dieObject.SetActive(true);
            this.GetComponent<SpriteRenderer>().enabled = false; //hide sprite player
            col.gameObject.GetComponent<SpriteRenderer>().enabled = false; //hide sprite mummy
            StartCoroutine(WaitAmimationGameOver());
        }
        else if (col.gameObject.tag == "mummyred")// GAME OVER
        {
            isGameOver = true;
            AudioSource.PlayClipAtPoint(gameover, transform.position, 1.0f);
            dieObjectRed.transform.position = this.transform.position;
            dieObjectRed.SetActive(true);
            this.GetComponent<SpriteRenderer>().enabled = false; //hide sprite player
            col.gameObject.GetComponent<SpriteRenderer>().enabled = false; //hide sprite mummy
            StartCoroutine(WaitAmimationGameOver());
        }
        else if (col.gameObject.tag == "key")
        {
            Animator a = gate.GetComponent<Animator>();
            bool b = a.GetBool("flagCloseGate");
            a.SetBool("flagCloseGate", !b);
        }
        else if (col.gameObject.tag == "trap1")
        {
            isGameOver = true;
            Vector3 vt = this.gameObject.transform.position;
            vt.y = 7.95f;
            //display block
            BlockTrap.SetActive(true);
            BlockTrap.transform.position = vt;

            //Down
            AudioSource.PlayClipAtPoint(blocktrap, transform.position, 1.0f);
            LeanTween.move(BlockTrap, new Vector3(nextPoint.x, nextPoint.y + 0.5f, 0f), 0.15f);
            //And GameOver
            StartCoroutine(WaitAmimationGameOver());
        }
    }

    /// <summary>
    /// Wait Animation and gameover
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitAmimationGameOver()
    {
        // Wait Animation Die Finish 
        yield return new WaitForSeconds(0.4f);
        lblTitlePopup.text = "GAME OVER";
        buttonReplayGame.SetActive(true);
        PopupPanel.SetActive(true);
        // todo hide video ads
        // if (Advertisement.isSupported && Advertisement.IsReady())
        // {
        //     Advertisement.Show("video", new ShowOptions { resultCallback = result => { } });
        // }
    }

    // Update is called once per frame
    void Update()
    {
        //Xử lý khi back
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("GameMenu");
        }

        //Check finish point
        if (flagMove)
        {
            if (this.transform.position.x == nextPoint.x && this.transform.position.y == nextPoint.y)
            {
                flagMove = false;         //Player da di den dich => Co the di chuyen tiep
                flagTurnOfPlayer = false; //Den luot Mummy di
                int valueState = anim.GetInteger("state");
                anim.SetInteger("state", valueState + 3);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }


    public void MoveUp()
    {
        if (flagMove == false && flagTurnOfPlayer == true)
        {
            flagMove = true;
            anim.SetInteger("state", 2);
            if (isCanMove(sightUp) == false)
                nextPoint = new Vector2(transform.position.x, transform.position.y);
            else
            {
                AudioSource.PlayClipAtPoint(walk, transform.position, 1.0f);
                nextPoint = new Vector2(transform.position.x, transform.position.y + step);
                LeanTween.move(this.gameObject, nextPoint, 0.4f);
            }

        }
    }

    public void MoveDown()
    {
        if (flagMove == false && flagTurnOfPlayer == true)
        {
            flagMove = true;
            anim.SetInteger("state", 0);
            if (isCanMove(sightDown) == false)
                nextPoint = new Vector2(transform.position.x, transform.position.y);
            else
            {
                AudioSource.PlayClipAtPoint(walk, transform.position, 1.0f);
                nextPoint = new Vector2(transform.position.x, transform.position.y - step);
                LeanTween.move(this.gameObject, nextPoint, 0.4f);
            }
        }
    }

    public void MoveLeft()
    {
        if (flagMove == false && flagTurnOfPlayer == true)
        {
            flagMove = true;
            anim.SetInteger("state", 1);
            //Flip player
            this.transform.localScale = new Vector3(-4.2f, 4.2f, 1);
            //Check can move
            if (isCanMove(sightLeft) == false)
                nextPoint = new Vector2(transform.position.x, transform.position.y);
            else
            {
                AudioSource.PlayClipAtPoint(walk, transform.position, 1.0f);
                nextPoint = new Vector2(transform.position.x - step, transform.position.y);
                LeanTween.move(this.gameObject, nextPoint, 0.4f);
            }
        }
    }

    public void MoveRight()
    {
        if (flagMove == false && flagTurnOfPlayer == true)
        {
            flagMove = true;
            anim.SetInteger("state", 1);
            //Flip player
            this.transform.localScale = new Vector3(4.2f, 4.2f, 1);
            //Check can move
            if (isCanMove(sightLeft) == false)
                nextPoint = new Vector2(transform.position.x, transform.position.y);
            else
            {
                AudioSource.PlayClipAtPoint(walk, transform.position, 1.0f);
                nextPoint = new Vector2(transform.position.x + step, transform.position.y);
                LeanTween.move(this.gameObject, nextPoint, 0.4f);
            }
        }
    }

}
