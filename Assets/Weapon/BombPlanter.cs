using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombPlanter : Weapon, IWeapon
{
    private PlayerAttackController attackController;




    public float timeToPlant;
    private float timeToElapse;
    private bool m_isPlanted = false;

    private WeaponUnit unit;
    private Bomb bomb;




    void Start()
    {
        attackController = transform.parent.GetComponent<WeaponManager>().attackController;
        unit = GetComponent<WeaponUnit>();
        timeToElapse = timeToPlant;


    }

    void Update()
    {
        
    }
    public override void Trigger()
    {


        m_isPlanted = true;
        Debug.Log("Plant Bomb Successfully"); 

        if (m_isPlanted && attackController.bomb == null)
        {
            attackController.PlantBomb();
            m_isPlanted = false;
        }
    }

    public void CheckPlantTimeChange()
    {
        if (timeToElapse != timeToPlant)
        {
            attackController.OnTimeElapsing.Invoke(timeToPlant - timeToElapse, timeToPlant);
            Debug.Log("Time elapsing");
        }
        else
        {
            attackController.OnSliderUpdated.Invoke();

        }
    }
    public void CheckPlanting()
    {
        if ( !m_isPlanted && timeToElapse > 0 && attackController.bomb == null)
        {
            timeToElapse -= Time.deltaTime;
            PreviewBombPlacement();
        }
        else
        {
            if (timeToElapse <= 0)
                Trigger();
            attackController.bombPreviewGO.GetComponent<SpriteRenderer>().enabled = false;
            timeToElapse = timeToPlant;
        }


    }

    public void PreviewBombPlacement()
    {
        if (m_isPlanted && attackController.bombPreviewGO == null)
            return;

        attackController.bombPreviewGO.GetComponent<SpriteRenderer>().enabled = true;

    }
    public void Shoot()
    {
        Debug.Log("Checking Shoot method");
        CheckPlantTimeChange();
        CheckPlanting();

    }

    public void Reload()
    {
        Debug.Log("You can't reload");
    }

    public void IncreaseAmmo()
    {
        Debug.Log("You can't increase ammo");

    }
}
