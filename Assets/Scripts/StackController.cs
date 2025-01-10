using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    //[SerializeField] private StackPartController[] stackPartControllers = null;
    [SerializeField] private Transform parentObject;
    public Transform[] childObjects; 
    
    private void Start()
    {
        parentObject = this.transform;
        
        int childCount = parentObject.childCount;
        childObjects = new Transform[childCount];

        int index = 0;
        foreach (Transform child in parentObject)
        {
            childObjects[index] = child;
            index++;
        }

    }

    public void ShatterAllParts()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
            FindObjectOfType<BallController>().IncreaseBrokenStacks();
        }

        for (int i = 0; i < childObjects.Length; i++)
        {
            childObjects[i].GetComponent<StackPartController>().Shatter();
        }
        // foreach (StackPartController o in stackPartControllers)
        // {
        //     o.Shatter();
        // }

        StartCoroutine(RemoveParts());
    }

    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}