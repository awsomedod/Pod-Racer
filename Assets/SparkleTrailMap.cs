using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SparkleTrailMap : MonoBehaviour
{
    public float nodeDistance;
    int currentNode;
    Map map;
    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Map>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToNextNode = Vector3.Distance(transform.position, map.map[currentNode].transform.position);
        if (distanceToNextNode <= nodeDistance)
        {
            currentNode++;
            transform.DOMove(map.map[currentNode].transform.position,0.5f);
        }
    }
}
