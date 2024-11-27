using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameObject target;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(SpawnTarget));
        
    }
    IEnumerator SpawnTarget() {
        float interval = 2.0f;
        while(true){
            var position = new Vector3(Random.Range(-10.0f, 10f), Random.Range(1f, 10f), 25);
            Instantiate(target, position, Quaternion.identity, parent.transform);
            yield return new WaitForSeconds(interval);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
