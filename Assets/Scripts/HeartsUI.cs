using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartsUI : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Transform heartsContainer;
    [SerializeField] private GameObject heartPrefab;

    private List<Image> heartImages = new();

    public void InitalizeHearts(int maxHearts)
    {
        foreach (var heart in heartImages)
        {
            if (heart != null)
            {
                Destroy(heart.gameObject);
            }
        }
        heartImages.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, heartsContainer);
            Image heartImage = heartObj.GetComponent<Image>();
            if (heartImage != null)
            {
                heartImages.Add(heartImage);
            }
        }
    }

    public void UpdateHearts(int currentHearts, int maxHearts)
    {
        if (heartImages.Count != maxHearts)
        {
            InitalizeHearts(maxHearts);
        }

        for (int i = 0; i < heartImages.Count; i++)
        {
            if (heartImages[i] != null)
            {
                heartImages[i].sprite = i < currentHearts ? fullHeart : emptyHeart;
            }
        }
    }
}
