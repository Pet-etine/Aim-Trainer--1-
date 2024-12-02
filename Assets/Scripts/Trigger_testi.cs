using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_testi : MonoBehaviour
{
    public GameObject parentObject;
    private TargetController parentCheck;

    // Start is called before the first frame update
    void Start()
    {
        parentCheck = parentObject.GetComponentInParent<TargetController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") {
        Debug.Log("Triggered!");
        parentCheck.childTrigger = true;
        }
    }
}
