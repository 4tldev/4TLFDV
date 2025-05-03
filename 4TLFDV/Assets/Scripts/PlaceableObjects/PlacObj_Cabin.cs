using System.Collections.Generic;
using UnityEngine;

public class PlacObj_Cabin : MonoBehaviour
{
    private const int maxWorkers = 5;
    private List<Worker> workers = new();

    public Vector3 GetRestingSpot()
    {
        return transform.position; // You can add offset logic later if needed
    }

    public bool HasRoom()
    {
        return workers.Count < maxWorkers;
    }

    public void RegisterWorker(Worker worker)
    {
        if (!workers.Contains(worker) && HasRoom())
        {
            workers.Add(worker);
        }
    }
}
