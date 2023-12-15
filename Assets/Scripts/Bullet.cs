using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    float tmp;
    // Start is called before the first frame update

    void OnEnable()
    {
        GetComponent<Collider2D>().enabled = true;
        speed=20f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(1,0)*Time.deltaTime*speed);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag=="Platform" || collider.gameObject.tag == "Monster" || collider.gameObject.tag == "Wall")
        {
            tmp=speed;
            speed=0;
            GetComponent<Animator>().SetTrigger("DESTROY");
            GetComponent<Collider2D>().enabled=false;
            StartCoroutine("destroy_crt");
        }
    }

    IEnumerator destroy_crt()
    {
        yield return new WaitForSeconds(0.5f);
        speed = tmp;
        gameObject.SetActive(false);
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }


}
