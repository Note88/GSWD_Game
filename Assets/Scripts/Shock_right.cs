using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock_right : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("d");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(13 * Time.deltaTime, 0), Space.World);
    }

    IEnumerator d()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
