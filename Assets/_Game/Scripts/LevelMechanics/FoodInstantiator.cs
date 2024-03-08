using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject[] protein, vegfruits, sweets = {};
    [SerializeField] private GameObject plateP, plateV, plateS;
    [SerializeField] private float rotateSpeed;
    public bool hasProtein, hasVegFruits, hasSweets = false;

    private void Awake() 
    {
        plateP = this.gameObject.transform.Find("Protein").gameObject;
        plateV = this.gameObject.transform.Find("VegFruits").gameObject;
        plateS = this.gameObject.transform.Find("Sweets").gameObject;
    }

    private void Update() 
    {
        //continuously rotate food objects
        if (hasProtein || hasVegFruits || hasSweets)
        {
            plateP.transform.Rotate(0, rotateSpeed, 0);
            plateV.transform.Rotate(0, rotateSpeed, 0);
            plateS.transform.Rotate(0, rotateSpeed, 0);
        }
    }

    public void InsantiateFood()
    {
        if (!hasProtein)
        {
            GameObject foodP = protein[Random.Range(0, protein.Length)];
            Instantiate(foodP, plateP.transform);
            hasProtein = true;
        }

        if (!hasVegFruits)
        {
            GameObject foodV = vegfruits[Random.Range(0, vegfruits.Length)];
            Instantiate(foodV, plateV.transform);
            hasVegFruits = true;
        }

        if (!hasSweets)
        {
            GameObject foodS = sweets[Random.Range(0, sweets.Length)];
            Instantiate(foodS, plateS.transform);
            hasSweets = true;
        }

        AudioManager.Instance.PlaySFX("Pop");
    }
}
