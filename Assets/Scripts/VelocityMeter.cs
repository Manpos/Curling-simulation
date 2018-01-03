using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityMeter : MonoBehaviour {
    public Vector3 initialPosition; 
    public Image velocityBar;
    public Image spinBar;
    public GameObject velocityParent;
    public GameObject spinParent;
    private bool progressUp;
    private bool velocityShot;
    private bool spinShot;
    private float finalVelocityValue;
    private float finalSpinValue;
    private bool shoot;

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

    public void ResetVariables()
    {
        velocityParent.SetActive(true);
        spinParent.SetActive(false);
        velocityBar.fillAmount = 0f;
        spinBar.fillAmount = 0f;
        velocityShot = true;
        progressUp = true;
        shoot = false;
        spinShot = false;
    }

    public bool GetShoot()
    {
        return shoot;
    }

    public void SetShoot(bool b)
    {
        shoot = b;
    }

    public float GetFinalVelocity()
    {
        return finalVelocityValue;
    }

    public float GetFinalSpin()
    {
        return finalSpinValue;
    }

}


