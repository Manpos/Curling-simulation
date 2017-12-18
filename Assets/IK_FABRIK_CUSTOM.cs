using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


#if true
public class IK_FABRIK_CUSTOM : MonoBehaviour
{
    struct MyJoint
    {
        public Vector position;
        public float angle;
    }

    public Transform[] joints;
    public float[] angleJoints;
    public Transform target;

    private MyJoint[] copy;
    public Transform origin;

    //private Vector[] copy;
    private float[] distances;
    private bool done;

    public int maxIterations;

    float treshold_condition = 0.1f;

    void Start()
    {
        maxIterations = 10;
        distances = new float[joints.Length - 1];
        copy = new MyJoint[joints.Length];
        for (int i = 0; i < angleJoints.Length -1; i++)
        {
            copy[i].angle = angleJoints[i];
        }
    }

    void Update()
    {
        // Copy the joints positions to work with
        //TODO
        copy[0].position = Vector.vector3ToVector(joints[0].position);
        for (int i = 0; i < copy.Length - 1; i++)
        {
            copy[i + 1].position = Vector.vector3ToVector(joints[i + 1].position);
            distances[i] = Vector.distance(copy[i + 1].position, copy[i].position);
        }

        //done = TODO
        done = (copy[copy.Length - 1].position - Vector.vector3ToVector(target.position)).module() < treshold_condition;
        
        if (!done)
        {
            float targetRootDist = Vector.distance(copy[0].position, Vector.vector3ToVector(target.position));
            
            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                for (int i = 0; i < copy.Length - 1; i++)
                {
                    float r = (Vector.vector3ToVector(target.position) - copy[i].position).module();
                    float lambda = distances[i] / r;
                    copy[i + 1].position = (1 - lambda) * copy[i].position + (lambda * Vector.vector3ToVector(target.position));
                    
                }

            }
            else
            {
                int iter = 0;
                // The target is reachable
                Vector b = copy[0].position;

                float distance = Vector.distance(copy[joints.Length - 1].position, Vector.vector3ToVector(target.position));
                //float distance = Vector3.Distance(copy[joints.Length - 1], target.position);

                while (distance > treshold_condition && iter < maxIterations)
                {
                    iter++;


                    // Constrians
#if false
                    // STAGE 1: FORWARD REACHING
                    copy[copy.Length - 1].position = Vector.vector3ToVector(target.position);
                    
                    for (int i = copy.Length - 2; i > 0; i--)
                    {
                        float r = Vector.distance(copy[i + 1].position, copy[i].position);
                        float lambda = distances[i] / r;
                        copy[i].position = (1 - lambda) * copy[i + 1].position + (lambda * copy[i].position);
                    }

                    // STAGE 2: BACKWARD REACHING
                    copy[0].position = b;
                    for (int i = 0; i < copy.Length - 1; i++)
                    {
                        // Find the direction vector of the CONE
                        Vector l = new Vector();
                        if (i == 0)
                        {
                            l =(copy[0].position - Vector.vector3ToVector(origin.position)).normalize();
                        }
                        else
                        {
                            l = (copy[i].position - copy[i - 1].position).normalize();
                        }

                        // Vector between "target" and joint
                        Vector CT = copy[i + 1].position - copy[i].position;

                        // Calculate angle between CT and l
                        float dotProduct = Vector.dot(CT.normalize(), l.normalize());
                        float angle = (Mathf.Acos(dotProduct)) * Mathf.Rad2Deg;

                        if (angle > copy[i].angle)
                        {
                            // Length of the vector OC
                            float S = Mathf.Cos(angle) * Mathf.Rad2Deg * CT.module();
                            // Point O (Projection of T on L)
                            Vector O = S * l.normalize();
                            // Length of the Vector OT'
                            float M = Mathf.Tan(copy[i].angle) * Mathf.Rad2Deg * S;
                            // Direction vector of the OT' vector
                            Vector OT = copy[i + 1].position - O;
                            // Find the new T'
                            Vector newT = M * OT.normalize();
                            // Set the position of the next joint on T'
                            copy[i + 1].position = newT;
                        }
                        else
                        {
                            float r = Vector.distance(copy[i + 1].position, copy[i].position);
                            float lambda = distances[i] / r;
                            copy[i + 1].position = (1 - lambda) * copy[i].position + (lambda * copy[i + 1].position);
                        }
                    }
                    distance = Vector.distance(copy[copy.Length - 1].position, Vector.vector3ToVector(target.position));
#endif


                    // NO Constrians
#if true
                    // STAGE 1: FORWARD REACHING
                    copy[copy.Length - 1].position = Vector.vector3ToVector(target.position);
                    //copy[copy.Length - 1] = target.position;
                    for (int i = copy.Length - 2; i > 0; i--)
                    {
                        float r = Vector.distance(copy[i + 1].position, copy[i].position);
                        //float r = Vector3.Distance(copy[i + 1], copy[i]);
                        float lambda = distances[i] / r;
                        copy[i].position = (1 - lambda) * copy[i + 1].position + (lambda * copy[i].position);
                    }

                    // STAGE 2: BACKWARD REACHING
                    copy[0].position = b;
                    for (int i = 0; i < copy.Length - 1; i++)
                    {
                        
                           float r = Vector.distance(copy[i + 1].position, copy[i].position);

                            //float r = Vector3.Distance(copy[i + 1], copy[i]);
                            float lambda = distances[i] / r;
                            copy[i + 1].position = (1 - lambda) * copy[i].position + (lambda * copy[i + 1].position);
                        
                    }

                    distance = Vector.distance(copy[copy.Length - 1].position, Vector.vector3ToVector(target.position));
                    //distance = Vector3.Distance(copy[copy.Length - 1], target.position);
#endif
                }
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
                Vector init = Vector.vector3ToVector(joints[i + 1].position) - Vector.vector3ToVector(joints[i].position);
                Vector now = copy[i + 1].position - copy[i].position;
                //Vector3 now = copy[i + 1] - copy[i];

                //float angle = Mathf.Acos(Vector3.Dot(init.normalized, now.normalized)) * Mathf.Rad2Deg;
                float cosa = Vector.dot(init.normalize(), now.normalize());
                float sina = Vector.cross(init.normalize(), now.normalize()).module();

                float angle = Mathf.Atan2(sina, cosa) * Mathf.Rad2Deg;

                Vector axisV = new Vector();
                MyQuaternion rotated = new MyQuaternion();
                MyQuaternion current = new MyQuaternion();
                MyQuaternion result = new MyQuaternion();

                if (angle != 0)
                {
                    axisV = Vector.cross(init, now).normalize();

                    rotated = MyQuaternion.axisAngle(axisV, angle);
                    current = MyQuaternion.QuatToMyQuat(joints[i].rotation);

                    result = rotated * current;
                    Quaternion quatResult = new Quaternion(result.x, result.y, result.z, result.w);
                    joints[i].rotation = quatResult;
                }
                
                //Vector3 axis = new Vector3(axisV.x, axisV.y, axisV.z);

               
                //Quaternion rotated = Quaternion.AngleAxis(angle, axis);
                //TODO 
                
                joints[i + 1].position.Set(copy[i + 1].position.x, copy[i + 1].position.y, copy[i + 1].position.z);

            }
        }
    }

}

#endif