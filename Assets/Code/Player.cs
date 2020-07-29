using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public SpriteRenderer spriteR;
    public float thrust = 10.0f;

    public Sprite idleR;
    public Sprite idleL;
    public Sprite moveR;
    public Sprite moveL;
    public Sprite jumpR;
    public Sprite jumpL;

    bool isRight = false;
    bool isClick = false;
    bool isJump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump != true)
            {
                changeAction(2);
                isJump = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || (isClick == true && isRight == true))
        {
            isRight = true;
            isClick = true;
            if (isJump == true)
            {
                rb2D.AddForce(transform.right * thrust);
                changeAction(2);
            }
            else
                changeAction(1);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isClick = false;
            if (isJump != true)
                changeAction(0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || (isClick == true && isRight == false))
        {
            isRight = false;
            isClick = true;
            if (isJump == true)
            {
                rb2D.AddForce(transform.right * thrust * (-1));
                changeAction(2);
            }
            else
                changeAction(1);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isClick = false;
            if (isJump != true)
                changeAction(0);
        }
    }

    void changeAction(int tmp) //0 idle, 1 move, 2 jump
    {
        if (isRight == true)
        {
            if (tmp == 0)
            {
                spriteR.sprite = idleR;
            }
            else if (tmp == 1)
            {
                spriteR.sprite = moveR;
                rb2D.AddForce(transform.right * thrust);
            }
            else
            {
                spriteR.sprite = jumpR;
                if(isJump == false)
                    rb2D.AddForce(transform.up * thrust * 50);
            }
        }
        else
        {
            if (tmp == 0)
            {
                spriteR.sprite = idleL;
            }
            else if (tmp == 1)
            {
                spriteR.sprite = moveL;
                rb2D.AddForce((-1) * transform.right * thrust);
            }
            else
            {
                spriteR.sprite = jumpL;
                if (isJump == false)
                    rb2D.AddForce(transform.up * thrust * 50);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        changeAction(0);
        isJump = false;
    }
}
