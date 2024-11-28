using UnityEngine;

public class CombinationManager : MonoBehaviour
{
    public CombinationData data;
    static string[] data_combiStr;
    static Item[] data_result;

    private void Start()
    {
        data_combiStr = data.combination_string;
        data_result = data.item_result;
    }

    public static Item Combine(Item item1, Item item2)
    {
        print("combining " + item1 + item2);

        var combi_string = string.Format("{0}{1}", item1.itemName, item2.itemName);
        var combi_string_reverse = string.Format("{0}{1}", item2.itemName, item1.itemName);

        int iteration = 0;
        foreach (string _combi in data_combiStr)
        {
            var compare = (combi_string == _combi || combi_string_reverse == _combi);
            print(compare);

            if (compare)
            {
                AudioManager.Instance.PlaySFXClone(AudioManager.Instance.combineItemsSfx);
                if (item1.disable_on_combine)
                {
                    item1.gameObject.SetActive(false);
                }

                if (item2.disable_on_combine)
                {
                    item2.gameObject.SetActive(false);
                }

                return data_result[iteration];
            }

            iteration++;
        }

        return null;
    }
}
