using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private FoodType foodType;
    private Animator bobbyAnim;
    private FoodInstantiator foodInst;
    private DrinkInstantiator drinkInst;
    private DiningActions diningAct;

    private void Awake() 
    {
        bobbyAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();
        foodInst = FindObjectOfType<FoodInstantiator>();
        drinkInst = FindObjectOfType<DrinkInstantiator>();
        diningAct = FindObjectOfType<DiningActions>();
    }
    public void GetFoodType()
    {
        bobbyAnim.Play("Chewing");

        switch (foodType)
        {
            case FoodType.Protein:
                DataManager.Instance.SubHealth(15);
                DataManager.Instance.AddHunger(30);
                diningAct.EatFood(0);
                foodInst.hasProtein = false;
                gameObject.SetActive(false);
            break;

            case FoodType.VegFruits:
                DataManager.Instance.SubHealth(10);
                DataManager.Instance.AddHunger(10);
                diningAct.EatFood(1);
                foodInst.hasVegFruits = false;
                gameObject.SetActive(false);
            break;

            case FoodType.Sweets:
                DataManager.Instance.SubHealth(20);
                DataManager.Instance.AddHunger(15);
                diningAct.EatFood(2);
                foodInst.hasSweets = false;
                gameObject.SetActive(false);
            break;

            case FoodType.Soda:
                DataManager.Instance.SubHealth(30);
                DataManager.Instance.AddHunger(10);
                diningAct.EatFood(3);
                drinkInst.hasDrink1 = false;
                drinkInst.cup1.enabled = true;
                gameObject.SetActive(false);
            break;

            case FoodType.Water:
                DataManager.Instance.SubHealth(1);
                DataManager.Instance.AddHunger(5);
                diningAct.EatFood(4);
                drinkInst.hasDrink2 = false;
                drinkInst.cup2.enabled = true;
                gameObject.SetActive(false);
            break;
        }

        Debug.Log("food type is " + foodType);
    }
}
