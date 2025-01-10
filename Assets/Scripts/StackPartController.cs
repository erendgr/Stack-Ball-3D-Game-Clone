﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackPartController : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    private StackController stackController;
    private Collider collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        stackController = transform.parent.GetComponent<StackController>();
    }

    public void Shatter()
    {
        rb.isKinematic = false;
        collider.enabled = false;
        
        Vector3 forcePoint = transform.parent.position;
        float parentXPos = transform.parent.position.x;
        float xPos = meshRenderer.bounds.center.x;
        
        Vector3 subDir = (parentXPos - xPos < 0) ? Vector3.right: Vector3.left;
        Vector3 dir = (Vector3.up * 1.5f + subDir).normalized;

        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);
        
        rb.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        rb.AddTorque(Vector3.left * torque);
        rb.velocity = Vector3.down;
    }

    public void RemoveAllChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).SetParent(null);
            i--;
        }
    }
}