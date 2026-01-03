using UnityEngine;

public class IgaguriGenerator : MonoBehaviour
{

    [SerializeField] private GameObject igaguriPref;
    [SerializeField] private GameObject ammoPref;

    public GameObject GenerateIgaguri()
    {
        return Instantiate(igaguriPref);
    }
    public GameObject GenerateAmmo()
    {
        return Instantiate(ammoPref);
    }
}
