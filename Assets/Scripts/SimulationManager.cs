using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimulationOptions
{
    public static float RADIUS = 0.145f;
    public static float MASS = 20.0f;
    public static float GRAVITY = 9.81f;
    public static float COEFFICIENT_OF_RESTITUTION = 0.8f;
    public static float FRICTION_COEFFICIENT_GROUND = 0.02f;
}

public class SimulationManager : MonoBehaviour {

    public GameObject hud;
    public GameObject model;
    public GameObject stone;
    public GameObject camera;

    public GameObject stonePrefab;
    public GameObject modelPrefab;

    private VelocityMeter vMeterScript;
    private IK_FABRIK_CUSTOM ikScript;
    private CurlingStone curlingScript;
    private CameraControler cameraScript;

    private float resetTimer;
    private bool stopped;

	// Use this for initialization
	void Start () {
        vMeterScript = hud.GetComponent<VelocityMeter>();
        ikScript = model.GetComponent<IK_FABRIK_CUSTOM>();
        curlingScript = stone.GetComponent<CurlingStone>();
        cameraScript = camera.GetComponent<CameraControler>();

        cameraScript.SetPlayer(stone);

        stopped = false;
        resetTimer = 3f;
    }
	
	// Update is called once per frame
	void Update () {

        if (vMeterScript.GetShoot())
        {
            SetStoneParameters();
            vMeterScript.SetShoot(false);
        }        

        if(curlingScript.GetStartShot() && Mathf.Abs(curlingScript.velocity.module()) < 0.1 && curlingScript.GetPreviousVelocity().module() != 0)
        {
            Debug.Log("The Stone is stopped");            
            curlingScript.SetStartShot(false);
            stopped = true;
        }

        if (stopped)
        {
            resetTimer -= Time.deltaTime;
            if(resetTimer <= 0)
            {
                ResetSimulation();
            }
        }
	}

    public void SetStoneParameters()
    {
        curlingScript.SetStartVelocity(vMeterScript.GetFinalVelocity());
        curlingScript.SetStartSpin(vMeterScript.GetFinalSpin());
        curlingScript.SetStartShot(vMeterScript.GetShoot());
    }

    public void ResetSimulation()
    {
        vMeterScript.ResetVariables();
        curlingScript.ResetVariables();
        stopped = false;
        resetTimer = 3f;
        stone = Instantiate(stonePrefab);
        Destroy(model);
        model = Instantiate(modelPrefab);

        vMeterScript = hud.GetComponent<VelocityMeter>();
        ikScript = model.GetComponent<IK_FABRIK_CUSTOM>();
        curlingScript = stone.GetComponent<CurlingStone>();
        cameraScript = camera.GetComponent<CameraControler>();

        ikScript.SetTarget(stone.transform);

        cameraScript.SetPlayer(stone);
    }

}
