using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


#if false
public class IK_FABRIK2 : MonoBehaviour
{
    public Transform[] joints;
    public Transform target;

    private Vector[] copy;
    //private Vector3[] copy;
    private float[] distances;
    private bool done;

    public int maxIterations;

    float treshold_condition = 0.1f;

    void Start()
    {
        maxIterations = 10;
        distances = new float[joints.Length - 1];
        copy = new Vector[joints.Length];
        //copy = new Vector3[joints.Length];
    }

    void Update()
    {
        // Copy the joints positions to work with
        //TODO
        copy[0] = Vector.vector3ToVector(joints[0].position);
        //copy[0] = joints[0].position;
        for (int i = 0; i < copy.Length - 1; i++)
        {
            copy[i + 1] = Vector.vector3ToVector(joints[i + 1].position);
            distances[i] = Vector.distance(copy[i + 1], copy[i]);
            //copy[i + 1] = joints[i + 1].position;
            //distances[i] = Vector3.Distance(copy[i + 1], copy[i]);
        }

        //done = TODO
        done = (copy[copy.Length - 1] - Vector.vector3ToVector(target.position)).module() < treshold_condition;
        //done = (copy[copy.Length - 1] - target.position).magnitude < treshold_condition;
        if (!done)
        {
            float targetRootDist = Vector.distance(copy[0], Vector.vector3ToVector(target.position));
            //float targetRootDist = Vector3.Distance(copy[0], target.position);
            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                for (int i = 0; i < copy.Length - 1; i++)
                {
                    float r = (Vector.vector3ToVector(target.position) - copy[i]).module();
                    //float r = (target.position - copy[i]).magnitude;
                    float lambda = distances[i] / r;
                    copy[i + 1] = (1 - lambda) * copy[i] + (lambda * Vector.vector3ToVector(target.position));
                    //copy[i + 1] = (1 - lambda) * copy[i] + (lambda * target.position);
                }

            }
            else
            {
                int iter = 0;
                // The target is reachable
                Vector b = copy[0];

                float distance = Vector.distance(copy[joints.Length - 1], Vector.vector3ToVector(target.position));
                //float distance = Vector3.Distance(copy[joints.Length - 1], target.position);

                while (distance > treshold_condition && iter < maxIterations)
                {
                    iter++;

                    // STAGE 1: FORWARD REACHING
                    copy[copy.Length - 1] = Vector.vector3ToVector(target.position);
                    //copy[copy.Length - 1] = target.position;
                    for (int i = copy.Length - 2; i > 0; i--)
                    {
                        float r = Vector.distance(copy[i + 1], copy[i]);
                        //float r = Vector3.Distance(copy[i + 1], copy[i]);
                        float lambda = distances[i] / r;
                        copy[i] = (1 - lambda) * copy[i + 1] + (lambda * copy[i]);
                    }

                    // STAGE 2: BACKWARD REACHING
                    copy[0] = b;
                    for (int i = 0; i < copy.Length - 1; i++)
                    {
                        float r = Vector.distance(copy[i + 1], copy[i]);
                        //float r = Vector3.Distance(copy[i + 1], copy[i]);
                        float lambda = distances[i] / r;
                        copy[i + 1] = (1 - lambda) * copy[i] + (lambda * copy[i + 1]);

                    }

                    distance = Vector.distance(copy[copy.Length - 1], Vector.vector3ToVector(target.position));
                    //distance = Vector3.Distance(copy[copy.Length - 1], target.position);
                }
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
                Vector init = Vector.vector3ToVector(joints[i + 1].position) - Vector.vector3ToVector(joints[i].position);
                //Vector3 init = joints[i + 1].position - joints[i].position;
                Vector now = copy[i + 1] - copy[i];
                //Vector3 now = copy[i + 1] - copy[i];

                //float angle = Mathf.Acos(Vector3.Dot(init.normalized, now.normalized)) * Mathf.Rad2Deg;
                float cosa = Vector.dot(init.normalize(), now.normalize());
                //float cosa = Vector3.Dot(init.normalized, now.normalized);
                float sina = Vector.cross(init.normalize(), now.normalize()).module();
                //float sina = Vector3.Cross(init.normalized, now.normalized).magnitude;

                float angle = Mathf.Atan2(sina, cosa) * Mathf.Rad2Deg;

                Vector axis = Vector.cross(init, now).normalize();
                //Vector3 axis = Vector3.Cross(init, now).normalized;
                Quaternion rotated = Quaternion.AngleAxis(angle, Vector.vectorToVector3(axis));
                //TODO 
                joints[i].rotation = rotated * joints[i].rotation;
                joints[i + 1].position = Vector.vectorToVector3(copy[i + 1]);
                //joints[i + 1].position = copy[i + 1];

            }
        }
    }

}

