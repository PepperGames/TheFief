using PopUp;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSystem : MonoBehaviour
{
    [SerializeField] protected List<PopUpItem> popUpItems;
    [SerializeField] protected List<PopUpOpenedItem> popUpOpenedItems;


    public virtual void Initialize()
    {
        foreach (var popUpItem in popUpItems)
        {
            SubscribeOpen(popUpItem);
        }
    }

    public virtual void OpenWindowByName(string windowName)
    {
        foreach (var item in popUpItems)
        {
            if (item.windowName.Equals(windowName))
            {
                OpenWindow(item);
                break;
            }
        }
    }

    protected virtual void OpenWindow(PopUpItem popUpItem)
    {
        GameObject window = Instantiate(popUpItem.window, popUpItem.openTransform);

        IOpenable openable = window.GetComponent<IOpenable>();

        Button closeButton = openable.Open();

        PopUpOpenedItem popUpOpenedItem = new PopUpOpenedItem(window, closeButton);

        popUpOpenedItems.Add(popUpOpenedItem);

        SubscribeClose(popUpOpenedItem);
    }

    protected virtual void CloseWindow(PopUpOpenedItem popUpOpenedItem)
    {
        Destroy(popUpOpenedItem.window);
        popUpOpenedItems.Remove(popUpOpenedItem);
    }

    protected virtual void SubscribeOpen(PopUpItem popUpItem)
    {
        popUpItem.openButton.onClick.AddListener(() =>
        {
            OpenWindow(popUpItem);
        });
    }

    protected virtual void SubscribeClose(PopUpOpenedItem popUpOpenedItem)
    {
        popUpOpenedItem.closeButton.onClick.AddListener(() =>
        {
            CloseWindow(popUpOpenedItem);
        });
    }
}

