using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    GameObject player;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        player = GameObject.Find("Player");
        Vector3 player_pos = player.transform.position;
        Vector3 pos = transform.position;

        if (player_pos.x - pos.x < 0) // Ÿ���� ���ʿ� ���� ��
        {
            transform.localScale = new Vector3(1f, 1f, 1);
        }
        else // Ÿ���� �����ʿ� ���� ��
        {
            transform.localScale = new Vector3(-1f, 1f, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0.3f)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }
}
