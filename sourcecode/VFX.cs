
using UnityEngine;

using UnityEngine.Audio;

public class VFX : MonoBehaviour
{
    public GameObject usedVFX;
    public AudioMixerGroup SFXOutput;
    private GameObject cloneVFX;
    public AudioClip soundFX;
    public float scaleX = 1f;
    public float scaleY = 1f ;
    public float scaleZ = 1f ; 

    void Start()
    {
        if (SFXOutput == null)
        {
            Debug.LogError("Error: Please assign an output for the VFX script in the Inspector!");
        }
        createVFX();
        
    }

    private void Update()
    {
        if (cloneVFX == null)
        {
            createVFX();
        }
    }
    public void createVFX()
    {
        cloneVFX = Instantiate(usedVFX, gameObject.transform.position, usedVFX.transform.rotation);
        cloneVFX.transform.SetParent(gameObject.transform);
        cloneVFX.SetActive(false);
        cloneVFX.AddComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") ) {
            cloneVFX.SetActive(true);
            cloneVFX.transform.localScale = new Vector3(scaleX,scaleY,scaleZ);
            AudioSource audio = cloneVFX.GetComponent<AudioSource>();
            audio.clip = soundFX;
            audio.volume += 3;
            audio.Play();
            audio.outputAudioMixerGroup = SFXOutput;
        }
    }

}
