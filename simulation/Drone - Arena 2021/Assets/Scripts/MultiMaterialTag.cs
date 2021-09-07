using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.GroundTruth;

[AddComponentMenu("Drone/RandomizerTags/MultiMaterialTag")]
public class MultiMaterialTag : MultipleChoiceTag
{
    [SerializeField] Material[] materials;
    [SerializeField] string[] labels;
    [SerializeField] IdLabelConfig labelConfig;

    #pragma warning disable 108
    MeshRenderer renderer;
    Labeling labeling;
    
    string base_label;

    void Start()
    {
        renderer = this.GetComponent<MeshRenderer>();
        labeling = this.GetComponent<Labeling>();
        base_label = labeling.labels[0];    
    }

    public override void SetChoice(float choice)
    {

        int index = float2choice(choice, materials.Length);

        this.renderer.material = materials[index];
        labeling.labels[0] = base_label+labels[index];
        labeling.RefreshLabeling();
    } 
}

