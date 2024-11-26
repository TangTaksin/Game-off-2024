using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject item_to_spawn;
    public bool disable_after_spawn;

    public void SpawnItem()
    {
        var to_be_spawn = item_to_spawn;
        to_be_spawn.transform.position = transform.position;
        Instantiate(to_be_spawn);

        if (disable_after_spawn)
            this.gameObject.SetActive(false);
    }

}
