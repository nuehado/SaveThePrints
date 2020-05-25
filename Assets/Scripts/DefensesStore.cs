using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensesStore : MonoBehaviour
{
    public List<GameObject> allDefenses = new List<GameObject>();
    [SerializeField] private List<GameObject> purchasableTowers = new List<GameObject>();
    [SerializeField] private List<GameObject> purchasableSupports = new List<GameObject>();
    [SerializeField] private List<GameObject> purchasableGlueSticks = new List<GameObject>();
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
        /*foreach (GameObject defenseObject in allDefenses)
        {
            defenseObject.SetActive(true);
            defenseObject.GetComponent<DefenseBuyer>().enabled = true;
        }
        */
        isBuyingActive = true;
        foreach (GameObject purchasedObject in purchasedDefenses)
        {
            purchasedObject.SetActive(true);
            /*purchasedObject.GetComponent<Outline>().enabled = true;
            purchasedObject.GetComponent<Outline>().OutlineColor = Color.green;
            */
        }
    }

    // Update is called once per frame
    void Update()
    {
        //HoverOverPurchasable();
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
            if (purchasableTowers.Contains(hit.transform.gameObject))//hit.transform.gameObject.tag == "Tower" || hit.transform.gameObject.tag == "DefenseSupport" || hit.transform.gameObject.tag == "SlowStick" && purchasableDefenses.Contains(hit.transform.gameObject))
            {
                selectedObject = hit.transform.gameObject;
                selectedObject.GetComponent<Outline>().enabled = true;
                selectedObject.GetComponent<Outline>().OutlineColor = Color.yellow;
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
            if (purchaseDrawer.Contains(hit.transform.gameObject))//hit.transform.gameObject.tag == "Tower" || hit.transform.gameObject.tag == "DefenseSupport" || hit.transform.gameObject.tag == "SlowStick" && purchasableDefenses.Contains(hit.transform.gameObject))
            {
                selectedObject = hit.transform.gameObject;
                selectedObject.GetComponent<Outline>().enabled = true;
                if (selectedObject.name.Contains("Tower"))
                {
                    if (purchasableTowers.Count > 0)
                    {
                        selectedObject.GetComponent<Outline>().OutlineColor = Color.green;
                    }
                    else
                    {
                        selectedObject.GetComponent<Outline>().OutlineColor = Color.red;
                    }
                }
                else if (selectedObject.name.Contains("Support"))
                {
                    if (purchasableSupports.Count > 0)
                    {
                        selectedObject.GetComponent<Outline>().OutlineColor = Color.green;
                    }
                    else
                    {
                        selectedObject.GetComponent<Outline>().OutlineColor = Color.red;
                    }
                }
                else if (selectedObject.name.Contains("Glue"))
                {
                    if (purchasableGlueSticks.Count > 0)
                    {
                        selectedObject.GetComponent<Outline>().OutlineColor = Color.green;
                    }
                    else
                    {
                        selectedObject.GetComponent<Outline>().OutlineColor = Color.red;
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
                //CloseStore();
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
            /*if (purchasableTowers.Contains(purchaseAttempt))
            {
                purchasableTowers.Remove(purchaseAttempt);
                purchasedDefenses.Add(purchaseAttempt);
                foreach (GameObject defenseObject in allDefenses)
                {
                    defenseObject.GetComponent<Outline>().enabled = false;
                }
                //selectedObject.GetComponent<Outline>().enabled = false;
                selectedObject = null;
                previousObject = null;
                loadManager.ChangeLevel(0);
                gameObject.GetComponent<DefensesStore>().enabled = false;
            }*/
        }
    }

    public void CloseStore()
    {
        selectedObject = null;
        previousObject = null;
        loadManager.ChangeLevel(0);// todo add animations and such before doing this
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

