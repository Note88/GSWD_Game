using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=transform.parent.gameObject.transform.position;
        transform.rotation=transform.parent.gameObject.transform.rotation;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag=="Platform" || col.gameObject.tag=="Wall")
            GameManager.instance.Split_able=false;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "Wall")
            GameManager.instance.Split_able = true;
    }
}
