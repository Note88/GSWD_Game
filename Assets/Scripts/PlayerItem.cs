using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerItem : MonoBehaviour
{
    public int heal_count, buff_count;
    public TMP_Text heal_count_ui, buff_count_ui;

    public AudioClip heal_sound, buff_sound, pickup_sound;
    bool buff_ing;

    public GameObject heal_effect;

    public Material buff_mt;

    public GameObject Split_icon1, Split_icon2;

    // Start is called before the first frame update
    void Start()
    {
        heal_count=0;
        buff_count=0;
        buff_ing=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && heal_count>0)
        {
            use_heal();
        }

        if (Input.GetKeyDown(KeyCode.D) && buff_count>0 && !buff_ing)
        {
            StartCoroutine("use_buff");
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("Heal_potion"))
        {
            col.gameObject.SetActive(false);
            heal_count++;
            heal_count_ui.text=heal_count.ToString();
            GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(pickup_sound);
        }

        if (col.gameObject.name.Contains("Buff_potion"))
        {
            col.gameObject.SetActive(false);
            buff_count++;
            buff_count_ui.text = buff_count.ToString();
            GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(pickup_sound);
        }

        if (col.gameObject.name=="Split_item")
        {
            col.gameObject.SetActive(false);
            GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(pickup_sound);
            GetComponent<PlayerSkill>().haveSplit=true;
            Split_icon1.SetActive(true);
            Split_icon2.SetActive(true);
        }

    }

    public void use_heal()
    {
        heal_count--;
        heal_count_ui.text = heal_count.ToString();
        StartCoroutine("heal_crt");
        GetComponent<PlayerHit>().hp+=30;
        if (GetComponent<PlayerHit>().hp>100)
            GetComponent<PlayerHit>().hp=100;
        GetComponent<PlayerHit>().hp_bar.fillAmount=(float)GetComponent<PlayerHit>().hp /(float)100;
        GetComponent<PlayerHit>().hp_text.text= GetComponent<PlayerHit>().hp + " / 100";
        GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(heal_sound);
        
        
    }

    public IEnumerator use_buff()
    {
        buff_ing=true;
        buff_count--;
        buff_count_ui.text = buff_count.ToString();
        Material tmp=GetComponent<SpriteRenderer>().material;
        GetComponent<SpriteRenderer>().material=buff_mt;
        GameObject.Find("SF_Manager").GetComponent<AudioSource>().PlayOneShot(buff_sound);
        GetComponent<PlayerControl>().shootDelay=0.12f;
        GetComponent<PlayerControl>().speed = 5;
        yield return new WaitForSeconds(10f);
        GetComponent<PlayerControl>().shootDelay = 0.15f;
        GetComponent<PlayerControl>().speed = 4;
        GetComponent<SpriteRenderer>().material = tmp;
        buff_ing =false;
    }

    IEnumerator heal_crt()
    {
        heal_effect.SetActive(true);
        yield return new WaitForSeconds(1f);
        heal_effect.SetActive(false);
    }
}
