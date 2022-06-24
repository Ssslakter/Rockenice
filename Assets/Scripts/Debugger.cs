using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    GameObject[] prefabs;
    // Start is called before the first frame update
    void Awake()
    {
        generate();
    }

    void generate()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            SpawnObject(Instantiate(prefabs[i]), transform.position + Vector3.right * 3 * i);
        }
    }

    private void SpawnObject(GameObject gameObject, Vector3 position)
    {
        ///<summary>
        ///This is a description of my function.
        ///</summary>
        ///<param name="flatnessCoef">flatnessCoef: 0 - Генерация на плоскости(чекпоинт) 1 - где угодно</param>
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
        gameObject.transform.parent = transform;
        gameObject.transform.localPosition = position;
        gameObject.transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (var item in Global.prefabs)
            {
                print(item);
            }
        }
    }
}
