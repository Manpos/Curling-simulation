using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    public static List<CurlingStone> stonesList;
    private static bool doneOnce;
    private Vector newVelocity;
    private Vector newAngularVelocity;

    private Vector newVelocity2;
    private Vector newAngularVelocity2;

    // Use this for initialization
    void Start ()
    {
        stonesList = new List<CurlingStone>();
        newVelocity = new Vector(0f, 0f, 0f);
        newAngularVelocity = new Vector(0f, 0f, 0f);

        newVelocity2 = new Vector(0f, 0f, 0f);
        newAngularVelocity2 = new Vector(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update ()
    {
#if false
        foreach (CurlingStone curlingStone in stonesList) {
            for (int i = 0; i < stonesList.Count; i++)
            {
                if (curlingStone != stonesList[i])
                {
                    if (Vector.distance(curlingStone.position, stonesList[i].position) < curlingStone.GetRadius() + stonesList[i].GetRadius())
                    {
                        if (!curlingStone.resetSimulationAfterCollision)
                        {
                            /*Debug.Log("Ha xocat amb la pedra: " + i);

                            curlingStone.resetSimulationAfterCollision = true;

                            CalculateCollision(curlingStone, stonesList[i]);

                            curlingStone.ApplyImpulse(newVelocity, newAngularVelocity);
                            stonesList[i].ApplyImpulse(newVelocity2, newAngularVelocity2);

                            curlingStone.SetStartShot(true);
                            //curlingStone.hasCollided = true;
                            stonesList[i].hasCollided = true;*/
                        }
                    }
                }
            }
        }
#endif
#if true
        if (stonesList.Count > 1)
        {
            // Pick a stone from the stonesList
            for (int i = 0; i < stonesList.Count; i++)
            {
                // And compare it with all the others on the list
                for (int j = i + 1; j < stonesList.Count; j++)
                {
                                            // If there's collision
                        if (Vector.distance(stonesList[i].position, stonesList[j].position) < (stonesList[i].GetRadius() + stonesList[j].GetRadius()))
                        {
                            if (!stonesList[i].haveAlreadyCollided)
                            {

                                //Debug.Log("La pedra J es: " + j);

                            if (Mathf.Abs(stonesList[j].velocity.module()) < 0.05)
                            {
                                //Debug.Log("Ha entrat aqui perque el modul es: " + Mathf.Abs(stonesList[j].velocity.module()));
                                newVelocity2.x = 0.06f;
                                newVelocity.x = -0.04f;
                            }
                            else
                            {
                                CalculateCollision(i, j);
                                //Debug.Log("La velocitat de la pedra " + i + " es: X " + newVelocity2.x + " Y " + newVelocity2.y + " Z " + newVelocity2.z);
                                //Debug.Log("La velocitat de la pedra " + j + " es: X " + newVelocity.x + " Y " + newVelocity.y + " Z " + newVelocity.z);
                            }
                                stonesList[i].ApplyImpulse(newVelocity2, newAngularVelocity2);
                                stonesList[j].ApplyImpulse(newVelocity, newAngularVelocity);
                                //stonesList[i].resetSimulationAfterCollision = true;  

                                stonesList[i].SetStartShot(true);
                                //stonesList[j].SetStartShot(true);

                                stonesList[i].hasCollided = true;
                            //stonesList[j].hasCollided = true;

                            stonesList[i].haveAlreadyCollided = true;

                        }
                    }

                    
                }
            }
        }
#endif
    }

    public static void AddStone(CurlingStone curlingStone)
    {
        stonesList.Add(curlingStone);
        //Debug.Log(stonesList.IndexOf(curlingStone));
        doneOnce = false;
        //Debug.Log("Stone added");
    }

    public void CalculateCollision(int i, int j)
    {
         Vector normal2 = (stonesList[i].position - stonesList[j].position).normalize();

        float relativeNormal = Vector.dot((stonesList[j].velocity - stonesList[i].velocity), normal2);

         float newVelocity2Magnitude = relativeNormal * (0.9f + 1) / 2;

         float newVelocityMagnitude = stonesList[j].velocity.module() - newVelocity2Magnitude;

         newVelocity2 = newVelocity2Magnitude * normal2;

        Vector tangent = new Vector(-normal2.y, normal2.x, normal2.z);

        newVelocity = newVelocityMagnitude * tangent.normalize();
        
    }
    
}
