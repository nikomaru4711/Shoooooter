using UnityEngine;

public class IgaguriGenerator : MonoBehaviour
{

    [SerializeField] private GameObject igaguriPref;

    public GameObject GenerateIgaguri()
    {
        return Instantiate(igaguriPref);
    }
}
