using System;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Drone/CameraRandomizer")]
public class CameraRandomizer : Randomizer
{
    public FloatParameter fov;


    protected override void OnIterationStart()
    {
        var tags = tagManager.Query<CameraTag>();
        foreach (var tag in tags)   //faz o loop da chamada do randomizador para cada objeto com CameraTag
        {
            tag.SetCamera(fov.Sample());    //chama o randomizador passando os parâmetro
                                                                                            //contendo valores aleatorios de 0 a 1 pelo .Sample()
        }
    }
}
