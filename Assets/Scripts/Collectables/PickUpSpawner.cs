using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [Range(3f, 10f)]
    [SerializeField] int dropChance;
    [SerializeField] GameObject goldCoin, healthGlobe, StaminaGlobe;

    public void DropItems()
    {
        int randomNum = Random.Range(1, 7);
        switch (randomNum)
        {
            case 1:
                int randomAmount = Random.Range(1, dropChance);
                for (int i = 0; i < randomAmount; i++)
                {
                    Instantiate(goldCoin, transform.position, Quaternion.identity);
                }
                break;
            case 2:
                Instantiate(healthGlobe, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(StaminaGlobe, transform.position, Quaternion.identity);
                break;
        }
    }
}
