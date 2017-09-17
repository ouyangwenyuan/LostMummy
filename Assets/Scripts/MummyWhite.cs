using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MummyWhite : MonoBehaviour
{
    public Transform sightUp;
    public Transform sightDown;
    public Transform sightLeft;
    public Transform sightRight;

    private Animator anim;
    private GameObject player;
    private GameObject gate;
    private Vector2 nextPoint = new Vector2(0, 0);
    private bool flagMove = false;
    private int sobuocdi = 0;

    public AudioClip walk;


    void Start()
    {
        flagMove = false;
        anim = this.gameObject.GetComponent<Animator>();
        gate = GameObject.FindGameObjectWithTag("gate");
        player = GameObject.FindGameObjectWithTag("player");

    }


    public void DiChuyenMummy()
    {
        bool getPlayerturn = player.GetComponent<Player>().GetTurnPlayerMove();
        if (getPlayerturn == false) //Nếu player đã di chuyển => đến lượt mummy di chuyển
        {
            Vector3 posPlayer = player.transform.position;
            Vector3 posMummy = this.transform.position;
            if (Mathf.Approximately(posPlayer.x, posMummy.x))
            {
                posMummy.x = posPlayer.x;
            }

            if (posPlayer.x < posMummy.x)
            {
                //Flip player
                this.transform.localScale = new Vector3(-4.2f, 4.2f, 1);
                if (isCanMove(sightRight) == false)
                {
                    if (posPlayer.y - posMummy.y > 0f)
                    {
                        MoveUp();
                    }
                    else if (posPlayer.y - posMummy.y < 0f)
                    {
                        MoveDown();
                    }
                    else MoveLeft();
                }
                else
                {
                    MoveLeft();
                }
            }
            else if (posPlayer.x > posMummy.x)
            {
                //Flip player
                this.transform.localScale = new Vector3(4.2f, 4.2f, 1);
                if (isCanMove(sightRight) == false)
                {
                    if (posPlayer.y - posMummy.y > 0f)
                    {
                        MoveUp();
                    }
                    else if (posPlayer.y - posMummy.y < 0f)
                    {
                        MoveDown();
                    }
                    else
                    {
                        MoveRight();
                    }
                }
                else
                {
                    MoveRight();
                }
            }
            else if (posPlayer.y > posMummy.y)
            {
                MoveUp();
            }
            else if (posPlayer.y < posMummy.y)
            {
                MoveDown();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "key")
        {
            Debug.Log("Cham vao chia khoa====================");
            Animator a = gate.GetComponent<Animator>();
            a.SetBool("flagCloseGate", true);
        }
    }

    void Update()
    {
        if (flagMove)
        {
            if (Mathf.Approximately(this.transform.position.x, nextPoint.x) && Mathf.Approximately(this.transform.position.y, nextPoint.y))
            {
                flagMove = false;         //Mummy da di den vi tri dung lai
                this.transform.position = nextPoint;
                //Set lai animation 
                anim.SetInteger("mummystate", anim.GetInteger("mummystate") + 3);
                //Chuyen luot di cho player khi den dich
                if (sobuocdi == 2)
                {
                    player.GetComponent<Player>().SetTurnPlayerMove(true); // den luot di của player
                    sobuocdi = 0;
                }
                else DiChuyenMummy();
            }
        }
        else
        {
            if (LevelMap.flagPassedLevel == false)
            {
                DiChuyenMummy();
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawLine(sightUp.position, sightDown.position);
    //    Gizmos.DrawLine(sightLeft.position, sightRight.position);
    //}

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
                return false;
            else if (b.collider.gameObject.tag == "gate")
            {
                Animator a = b.collider.gameObject.GetComponent<Animator>();
                return a.GetBool("flagCloseGate");
            }
        }

        return true;
    }


    public void MoveUp()
    {
        if (flagMove == false)
        {
            sobuocdi++;
            flagMove = true;
            anim.SetInteger("mummystate", 1);
            if (isCanMove(sightUp) == false)
                nextPoint = new Vector2(transform.position.x, transform.position.y);
            else
            {
                AudioSource.PlayClipAtPoint(walk, transform.position, 1.0f);
                nextPoint = new Vector2(transform.position.x, transform.position.y + 2.50f);
                LeanTween.move(this.gameObject, nextPoint, 0.4f);
            }
        }
    }

    public void MoveDown()
    {
        if (flagMove == false)
        {
            sobuocdi++;
            flagMove = true;
            anim.SetInteger("mummystate", 0);
            if (isCanMove(sightDown) == false) nextPoint = new Vector2(transform.position.x, transform.position.y);
            else
            {
                AudioSource.PlayClipAtPoint(walk, transform.position, 1.0f);
                nextPoint = new Vector2(transform.position.x, transform.position.y - 2.50f);
                LeanTween.move(this.gameObject, nextPoint, 0.4f);
            }
        }
    }

    public void MoveLeft()
    {
        if (flagMove == false)
        {
            sobuocdi++;
            flagMove = true;
            anim.SetInteger("mummystate", 2);

            //Check can move
            if (isCanMove(sightRight) == false) nextPoint = new Vector2(transform.position.x, transform.position.y);
            else
            {
                AudioSource.PlayClipAtPoint(walk, transform.position, 1.0f);
                nextPoint = new Vector2(transform.position.x - 2.50f, transform.position.y);
                LeanTween.move(this.gameObject, nextPoint, 0.4f);
            }
        }
    }

    public void MoveRight()
    {
        if (flagMove == false)
        {
            sobuocdi++;
            flagMove = true;
            anim.SetInteger("mummystate", 2);
            //Check can move
            if (isCanMove(sightRight) == false) nextPoint = new Vector2(transform.position.x, transform.position.y);
            else
            {
                AudioSource.PlayClipAtPoint(walk, transform.position, 1.0f);
                nextPoint = new Vector2(transform.position.x + 2.50f, transform.position.y);
                LeanTween.move(this.gameObject, nextPoint, 0.4f);
            }
        }
    }


}
