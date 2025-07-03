using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel;

    public void ShowPopup()
    {
        popupPanel.SetActive(true); // Tampilkan pop-up
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false); // Sembunyikan pop-up
    }
}
