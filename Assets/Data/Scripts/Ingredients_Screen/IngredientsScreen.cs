using System.Collections.Generic;
using UnityEngine;

public class IngredientsScreen : MonoBehaviour
{

    public List<MonsterPart> monsterParts; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenIngredientScreen(List<MonsterPart> parts)
    {
        monsterParts = parts;
    }

}
