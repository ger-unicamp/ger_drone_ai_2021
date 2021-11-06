using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.GroundTruth;

/// <summary>
/// Randomiza a textura de um mostrador digital para o Desafio Petrobrás de Robótica.
/// </summary>
[AddComponentMenu("Perception/RandomizerTags/MostradorTag")]
public class MostradorTag : RandomizerTag
{

    #pragma warning disable 108
    MeshRenderer renderer;
    Labeling labeling;
    
    string base_label;

    /// <summary>
    /// Procura os componentes necessários.
    /// </summary>
    void Start()
    {
        renderer = this.GetComponent<MeshRenderer>();
        labeling = this.GetComponent<Labeling>();
        base_label = labeling.labels[0];    
    }

    /// <summary>
    /// Define o número para ser colocado no mostrador.
    /// </summary>
    /// <param name="digit1">Dezena do primeiro número (0-9).</param>
    /// <param name="digit2">Unidade do primeiro número (0-9).</param>
    /// <param name="digit3">Unidade do segunda número (0-9)</param>
    /// <param name="minus">Se o segundo número é negativo (true).</param>
    /// <param name="one">Se o segundo número possui dezena=10 (true).</param>   
    public void SetNumber(int digit1, int digit2, int digit3, bool minus, bool one)
    {   
        string number = "";
        number += System.Convert.ToString(digit1)+System.Convert.ToString(digit2);
        number += "_";
        
        if(minus)
        {
            number += "-";
        }
        if(one)
        {
            number += "1";
        }

        number += System.Convert.ToString(digit3);

        string sprite_name = "img_";
        sprite_name += number;


        Sprite sprite = Resources.Load(sprite_name, typeof(Sprite)) as Sprite;

        Debug.Log(sprite_name);
        Debug.Log(sprite);

        renderer.material.mainTexture = sprite.texture;
        //renderer.sprite = sprite;

        labeling.labels[0] = base_label+"_"+number;
        labeling.RefreshLabeling();
    } 
}

