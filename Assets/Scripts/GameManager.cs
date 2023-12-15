using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject bullet;
    public GameObject[] bullets;   
    public int bulleti; 

    Vector2 obj_pool_pos=new Vector2(-100,0);

    public Material red_mt;
    
    public AudioClip m_hitsound;

    public bool Split_able; //분열 스킬을 사용할 수 있는지

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Split_able=true;
        bullets =new GameObject[80];
        bulleti=0;
        for (int i=0; i<80; i++)
        {
            bullets[i]=Instantiate(bullet, obj_pool_pos, Quaternion.identity);
            bullets[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator end_crt()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("End");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
