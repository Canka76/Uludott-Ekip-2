using UnityEngine;

public class RandomChildDeactivator : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Array of parent GameObjects with the target child and PipeDamage script.")]
    public GameObject[] parentObjects;

    [Tooltip("Name of the child object to deactivate.")]
    public string childName = "ChildToDeactivate";

    [Tooltip("Percentage of parent objects to affect (0 to 100).")]
    [Range(0, 100)]
    public int percentageToDeactivate = 50;

    void Start()
    {
        DeactivateChildrenAndActivatePipeDamage();
    }

    void DeactivateChildrenAndActivatePipeDamage()
    {
        if (parentObjects == null || parentObjects.Length == 0)
        {
            Debug.LogWarning("No parent objects assigned.");
            return;
        }

        int countToAffect = Mathf.RoundToInt(parentObjects.Length * (percentageToDeactivate / 100f));
        GameObject[] shuffledParents = ShuffleArray(parentObjects);

        for (int i = 0; i < countToAffect; i++)
        {
            GameObject parent = shuffledParents[i];

            // Deactivate the specified child
            Transform child = parent.transform.Find(childName);
            if (child != null)
            {
                child.gameObject.SetActive(false);
                Debug.Log($"Deactivated child '{childName}' in parent '{parent.name}'.");
            }
            else
            {
                Debug.LogWarning($"Child '{childName}' not found in parent '{parent.name}'.");
            }

            // Activate the PipeDamage script's shouldFire property
            PipeDamage pipeDamage = parent.GetComponent<PipeDamage>();
            if (pipeDamage != null)
            {
                pipeDamage.shouldFire = true;
                Debug.Log($"Activated PipeDamage (shouldFire = true) on parent '{parent.name}'.");
            }
            else
            {
                Debug.LogWarning($"PipeDamage script not found on parent '{parent.name}'.");
            }
        }
    }

    GameObject[] ShuffleArray(GameObject[] array)
    {
        GameObject[] newArray = (GameObject[])array.Clone();
        for (int i = newArray.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (newArray[i], newArray[randomIndex]) = (newArray[randomIndex], newArray[i]);
        }
        return newArray;
    }
}
