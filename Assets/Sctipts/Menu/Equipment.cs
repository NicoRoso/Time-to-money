using UnityEngine.SceneManagement;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static string weapon = "M4A1";

    public void SelectWeapon(string weaponName)
    {
        weapon = weaponName;
    }
}
