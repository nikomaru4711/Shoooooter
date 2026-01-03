using UnityEngine;

public class IgaguriController : MonoBehaviour
{
    private AudioManager audioManager;
    public AudioClip collideSound;
    private bool isTouched = false;
    private bool onGround = false;
    [SerializeField] private bool isAmmo = false;

    //いがぐりを発射する。dirは単位ベクトルで与える
    public void Shoot(Vector3 dir)
    {
        if (isAmmo)
        {
            GetComponent<Rigidbody>().AddForce(dir * 10000);
        } else
        {
            GetComponent<Rigidbody>().AddForce(dir * 500);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        if (!isTouched && collision.gameObject.tag == "prize")
        {
            if(GetComponent<ParticleSystem>() != null)
            {
                GetComponent<ParticleSystem>().Play();
                isTouched = true;
            }
        }

        float distance = (collision.contacts[0].point - Camera.main.transform.position).magnitude;
        if (!onGround && distance < 40.0f)
        {
            audioManager.PlaySound(collideSound, 0.15f);
            if(collision.gameObject.tag == "ground")
            {
                onGround = true;
            }
        }
        Destroy(gameObject, 20f);
    }
}
