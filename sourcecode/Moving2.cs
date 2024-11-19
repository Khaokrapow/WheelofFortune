using UnityEngine;
using UnityEngine.UIElements;

public class Moving2 : MonoBehaviour
{

    float speedY = 0.3f;
    float height = 0.1f;



    void Update()
    {
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speedY) * height + transform.position.y;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);


    }
}
