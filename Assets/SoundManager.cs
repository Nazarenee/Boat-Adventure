using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public Button soundToggleButton;
    public TMP_Text buttonText; // Use TMP_Text instead of Text
    private bool isSoundOn = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        UpdateButtonText();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        audioSource.mute = !isSoundOn;

        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        buttonText.text = isSoundOn ? "SOUND OFF" : "SOUND ON";
    }
}