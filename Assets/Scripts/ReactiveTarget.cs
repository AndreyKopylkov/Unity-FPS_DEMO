using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{

    public void ReactToHit() //метод, вызванный сценарием стрельбы
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }

        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        int counter = 0;
        Quaternion playerPosition = GameObject.Find("Player").transform.rotation;
       // Vector3 playerAngle = playerPosition.eulerAngles;
       // Vector3 downAngle = new Vector3(-75, 0, 0);
        transform.Translate(0, -1, 0);
        while (counter < 30)
        {
            transform.Rotate(-3, 0, 0);
            yield return new WaitForEndOfFrame();
            counter += 1;
        }
        //transform.Rotate(-5, 0, 0);

        yield return new WaitForSeconds(2f); //остановка подпрограммы

        Destroy(gameObject); //уничтожение этого объекта
    }
}
