using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;

public abstract class MultipleChoiceTag : RandomizerTag
{
    protected int float2choice(float choice, int nChoice)
    {
        return (int) (choice*(float)nChoice);
    }

    abstract public void SetChoice(float choice);

}

