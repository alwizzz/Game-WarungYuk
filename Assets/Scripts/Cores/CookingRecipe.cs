using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class CookingRecipe 
{
    public string region;
    public List<string> completedDishes;
    public List<string> rawIngredients;
    public List<string> processedIngredients;
    public List<string> processes;
    public List<DishProcessTransition> dishProcessTransitions;
    public List<IngredientProcessTransition> ingredientProcessTransitions;
    public List<DishState> plateDishStates;
    public List<DishState> bowlDishStates;


    [System.Serializable]
    public class DishState
    {
        public string name;
        public List<DishTransition> transitions;

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

    public List<DishProcessTransition> GetDishProcessTransitions(string name)
    {
        if(dishProcessTransitions == null) { return null; }

        List<DishProcessTransition> result = new List<DishProcessTransition>();
        foreach (DishProcessTransition dpt in dishProcessTransitions)
        {
            if(dpt.process == name)
            {
                result.Add(dpt);
            }
        }

        return result;
    }

    public List<IngredientProcessTransition> GetIngredientProcessTransitions(string name)
    {
        if (ingredientProcessTransitions == null) { return null; }

        List<IngredientProcessTransition> result = new List<IngredientProcessTransition>();
        foreach (IngredientProcessTransition ipt in ingredientProcessTransitions)
        {
            if (ipt.process == name)
            {
                result.Add(ipt);
            }
        }

        return result;
    }

    public List<DishState> GetPlateDishStates()
    {
        return plateDishStates;
    }
    public List<DishState> GetBowlDishStates()
    {
        return bowlDishStates;
    }
}
