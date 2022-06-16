using UnityEngine;

[CreateAssetMenu(fileName = "New Building Stats", menuName = "Building/Requirements")]
public class BuildingsStats : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public BuildingRequirements[] requirements;
}
