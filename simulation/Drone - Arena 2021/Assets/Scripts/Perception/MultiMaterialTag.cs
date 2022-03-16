using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
using UnityEngine.Perception.GroundTruth;

/// <summary>
/// Escolhe um material entre os disponíveis.
/// </summary>
[AddComponentMenu("Drone/RandomizerTags/MultiMaterialTag")]
public class MultiMaterialTag : MultipleChoiceTag
{
    [Tooltip("Materiais que podem ser escolhidos.")] [SerializeField] Material[] materials;
    [Tooltip("Label para ser utilizada com cada material. Deve possuir a mesma quantidade que 'materials'.")] [SerializeField] string[] labels;
    [Tooltip("IdLabelConfig que terá label alterada.")] [SerializeField] IdLabelConfig labelConfig;

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
    /// Define o material do objeto segundo a amostra.
    /// </summary>
    /// <param name="choice">Variável aleatória entre 0 e 1.</param>
    public override void SetChoice(float choice)
    {

        int index = float2choice(choice, materials.Length);

        this.renderer.material = materials[index];
        labeling.labels[0] = base_label+labels[index];
        labeling.RefreshLabeling();
    } 
}

