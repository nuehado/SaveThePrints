using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensesStore : MonoBehaviour
{
    public List<GameObject> allDefenses = new List<GameObject>();
    [SerializeField] private List<GameObject> purchasableDefenses = new List<GameObject>();
    public List<GameObject> purchasedDefenses = new List<GameObject>();

    private Ray ray;
    private TowerFiring towerFiring;
    private GameObject selectedObject;
    private GameObject previousObject;

    private LoadManager loadManager;

    private void Start()
    {
        loadManager = FindObjectOfType<LoadManager>();
    }

    private void OnEnable()
    {
        foreach (GameObject defenseObject in allDefenses)
        {
            defenseObject.SetActive(true);
            defenseObject.GetComponent<DefenseBuyer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HoverOverPurchasable();

        if (Input.GetMouseButtonDown(0))
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
            if (hit.transform.gameObject.tag == "Tower" || hit.transform.gameObject.tag == "DefenseSupport" || hit.transform.gameObject.tag == "SlowStick")
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

    private void UnhoverOverPurchasable()
    {
        if (previousObject != selectedObject)
        {
            previousObject.GetComponent<Outline>().enabled = false;
        }
    }

    private void AddDefenseToPurchased(GameObject purchaseAttempt)
    {
        if (purchasableDefenses.Contains(purchaseAttempt))
        {
            purchasableDefenses.Remove(purchaseAttempt);
            purchasedDefenses.Add(purchaseAttempt);
            selectedObject.GetComponent<Outline>().enabled = false;
            selectedObject = null;
            loadManager.ChangeLevel(0);
            gameObject.GetComponent<DefensesStore>().enabled = false;
        }
    }
}

