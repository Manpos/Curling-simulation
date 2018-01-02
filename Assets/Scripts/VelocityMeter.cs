using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityMeter : MonoBehaviour {

    public Image velocityBar;
    public Image spinBar;
    public GameObject velocityParent;
    public GameObject spinParent;
    private static bool progressUp;
    private bool velocityShot;
    private bool spinShot;
    public static float finalVelocityValue;
    public static float finalSpinValue;
    public static bool shoot;

    void Start()
    {
        spinParent.SetActive(false);
        velocityBar.fillAmount = 0f;
        spinBar.fillAmount = 0f;
        velocityShot = true;
        progressUp = true;
        shoot = false;
        spinShot = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (!spinShot && !shoot)
            {
                finalVelocityValue = velocityBar.fillAmount;
                velocityParent.SetActive(false);
                spinParent.SetActive(true);
                velocityShot = false;
                progressUp = true;
                spinShot = true;
            }else if(!shoot)
            {
                spinParent.SetActive(false);
                finalSpinValue = spinBar.fillAmount;
                shoot = true;
            }
            
        }

        if (velocityShot)
        {
            if (velocityBar.fillAmount < 1 && progressUp)
            {
                velocityBar.fillAmount += Time.deltaTime * 0.5f;
                if (velocityBar.fillAmount >= 1)
                {
                    progressUp = false;
                }
            }
            else if (velocityBar.fillAmount > 0 && !progressUp)
            {
                velocityBar.fillAmount -= Time.deltaTime * 0.5f;
                if (velocityBar.fillAmount <= 0)
                {
                    progressUp = true;
                }
            }
        }
        else
        {
            if (spinBar.fillAmount < 1 && progressUp)
            {
                spinBar.fillAmount += Time.deltaTime * 0.5f;
                if (spinBar.fillAmount >= 1)
                {
                    progressUp = false;
                }
            }
            else if (spinBar.fillAmount > 0 && !progressUp)
            {
                spinBar.fillAmount -= Time.deltaTime * 0.5f;
                if (spinBar.fillAmount <= 0)
                {
                    progressUp = true;
                }
            }
        }
        
    }

    public static void ResetVariables()
    {        
        progressUp = true;
        shoot = false;
    }

}


