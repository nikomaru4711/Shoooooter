using UnityEngine;

public class Button : MonoBehaviour
{
    public void onPointerEnter()
    {
        transform.position += Vector3.up * 0.3f;
        transform.localScale = Vector3.one * 1.1f;
    }

    public void onPointerExit()
    {
        transform.position -= Vector3.up * 0.3f;
        transform.localScale = Vector3.one;
    }
}
