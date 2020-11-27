using UnityEngine;

public class Location
{
    Quaternion rotation;
    Vector3 position;
    GameObject parent;

    public Location(Quaternion rotation_, Vector3 position_, GameObject parent_)
    {
        rotation = rotation_;
        position = position_;
        parent = parent_;
    }
    public Quaternion GetRotation()
    {
        return rotation;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public GameObject GetParent()
    {
        return parent;
    }
}
