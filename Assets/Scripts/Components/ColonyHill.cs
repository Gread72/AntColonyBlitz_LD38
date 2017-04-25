using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyHill : MonoBehaviour {
    
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ant_M")
        {
            AntMember ant = col.gameObject.GetComponent<AntMember>();
            if (ant.ParticleFound)
            {
                GameManager.Instance.ParticlesFound += 1;
                Debug.Log("Particle Found");

                SoundManager.Instance.PlaySound(SoundManager.POS_HIT_AUDIO);
            }

            if (ant.IsDeployed)
            {
                GameManager.Instance.AntsDeployed -= 1;
                Destroy(col.gameObject);
            } 
        }
    }
}
