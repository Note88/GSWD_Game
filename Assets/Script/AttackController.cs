using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    GameObject player;
    float dir;
    float moveSpeed = 4.0f;

    void Start()
    {
        player = GameObject.Find("Player");
        Vector3 player_pos = player.transform.position;
        Vector3 pos = transform.position;

        if(gameObject.tag == "light")
        {
            gameObject.transform.Rotate(new Vector3(0, 0, -90));
        }

        if (player_pos.x - pos.x < 0) // Ÿ���� ���ʿ� ���� ��
        {
            transform.localScale = new Vector3(-0.15f, 0.1f, 1);
        }
        else // Ÿ���� �����ʿ� ���� ��
        {
            transform.localScale = new Vector3(0.15f, 0.1f, 1);
        }
        dir = player_pos.x - pos.x;
        dir = (dir< 0) ? -1 : 1;
    }

    void Update()
    {
        transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // �浹�� �÷��̾��� ü���� ����
            if (player.GetComponent<PlayerHit>().neverDie==false)
            {
                player.GetComponent<PlayerHit>().StartCoroutine(player.GetComponent<PlayerHit>().Hit(10));
            }
            
            Destroy(gameObject);
        }

        if (col.gameObject.tag=="Platform")
        {
            gameObject.SetActive(false);
        }
    }

    void OnBecameInvisible() //ȭ������� ���� ������ �ʰ� �Ǹ� ȣ���� �ȴ�.
    {
        Destroy(this.gameObject); //��ü�� �����Ѵ�.
    }
}