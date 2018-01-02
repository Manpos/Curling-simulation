using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimulationOptions
{
    public static float RADIUS = 1.0f;
    public static float HEIGHT = 1.0f;
    public static float MASS = 20.0f;
    public static float GRAVITY = 9.81f;
    public static float LINEAR_DRAG_COEFFICIENT = 0.5f;
    public static float ANGULAR_DRAG_COEFFICIENT = 0.5f;
    public static float FRICTION_FACTOR = 0.5f;
    public static float COEFFICIENT_OF_RESTITUTION = 0.8f;
    public static float COEFFICIENT_OF_RESTITUTION_GROUND = 0.1f;
    public static float FRICTION_COEFFICIENT_STONES = 0.1f;
    public static float FRICTION_COEFFICIENT_GROUND = 0.02f;
    public static float CURLING_RESISTANCE_COEFFICIENT = 0.025f;
}

public class CurlingStone : MonoBehaviour {

    public float mass = 20;
    //public Matrix3x3 inertia;
    //public Matrix3x3 inertiaInverse;
    public float inertia;
    public float inertiaInverse;
    public Vector position;
    public Vector velocity;
    public Vector velocityBody;
    public Vector acceleration;
    public Vector angularAcceleration;
    public Vector angularAccelerationGlobal;

    public Vector angularVelocity;
    public Vector angularVelocityGlobal;
    public Vector eulerAngles;
    public float speed;
    public MyQuaternion orientation;
    public Vector forces;
    public Vector moments;
    public Matrix3x3 ieInverse;
    public float radius;
    public float height;

    //Shot parameters
    public float initialVelocityX;
    public float initialVelocityY;

    public float initialAngularVelocity;
    

	// Use this for initialization
	void Start () {
        // Set initial position
        position = Vector.vector3ToVector(transform.position);

        // Set initial velocity
        velocity = new Vector();
        velocity.x = initialVelocityX;
        velocity.y = initialVelocityY;
        velocity.z = 0.0f;
        speed = velocity.module();

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

        // Set inertia tensor
        inertia = (1f / 2f) * mass * (radius * radius);
        inertiaInverse = 1f / inertia;

        //inertia = new Matrix3x3();
        //inertia.m11 = (1 / 12) * mass * (3 * (radius * radius) + (height * height));
        //inertia.m22 = inertia.m11;
        //inertia.m33 = (1 / 2) * mass * (radius * radius);

        //inertiaInverse = inertia.Inverse();

    }
	
	// Update is called once per frame
	void Update () {
        EulerStep(Time.deltaTime);
		
	}

    public void CalcMoment() { }

    public void EulerStep(float dt) {

        CalculateForces();

        // Integrate equations of motion

        // Calculate acceleration
        acceleration = forces / mass;

        // Calculate Velocity
        velocity += acceleration * dt;

        // Calculate Position
        position += velocity * dt;

        // Rotations
        angularAcceleration = moments/inertia;

        angularVelocity += angularAcceleration * dt;

        float newAngle = angularVelocity.z * dt;

        orientation = MyQuaternion.axisAngle(new Vector(0f, 0f, 1f), newAngle) * orientation;

        transform.rotation = new Quaternion(orientation.x, orientation.y, orientation.z, orientation.w);

        transform.position = Vector.vectorToVector3(position);

    }

    public void CalculateForces()
    {
        forces = new Vector();
        forces = (-1f) * velocity.normalize() * (SimulationOptions.FRICTION_COEFFICIENT_GROUND * mass * (SimulationOptions.GRAVITY));
        moments = new Vector();
        moments = -(1f) * angularVelocity.normalize() * (SimulationOptions.FRICTION_COEFFICIENT_GROUND * mass * (SimulationOptions.GRAVITY)) * radius * Mathf.Rad2Deg* (Mathf.Sin(90));

    }

    public void CollisionCheck() { }
    public void ApplyImpulse() { }
}
