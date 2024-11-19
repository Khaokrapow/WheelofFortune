using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    //public GameObject[] Items;
    public ItemCreation itemDB;
    public int abilityItem;
    private int randomItem;
    public GameObject itemParent;
    private int spinSpeed = 30;
    private GameObject obj;
    private bool isFinish = false;
    private Vector3 scaleObject;
    private Vector3 scaleCollider;
    
    // VFX
    public GameObject debuffVFX;
    public GameObject buffVFX;
    public GameObject nothingVFX;
    private GameObject VFX;
    public GameObject areaVFX;
    private GameObject createAreaVFX;
    private float locationAreaVFX;

    void Start()
    {
        createItem();
    }

    private void Update()
    {
        if (obj != null)
        {
            obj.transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        }
        else {
            StartCoroutine(cooldownTimeForSpawnItem());
        }

        if (isFinish  && (obj == null)) { 
            createItem();
            isFinish = false;
        }
    }
    public void createItem()
    {
        randomItem = Random.Range(0, itemDB.itemCount());
        Item itemInstantiate = itemDB.getItem(randomItem);

        // obj = Instantiate(Items[randomItem] , new Vector3( 260,412,-507 ), Items[randomItem].transform.rotation);
        obj = Instantiate(itemDB.getItem(randomItem).getObjectItem(), new Vector3(260, 412, -507), itemDB.getItem(randomItem).getObjectItem().transform.rotation);
        obj.transform.SetParent(itemParent.transform);

        // Random location x , z with 
        int randomNumberX = Random.Range(1, 10);
        int randomNumberZ = Random.Range(1, 10);

        // Set Ability
        abilityItem = Random.Range(-3, 6);
        //abilityItem = 0;


        // Set ตุณสมบัติ item
        switch (obj.gameObject.name)
        {
            case "water(Clone)":
                //position
                obj.transform.position = new Vector3(itemParent.transform.position.x+randomNumberX , itemParent.transform.position.y , itemParent.transform.position.z+randomNumberZ); ;
                //scale
                scaleObject = new Vector3(0.004f, 0.004f, 0.004f);
                scaleCollider = new Vector3(300,300,300);
                locationAreaVFX = 0;
                break;

            case "rice(Clone)":
                //position
                obj.transform.position = new Vector3(itemParent.transform.position.x + randomNumberX, itemParent.transform.position.y + 1.9f, itemParent.transform.position.z + randomNumberZ);
                //scale
                scaleObject = new Vector3(0.2f, 0.2f, 0.2f);
                scaleCollider = new Vector3(10, 10, 10);
                locationAreaVFX = 1.9f;
                break;
            case "banana(Clone)":
                //position
                obj.transform.position = new Vector3(itemParent.transform.position.x + randomNumberX, itemParent.transform.position.y + 2.0f, itemParent.transform.position.z + randomNumberZ);
                //scale
                scaleObject = new Vector3(0.3f, 0.3f, 0.3f);
                scaleCollider = new Vector3(12, 12, 12);
                locationAreaVFX = 2.0f;
                break;
            case "garland(Clone)":
                //position
                obj.transform.position = new Vector3(itemParent.transform.position.x + randomNumberX, itemParent.transform.position.y + 1.9f, itemParent.transform.position.z + randomNumberZ);
                //scale
                scaleObject = new Vector3(3f, 3f, 3f);
                scaleCollider = new Vector3(0.5f,0.5f ,0.5f);
                locationAreaVFX = 1.9f;
                break;

        }

        // Add collider
        obj.gameObject.AddComponent<BoxCollider>();
        obj.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        obj.gameObject.GetComponent<BoxCollider>().size = scaleCollider;

        //obj.gameObject.GetComponent<Item>().setName(obj.gameObject.name);
        //obj.gameObject.GetComponent<Item>().setAbility(ability);
        
        //Debug.Log("Ability = " + obj.gameObject.GetComponent<Item>().getNameItem());
        //Debug.Log("Ability = " + obj.gameObject.GetComponent<Item>().getAbility());
        
        /*Light lightComp = obj.AddComponent<Light>();
        lightComp.range = 10;
        lightComp.color = Random.ColorHSV();
        lightComp.intensity = 5;*/

        obj.transform.localScale = scaleObject;
        obj.tag = "Item";

        // create VFX
        if (abilityItem > 0)
        {
           VFX = Instantiate(buffVFX, obj.transform.position, buffVFX.transform.rotation);
        }
        else if (abilityItem < 0)
        {
           VFX = Instantiate(debuffVFX, obj.transform.position, debuffVFX.transform.rotation);
        }
        else if (abilityItem == 0)
        {
            VFX = Instantiate(nothingVFX, obj.transform.position, nothingVFX.transform.rotation);
        }
        VFX.SetActive(false);
        VFX.transform.SetParent(obj.transform);

        // create magic area effect
        createAreaVFX = Instantiate(areaVFX,obj.transform.position, areaVFX.transform.rotation);
        createAreaVFX.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - locationAreaVFX, obj.transform.position.z);
        createAreaVFX.transform.SetParent(obj.transform);
        createAreaVFX.transform.localScale = scaleCollider;

    }

    IEnumerator cooldownTimeForSpawnItem()
    {
        //Debug.Log("wait a minute");
        //Debug.Log(cooldown);
        yield return new WaitForSeconds(30);
        isFinish = true;

    }

    public int getAbility() {
        return abilityItem;
    }
    public GameObject getVFX() {
        return VFX;
    }
}
