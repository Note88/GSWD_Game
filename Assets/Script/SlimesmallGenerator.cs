using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimesmallGenerator : MonoBehaviour
{
    public GameObject obj;
    public GameObject SlimesmallPrefab;
    Enemy Slime;

    float timer = 0f;
    float waittime = 0f;

    float to_x;
    float to_y;
    float go_x;

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.FindWithTag("Slime");
        Slime = obj.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Slime.nowHp <= 0 && timer == 0f)
        {
            waittime += Time.deltaTime;
            // 슬라임이 죽고 바로 생성하지 않고 일정 시간 후에 생성
            if (waittime >= 0.5f)
            {
                this.timer += Time.deltaTime;
                to_x = obj.transform.position.x;
                to_y = obj.transform.position.y;
                for (int i = 0; i < 2; i++)
                {
                    GameObject go = Instantiate(SlimesmallPrefab);
                    if (i == 0)
                    {
                        go_x = -1f;
                    }
                    else
                    {
                        go_x = 1f;
                    }
                    go.transform.position = new Vector3(to_x + go_x, to_y, 0);
                }
            }
        }
    }
}
