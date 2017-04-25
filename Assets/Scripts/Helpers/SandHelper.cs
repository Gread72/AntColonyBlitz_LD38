using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandHelper : MonoBehaviour, IHelper {

    // Use this for initialization
    //void Start () {

    //}

    // Update is called once per frame
    //void Update () {

    //}

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ant_M")
        {
            AntMember ant = col.gameObject.GetComponent<AntMember>();
            ant.SandParticles += 1;
            ant.ParticleFound = true;
            ant.Target = null;
            //Debug.Log("Sand Found");

            GameManager.Instance.NumParticlesFound += 1;

            GameManager.Instance.RemoveItem(this.gameObject);
            
            Destroy(gameObject);
        }
    }
}
