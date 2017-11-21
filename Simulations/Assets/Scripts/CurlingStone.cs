using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingStone : MonoBehaviour {

    public Transform t;
    public Vector position, velocity, angularVelocity, Force;
    public float mass = 20, orientation, radius, frictionCoeficient = 0.4f, restitutionCoeficent;
    public float gravity = 9.81f;
    private float N;

    // Velocity and position
    public float v;
    private float s;

	// Use this for initialization
	void Start () {
        s = t.position.x;
        N = mass * gravity;
	}
	
	// Update is called once per frame
	void Update () {
        if(v > 0)
            EulerStep(Time.deltaTime);
		
	}

    public void CalcMoment() { }
    public void EulerStep(float dt) {
        // New velocity, new position
        float vNew;
        float sNew;

        // Calculate velocity
        vNew = -frictionCoeficient * gravity * dt + v;

        // Calculate new position
        sNew = s + v * dt - 0.5f * gravity * frictionCoeficient * (dt * dt);

        // Update velocity and position
        s = sNew;
        v = vNew;
        t.position = new Vector3(s,0,0);

    }
    public void CollisionCheck() { }
    public void ApplyImpulse() { }
}
