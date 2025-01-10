using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private bool smash;

    public enum BallState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }
    
    public BallState ballState = BallState.Prepare;
    
    public AudioClip bounceClip, loseClip, winClip, destroyClip, iDestroyClip;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (ballState == BallState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                smash = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                smash = false;
            }
        }

        if (ballState == BallState.Prepare)
        {
            if (Input.GetMouseButtonDown(0))
                ballState = BallState.Playing;
        }

        if (ballState == BallState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().NextLevel();
            }
        }
    }

    private void FixedUpdate()
    {
        if (ballState == BallState.Playing)
        {
            if (Input.GetMouseButton(0))
            {
                smash = true;
                rb.velocity = new Vector3(0, -100 * Time.deltaTime * 7, 0);
            }
        }

        if (rb.velocity.y > 5)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5f, rb.velocity.z);
        }
    }
    //daha sonra taşı
    public void IncreaseBrokenStacks()
    {
        ScoreManager.Instance.AddScore(1);
        SoundManager.Instance.PlaySound(destroyClip, 0.5f);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            SoundManager.Instance.PlaySound(bounceClip, 0.5f);
        }
        else
        {
            if (other.gameObject.tag == "enemy")
            {
                other.transform.parent.GetComponent<StackController>().ShatterAllParts();
            }

            if (other.gameObject.tag == "plane")
            {
                Debug.Log("You Failed!");
                Time.timeScale = 0;
                ScoreManager.Instance.ResetScore();
                SoundManager.Instance.PlaySound(loseClip, 0.5f);
            }
        }

        if (other.gameObject.tag == "Finish" && ballState == BallState.Playing)
        {
            ballState = BallState.Finish;
            SoundManager.Instance.PlaySound(winClip, 0.5f);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!smash || other.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}