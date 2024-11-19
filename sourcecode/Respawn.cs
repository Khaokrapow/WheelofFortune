using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform startPosition; // จุดเริ่มต้นที่ต้องการกำหนด
    public GameObject playerCar; // รถยนต์ของผู้เล่น

    // ฟังก์ชันนี้จะถูกเรียกเมื่อกดปุ่มเพื่อย้ายรถยนต์ไปยังจุดเริ่มต้น
    public void RespawnButton()
    {
        if (playerCar != null && startPosition != null)
        {
            // กำหนดตำแหน่งใหม่ให้กับรถยนต์ตามตำแหน่งของ startPosition
            playerCar.transform.position = startPosition.position;

            // หากต้องการ reset การหมุนของรถยนต์ด้วย
            playerCar.transform.rotation = startPosition.rotation;
        }
    }
}