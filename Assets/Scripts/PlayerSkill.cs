using System.Collections;
using System.Collections.Generic;
using ca.HenrySoftware.Rage;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    public GameObject Split_obj; //분열 스킬 obj
    public AudioClip Split_sound;
    bool Split_able; //게임매니저에도 존재

    public GameObject blink_obj; //순간이동 이펙트
    public AudioClip blink_sound;
    bool blink_able;
    float max_blink_distance;

    public GameObject blink_icon, Split_icon;

    public bool haveSplit;

    // Start is called before the first frame update
    void Start()
    {
        Split_able=true;
        blink_able=true;
        max_blink_distance=2f;
        haveSplit=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Split_able && GameManager.instance.Split_able && Input.GetKeyDown(KeyCode.A) && haveSplit) //분열
        {
            StartCoroutine("Split_crt");
        }

        if (Input.GetKey(KeyCode.RightArrow)) //순간이동
        {
            if (blink_able && Input.GetKeyDown(KeyCode.Z))
            {
                blink_able=false;
                
                int layerMask = (1 << LayerMask.NameToLayer("Platform")) + (1 << LayerMask.NameToLayer("Wall"));
                RaycastHit2D hit;
                float distance=2f;
                if ((hit = Physics2D.Raycast(transform.position, new Vector2(1,0), max_blink_distance, layerMask))==true)
                {
                    distance=Mathf.Abs(hit.transform.position.x-transform.position.x)-hit.collider.gameObject.GetComponent<BoxCollider2D>().size.x/2;
                    
                }
                transform.Translate(transform.right*distance);
                blink_obj.transform.position = transform.position;
                blink_obj.SetActive(true);
                GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(blink_sound);
                StartCoroutine("blink_crt");


            }
        }

        if (Input.GetKey(KeyCode.LeftArrow)) //순간이동
        {
            if (blink_able && Input.GetKeyDown(KeyCode.Z))
            {
                blink_able = false;

                int layerMask = (1 << LayerMask.NameToLayer("Platform")) + (1 << LayerMask.NameToLayer("Wall"));
                RaycastHit2D hit;
                float distance = 2f;
                if ((hit = Physics2D.Raycast(transform.position, new Vector2(-1,0), max_blink_distance, layerMask)) == true)
                {
                    distance = Mathf.Abs(hit.transform.position.x - transform.position.x) - hit.collider.gameObject.GetComponent<BoxCollider2D>().size.x / 2;

                }
                transform.Translate(-transform.right * distance);
                blink_obj.transform.position = transform.position;
                blink_obj.SetActive(true);
                GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(blink_sound);
                StartCoroutine("blink_crt");


            }
        }



    }
    
    IEnumerator Split_crt()
    {
        Split_icon.GetComponent<Image>().fillAmount=0;
        if (transform.rotation.y > -0.1f && transform.rotation.y < 0.1f)
            Split_obj.gameObject.transform.position=new Vector2(transform.position.x-1f,transform.position.y);
        else
            Split_obj.gameObject.transform.position = new Vector2(transform.position.x+1f, transform.position.y);
        Split_obj.gameObject.transform.rotation=transform.rotation;
        Split_obj.gameObject.SetActive(true);
        GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(Split_sound);
        Split_able=false;
        
        for (int i=0; i<10; i++)
        {
            yield return new WaitForSeconds(1f);
            Split_icon.GetComponent<Image>().fillAmount += 1.0f / 30.0f;
        }
        Split_obj.gameObject.SetActive(false);
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(1f);
            Split_icon.GetComponent<Image>().fillAmount += 1.0f / 30.0f;
        }
        Split_icon.GetComponent<Image>().enabled=false;
        yield return new WaitForSeconds(0.25f);
        Split_icon.GetComponent<Image>().enabled = true;

        Split_able =true;
    }

    IEnumerator blink_crt()
    {
        blink_icon.GetComponent<Image>().fillAmount=0;
        yield return new WaitForSeconds(1f);
        blink_obj.SetActive(false);
        blink_icon.GetComponent<Image>().fillAmount += 0.2f;
        for (int i=0; i<4; i++)
        {

            yield return new WaitForSeconds(1f);
            blink_icon.GetComponent<Image>().fillAmount += 0.2f;

        }
        blink_icon.GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        blink_icon.GetComponent<Image>().enabled = true;
        blink_able =true;
    }
}
