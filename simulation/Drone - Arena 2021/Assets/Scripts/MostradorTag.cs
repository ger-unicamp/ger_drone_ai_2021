using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.GroundTruth;

[AddComponentMenu("Perception/RandomizerTags/MostradorTag")]
public class MostradorTag : RandomizerTag
{

    MeshRenderer renderer;
    Labeling labeling;
    
    string base_label;

    void Start()
    {
        renderer = this.GetComponent<MeshRenderer>();
        labeling = this.GetComponent<Labeling>();
        base_label = labeling.labels[0];    
    }

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

    } 
}

