using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CurlingStone : MonoBehaviour {

    private float mass;
    public Vector position;
    public Vector velocity;
    public Vector velocityBody;
    public Vector acceleration;
    public Vector angularAcceleration;

    public LineRenderer accLine;
    public LineRenderer velLine;

    public Vector angularVelocity;
    public MyQuaternion orientation;
    public Vector forces;
    public Vector moments;
    private float radius;

    //Shot parameters
    public float minInitialVelocityX;
    public float maxInitialVelocityX;

    public float minInitialAngularVelocity;
    public float maxInitialAngularVelocity;

    private bool startShot = false;
    private bool once = true;

    private float startVelocity;
    private float startSpin;

    private Vector previousVelocity;
    

	// Use this for initialization
	void Start () {
        // Set initial position
        position = Vector.vector3ToVector(transform.position);

        // Set initial velocity
        velocity = new Vector();
        previousVelocity = new Vector();

        // Set initial angular velocity
        angularVelocity = new Vector();

        // Set initial angular acceleration
        angularAcceleration = new Vector();

        // Set initial acceleration
        acceleration = new Vector();

        // Set initial forces and moments
        forces = new Vector();
        moments = new Vector();

        // Set the initial orientation
        orientation = MyQuaternion.QuatToMyQuat(transform.rotation);

        // Set mass, radius and height
        mass = SimulationOptions.MASS;
        radius = SimulationOptions.RADIUS;

    }
	
	// Update is called once per frame
	void Update () {
        if (startShot)
        {
            if (once)
            {
                velocity.x = Remap(0f, 1f, minInitialVelocityX, maxInitialVelocityX, startVelocity);
                angularVelocity.z = Remap(0f, 1f, maxInitialAngularVelocity, minInitialAngularVelocity, startSpin);
                once = false;
            }
            EulerStep(Time.deltaTime);
        }        		
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
        previousVelocity = velocity;
        velocity += acceleration * dt;
        velocity.y = angularVelocity.z * radius;

        // Calculate Position
        position += velocity * dt;        

        // Set values to the transform of the object
        orientation = MyQuaternion.axisAngle(new Vector(0f, 0f, 1f), newAngle * Mathf.Rad2Deg) * orientation;

        transform.rotation = new Quaternion(orientation.x, orientation.y, orientation.z, orientation.w);

        transform.position = Vector.vectorToVector3(position);

        DrawWireframe();
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


    public void DrawWireframe()
    {
        Vector3[] positions = new[] { Vector.vectorToVector3(position), Vector.vectorToVector3(position + acceleration) };
        accLine.SetPositions(positions);
        positions = new[] { Vector.vectorToVector3(position), Vector.vectorToVector3(position + velocity) };
        velLine.SetPositions(positions);
    }

    public float Remap(float a1, float a2, float b1, float b2, float s)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public void SetStartShot(bool b)
    {
        startShot = b;
    }

    public bool GetStartShot()
    {
        return startShot;
    }

    public void SetStartVelocity(float f)
    {
        startVelocity = f;
    }

    public void SetStartSpin(float f)
    {
        startSpin = f;
    }

    public Vector GetPreviousVelocity()
    {
        return previousVelocity;
    }

    public void ResetVariables()
    {
        // Set initial velocity
        velocity = new Vector();
        previousVelocity = new Vector();

        // Set initial angular velocity
        angularVelocity = new Vector();

        // Set initial angular acceleration
        angularAcceleration = new Vector();

        // Set initial acceleration
        acceleration = new Vector();

        // Set initial forces and moments
        forces = new Vector();
        moments = new Vector();
        
    }
}
