using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TriggerItem : MonoBehaviour
{

    //private int speed;
    private int timeItem;
    private Rigidbody body;
    public Player player;
    public Image stopImage;
    //public Image stopBackground;
    public TMP_Text timeADDSUB;
    public TMP_Text timeADDSUBWithItem;
    public TMP_Text skillActiveText;
    private GameObject Vfx;
    public AudioClip soundDebuffEffect;
    public AudioClip soundBuffEffect;
    public AudioClip soundNothingEffect;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        stopImage.enabled = false;
        //stopBackground.enabled = false;
        timeADDSUB.enabled = false;
        timeADDSUBWithItem.enabled = false;
        skillActiveText.enabled = false;
  
    }

    public void OnTriggerEnter (Collider collider)
    {
        if (collider.CompareTag("Item")) 
        {
            timeItem = collider.gameObject.GetComponentInParent<RandomItem>().getAbility();
            
            //show value time 
            timeADDSUB.enabled = true;
            int showTime = 0;
            showTime = timeItem * 10;
            //show VFX and SFX
            Vfx = collider.gameObject.GetComponentInParent<RandomItem>().getVFX();
            GameObject parent = GameObject.Find("RealPlayer");
            Vfx.transform.SetParent(parent.transform);
            Vfx.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);
            AudioSource audio = GetComponent<AudioSource>();

            // Buff Item
            if (timeItem > 0)
            {
                if (player.percentAddOrSub() && player.checkSkill() == EnumAbilityCode.ADD_TIME)
                {
                    timeItem += 1;
                    skillActiveText.enabled = true;
                    timeADDSUBWithItem.enabled = true;

                    // set VFX of Skill
                    GameObject skilVFX = player.createVFX();
                    skilVFX.SetActive(true);
                    skilVFX.transform.SetParent(parent.transform);
                    skilVFX.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);
                    skilVFX.transform.localScale = new Vector3(5, 5, 5);

                    skillActiveText.text = "Skill Active!";
                    timeADDSUBWithItem.text = "+ 10";
                    skillActiveText.color = Color.yellow;
                    timeADDSUBWithItem.color = Color.yellow;

                    // sound VFX
                    //audio.clip = ;
                    showTime = timeItem * 10;

                }


                timeADDSUB.text = "+ " + (showTime).ToString() + "s";
                timeADDSUB.color = Color.green;

                //sound
                audio.clip = soundBuffEffect;

                int count = 0;
               
                if (true)
                {
                    while (timeItem > count)
                    {
                        Timer.AddTime();
                        count++;
                    }
                }
                //speed
                StartCoroutine(FreezTime(3));
            }
            
            // Debuff Item
            else if (timeItem < 0)
            {
                if (player.percentAddOrSub() && player.checkSkill() == EnumAbilityCode.SUB_TIME)
                {
                    timeItem += 1;
                    skillActiveText.enabled = true;
                    timeADDSUBWithItem.enabled = true;

                    // set VFX of Skill
                    GameObject skilVFX = player.createVFX();
                    skilVFX.SetActive(true);
                    skilVFX.transform.SetParent(parent.transform);
                    skilVFX.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);
                    skilVFX.transform.localScale = new Vector3(5, 5, 5);

                    skillActiveText.text = "Skill Active!";
                    timeADDSUBWithItem.text = "+ 10";
                    skillActiveText.color = Color.yellow;
                    timeADDSUBWithItem.color = Color.yellow;
                    showTime = timeItem * 10;
                }

                timeADDSUB.text = (showTime).ToString() + "s";
                timeADDSUB.color = Color.red;

                //sound
                audio.clip = soundDebuffEffect;
                audio.volume += 10;

                int count = 0;
                
                while (timeItem < count)
                {
                    Timer.SubtractTime();
                    count--;
                }
                StartCoroutine(showTextWhenAddTime(3));
            }
            else if (timeItem == 0)
            { 
                timeADDSUB.enabled = true;
                timeADDSUB.text = "0";
                timeADDSUB.color = Color.white;
                //sound
                audio.clip = soundNothingEffect;
                audio.volume += 10;
                StartCoroutine(showTextWhenAddTime(3));
            }
            Vfx.SetActive(true);
            audio.Play();
            Destroy(collider.gameObject);
            
        }
       
    }

    IEnumerator FreezTime(int cooldown)
    {
        body.constraints = RigidbodyConstraints.FreezePosition;
        stopImage.enabled = true;
        //stopBackground.enabled = true;
      
        yield return new WaitForSeconds(cooldown);
        body.constraints = RigidbodyConstraints.None;
        stopImage.enabled = false;
        //stopBackground.enabled = false;
        timeADDSUB.enabled = false;
        skillActiveText.enabled = false;
        timeADDSUBWithItem.enabled = false;
    }

    IEnumerator showTextWhenAddTime(int cooldown)
    {
    
        yield return new WaitForSeconds(cooldown);
        timeADDSUB.enabled = false;
        skillActiveText.enabled = false;
        timeADDSUBWithItem.enabled = false;
    }


}
