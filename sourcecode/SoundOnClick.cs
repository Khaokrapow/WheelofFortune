using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnClick : MonoBehaviour
{
    public AudioClip clickSound; // คลิปเสียงที่จะเล่น
    public AudioSource audioSource; // ตัว AudioSource ที่จะใช้เล่นเสียง

    
    // ฟังก์ชันนี้จะถูกเรียกเมื่อมีการคลิก
    public void PlaySound()
    {
        if (clickSound != null && audioSource != null)
        {
            // ตั้งค่า AudioClip สำหรับ AudioSource
            audioSource.clip = clickSound;

            // เล่นเสียง
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource หรือ AudioClip ยังไม่ได้ตั้งค่า!");
        }
    }
}
