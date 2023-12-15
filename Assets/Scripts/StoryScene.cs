using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("NextScene_crt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator NextScene_crt()
    {
        yield return new WaitForSeconds(29f);
        SceneManager.LoadScene("Stage");
    }
}