#endif

#if true

public class IK_FABRIK2 : MonoBehaviour
{
    public Transform[] joints;
    public Transform target;

    private Vector3[] copy;
    private float[] distances;
    private bool done;

    public int maxIterations;

    float treshold_condition = 0.1f;

    void Start()
    {
        maxIterations = 10;
        distances = new float[joints.Length - 1];
        copy = new Vector3[joints.Length];
    }

    void Update()
    {
        // Copy the joints positions to work with
        //TODO
        copy[0] = joints[0].position;
        for (int i = 0; i < copy.Length - 1; i++)
        {
            copy[i + 1] = joints[i + 1].position;
            distances[i] = Vector3.Distance(copy[i + 1], copy[i]);
        }

        //done = TODO
        done = (copy[copy.Length - 1] - target.position).magnitude < treshold_condition;

        if (!done)
        {
            float targetRootDist = Vector3.Distance(copy[0], target.position);

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                for (int i = 0; i < copy.Length - 1; i++)
                {
                    float r = (target.position - copy[i]).magnitude;
                    float lambda = distances[i] / r;
                    copy[i + 1] = (1 - lambda) * copy[i] + (lambda * target.position);

                }

            }
            else
            {
                int iter = 0;
                // The target is reachable
                Vector3 b = copy[0];

                float distance = Vector3.Distance(copy[joints.Length - 1], target.position);

                while (distance > treshold_condition && iter < maxIterations)
                {
                    iter++;

                    // STAGE 1: FORWARD REACHING
                    copy[copy.Length - 1] = target.position;
                    for (int i = copy.Length - 2; i > 0; i--)
                    {
                        float r = Vector3.Distance(copy[i + 1], copy[i]);
                        float lambda = distances[i] / r;
                        copy[i] = (1 - lambda) * copy[i + 1] + (lambda * copy[i]);
                    }

                    // STAGE 2: BACKWARD REACHING
                    copy[0] = b;
                    for (int i = 0; i < copy.Length - 1; i++)
                    {
                        float r = Vector3.Distance(copy[i + 1], copy[i]);
                        float lambda = distances[i] / r;
                        copy[i + 1] = (1 - lambda) * copy[i] + (lambda * copy[i + 1]);

                    }

                    distance = Vector3.Distance(copy[copy.Length - 1], target.position);
                }
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
                Vector3 init = joints[i + 1].position - joints[i].position;
                Vector3 now = copy[i + 1] - copy[i];

                //float angle = Mathf.Acos(Vector3.Dot(init.normalized, now.normalized)) * Mathf.Rad2Deg;
                float cosa = Vector3.Dot(init.normalized, now.normalized);
                float sina = Vector3.Cross(init.normalized, now.normalized).magnitude;

                float angle = Mathf.Atan2(sina, cosa) * Mathf.Rad2Deg;

                Vector3 axis = Vector3.Cross(init, now).normalized;
                Quaternion rotated = Quaternion.AngleAxis(angle, axis);
                //TODO 
                joints[i].rotation = rotated * joints[i].rotation;
                joints[i + 1].position = copy[i + 1];

            }
        }
    }

}

#endif
