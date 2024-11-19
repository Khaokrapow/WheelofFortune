using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

using UnityEngine.Audio;

public class Move: MonoBehaviour {

    // transform
    public float hightY = 0f;
    public float hightX = 0f;
    public float hightZ = 0f;
    //rotate
    public float rotateY = 0f;
    public float rotateX = 0f;
    public float rotateZ = 0f;
    //speed
    public float speed = 0f;

    //type
   [Header("Type : Wall, Slow")]
    public string moveType;

    //vfx
    public GameObject useVFX;
    private GameObject inputVFX;
    public AudioClip sfxWhenCollision;
    public AudioMixerGroup OutputSound;
    private AudioSource audio;
    

    private void Start()
    {
       audio = gameObject.AddComponent<AudioSource>();
       audio.outputAudioMixerGroup = OutputSound;
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (hightX != 0)
            {
                //calculate what the new X position will be
                float newX = Mathf.Sin(Time.time * speed) * hightX + transform.position.x;
                //set the object's X to the new calculated X
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            if (hightY != 0)
            {
                //calculate what the new Y position will be
                float newY = Mathf.Sin(Time.time * speed) * hightY + transform.position.y;
                //set the object's Y to the new calculated Y
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            if (hightZ != 0)
            {
                //calculate what the new Z position will be
                float newZ = Mathf.Sin(Time.time * speed) * hightZ + transform.position.z;
                //set the object's Z to the new calculated Z
                transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
            }
            if (rotateY != 0)
            {
                transform.Rotate(0, speed * Time.deltaTime, 0);
            }
            if (rotateX != 0)
            {
                transform.Rotate(speed * Time.deltaTime, 0, 0);
            }
            if (rotateZ != 0)
            {
                transform.Rotate(0, 0, speed * Time.deltaTime);
            }


        }
        //time = 0 dont do anything
        else 
        { 
        }

        if (inputVFX == null) {

            //useVFX = transform.parent.gameObject.GetComponent<RandomObstacle>().createVFX();
            // create VFX
            inputVFX = Instantiate(useVFX, transform.position, useVFX.transform.rotation);
            inputVFX.transform.SetParent(transform);
            inputVFX.transform.localScale = new Vector3(3,3,3);
            inputVFX.SetActive(false);
        }

    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            // ดึง Collider ทั้งหมดใน GameObject และลูกหลาน
            Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

            // ปิดการใช้งานการชนของแต่ละ Collider
            foreach (Collider col in colliders)
            {
                //if (moveType == "Wall" && )
                //{
                col.isTrigger = true;
                StartCoroutine(CooldownTime(5));
                //}
                Debug.Log("bot can pass obstacle");
            }
            
        }
        if (other.gameObject.CompareTag("Player"))
        {
            inputVFX.SetActive(true);

            audio.clip = sfxWhenCollision;
            audio.volume += 3;
            audio.Play();
            if (moveType == "Slow")
            {
                StartCoroutine(FreezBody(15, other));
            }
        }
    }

    IEnumerator CooldownTime(int cooldownTime)
    {
        // รอเวลาตาม cooldown
        yield return new WaitForSeconds(cooldownTime);

        // ดึง Collider ทั้งหมดใน GameObject และลูกหลาน
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

        // ปิดการใช้งานการชนของแต่ละ Collider
        foreach (Collider col in colliders)
        {
            col.isTrigger = false;
            Debug.Log("open obstacle");
        }
    }

    IEnumerator FreezBody(int cooldownTime, Collision other)
    {
        other.body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        GameObject parent = GameObject.FindGameObjectWithTag("Player");
        parent.transform.position = new Vector3(parent.transform.position.x + 30, parent.transform.position.y, parent.transform.position.z + 30);
        yield return new WaitForSeconds(cooldownTime);
        other.body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
