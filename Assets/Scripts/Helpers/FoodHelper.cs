using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHelper : MonoBehaviour, IHelper {

    public int HealthPointToAdd = 1;
	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	//void Update () {
 //       Vector3 up = transform.TransformDirection(Vector3.forward);

 //       Debug.DrawRay(transform.position, up, Color.green);
 //   }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ant_M")
        {
            AntMember ant = col.gameObject.GetComponent<AntMember>();
            ant.Health += HealthPointToAdd;
            Debug.Log("Food Found");

            //GameObject go = GameManager.Instance.GetNextItem(ant.gameObject);
            //if (go != null) ant.Target = go.transform;

            Destroy(gameObject);
        }
    }
}
