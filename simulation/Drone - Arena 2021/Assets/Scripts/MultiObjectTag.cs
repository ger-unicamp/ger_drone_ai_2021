using UnityEngine;
using UnityEngine.Perception.Randomization.Randomizers;
[AddComponentMenu("Perception/RandomizerTags/MultiObjectTag")]
public class MultiObjectTag : MultipleChoiceTag
{
    [SerializeField] GameObject[] objects;

    void Start()
    {
        foreach  (GameObject obj in objects)
        {
            if(obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    public override void SetChoice(float choice)
    {
        foreach  (GameObject obj in objects)
        {
            if(obj != null)
            {
                obj.SetActive(false);
            }
        }

        int index = float2choice(choice, objects.Length);

        GameObject objchoice = objects[index];

        if(objchoice != null)
        {
            objchoice.SetActive(true);
        }

        //Debug.Log(string.Format("{0} - {1} - {2} - {3}", choice, index, objects.Length, objects));

    } 
}

