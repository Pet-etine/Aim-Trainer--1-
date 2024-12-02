using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetController : MonoBehaviour
{
    public GameObject target;
    public GameObject parent;
    public BoxCollider spawnBox;
    public int targetCount;

    public bool childTrigger;
    private bool triggeredFlag;
    private float x_Min, x_Max, y_Min, y_Max, z_Min, z_Max;


    /*public Vector3 GetRandomPointInsideCollider(BoxCollider boxCollider)
    {

        Vector3 extents = boxCollider.size / 2f;
        Debug.Log("BoxCollider extents: "+ extents);
        Vector3 point = new Vector3(
            Random.Range( -extents.x, extents.x ),
            Random.Range( -extents.y, extents.y ),
            Random.Range( -extents.z, extents.z )
        );
        Debug.Log(point);
        return boxCollider.transform.TransformPoint( point );
    }*/

    public Vector3 GetRandomFromCollider(BoxCollider boxCollider){
        x_Min = boxCollider.bounds.min.x;
        x_Max = boxCollider.bounds.max.x;
        y_Min = boxCollider.bounds.min.y;
        y_Max = boxCollider.bounds.max.y;
        z_Min = boxCollider.bounds.min.z;
        z_Max = boxCollider.bounds.max.z;
        Vector3 point = new Vector3(
            Random.Range(x_Min,x_Max),
            Random.Range(y_Min,y_Max),
            Random.Range(z_Min,z_Max)
        );
        return point;
    }
    // Start is called before the first frame update
    
    void Start()
    {
        Debug.Log("BoxCollider bounds: "+ spawnBox.bounds);
        Debug.Log("BoxCollider Min: "+ spawnBox.bounds.min.x);
        Debug.Log("BoxCollider Max: "+ spawnBox.bounds.max.x);
        
        childTrigger = false;
        //StartCoroutine(nameof(SpawnTarget));
        triggeredFlag = false;
    }
    IEnumerator SpawnTarget() {
        float interval = 1.0f;
        
        for (int i = 0; i < targetCount; i++){
        //while(true){
            //var position = new Vector3(Random.Range(-10.0f, 10f), Random.Range(1f, 10f), 25);
            //var position = GetRandomPointInsideCollider(spawnBox);
            var position = GetRandomFromCollider(spawnBox);
            Instantiate(target, position, Quaternion.identity, parent.transform);
            yield return new WaitForSeconds(interval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (childTrigger == true && triggeredFlag != true) {
            StartCoroutine(nameof(SpawnTarget));
            triggeredFlag = true;
        }
        
    }
}
