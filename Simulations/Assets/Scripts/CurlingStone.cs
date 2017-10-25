using System.Collections;
using System.Collections.Generic;

public class CurlingStone {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector position, velocity, angularVelocity, Force;
    public float mass, speed, orientation, radius, frictionCoeficient, restitutionCoeficent;
    public void CalcMoment() { }
    public void EulerStep() { }
    public void CollisionCheck() { }
    public void ApplyImpulse() { }
}
