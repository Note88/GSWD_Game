using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed;
    public float moveSpeed;
    public float atkRange;
    public float fieldOfVision;
    public bool unbeatable;
    public Animator enemyAnimator;

    //�׽�Ʈ �뵵
    //public Sword_Man sword_man;
    public float height = 1.7f;

    // enemy�� ���� ����
    private void SetEnemyStatus(string _enemyName, int _maxHp, int _atkDmg, float _atkSpeed, float _moveSpeed, float _atkRange, float _fieldOfVision)
    {
        enemyName = _enemyName;
        maxHp = _maxHp;
        nowHp = _maxHp;
        atkDmg = _atkDmg;
        atkSpeed = _atkSpeed;
        moveSpeed = _moveSpeed;
        atkRange = _atkRange;
        fieldOfVision = _fieldOfVision;
    }

    // Start is called before the first frame update
    void Start()
    {
        //�׽�Ʈ �뵵
        //sword_man = GameObject.Find("sword_man").GetComponent<Sword_Man>();
        string name = gameObject.tag;
        // �������¿� ���� ����
        unbeatable = false;

        // ������ �̸��� ���� ���� ����
        switch (name)
        {
            case "Bat":
                SetEnemyStatus("Bat", 150, 10, 2f, 2, 1.5f, 7f);
                break;

            case "Eel":
                SetEnemyStatus("Eel", 150, 10, 2f, 2, 5f, 10f);
                break;

            case "EyeBall":
                SetEnemyStatus("EyeBall", 800, 10, 1.5f, 2, 5f, 10f);
                break;

            case "Tentacle":
                SetEnemyStatus("Tentacle", 100, 10, 1f, 0, 1f, 7f);
                break;

            case "Klackon":
                SetEnemyStatus("Klackon", 200, 10, 2f, 2, 1f, 7f);
                break;

            case "Lurker":
                SetEnemyStatus("Lurker", 200, 10, 2f, 2, 5f, 10f);
                break;

            case "Scorpion":
                SetEnemyStatus("Scorpion", 100, 10, 1f, 2, 1f, 7f);
                break;

            case "Slime":
                SetEnemyStatus("Slime", 500, 10, 1f, 2, 1f, 7f);
                break;

            case "Slimesmall":
                SetEnemyStatus("Slimesmall", 200, 10, 1f, 2, 1f, 7f);
                break;

            default:
                break;
        }

        SetAttackSpeed(atkSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    /*private void OnTriggerEnter2D(Collider2D col)
    {


        // �÷��̾��� ���ݿ� �¾��� ��쿡
        if (col.CompareTag("Player"))
        {
            if (sword_man.attacked)
            {
                // �÷��̾��� ���ݷ¸�ŭ �ǰ� ����
                // �������°� �ƴҶ���
                if (!unbeatable)
                {
                    nowHp -= sword_man.atkDmg;
                }
                sword_man.attacked = false;
                if (nowHp <= 0) // �� ���
                {
                    GetComponent<EnemyAI>().enabled = false;    // ���� ��Ȱ��ȭ
                    GetComponent<Collider2D>().enabled = false; // �浹ü ��Ȱ��ȭ
                    Destroy(GetComponent<Rigidbody2D>());       // �߷� ��Ȱ��ȭ
                    if(enemyName == "Tentacle")
                    {
                        Destroy(gameObject, 0.5f);               // 0.5���� ����
                        return;
                    }
                    Destroy(gameObject, 2);                     // 2���� ����
                }
            }
        }
    }*/

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="Bullet")
        {
            nowHp-=15;
            if (nowHp <= 0) // �� ���
            {
                GetComponent<EnemyAI>().enabled = false;    // ���� ��Ȱ��ȭ
                GetComponent<Collider2D>().enabled = false; // �浹ü ��Ȱ��ȭ
                Destroy(GetComponent<Rigidbody2D>());       // �߷� ��Ȱ��ȭ
                if (enemyName=="Slime")
                {
                    GameObject.Find("Split_item").transform.position=transform.position;
                }
                if (enemyName=="EyeBall")
                {
                    GameObject.Find("portal").transform.position=new Vector2(162.27f, -5.85f);
                }
                if (enemyName == "Tentacle" || enemyName=="Slime" || enemyName=="EyeBall")
                {
                    //Destroy(gameObject, 0.5f);               // 0.5���� ����
                    Destroy(gameObject,0.5f);
                    
                }
                else
                    Destroy(gameObject);                     // 2���� ����
            }
        }
    }

    // ���ݼӵ� ����
    void SetAttackSpeed(float speed)
    {
        enemyAnimator.SetFloat("attackSpeed", speed);
    }
}
