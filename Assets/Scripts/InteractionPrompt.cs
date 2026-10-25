
using TMPro;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text promptText;

    public void ShowPrompt(bool show)
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(show);
        }
    }
}
