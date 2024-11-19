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
            tips.Add(" �ͧ���������ҹҧ�������͡������ⴹ�ͧŴ����\r\n �������� ���� ����Դ���â�����");
            tips.Add(" ��Ҿ�ҡ���Ҩ�к��ѧ��ȹ����¢���ع�ç��\r\n �֧��ô�Ἱ�����硷�������ҹ��Һ��ͧ˹�Ҩͻ�Сͺ������");
            tips.Add("�óշ��ö�س��ԡ���� �س����ö�ҧö������ ��е�ͧ������Ѻ�����ա���駷��ش�������");
            tips.Add("㹴�ҹ�ѧ�Ҵ�Ũ����������������Ƿ������ö���آմ�ӡѴ���������٧�ش�ͧö�س��");
            tips.Add("��Ҫԧ�����ᴧ���س�о�㹴�ҹ��áѺ���\r\n����ö����͹���¤س��ѧ���˹觢ͧ��Ҫԧ���������\r\n �������ѧ���С������¹���˹��Ҩ�з����س���������㹡�âѺö��");
            tips.Add("㹴�ҹᴹ�Ծҡ�� �����ҧ���س��ͧ�ͺ�Ӷ���ҡ�ǵ������ǧ\r\n�س���ͧ���������ҷ����ѧŴŧ���");
            tips.Add("��������� �س��ͧ��ҹ�ش�社�·� ��͹�����鹪��\r\n ��觨��������ҳ���觷ҧ�ͧ���д�ҹ");
            tips.Add("㹴�ҹ���ǧ������ѧࡵ��ʹ�տ����������Ҥس�Ҷ١�ҧ��������");
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
