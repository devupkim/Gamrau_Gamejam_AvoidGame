using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject ItemsObeject;
    
    public float speed=5;
    public Vector2 speed_vec;

    public GameObject smallPlayerObject;
    public GameObject SmallCircleItemObject;
    public GameObject flagObject;

    public Vector3 PosofSmall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Debug.Log("아이템 획득!");
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            Debug.Log("적에 닿아서 죽음");
            Dead();
        }
        else if (collision.CompareTag("SmallerCircle"))
        {
            PosofSmall = new Vector3(SmallCircleItemObject.gameObject.transform.position.x,
                SmallCircleItemObject.gameObject.transform.position.y, 0);
            Destroy(collision.gameObject);
            Debug.Log("변신!!");
            if (gameObject.name=="bigPlayer")
            {
                Debug.Log("gameObject.name이 bigPlayer이라면 전환");
                smallPlayerObject.transform.position = new Vector3(PosofSmall.x,PosofSmall.y,0);
                //Instantiate(smallPlayerObject, new Vector3(PosofSmall.x,PosofSmall.y,0),Quaternion.identity);
                //smallPlayerObject.GetComponent<Player>().enabled = true;
                gameObject.SetActive(false);
            }
        }
        else if (collision.CompareTag("Flag"))
        {
            Quit();
        }
    }

    public void isGetAllItem()
    {
        flagObject.transform.position = new Vector3((float)9.34, (float)-1.7, 0);
    }


    public void Quit()
    {
        Application.Quit();
    }
    

    void Start()
    {
        smallPlayerObject.transform.position = new Vector3(1000, 1000, 0);
        //flagObject.transform.position=new Vector3(2000, 2000, 0);
        //Destroy(smallPlayerObject.gameObject);
        //smallPlayerObject.GetComponent<Player>().enabled = false;
        //smallPlayerObject.gameObject.SetActive(false);
    }

    void Update()
    {
        speed_vec = Vector2.zero;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            speed_vec.x += speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speed_vec.x -= speed;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed_vec.y += speed;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            speed_vec.y -= speed;
        }
        GetComponent<Rigidbody2D>().velocity = speed_vec;
        if (ItemsObeject.gameObject.transform.childCount <= 0)
        {
            isGetAllItem();
        }
        
    }
    public void Dead()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("SampleScene");
    }
}
