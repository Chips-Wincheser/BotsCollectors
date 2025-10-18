using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private List<Resource> _freeResources = new List<Resource>();
    private List<Resource> _takenResources = new List<Resource>();

    public void AddResource(Resource resource)
    {
        if (_freeResources.Contains(resource)==false && _takenResources.Contains(resource)==false)
        {
            _freeResources.Add(resource);
        }
    }

    public Resource TakeOneResource()
    {
        if (_freeResources.Count > 0)
        {
            Resource resource = _freeResources[0];
            _freeResources.RemoveAt(0);
            _takenResources.Add(resource);
            return resource;
        }

        return null;
    }
}
