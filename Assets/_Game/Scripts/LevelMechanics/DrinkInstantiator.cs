using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrinkInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject[] drink1, drink2 = {};
    [SerializeField] private GameObject slot1, slot2;
    public MeshRenderer cup1, cup2;
    public bool hasDrink1, hasDrink2 = false;

    private void Awake() 
    {
        slot1 = this.gameObject.transform.Find("Left").gameObject;
        slot2 = this.gameObject.transform.Find("Right").gameObject;
        cup1 = slot1.GetComponent<MeshRenderer>();
        cup2 = slot2.GetComponent<MeshRenderer>();
    }
    public void InstantiateDrink()
    {
        if (!hasDrink1)
        {
            GameObject drinkL = drink1[Random.Range(0, drink1.Length)];
            Instantiate(drinkL, slot1.transform);
            cup1.enabled = false;
            hasDrink1 = true;
        }

        if (!hasDrink2)
        {
            GameObject drinkR = drink2[Random.Range(0, drink2.Length)];
            Instantiate(drinkR, slot2.transform);
            cup2.enabled = false;
            hasDrink2 = true;
        }
    }
}
