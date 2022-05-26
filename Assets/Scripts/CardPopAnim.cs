using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardPopAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cardChild;
	public Card cardScript;

	public void OnPointerEnter(PointerEventData eventData)
	{
		cardScript.CardPopUpAnim(0);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		cardScript.CardPopUpAnim(1);
	}

	

	public void Awake()
	{
		
	}
	// Start is called before the first frame update
	void Start()
    {
		cardChild = transform.Find("Card").gameObject;
		cardScript = cardChild.GetComponent<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
