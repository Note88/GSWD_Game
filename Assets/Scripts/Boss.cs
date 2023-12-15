using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    int hp=5000;
    public Image hp_bar;

    public bool pt1_ing, pt2_ing, down_ing, pt1_able, pt2_able, ball_moving, pt3_able, pt3_ing, hand_ing, hand_ing2;
    public GameObject player, ball;
    public float ball_speed, ball_rotate_speed, hand_speed;
    public float speed;

    public GameObject right,left, hand;

    public AudioClip kwang, hwick;

    Vector3 ball_pos, handpos;
    // Start is called before the first frame update
    void Start()
    {
        hand_ing2=false;
        hand_ing=false;
        ball_moving=false;
        ball_speed=1f;
        ball_rotate_speed=30f;
        pt1_ing=false;
        pt2_ing=false;
        pt3_ing=false;
        down_ing=false;
        player=GameObject.Find("Player");
        pt1_able=false;
        pt2_able=false;
        pt3_able=false;
        StartCoroutine("pt1_cool");
        StartCoroutine("pt2_cool");
        StartCoroutine("pt3_cool");
        ball_pos=ball.transform.position;

        //StartCoroutine("pt1");
    }

    // Update is called once per frame
    void Update()
    {
        if (hp<=0)
        {
            GameManager.instance.StartCoroutine("end_crt");
            gameObject.SetActive(false);
        }

        if (!pt1_ing && !pt2_ing && !pt3_ing)
        {
            if (player.transform.position.x < transform.position.x)
            {
                transform.rotation=Quaternion.Euler(new Vector3(0,180,0));
                transform.Translate(new Vector2(-speed*Time.deltaTime,0),Space.World);
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                transform.Translate(new Vector2(speed * Time.deltaTime, 0),Space.World);
            }
        }

        

        if (down_ing)
        {
            transform.Translate(new Vector2(0,-20*Time.deltaTime));
            if (transform.position.y<=-52.9f)
            {
                down_ing=false;
            }
        }

        if (pt2_able && !pt1_ing && !pt3_ing)
        {
            pt2_able=false;
            StartCoroutine("pt2");
        }

        if (pt1_able && !pt2_ing && !pt3_ing)
        {
            pt1_able=false;
            StartCoroutine("pt1");
        }

        if (pt3_able && !pt1_ing && !pt2_ing && Mathf.Abs(player.transform.position.x-transform.position.x)<7f)
        {
            pt3_able=false;
            StartCoroutine("pt3");
        }

        if (ball_moving)
        {
            ball.transform.Translate(new Vector2(ball_speed*Time.deltaTime,0),Space.World);
            ball.transform.Rotate(0,0,-ball_rotate_speed*Time.deltaTime);

            ball_speed*=1f+1f*Time.deltaTime;
            ball_rotate_speed*=1f+1f*Time.deltaTime;
        }

        if (hand_ing)
        {
            hand.transform.Translate(new Vector2(hand_speed*Time.deltaTime,0));
            hand_speed*=1f-1*Time.deltaTime;
        }

        if (hand_ing2)
        {
            hand.transform.Translate(new Vector2(-8 * Time.deltaTime, 0));
            if (Mathf.Abs(hand.transform.position.x-handpos.x)<0.1f)
            {
                hand_ing2=false;
            }
        }

    }

    public IEnumerator pt3()
    {
        pt3_ing=true;
        handpos=hand.transform.position;
        hand_speed=7f;
        hand_ing=true;
        GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(hwick);
        yield return new WaitForSeconds(2f);
        hand_ing=false;
        hand_ing2=true;
        yield return new WaitForSeconds(1f);
        hand.transform.position=handpos;
        yield return new WaitForSeconds(1f);
        pt3_ing=false;
        StartCoroutine("pt3_cool");  
    }

    public IEnumerator pt2()
    {
        pt2_ing=true;
        ball_speed=1f;
        ball_rotate_speed=30f;
        ball.transform.position=ball_pos;
        ball.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(kwang);
        transform.position=new Vector2(211.67f,transform.position.y+5);
        yield return new WaitForSeconds(0.5f);
        down_ing =true;
        yield return new WaitForSeconds(0.3f);
        transform.position = new Vector2(transform.position.x, -52.9f);
        Camera.main.transform.rotation=Quaternion.Euler(new Vector3(0,0,10));
        Instantiate(right, new Vector2(transform.position.x, transform.position.y - 0.7f), Quaternion.identity);
        Instantiate(left, new Vector2(transform.position.x, transform.position.y - 0.7f), Quaternion.identity);
        GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(kwang);
        ball_moving =true;
        yield return new WaitForSeconds(3.3f);
        ball_moving=false;
        ball.SetActive(false);

        transform.position = new Vector2(188.59f, transform.position.y + 5);
        yield return new WaitForSeconds(0.5f);
        down_ing = true;
        yield return new WaitForSeconds(0.3f);
        transform.position = new Vector2(transform.position.x, -52.9f);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        Instantiate(right, new Vector2(transform.position.x, transform.position.y - 0.7f), Quaternion.identity);
        Instantiate(left, new Vector2(transform.position.x, transform.position.y - 0.7f), Quaternion.identity);
        GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(kwang);



        yield return new WaitForSeconds(1f);
        pt2_ing = false;
        StartCoroutine("pt2_cool");
        

    }

    public IEnumerator pt1()
    {
        
        pt1_ing=true;
        transform.position=new Vector2(player.transform.position.x, transform.position.y+5);
        yield return new WaitForSeconds(0.5f);
        down_ing=true;
        yield return new WaitForSeconds(0.3f);
        transform.position=new Vector2(transform.position.x,-52.9f);
        Instantiate(right,new Vector2(transform.position.x,transform.position.y-0.7f),Quaternion.identity);
        Instantiate(left, new Vector2(transform.position.x, transform.position.y - 0.7f), Quaternion.identity);
        GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(kwang);
        yield return new WaitForSeconds(1f);
        pt1_ing=false;
        StartCoroutine("pt1_cool");
    }

    IEnumerator pt1_cool()
    {
        yield return new WaitForSeconds(5f);
        pt1_able=true;
    }

    IEnumerator pt2_cool()
    {
        yield return new WaitForSeconds(16f);
        pt2_able=true;

    }

    IEnumerator pt3_cool()
    {
        yield return new WaitForSeconds(9f);
        pt3_able = true;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("check");
        if (col.gameObject.tag == "Bullet")
        {
            hp-=15;
            hp_bar.fillAmount -= (float)((float)15 / (float)5000);
        }
    }
}
