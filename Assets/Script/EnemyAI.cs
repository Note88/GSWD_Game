using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    float attackDelay;
    float timer;
    float waitingTime;
    public int nextMove;

    // �������� �ٷ� ��� ���� ����
    bool check_face = true;

    GameObject obj;
    Enemy enemy;
    Animator enemyAnimator;
    SpriteRenderer spriteRenderer;
    Vector3 pos_obj;
    Vector3 pos_target;
    Vector3 scale_obj;

    // Eel�� ���� ����Ʈ
    public GameObject ElectPrefab;
    // Eyeball�� Lurker�� ���� ����Ʈ
    public GameObject LightPrefab;
    // ������ ������ ���� ����Ʈ
    public GameObject Physic_effectPrefab;

    void Start()
    {
        obj = gameObject;
        enemy = GetComponent<Enemy>();
        enemyAnimator = enemy.enemyAnimator;
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = 0f;
        // target = �÷��̾�
        //target = GameObject.Find("sword_man").GetComponent<Transform>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        Invoke("Think", 2);
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        float distance = Vector3.Distance(transform.position, target.position);

        if (attackDelay == 0 && distance <= enemy.fieldOfVision)
        {
            FaceTarget();

            //CancelInvoke();

            //check_idle = true;

            if (distance <= enemy.atkRange)
            {
                // Attack ���� ����
                AttackTarget();
            }
            else if (distance > enemy.atkRange)
            {
                if (!enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    // Walk ���� ����
                    MoveToTarget();
                }
            }
        }
        else if(distance > enemy.fieldOfVision && attackDelay == 0f)
        {
            enemyAnimator.SetBool("moving", false);
            Idle_Move();

        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        if(nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        float time = Random.Range(2f, 5f);

        Invoke("Think", time);
    }

    void Turn()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke("Think");
        Invoke("Think", 2);
    }

    void Idle_Move()
    {
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

        rigidbody.transform.Translate(new Vector2(nextMove, 0) * enemy.moveSpeed * Time.deltaTime, Space.World);

        Vector2 frontVec = new Vector2(obj.transform.position.x + nextMove * 0.1f, obj.transform.position.y);
        RaycastHit2D rayHit_down = Physics2D.Raycast(frontVec, Vector3.down, 1.6f, LayerMask.GetMask("Platform"));
        frontVec = new Vector2(obj.transform.position.x + nextMove * 0.1f, obj.transform.position.y + 0.1f);
        RaycastHit2D rayHit_left = Physics2D.Raycast(frontVec, Vector3.left, 1, LayerMask.GetMask("Platform"));
        RaycastHit2D rayHit_right = Physics2D.Raycast(frontVec, Vector3.right, 1, LayerMask.GetMask("Platform"));
        Debug.DrawRay(frontVec, Vector3.right, new Color(0, 1, 0));
        // ���� �����������
        if (rayHit_down == false)
        {
            Turn();
        }
        // �տ� ���� �ִٸ�
        if(rayHit_left == true || rayHit_right == true)
        {
            Turn();
        }
    }

    // �þ� ���� �ȿ� ������ �÷��̾ ���� �̵�
    void MoveToTarget()
    {
        float dir = target.position.x - obj.transform.position.x;
        RaycastHit2D rayHit_side;
        Vector2 frontVec;
        if ( dir < 0)
        {
            dir = -1;
            frontVec = new Vector2(obj.transform.position.x + dir * 0.1f, obj.transform.position.y + 0.1f);
            rayHit_side = Physics2D.Raycast(frontVec, Vector3.left, 1, LayerMask.GetMask("Platform"));
        }
        else
        {
            dir = 1;
            frontVec = new Vector2(obj.transform.position.x + dir * 0.1f, obj.transform.position.y + 0.1f);
            rayHit_side = Physics2D.Raycast(frontVec, Vector3.right, 1, LayerMask.GetMask("Platform"));
        }

        frontVec = new Vector2(obj.transform.position.x + dir * 0.1f, obj.transform.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1.6f, LayerMask.GetMask("Platform"));

        if (rayHit.collider == true && rayHit_side == false) // ���� ���������� �ƴϸ� ���� ���ٸ�
        {
            obj.transform.Translate(new Vector2(dir, 0) * enemy.moveSpeed * Time.deltaTime, 0);
            // moving�� true�� �����Ͽ� Walk���·� ����
            enemyAnimator.SetBool("moving", true);
        }
        else // ���� ���������� Idle ���·� ���ư�
        {
            Idle_Move();
        }
    }

    // �þ� ���� �ȿ� ������ �÷��̾ ���� ���� ���� ��ȯ
    void FaceTarget()
    {
        scale_obj = obj.transform.localScale;
        if (target.position.x - obj.transform.position.x < 0) // Ÿ���� ���ʿ� ���� ��
        {
            check_face = false;
            spriteRenderer.flipX = check_face;
        }
        else // Ÿ���� �����ʿ� ���� ��
        {
            check_face = true;
            spriteRenderer.flipX = check_face;
        }
    }

    // ���� ���� �ȿ� ������ �÷��̾ ���� ��
    void AttackTarget()
    {
        // ���Ϳ� ���� ���ݻ���
        // �⺻������ ���ݹ��� �ȿ� ���� ������ �����ϸ�
        // �÷��̾��� �ǰ� ���̵��� �����Ǿ� ����
        // ������ target.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;�� �����
        // �浹 �ÿ� �ǰ� ���̵��� �ڵ带 �߰��ϸ� �ɵ���
        switch (enemy.enemyName)
        {
            case "Bat":
                waitingTime = 0.3f;

                if (timer == 0f)
                {
                    //target.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;

                    enemyAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
                    effectGenerate();
                    pos_obj = obj.transform.position;
                    pos_target = target.position;
                    // �÷��̾� ������ �̵��ߴٰ�
                    if(pos_target.x < pos_obj.x)
                    {
                        obj.transform.position = new Vector3(pos_target.x + 0.3f, pos_obj.y, 0);

                    }
                    else
                    {
                        obj.transform.position = new Vector3(pos_target.x - 0.3f, pos_obj.y, 0);

                    }
                }
                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    // ���� ��ġ�� �ǵ��ƿ�
                    obj.transform.position = pos_obj;
                    timer = 0f;
                    attackDelay = enemy.atkSpeed; // ������ ����
                }
                break;

            case "Eel":
                enemyAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
                effectGenerate();
                attackDelay = enemy.atkSpeed; // ������ ����
                break;

            case "EyeBall":
                enemyAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
                effectGenerate(); 
                attackDelay = enemy.atkSpeed; // ������ ����
                break;

            case "Tentacle":
                //target.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;
                enemyAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
                effectGenerate();
                attackDelay = enemy.atkSpeed; // ������ ����
                break;

            case "Klackon":
                //target.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;
                enemyAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
                effectGenerate();
                attackDelay = enemy.atkSpeed; // ������ ����
                break;

            case "Lurker":
                enemyAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
                effectGenerate();
                attackDelay = enemy.atkSpeed; // ������ ����
                break;

            case "Scorpion":
                //target.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;
                enemyAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
                effectGenerate();
                attackDelay = enemy.atkSpeed; // ������ ����
                break;

            case "Slime":
                waitingTime = 0.1f;

                if(timer == 0f)
                {
                    // �÷��̾��� y��ǥ ���� �̵��ߴٰ� �߷¿� ���� ������
                    pos_target = target.position;
                    pos_obj = new Vector3(pos_target.x, pos_target.y + 3f, 0);
                    obj.transform.position = pos_obj;
                }
                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    timer = 0f;
                }
                //target.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;
                enemyAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
                effectGenerate();
                attackDelay = enemy.atkSpeed; // ������ ����
                break;

            case "Slimesmall":
                //target.GetComponent<Sword_Man>().nowHp -= enemy.atkDmg;
                enemyAnimator.SetTrigger("attack"); // ���� �ִϸ��̼� ����
                effectGenerate();
                attackDelay = enemy.atkSpeed; // ������ ����
                break;

            default:
                break;
        }
    }

    // ����Ʈ �����Լ�
    void effectGenerate()
    {
        pos_obj = obj.transform.position;
        if(enemy.enemyName == "Eel")
        {
            // elect ���Ÿ� ���� ����Ʈ ����
            GameObject elect = Instantiate(ElectPrefab);
            if(target.position.x - obj.transform.position.x < 0) // Ÿ���� ���ʿ� ���� ��
            {
                elect.transform.position = new Vector3(pos_obj.x - 1f, pos_obj.y, 0);
            }
            else // Ÿ���� �����ʿ� ���� ��
            {
                elect.transform.position = new Vector3(pos_obj.x + 1f, pos_obj.y, 0);
            }
        }
        else if(enemy.enemyName == "Lurker" || enemy.enemyName == "EyeBall")
        {
            // light ���Ÿ� ���� ����Ʈ ����
            GameObject light = Instantiate(LightPrefab);
            if (target.position.x - obj.transform.position.x < 0) // Ÿ���� ���ʿ� ���� ��
            {
                light.transform.position = new Vector3(pos_obj.x - 1f, pos_obj.y, 0);
            }
            else // Ÿ���� �����ʿ� ���� ��
            {
                light.transform.position = new Vector3(pos_obj.x + 1f, pos_obj.y, 0);
            }
        }
        else
        {
            // ���� ����Ʈ ����
            GameObject physic_effect = Instantiate(Physic_effectPrefab);
            if(enemy.enemyName == "Bat")
            {
                physic_effect.transform.position = new Vector3(pos_obj.x, pos_obj.y - 0.5f, 0);
            }
            else
            {
                if (target.position.x - obj.transform.position.x < 0) // Ÿ���� ���ʿ� ���� ��
                {
                    physic_effect.transform.position = new Vector3(pos_obj.x + 0.5f, pos_obj.y - 0.5f, 0);
                }
                else // Ÿ���� �����ʿ� ���� ��
                {
                    physic_effect.transform.position = new Vector3(pos_obj.x - 0.5f, pos_obj.y - 0.5f, 0);
                }
            }
        }
    }
}
