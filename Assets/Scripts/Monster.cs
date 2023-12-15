using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    
    Material tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("check");
        if (col.gameObject.tag=="Bullet")
        {
            hit();
            StartCoroutine("hit_crt");
        }
    }

    IEnumerator hit_crt()
    {
        
        GetComponent<SpriteRenderer>().material=GameManager.instance.red_mt;
        yield return new WaitForSeconds(0.02f);
        GetComponent<SpriteRenderer>().material = tmp;
    }

    public void hit()
    {
        GameObject.Find("SF_Manager2").GetComponent<AudioSource>().PlayOneShot(GameManager.instance.m_hitsound);
    }
}
