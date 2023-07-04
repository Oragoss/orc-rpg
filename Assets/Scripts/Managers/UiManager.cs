using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.Managers
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager ui;

        [SerializeField] GameObject openInventoryButton;
        [SerializeField] GameObject closeInventoryButton;
        [SerializeField] List<GameObject> slots;

        private void Awake()
        {
            if (ui == null)
            {
                DontDestroyOnLoad(gameObject);
                ui = this;
            }
            else if (ui != this)
            {
                Destroy(gameObject);
            }
        }

        public void UpdatePlayerInventory(List<Inventory> inventory)
        {
            for (var i = 0; i < inventory.Count; i++)
            {
                //TODO: This grabs the slot background, we want the image inside the slot to change
                var slotSprite = slots[i].transform.GetChild(0).GetComponent<Image>();
                var slotText = slots[i].transform.GetChild(1).GetComponent<TMP_Text>();
                if (slotSprite && slotText)
                {
                    slotSprite.sprite = inventory[i].Sprite;
                    slotText.text = inventory[i].Amount.ToString();
                }
                else
                {
                    Debug.LogError($"Could not find the slot image or text for {slots[i]}.");
                }
            }
        }

        public void TogglePlayerInventory()
        {
            if (openInventoryButton.activeSelf)
            {
                openInventoryButton.GetComponent<Button>().onClick.Invoke();
            }
            else if (closeInventoryButton.activeSelf)
            {
                closeInventoryButton.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}