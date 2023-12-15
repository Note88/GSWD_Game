using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGenerator : MonoBehaviour
{
    public GameObject TentaclePrefab;
    GameObject Eyeball;
    float timer;
    float waitingTime;
    float to_x;
    float to_y;
    float go_x;

    // Start is called before the first frame update
    void Start()
    {
        Eyeball = GameObject.FindWithTag("EyeBall");
        timer = 0f;
        waitingTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.timer == 0f)
        {
            this.timer += Time.deltaTime;
            to_x = Eyeball.transform.position.x;
            to_y = Eyeball.transform.position.y;
            for (int i = 0; i < 4; i++)
            {
                GameObject go = Instantiate(TentaclePrefab);
                go_x = Random.Range(-5f, 5f);
                if(go_x > -1f && go_x <= 0)
                {
                    go_x = -1f;
                }
                else if(go_x > 0 && go_x < 1f)
                {
                    go_x = 1f;
                }
                go.transform.position = new Vector3(to_x + go_x, to_y, 0);
            }
            // Eyeball�� ���� ���·� ����
            Eyeball.GetComponent<Enemy>().unbeatable = true;
        }

        // ���� �ʿ� Tentacle�� ������ Eyeball�� ���� ���¸� �����ϰ�
        // 3�ʵڿ� �ٽ� Tentacle ����
        if(GameObject.FindWithTag("Tentacle") == null)
        {
            waitingTime += Time.deltaTime;
            if(waitingTime > 3f)
            {
                timer = 0f;
            }
            Eyeball.GetComponent<Enemy>().unbeatable = false;
        }
        
        if(Eyeball.GetComponent<Enemy>().nowHp <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
