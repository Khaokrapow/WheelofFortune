using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform startPosition; // �ش������鹷���ͧ��á�˹�
    public GameObject playerCar; // ö¹��ͧ������

    // �ѧ��ѹ���ж١���¡����͡�������������ö¹����ѧ�ش�������
    public void RespawnButton()
    {
        if (playerCar != null && startPosition != null)
        {
            // ��˹����˹��������Ѻö¹�������˹觢ͧ startPosition
            playerCar.transform.position = startPosition.position;

            // �ҡ��ͧ��� reset �����ع�ͧö¹�����
            playerCar.transform.rotation = startPosition.rotation;
        }
    }
}