using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrounManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform startPoint;
    [SerializeField]
    private RectTransform endPoint;

    Ground[] grounds;

    private void Awake()
    {
        grounds = GetComponentsInChildren<Ground>();     
    }

    public void MovingGrounds()
    {
        if (grounds !=null)
        {
            foreach (var ground in grounds)
            {
                ground.transform.position = new Vector3(ground.transform.position.x-0.005f , ground.transform.position.y, ground.transform.position.z);
                if (ground.transform.position.x <= endPoint.position.x)
                {
                    ground.transform.position = startPoint.position;
                }
            }
        }
    }
}
