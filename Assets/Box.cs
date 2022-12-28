using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject disableEffect;
    
    public void DisableBox()
    {
        //Instantiate(disableEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
