using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHit : MonoBehaviour
{
    public int hp=100;
    public bool neverDie;

    public AudioClip hit_sound, die_sound;
    SpriteRenderer spriteRenderer;

    public TMP_Text hp_text;
    public Image hp_bar;

    public GameObject Gameover_text;
    bool isGameover;

    public bool hit_ing;

    public GameObject BGM_Manager, Boss_healthbar, Boss_icon, Boss;
    // Start is called before the first frame update
    void Start()
    {
        neverDie=false;
        spriteRenderer=GetComponent<SpriteRenderer>();
        isGameover=false;
        hit_ing=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameover)
        {
            SceneManager.LoadScene("Stage");
        }

        if (hit_ing && !neverDie && !isGameover)
        {
            StartCoroutine(Hit(8));
        }
    }

    public IEnumerator Hit(int damage)
    {
        neverDie = true;
        if (hp>0)
        {

        hp-=damage;
        hp_bar.fillAmount-= (float)((float)damage/(float)100);
        
        hp_text.text=hp + " / 100";
        
        GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(hit_sound);
        }

        //neverDie = true;

        if (hp<=0 && !isGameover)
        {
            StartCoroutine("GameOver");
            
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
        neverDie=false;
        


    }

    IEnumerator GameOver()
    {
        
        GetComponent<PlayerControl>().enabled=false;
        GetComponent<PlayerItem>().enabled=false;
        GetComponent<PlayerSkill>().enabled=false;

        GetComponent<Animator>().SetBool("RUN", false);
        GetComponent<Animator>().SetBool("JUMP", false);
        GetComponent<Animator>().SetBool("UP", false);
        GetComponent<Animator>().SetBool("IDLE", false);
        GetComponent<Animator>().SetBool("DIE",true);

        
        GameObject.Find("SF_Manager2").GetComponent<AudioSource>().PlayOneShot(die_sound);


        yield return new WaitForSeconds(1f);
        isGameover=true;
        Gameover_text.SetActive(true);

    }

    /*public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag=="Monster")
        {
            if (neverDie==false)
            {
                StartCoroutine(Hit(5));
            }
        }
    }*/

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            hit_ing=true;
        }

        if (col.gameObject.name=="portal")
        {
            GameObject.Find("BGM_Manager").SetActive(false);
            GameObject.Find("ruinBG").SetActive(false);
            GameObject.Find("CM vcam1").SetActive(false);
            GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = 8;
            GameObject.Find("Main Camera").transform.position=new Vector3(200,-48,-10);
            Boss.SetActive(true);
            Boss_healthbar.SetActive(true);
            Boss_icon.SetActive(true);
            transform.position=new Vector2(190.55f,-53.12f);
            StartCoroutine("BGM_crt");
            //BGM_Manager.SetActive(true);

        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            hit_ing = false;
        }
    }

    IEnumerator BGM_crt()
    {
        yield return new WaitForSeconds(1f);
        BGM_Manager.SetActive(true);
    }
}
