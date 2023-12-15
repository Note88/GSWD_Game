using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword_Man : MonoBehaviour
{
    Animator animator;
    float speed = 5;

    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed = 1;
    public bool attacked = false;

    void AttackTrue()
    {
        attacked = true;
    }
    
    void AttackFalse()
    {
        attacked = false;
    }

    void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        atkSpeed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHp = 50;
        nowHp = 50;
        atkDmg = 10;

        animator = GetComponent<Animator>();
        SetAttackSpeed(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(-0.8f, 0.8f, 2);
            animator.SetBool("moving", true);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 2);
            animator.SetBool("moving", true);
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("moving", true);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else animator.SetBool("moving", false);

        if(Input.GetKeyDown(KeyCode.A) && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("attack");
        }
    }
}
