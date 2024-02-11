using UnityEngine;
using Photon.Pun;
using System.Collections;

public class CheckCollider : MonoBehaviour
{
    
    public GameObject healEffect;
    public GameObject ammoEffect;
    
    
    
    public float checkRaduis;



    void Update()
    {
        CheckBounds();
    }
    
    
    private void CheckBounds()
    {
        Vector2 direction = new Vector2(transform.position.x + checkRaduis, transform.position.y + checkRaduis);
        RaycastHit2D bound = Physics2D.CircleCast(transform.position, checkRaduis, direction);
        if (bound.collider != null)
        {
            if (bound.collider != this.gameObject.GetComponent<BoxCollider2D>())
            {

                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                //Debug.Log(bound.collider);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "HP")
        {
            this.gameObject.GetComponent<HealthSystem>().GetHealth(8);
            GameObject particle = Instantiate(healEffect, collision.transform.position, Quaternion.identity);
            Destroy(particle, 2f);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "DoorTrigger")
        {
            //PlayerPhotonManager.Instance.DestroyPlayer();
            TransitionHandler.Instance.MapSwitchingCoroutine(collision.gameObject);

        }
        else if (collision.gameObject.tag == "Ammo")
        {
            if (GUIManager.instance.gameMode == GUIManager.GameMode.PVP)
            {
                this.gameObject.GetComponent<PlayerAttackController>().weapon.GetComponent<IWeapon>().IncreaseAmmo();
                GameObject particle = Instantiate(ammoEffect, collision.transform.position, Quaternion.identity);
                Destroy(particle, 2f);
            }
            if (GUIManager.instance.gameMode == GUIManager.GameMode.PVE)
            {
                this.gameObject.GetComponent<PlayerAttackController>().weapon.GetComponent<IWeapon>().IncreaseAmmo();
                GameObject particle = Instantiate(ammoEffect, collision.transform.position, Quaternion.identity);
                Destroy(particle, 2f);
            }

            Destroy(collision.gameObject);
        }
    }
    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRaduis);
    }

}
