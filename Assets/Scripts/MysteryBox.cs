
using System.Collections;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private KeyCode openKey = KeyCode.E;
    [SerializeField] private Transform player;
    [SerializeField] private float interactionDistance = 2.5f;

    [Header("Possible Rewards")]
    [SerializeField] private ItemData[] possibleRewards;

    [Header("Systems")]
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private RewardUI rewardUI;
    [SerializeField] private InteractionPrompt interactionPrompt;

    [Header("Reward Model")]
    [SerializeField] private Transform rewardSpawnPoint;
    [SerializeField] private float rewardDisplayHeight = 1f;
    [SerializeField] private float rewardDisplayScale = 0.5f;

    [Header("Opening Animation")]
    [SerializeField] private float animationDuration = 1.2f;
    [SerializeField] private float riseHeight = 0.5f;

    private bool isOpened;
    private Vector3 originalPosition;
    private static MysteryBox nearestBox;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (player == null || isOpened)
            return;

        float distance = Vector3.Distance(
            player.position, transform.position);

        if (distance <= interactionDistance)
        {
            if (nearestBox == null || nearestBox.isOpened)
            {
                nearestBox = this;
            }
            else if (distance < Vector3.Distance(
                player.position, nearestBox.transform.position))
            {
                nearestBox = this;
            }
        }

        if (nearestBox == this && distance > interactionDistance)
            nearestBox = null;

        if (nearestBox == this && Input.GetKeyDown(openKey))
            OpenBox();
    }

    void LateUpdate()
    {
        if (interactionPrompt == null || player == null)
            return;

        bool showPrompt = nearestBox != null
            && !nearestBox.isOpened
            && Vector3.Distance(
                player.position,
                nearestBox.transform.position)
                <= nearestBox.interactionDistance;

        interactionPrompt.ShowPrompt(showPrompt);
    }

    void OpenBox()
    {
        if (possibleRewards == null || possibleRewards.Length == 0)
        {
            Debug.LogWarning("No rewards assigned to " + name);
            return;
        }

        float totalWeight = 0f;

        foreach (ItemData item in possibleRewards)
        {
            if (item != null && item.DropWeight > 0f)
                totalWeight += item.DropWeight;
        }

        if (totalWeight <= 0f)
        {
            Debug.LogWarning("No valid reward weights!");
            return;
        }

        float randomValue = Random.Range(0f, totalWeight);
        ItemData reward = null;

        foreach (ItemData item in possibleRewards)
        {
            if (item == null || item.DropWeight <= 0f)
                continue;

            randomValue -= item.DropWeight;

            if (randomValue <= 0f)
            {
                reward = item;
                break;
            }
        }

        if (reward == null)
            return;

        isOpened = true;

        if (inventory != null)
            inventory.AddItem(reward);
        else
            Debug.LogWarning("InventorySystem is not assigned!");

        if (rewardUI != null)
            rewardUI.ShowReward(reward);

        SpawnRewardModel(reward);

        if (nearestBox == this)
            nearestBox = null;

        if (interactionPrompt != null)
            interactionPrompt.ShowPrompt(false);

        StartCoroutine(AnimateOpening());
    }

    void SpawnRewardModel(ItemData reward)
    {
        if (reward.ItemPrefab == null)
        {
            Debug.LogWarning(
                "No 3D model assigned to reward: " + reward.ItemName);
            return;
        }

        Vector3 spawnPosition = rewardSpawnPoint != null
            ? rewardSpawnPoint.position
            : transform.position + Vector3.up * rewardDisplayHeight;

        GameObject spawnedItem = Instantiate(
            reward.ItemPrefab,
            spawnPosition,
            Quaternion.identity);

        spawnedItem.transform.localScale *= rewardDisplayScale;

        Debug.Log("Spawned reward model: " + reward.ItemName);
    }

    IEnumerator AnimateOpening()
    {
        float elapsed = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition =
            originalPosition + Vector3.up * riseHeight;
        Quaternion startRotation = transform.rotation;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(
                elapsed / animationDuration);

            transform.position = Vector3.Lerp(
                startPosition, targetPosition, progress);

            transform.rotation = startRotation
                * Quaternion.Euler(0f, 360f * progress, 0f);

            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = startRotation;
    }

    void OnDisable()
    {
        if (nearestBox == this)
            nearestBox = null;
    }
}
