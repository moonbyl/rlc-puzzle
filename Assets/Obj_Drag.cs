using UnityEngine;

public class Obj_Drag : MonoBehaviour
{
    public Vector2 SavePosisi;
    public bool IsDiatasObj;
    private bool IsTerkunci = false;
    private Vector2 originalScale;
    public int ID; // ID objek ini
    private Tempat_Drop tempatDropAsal; // tempat drop yang terakhir ditempati


    private Tempat_Drop tempatDrop; // Referensi ke script Tempat_Drop

    void Start()
    {
        SavePosisi = transform.position;
        originalScale = transform.localScale;
        IsTerkunci = false; 
    }

    private void OnMouseDown()
    {
        if (!IsTerkunci)
        {
            SavePosisi = transform.position;
        }
    }

    private void OnMouseDrag()
    {
        if (!IsTerkunci)
        {
            Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Pos;
        }
    }

private void OnMouseUp()
{
    if (IsDiatasObj && tempatDrop != null)
    {
        if (!tempatDrop.isOccupied)
        {
            if (tempatDrop.acceptedIDs.Contains(this.ID))
            {
                transform.position = tempatDrop.transform.position;
                IsTerkunci = true;
                tempatDrop.GetComponent<SpriteRenderer>().enabled = false;

                tempatDrop.isOccupied = true;
                tempatDrop.currentComponent = this;
                tempatDrop.currentID = this.ID;

                // ✅ Cek apakah ID benar
                bool isBenar = (tempatDrop.currentID == tempatDrop.correctID);
                tempatDrop.isCorrectlyPlaced = isBenar;

                tempatDropAsal = tempatDrop;

                // ✅ Kurangi nyawa kalau ID salah
                if (!isBenar)
                {
                    LifeManager lifeManager = FindObjectOfType<LifeManager>();
                    if (lifeManager != null)
                    {
                        lifeManager.KurangiNyawa();
                    }
                }

                // 🔄 Update lampu (visual)
                LightController lightController = FindObjectOfType<LightController>();
                if (lightController != null)
                {
                    //lightController.UpdateLampuStatus(); // hanya update tampilan lampu
                }
            }
            else
            {
                // Tidak termasuk dalam acceptedID (misalnya baterai tidak boleh di sini)
                transform.position = SavePosisi;
            }
        }
        else
        {
            transform.position = SavePosisi;
        }
    }
    else
    {
        transform.position = SavePosisi;
    }
}


    private void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Drop"))
        {
            tempatDrop = trig.GetComponent<Tempat_Drop>();
            if (tempatDrop != null)
            {
                IsDiatasObj = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Drop"))
        {
            IsDiatasObj = false;
            tempatDrop = null;
            
        }
    }

    private void OnMouseOver()
{
    if (Input.GetMouseButtonDown(0) && IsTerkunci)
    {
        IsTerkunci = false;

        if (tempatDropAsal != null)
        {
            tempatDropAsal.GetComponent<SpriteRenderer>().enabled = true;
            tempatDropAsal.isOccupied = false;
            tempatDropAsal.isCorrectlyPlaced = false;
            tempatDropAsal.currentComponent = null;
            tempatDropAsal.currentID = -1;

            LightController lightController = FindObjectOfType<LightController>();
            if (lightController != null)
            {
                //lightController.UpdateLampuStatus(); // update tampilan saja
            }
        }

        tempatDrop = null;
        tempatDropAsal = null;
    }
}



}