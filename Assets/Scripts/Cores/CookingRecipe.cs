using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CookingRecipe 
{
    public string region;
    public string[] completedDishes;
    public string[] rawIngredients;
    public string[] processedIngredients;
    public string[] processes;
    public string[] dishProcessTransitions;
    public string[] ingredientProcessTransitions;
    public string[] dishStates;


    [System.Serializable]
    public class DishState
    {
        public string name;
        public DishTransition[] transitions;

        [System.Serializable]
        public class DishTransition
        {
            public string input;
            public string nextState;
            public bool intoCompletedDish;
        }
    }

    [System.Serializable]
    public class DishProcessTransition
    {
        public string process;
        public string input;
        public string output;
        public bool isSynchronous;
    }

    [System.Serializable]
    public class IngredientProcessTransition
    {
        public string process;
        public string input;
        public string output;
        public bool isSynchronous;
    }
}
