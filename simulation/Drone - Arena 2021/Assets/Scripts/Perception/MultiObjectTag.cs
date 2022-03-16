using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;

/// <summary>
/// Escolhe um objeto para ativar, e desativa os outros.
/// </summary>
[AddComponentMenu("Perception/RandomizerTags/MultiObjectTag")]
public class MultiObjectTag : MultipleChoiceTag
{
    [Tooltip("Objetos que podem ser escolhidos para ativar ou desativar.")] [SerializeField] GameObject[] objects;

    /// <summary>
    /// Desativa todos os objetos na inicialização.
    /// </summary>
    void Start()
    {
        DisabelAll();
    }
    
    /// <summary>
    /// Ativa o objeto segunda a escolha aleatória. 
    /// </summary>
    /// <param name="choice">Variável aleatória entre 0 e 1, indicando qual objeto será ativado.</param>
    public override void SetChoice(float choice)
    {
        DisabelAll();

        int index = float2choice(choice, objects.Length);

        GameObject objchoice = objects[index];

        if(objchoice != null)
        {
            objchoice.SetActive(true);
        }
    } 

    /// <summary>
    /// Desativa todos os objetos.
    /// </summary>
    private void DisabelAll()
    {
        foreach  (GameObject obj in objects)
        {
            if(obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}

