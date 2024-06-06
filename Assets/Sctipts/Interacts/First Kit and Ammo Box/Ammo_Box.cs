using UnityEngine;

public class Ammo_Box : MonoBehaviour
{
    [SerializeField] private int amount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHp>())
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject[] allWeapons = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject weapon in allWeapons)
        {
            if (weapon.CompareTag("Weapon"))
            {
                Gun gunComponent = weapon.GetComponent<Gun>();
                if (gunComponent != null)
                {
                    gunComponent.PlusAmmo(amount);
                }
            }
        }
    }
}
