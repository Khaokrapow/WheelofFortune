using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceNPCPath : MonoBehaviour
{
    public Color colorLine;

    private List<Transform> pathList = new List<Transform>();

    void OnDrawGizmos()
    {
        Gizmos.color = colorLine;

        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        pathList = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i += 1)
        {
            if (pathTransforms[i] != transform)
            {
                pathList.Add(pathTransforms[i]);
            }
        }
        for (int i = 0; i < pathList.Count; i += 1)
        {
            Vector3 currentNode = pathList[i].position;
            Vector3 previousNode = Vector3.zero;

            //Solution AI not order 
            if (i > 0)
            {
                previousNode = pathList[i - 1].position;
            }
            else if (i == 0 && pathList.Count > 1)
            {
                previousNode = pathList[pathList.Count - 1].position;
            }
            Gizmos.DrawLine(previousNode, currentNode);

            //set shape of node
            Gizmos.DrawWireSphere(currentNode, 0.3f);
        }
    }
}
