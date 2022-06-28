using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.AI;

public class GenerateEnemy : MonoBehaviour
{

    public GameObject enemy;
    public int xPos, yPos, zPos;
    public int enemyCount;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(EnemyDrop());
         
    }

    IEnumerator EnemyDrop()
    {
        while (counter < enemyCount)
        {
            xPos = Random.Range(20, 50);
            zPos = Random.Range(0, 40);
            Instantiate(enemy, new Vector3(xPos, yPos, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            counter++;
        }
    }
    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
