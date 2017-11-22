using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingStone : MonoBehaviour {

    public Transform t;
    public Vector position, velocity, angularVelocity, Force;
    public float vX, vY;
    public float mass = 20, orientation, radius, frictionCoeficientF = 0.4f, frictionCoeficientB = 0.6f, restitutionCoeficent;
    public float gravity = 9.81f;

    // Velocity and position
    private Vector v;
    private Vector s;

	// Use this for initialization
	void Start () {
        v = new Vector();
        s = new Vector();

        s.x = t.position.x;
        s.y = t.position.y;

        v.x = vX;
        v.y = vY;
	}
	
	// Update is called once per frame
	void Update () {
        if(v.x > 0)
            EulerStep(Time.deltaTime);
		
	}

    public void CalcMoment() { }

    public void EulerStep(float dt) {

        // New velocity, new position
        Vector vNew = new Vector();
        Vector sNew = new Vector();

        // Calculate acceleration
        Vector acceleration = new Vector();
        acceleration.x = -((frictionCoeficientF + frictionCoeficientB) / 2) * gravity;
        acceleration.y = (frictionCoeficientF - frictionCoeficientB) * gravity; 

        // Calculate velocity
        vNew.x = -((frictionCoeficientF + frictionCoeficientB) / 2) * gravity * dt + v.x;
        vNew.y = (frictionCoeficientF - frictionCoeficientB) * gravity * dt + v.y;

        // Calculate new position
        sNew.x = s.x + v.x * dt - 0.5f * gravity * (frictionCoeficientF - frictionCoeficientB) * (dt * dt);
        sNew.y = s.y + v.y * dt -0.5f * gravity * ((frictionCoeficientF + frictionCoeficientB)/2) * (dt * dt);

        // Update velocity and position
        s = sNew;
        v = vNew;

        t.position = new Vector3(s.x, s.y,0);

    }


    public void CollisionCheck() { }
    public void ApplyImpulse() { }
}
