using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastSpell : MonoBehaviour {
    public EnemyAIScript enemyAI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CastSpell()
    {
        enemyAI.CastSpell();
    }
}
