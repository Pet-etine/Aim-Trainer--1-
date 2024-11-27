using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int health;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0f;
        StartCoroutine(nameof(checkStatus));
        StartCoroutine(nameof(timer));
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
    IEnumerator timer(){
        float interval = 0.1f; // 100ms interval
        while(true) {
            if(timeElapsed == 3f){
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(interval);
            timeElapsed += interval;
        }
    }
}
