using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimulationOptions
{
    public static float RADIUS = 0.145f;
    public static float HEIGHT = 0.20f;
    public static float MASS = 20.0f;
    public static float GRAVITY = 9.81f;
    public static float COEFFICIENT_OF_RESTITUTION = 0.8f;
    public static float FRICTION_COEFFICIENT_GROUND = 0.02f;
}

public class CurlingStone : MonoBehaviour {

    private float mass;
    public Vector position;
    public Vector velocity;
    public Vector velocityBody;
    public Vector acceleration;
    public Vector angularAcceleration;

    public Vector angularVelocity;
    public MyQuaternion orientation;
    public Vector forces;
    public Vector moments;
    private float radius;
    private float height;

    //Shot parameters
    public float initialVelocityX;

    public float initialAngularVelocity;
    

	// Use this for initialization
	void Start () {
        // Set initial position
        position = Vector.vector3ToVector(transform.position);

        // Set initial velocity
        velocity = new Vector();
        velocity.x = initialVelocityX;

        // Set initial angular velocity
        angularVelocity = new Vector();
        angularVelocity.z = initialAngularVelocity;

        // Set initial angular acceleration
        angularAcceleration = new Vector();

        // Set initial acceleration
        acceleration = new Vector();

        // Set initial forces and moments
        forces = new Vector();
        moments = new Vector();

        // Set velocity in space coordinates to 0
        velocityBody = new Vector();

        // Set the initial orientation
        orientation = MyQuaternion.QuatToMyQuat(transform.rotation);

        // Set mass, radius and height
        mass = SimulationOptions.MASS;
        radius = SimulationOptions.RADIUS;
        height = SimulationOptions.HEIGHT;

    }
	
	// Update is called once per frame
	void Update () {
        EulerStep(Time.deltaTime);		
	}

    public void CalcMoment() { }

    public void EulerStep(float dt) {

        orientation = MyQuaternion.QuatToMyQuat(transform.rotation);

        CalculateForces();

        // Integrate equations of motion

        // Rotations
        angularAcceleration = moments / mass;

        angularVelocity += angularAcceleration * dt;

        float newAngle = angularVelocity.z * dt;

        // Calculate acceleration
        acceleration = forces / mass;

        // Calculate Velocity
        velocity += acceleration * dt;
        velocity.y = angularVelocity.module() * radius;

        // Calculate Position
        position += velocity * dt;

        

        orientation = MyQuaternion.axisAngle(new Vector(0f, 0f, 1f), newAngle * Mathf.Rad2Deg) * orientation;

        transform.rotation = new Quaternion(orientation.x, orientation.y, orientation.z, orientation.w);

        transform.position = Vector.vectorToVector3(position);

    }

    public void CalculateForces()
    {
        forces = new Vector();
        forces = (-1f) * velocity.normalize() * (SimulationOptions.FRICTION_COEFFICIENT_GROUND * mass * (SimulationOptions.GRAVITY));
        moments = new Vector();
        moments = -(1f) * angularVelocity.normalize() * (SimulationOptions.FRICTION_COEFFICIENT_GROUND * mass * (SimulationOptions.GRAVITY));

    }

    public void CollisionCheck() { }
    public void ApplyImpulse() { }
}
