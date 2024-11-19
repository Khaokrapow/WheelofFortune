using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    public int code;
    public float cooldown =15f;
    public Vector3 positionToAddOffset;
    private GameObject parent;
    public AudioClip soundWhenWarp;
    private AudioSource audioT;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            audioT = gameObject.GetComponent<AudioSource>();
            audioT.clip = soundWhenWarp;
            audioT.volume += 3;
            audioT.Play();
            StartCoroutine(CooldownTime(other));
           
        }
    }

    IEnumerator CooldownTime(Collider other) {

        //Debug.Log("wait");
        yield return new WaitForSeconds(cooldown);
        foreach (TeleportArea tp in FindObjectsOfType<TeleportArea>())
        {
            if (tp.code == code && tp != this)
            { 
                Vector3 position = tp.gameObject.transform.position;

                position = new Vector3(position.x + positionToAddOffset.x,
                                     position.y + positionToAddOffset.y,
                                     position.z + positionToAddOffset.z);

                parent = GameObject.FindGameObjectWithTag("Player");
                //Debug.Log(parent.name);
                //teleport
                parent.gameObject.transform.position = position;
                // fix cooldown non use

            }
        }
        yield return new WaitForSeconds(cooldown);
    }

  

     

}
