
using System.Collections;
using UnityEngine;
using TMPro;

public class RewardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private float displayDuration = 3f;

    private Coroutine hideCoroutine;

    public void ShowReward(ItemData item)
    {
        if (item == null || rewardText == null)
            return;

        rewardText.text =
            "REWARD RECEIVED!\n\n" +
            item.ItemName + "\n" +
            "Rarity: " + item.Rarity + "\n" +
            "Value: " + item.Value;

        rewardText.gameObject.SetActive(true);

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        if (rewardText != null)
            rewardText.gameObject.SetActive(false);

        hideCoroutine = null;
    }
}
