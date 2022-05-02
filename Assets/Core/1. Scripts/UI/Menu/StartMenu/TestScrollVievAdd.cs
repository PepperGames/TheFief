using UnityEngine;

public class TestScrollVievAdd : MonoBehaviour
{
    public GameObject ScrollViewGameObject;

    public GameObject[] cards;

    private void Start()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            GameObject card = Instantiate(cards[i]) as GameObject;
            
            if (ScrollViewGameObject != null)
            {
                card.transform.SetParent(ScrollViewGameObject.transform, false);
            }
        }
    }


}
