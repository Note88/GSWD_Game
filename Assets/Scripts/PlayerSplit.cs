using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSplit : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;
    int jumpCount = 1;

    bool moving;

    public bool moveAble, up_shooting, jumping;

    public float speed;
    public float jumpForce1, jumpForce2;
    float time;
    float jumptime;

    public AudioClip shot_sound;

    float delta;
    float beforePos;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moving = false;

        time = 0f;
        moveAble = true;
        up_shooting = false;
        jumping = false;

        delta = 0f;
        beforePos = transform.position.y;

    }

    void FixedUpdate()
    {
        if (jumpCount == 0)
        {
            delta += Time.deltaTime;
            if (delta > 0.15f)
            {
                delta = 0f;
                if (Mathf.Abs(beforePos - transform.position.y) < 0.01f)
                {
                    animator.SetBool("RUN", false);
                    animator.SetBool("JUMP", false);
                    animator.SetBool("IDLE", true);
                    jumpCount = 1;
                }
                beforePos = transform.position.y;
            }
        }
        else
            delta = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);




        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && moveAble)
        {
            rigidbody.transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            moving = true;
            if (jumpCount == 1)
            {
                animator.SetBool("IDLE", false);
                animator.SetBool("JUMP", false);
                animator.SetBool("RUN", true);
            }

        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && moveAble)
        {
            rigidbody.transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            moving = true;

            if (jumpCount == 1)
            {
                animator.SetBool("IDLE", false);
                animator.SetBool("JUMP", false);
                animator.SetBool("RUN", true);
            }

        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && moveAble) //뗀 순간
        {

            if (jumpCount == 1)
            {
                animator.SetBool("RUN", false);
                animator.SetBool("JUMP", false);
                animator.SetBool("IDLE", true);
                moving = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && moveAble) //뗀 순간
        {

            if (jumpCount == 1)
            {
                animator.SetBool("RUN", false);
                animator.SetBool("JUMP", false);
                animator.SetBool("IDLE", true);
                moving = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) && moveAble)
        {
            if (jumpCount == 1)
            {
                animator.SetBool("IDLE", false);
                animator.SetBool("JUMP", false);
                animator.SetBool("RUN", true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow) && moveAble)
        {
            if (jumpCount == 1)
            {
                animator.SetBool("IDLE", false);
                animator.SetBool("JUMP", false);
                animator.SetBool("RUN", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && moveAble)
        {
            if (jumpCount == 1)
            {
                animator.SetBool("RUN", false);
                animator.SetBool("IDLE", false);
                animator.SetBool("JUMP", true);
                jumpCount--;
                rigidbody.velocity = Vector3.zero;
                //rigidbody.AddForce(new Vector2(0, jumpForce1));
                rigidbody.AddForce(new Vector2(0, jumpForce1));
                jumptime = Time.time;
            }

        }



        if (Input.GetKey(KeyCode.C) && moveAble && Time.time - jumptime <= 0.3f && jumpCount == 0)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce2 * Time.deltaTime));
        }



        if (Input.GetKey(KeyCode.X) && !Input.GetKey(KeyCode.UpArrow) && Time.time - time > 0.15f && moveAble)
        {
            if (GameManager.instance.bulleti >= 80)
                GameManager.instance.bulleti = 0;
            time = Time.time;

            GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(shot_sound);

            if (transform.rotation.y > -0.1f && transform.rotation.y < 0.1f)
                GameManager.instance.bullets[GameManager.instance.bulleti].gameObject.transform.position = new Vector2(transform.position.x + 0.4f, transform.position.y + 0.15f);
            else
                GameManager.instance.bullets[GameManager.instance.bulleti].gameObject.transform.position = new Vector2(transform.position.x - 0.4f, transform.position.y + 0.15f);

            GameManager.instance.bullets[GameManager.instance.bulleti].gameObject.transform.rotation = transform.rotation;




            GameManager.instance.bullets[GameManager.instance.bulleti].gameObject.SetActive(true);
            GameManager.instance.bulleti++;


        }



        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.X) && Time.time - time > 0.15f && jumpCount == 1)
        {
            if (GameManager.instance.bulleti >= 80)
                GameManager.instance.bulleti = 0;
            time = Time.time;

            GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(shot_sound);

            if (transform.rotation.y > -0.1f && transform.rotation.y < 0.1f)
                GameManager.instance.bullets[GameManager.instance.bulleti].gameObject.transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y + 0.3f);
            else
                GameManager.instance.bullets[GameManager.instance.bulleti].gameObject.transform.position = new Vector2(transform.position.x - 0.13f, transform.position.y + 0.3f);

            GameManager.instance.bullets[GameManager.instance.bulleti].gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

            moveAble = false;
            up_shooting = true;
            animator.SetBool("RUN", false);
            animator.SetBool("IDLE", false);
            animator.SetBool("JUMP", false);
            animator.SetBool("UP", true);


            GameManager.instance.bullets[GameManager.instance.bulleti].gameObject.SetActive(true);
            GameManager.instance.bulleti++;
        }



        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            up_shooting = false;
            moveAble = true;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                animator.SetBool("IDLE", false);
                animator.SetBool("JUMP", false);
                animator.SetBool("UP", false);
                animator.SetBool("RUN", true);
            }
            else
            {
                if (jumpCount == 1)
                {
                    animator.SetBool("RUN", false);
                    animator.SetBool("JUMP", false);
                    animator.SetBool("UP", false);
                    animator.SetBool("IDLE", true);
                }
            }
        }




    }

    public void OnCollisionEnter2D(Collision2D collision)   // 점프 카운트
    {
        if (collision.gameObject.tag == "Platform" && collision.contacts[0].normal.y > 0.7f)
        {

            animator.SetBool("RUN", false);
            animator.SetBool("JUMP", false);
            animator.SetBool("IDLE", true);

            StartCoroutine("jump_crt");

        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform" && rigidbody.velocity.y < -0.01f)
        {

            jumpCount = 0;
        }
    }

    IEnumerator jump_crt()
    {
        yield return new WaitForSeconds(0.01f);
        jumpCount = 1;
    }
}