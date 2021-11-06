using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;

/// <summary>
/// Tag para indicar que o objeto irá realizar uma escolha.
/// </summary>
public abstract class MultipleChoiceTag : RandomizerTag
{
    /// <summary>
    /// Converte uma amostra entre 0 e 1 para uma escolha discreta.
    /// </summary>
    /// <param name="choice">Amostra entre 0 e 1</param>
    /// <param name="nChoice">Quantidade possível de escolhas</param>
    /// <returns>Valor inteiro entre 0 e nChoice+1, inclusive.</returns>
    protected int float2choice(float choice, int nChoice)
    {
        return (int) (choice*(float)nChoice);
    }

    /// <summary>
    /// Deve setar a escolha de acordo com a amostra.
    /// </summary>
    /// <param name="choice">Variável aleatória entre 0 e 1.</param>
    abstract public void SetChoice(float choice);

}

