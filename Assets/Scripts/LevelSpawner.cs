using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] models;
    [HideInInspector]
    public GameObject[] ringPrefabs = new GameObject[4];
    public GameObject windPrefabs;

    private GameObject temp1, temp2;

    public int level = 1, addOn = 7;

    private float i;
    
    
    void Start()
    {
        level = PlayerPrefs.GetInt("Level", 1);
        
        if (level > 9)
            addOn = 0;

        ModelSelection();
        for (i = 0; i > -level - addOn; i -= 0.5f)
        {
            if (level <= 20)
                temp1 = Instantiate(ringPrefabs[Random.Range(0, 2)]);
            if(level > 20 && level <= 50)
                temp1 = Instantiate(ringPrefabs[Random.Range(1, 3)]);
            if(level > 50 && level <= 100)
                temp1 = Instantiate(ringPrefabs[Random.Range(2, 4)]);
            if(level > 100)
                temp1 = Instantiate(ringPrefabs[Random.Range(3, 4)]);
            
            temp1.transform.position = new Vector3(0, i - 0.01f, 0);
            temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
            
            temp1.transform.parent = FindObjectOfType<Rotator>().transform;
        }
        temp2 = Instantiate(windPrefabs);
        temp2.transform.position = new Vector3(0, i - 0.01f, 0);
    }

    private void ModelSelection()
    {
        int randomModel = Random.Range(0, 5);
        switch (randomModel)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                    ringPrefabs[i] = models[i];
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                    ringPrefabs[i] = models[i + 4];
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                    ringPrefabs[i] = models[i + 8];
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                    ringPrefabs[i] = models[i + 12];
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                    ringPrefabs[i] = models[i + 16];
                break;
        }
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")+ 1);
        SceneManager.LoadScene(0);
    }
}