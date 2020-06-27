using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class DefensesStore : MonoBehaviour
{
    public List<GameObject> allDefenses = new List<GameObject>();
    public List<GameObject> purchasableTowers = new List<GameObject>();
    public List<GameObject> purchasableSupports = new List<GameObject>();
    public List<GameObject> purchasableGlueSticks = new List<GameObject>();
    public List<GameObject> purchasedDefenses = new List<GameObject>();
    [SerializeField] private List<GameObject> purchaseDrawer = new List<GameObject>();
    [SerializeField] private AudioSource drawerSFX;
    [SerializeField] private AudioSource unlockSFX;

    private Ray ray;
    private GameObject selectedObject;
    private GameObject previousObject;

    private LoadManager loadManager;
    private bool isBuyingActive = false;
    private void Start()
    {
        loadManager = FindObjectOfType<LoadManager>();
    }

    private void OnEnable()
    {
        isBuyingActive = true;
        foreach (GameObject purchasedObject in purchasedDefenses)
        {
            purchasedObject.SetActive(true);
        }
    }

    void Update()
    {
        if (isBuyingActive == true)
        {
            HoverOverPurchaseDrawer();
        }

        if (Input.GetMouseButtonDown(0) && isBuyingActive == true)
        {
            if (selectedObject != null)
            {
                AddDefenseToPurchased(selectedObject);
            }
        }
    }

    private void HoverOverPurchasable()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 500.0f))
        {
            if (purchasableTowers.Contains(hit.transform.gameObject))
            {
                selectedObject = hit.transform.gameObject;
                selectedObject.GetComponent<Outline>().enabled = true;
                selectedObject.GetComponent<Outline>().color = 2; //todo change color of drawer outline
                if (previousObject != null)
                {
                    UnhoverOverPurchasable();
                }
                previousObject = selectedObject;
            }
        }
    }

    private void HoverOverPurchaseDrawer()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 2000.0f))
        {
            if (purchaseDrawer.Contains(hit.transform.gameObject))
            {
                selectedObject = hit.transform.gameObject;
                selectedObject.GetComponent<Outline>().enabled = true;
                if (selectedObject.name.Contains("Tower"))
                {
                    if (purchasableTowers.Count > 0)
                    {
                        selectedObject.GetComponent<Outline>().color = 1; //todo change color of drawer outline
                    }
                    else
                    {
                        selectedObject.GetComponent<Outline>().color = 2; //todo change color of drawer outline
                    }
                }
                else if (selectedObject.name.Contains("Support"))
                {
                    if (purchasableSupports.Count > 0)
                    {
                        selectedObject.GetComponent<Outline>().color = 1; //todo change color of drawer outline
                    }
                    else
                    {
                        selectedObject.GetComponent<Outline>().color = 2; //todo change color of drawer outline
                    }
                }
                else if (selectedObject.name.Contains("Glue"))
                {
                    if (purchasableGlueSticks.Count > 0)
                    {
                        selectedObject.GetComponent<Outline>().color = 1; //todo change color of drawer outline
                    }
                    else
                    {
                        selectedObject.GetComponent<Outline>().color = 2; //todo change color of drawer outline
                    }
                }
                if (previousObject != null)
                {
                    UnhoverOverPurchasable();
                }
                previousObject = selectedObject;
            }
        }
    }

    private void UnhoverOverPurchasable()
    {
        if (previousObject != selectedObject)
        {
            previousObject.GetComponent<Outline>().enabled = false;
        }
    }

    private void AddDefenseToPurchased(GameObject purchaseAttempt)
    {
        if (purchaseAttempt.name.Contains("Tower"))
        {
            if (purchasableTowers.Count > 0)
            {
                isBuyingActive = false;
                purchaseAttempt.GetComponent<Outline>().enabled = false;
                purchasedDefenses.Add(purchasableTowers[0]);
                purchasableTowers.Remove(purchasableTowers[0]);
                purchaseAttempt.GetComponent<Animator>().SetTrigger("Open");
            }
        }
        else if (purchaseAttempt.name.Contains("Support"))
        {
            if (purchasableSupports.Count > 0)
            {
                isBuyingActive = false;
                purchaseAttempt.GetComponent<Outline>().enabled = false;
                purchasedDefenses.Add(purchasableSupports[0]);
                purchasableSupports.Remove(purchasableSupports[0]);
                purchaseAttempt.GetComponent<Animator>().SetTrigger("Open");
            }
        }
        else if (purchaseAttempt.name.Contains("Glue"))
        {
            if (purchasableGlueSticks.Count > 0)
            {
                isBuyingActive = false;
                purchaseAttempt.GetComponent<Outline>().enabled = false;
                purchasedDefenses.Add(purchasableGlueSticks[0]);
                purchasableGlueSticks.Remove(purchasableGlueSticks[0]);
                purchaseAttempt.GetComponent<Animator>().SetTrigger("Open");
                
            }
        }
    }

    public void CloseStore()
    {
        selectedObject = null;
        previousObject = null;
        loadManager.ChangeLevel(0);
        gameObject.GetComponent<DefensesStore>().enabled = false;
    }
    
    public void PlayDrawerSFX()
    {
        drawerSFX.Play();
    }

    public void PlayUnlockSFX()
    {
        unlockSFX.Play();
    }
}

