using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    private int score = 0;

    public void init()
    {
        score = 0;
        uiManager.UpdateScore(score);
    }
    public void AddScore(int point)
    {
        score += point;
        uiManager.UpdateScore(score);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "prize" && !other.gameObject.GetComponent<prize>().isAdded)
        {
            AddScore(other.gameObject.GetComponent<prize>().pointValue);
            other.gameObject.GetComponent<prize>().isAdded = true;
        }
    }
}
