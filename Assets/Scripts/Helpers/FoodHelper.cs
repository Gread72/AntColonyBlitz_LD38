using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHelper : MonoBehaviour, IHelper {

    public int HealthPointToAdd = 1;
	
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ant_M")
        {
            AntMember ant = col.gameObject.GetComponent<AntMember>();
            ant.Health += HealthPointToAdd;
            Debug.Log("Food Found");

            Destroy(gameObject);
        }
    }
}
