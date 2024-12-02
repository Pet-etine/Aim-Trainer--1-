using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health;
    //private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        //timeElapsed = 0f;
        StartCoroutine(nameof(checkStatus));
        StartCoroutine(nameof(selfDestruct));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator checkStatus(){
        while(true){
            if(health == 0) {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(0.3f);

        }
    }
    IEnumerator selfDestruct(){
            yield return new WaitForSeconds(7f); // destroy target after 7 sec.
            Destroy(gameObject);
        }
    }

