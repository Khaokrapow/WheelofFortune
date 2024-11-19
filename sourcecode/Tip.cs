using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Tip : MonoBehaviour
{
    public TMP_Text tipText; // Drag your Text component here
    public List<string> tips = new List<string>(); // List of tips
    private float tipChangeInterval = 5f; // Interval in seconds
    private Coroutine tipCoroutine;

    void Start()
    {
        // Add some sample tips if none exist
        if (tips.Count == 0)
        {
            tips.Add(" ของไหว้แม่ย่านางแม้จะมีโอกาสสุ่มโดนของลดเวลา\r\n เพิ่มเวลา หรือ ไม่เกิดอะไรขึ้นเลย");
            tips.Add(" สภาพอากาศอาจจะบดบังทัศนวิสัยขั้นรุนแรงได้\r\n จึงควรดูแผนที่เล็กที่อยู่ด้านขวาบนของหน้าจอประกอบการเล่น");
            tips.Add("กรณีที่รถคุณพลิกคว่ำ คุณสามารถวางรถใหม่ได้ แต่จะต้องเริ่มขับใหม่อีกครั้งที่จุดเริ่มต้น");
            tips.Add("ในด่านวังบาดาลจะมีแผ่นเพิ่มความเร็วที่สามารถทะลุขีดจำกัดความเร็วสูงสุดของรถคุณได้");
            tips.Add("เสาชิงช้าสีแดงที่คุณจะพบในด่านนครกับป่า\r\nสามารถเคลื่อนย้ายคุณไปยังตำแหน่งของเสาชิงช้าอื่นๆได้\r\n แต่ควรระวังเพราะการเปลี่ยนตำแหน่งอาจจะทำให้คุณเสียสมดุลในการขับรถได้");
            tips.Add("ในด่านแดนพิพากษา ระหว่างที่คุณต้องตอบคำถามจากเวตาลสีม่วง\r\nคุณจะมองไม่เห็นเวลาที่กำลังลดลงเลย");
            tips.Add("จำไว้ให้ดี คุณต้องผ่านจุดเช็คพอยท์ ก่อนเข้าเส้นชัย\r\n ซึ่งจะอยู่ประมาณครึ่งทางของแต่ละด่าน");
            tips.Add("ในด่านป่าวงกตให้สังเกตหลอดสีฟ้าเพื่อเช็คว่าคุณมาถูกทางหรือเปล่า");
        }

        // Start the tip changing routine
        tipCoroutine = StartCoroutine(ChangeTipRoutine());
    }

    void Update()
    {
        // Detect click and change tip immediately
        if (Input.GetMouseButtonDown(0)) // Left mouse button or screen tap on mobile
        {
            ChangeTip();
        }
    }

    IEnumerator ChangeTipRoutine()
    {
        while (true)
        {
            ChangeTip();
            // Wait for the specified interval
            yield return new WaitForSeconds(tipChangeInterval);
        }
    }

    void ChangeTip()
    {
        tipText.text = GetRandomTip();
    }

    string GetRandomTip()
    {
        int randomIndex = Random.Range(0, tips.Count); // Get a random index
        return tips[randomIndex];
    }

    private void OnDestroy()
    {
        // Stop the coroutine when this object is destroyed to prevent errors
        if (tipCoroutine != null)
        {
            StopCoroutine(tipCoroutine);
        }
    }
}
