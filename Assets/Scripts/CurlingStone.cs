using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingStone : MonoBehaviour {

    public Transform t;
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
        

        s.x = t.position.x;
        s.y = t.position.y;

        v.x = vXinit;
        v.y = vYinit;

        w.z = wZInit;
        r.z = t.rotation.z;
	}
	
	// Update is called once per frame
	void Update () {
        if(v.x > 0)
            EulerStep(Time.deltaTime);
		
	}

    public void CalcMoment() { }

    public void EulerStep(float dt) {

        // New velocity, new position, new angular velocity, orientation
        Vector vNew = new Vector();
        Vector sNew = new Vector();
        Vector wNew = new Vector();
        Vector rNew = new Vector();

        // Calculate angular velocity
        wNew.z = (gravity * (frictionCoeficientF - frictionCoeficientB) / radius) * dt + w.z;

        // Calculate new angle
        rNew.z = wNew.z * dt + r.z;

        // Calculate velocity
        vNew.x = -((frictionCoeficientF + frictionCoeficientB) / 2) * gravity * dt + v.x;
        vNew.y = wNew.z * radius;

        // Calculate new position
        sNew.x = s.x + v.x * dt - 0.5f * gravity * ((frictionCoeficientF + frictionCoeficientB) / 2) * (dt * dt);
        sNew.y = s.y + vNew.y * dt;

        // Update velocity and position
        s = sNew;
        v = vNew;
        w = wNew;
        r = rNew;
        

        t.position = new Vector3(s.x, s.y,0);
        t.rotation = new Quaternion(0, 0, 1, r.z);

    }


    public void CollisionCheck() { }
    public void ApplyImpulse() { }
}
