using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacle : MonoBehaviour
{
    private int randomType;
    public GameObject itemParent;
    public GameObject obstacel;
    private GameObject obj;
    public GameObject vfx;
    private Vector3 scaleObject;
    private Transform scaleCollider;
    //scaleObstacle
    public float scaleX = 1f;
    public float scaleY = 1f;
    public float scaleZ = 1f;
    //Move
    // transform
    private float hightY = 0f;
    private float hightX = 0f;
    private float hightZ = 0f;
    //rotate
    private float rotateY = 0f;
    private float rotateX = 0f;
    private float rotateZ = 0f;
    //speed
    private float speed = 0f;

    //type
    enum ObstacelType { Wall, Slow }
    [SerializeField] private ObstacelType obstacleType;

    //sfx
    public AudioClip sfx;

    void Start()
    {
        createObstacle();
    }

    public void createObstacle()
    {
        randomType = Random.Range(0, 2);
        if (randomType == 0) {
            obstacleType = ObstacelType.Wall;
        }
        else {
            obstacleType = ObstacelType.Slow;
        }
        obj = Instantiate(obstacel, new Vector3(0,0,0), obstacel.transform.rotation);
        obj.transform.SetParent(itemParent.transform);

        // Random location x , z with 
        int randomNumberX = Random.Range(1, 10);
        int randomNumberZ = Random.Range(1, 10);
        //int randomNumberY = Random.Range(0, 11);
        //Debug.Log(randomNumberY);
        //position
        obj.transform.position = new Vector3(itemParent.transform.position.x + randomNumberX, itemParent.transform.position.y ,itemParent.transform.position.z + randomNumberZ);

        // Set scale
        scaleObject = new Vector3(scaleX, scaleY, scaleZ);
        obj.transform.localScale = scaleObject;


        obj.transform.localScale = scaleObject;
        obj.tag = "Obstacle";
        obj.gameObject.AddComponent<Move>();
        obj.gameObject.GetComponent<Move>().useVFX = createVFX();
        obj.gameObject.GetComponent<Move>().sfxWhenCollision = sfx;
        obj.gameObject.GetComponent<Move>().moveType = obstacleType.ToString();

        // transform
        hightY = Random.Range(0, 1)/10;
        hightX = Random.Range(0, 1)/10;
        hightZ = Random.Range(0, 1)/10;
        //rotate
        rotateY = Random.Range(0, 1);
        rotateX = Random.Range(0, 90);
        rotateZ = Random.Range(0, 1);
        //speed
        speed = Random.Range(10, 20);
        

        obj.gameObject.GetComponent<Move>().hightY = hightY;
        obj.gameObject.GetComponent<Move>().hightX = hightX;
        obj.gameObject.GetComponent<Move>().hightZ = hightZ;

        obj.gameObject.GetComponent<Move>().rotateY = rotateY;
        obj.gameObject.GetComponent<Move>().rotateX = rotateX;
        obj.gameObject.GetComponent<Move>().rotateZ = rotateZ;

        obj.gameObject.GetComponent<Move>().speed = speed;

    }

    public GameObject createVFX() {
        // create VFX
        GameObject vfxCreation = Instantiate(vfx, itemParent.transform.position, vfx.transform.rotation);
        vfxCreation.transform.SetParent(obj.transform);
        vfxCreation.transform.localScale = obj.transform.localScale; //
        vfxCreation.SetActive(false);
        return vfxCreation;
    }


}
