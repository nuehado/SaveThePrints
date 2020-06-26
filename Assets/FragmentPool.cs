using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentPool : MonoBehaviour
{
    [SerializeField] private GameObject fracturedBenchy;

    private Queue<GameObject> benchyFragments = new Queue<GameObject>();

    public static FragmentPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AddFragment(300);
    }

    public GameObject Get()
    {
        if ( benchyFragments.Count == 0)
        {
            AddFragment(1);
        }
        return benchyFragments.Dequeue();
    }

    private void AddFragment(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject benchyFragment = Instantiate(fracturedBenchy);
            benchyFragment.SetActive(false);
            benchyFragments.Enqueue(benchyFragment);
        }
    }

    public void ReturnToPool(GameObject benchyFragment)
    {
        benchyFragment.SetActive(false);
        benchyFragments.Enqueue(benchyFragment);
    }

}
