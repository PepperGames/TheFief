using System;
using UnityEngine;
using UnityEngine.UI;

namespace PopUp
{
    [Serializable]
    public class PopUpItem
    {
        public string windowName;
        public GameObject window;
        public Button openButton;

        public Transform openTransform;
    }

    [Serializable]
    public class PopUpOpenedItem
    {
        public GameObject window;
        public Button closeButton;

        public PopUpOpenedItem(GameObject window, Button closeButton)
        {
            this.window = window;
            this.closeButton = closeButton;
        }
    }
}
