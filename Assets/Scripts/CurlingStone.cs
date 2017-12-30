using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingStone : MonoBehaviour {

    public float vXinit, vYinit, wZInit;
    public float mass = 20, orientation, radius = 1.0f, frictionCoeficientF = 0.4f, frictionCoeficientB = 0.6f, restitutionCoeficent;
    public float gravity = 9.81f;

    // Velocity and position
    private Vector v;
    private Vector s;

    // Angular velocity and rotation    
    private Vector w;
    private Vector r;

	// Use this for initialization
	void Start () {
        v = new Vector();
        s = new Vector();

        w = new Vector();
        r = new Vector();
        

        s.x = transform.position.x;
        s.y = transform.position.y;

        v.x = vXinit;
        v.y = vYinit;

        w.z = wZInit;
        r.z = transform.rotation.z;
	}
	
	// Update is called once per frame
	void Update () {
        if(v.x > 0)
            EulerStep(Time.deltaTime);
		
	}

    public void CalcMoment() { }

    public void EulerStep(float dt) {

        

    }


    public void CollisionCheck() { }
    public void ApplyImpulse() { }
}
