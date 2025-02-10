using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecycledScrollList : MonoBehaviour
{
    public RectTransform contentPanel; // Контейнер списка
    public ScrollRect scrollRect;
    public GameObject itemPrefab; // Префаб элемента списка

    private List<GameObject> activeItems = new List<GameObject>();
    private Queue<GameObject> itemPool = new Queue<GameObject>();

    private int totalItems = 1000; // Количество элементов в списке
    private int visibleCount; // Сколько элементов одновременно видно
    private float itemHeight;

    void Start()
    {
        itemHeight = itemPrefab.GetComponent<RectTransform>().rect.height;
        visibleCount = Mathf.CeilToInt(scrollRect.viewport.rect.height / itemHeight) + 2; // Запас +2

        contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, totalItems * itemHeight);
        scrollRect.onValueChanged.AddListener(OnScroll);

        // Инициализация объектов списка
        for (int i = 0; i < visibleCount; i++)
        {
            SpawnItem(i);
        }
    }

    void SpawnItem(int index)
    {
        GameObject item = GetItemFromPool();
        item.transform.SetParent(contentPanel, false);
        item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -index * itemHeight);
        item.GetComponent<ItemUI>().SetData(index); // Устанавливаем данные
        activeItems.Add(item);
    }

    void OnScroll(Vector2 scrollPos)
    {
        float contentY = contentPanel.anchoredPosition.y;
        int firstVisibleIndex = Mathf.Max(0, Mathf.FloorToInt(contentY / itemHeight));

        // Перемещение и обновление данных элементов
        for (int i = 0; i < activeItems.Count; i++)
        {
            int newIndex = firstVisibleIndex + i;
            if (newIndex < totalItems)
            {
                activeItems[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -newIndex * itemHeight);
                activeItems[i].GetComponent<ItemUI>().SetData(newIndex);
            }
        }
    }

    GameObject GetItemFromPool()
    {
        if (itemPool.Count > 0)
        {
            var item = itemPool.Dequeue();
            item.SetActive(true);
            return item;
        }
        return Instantiate(itemPrefab);
    }

    void ReturnToPool(GameObject item)
    {
        item.SetActive(false);
        itemPool.Enqueue(item);
    }
}
