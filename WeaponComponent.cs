using UnityEngine;
using System.Collections;

/**
 * A tool used for mining ore and chopping wood.
 * Tools have strength that gets used up each time
 * they are used. When their strength is depleted
 * they should be destroyed by the user.
 */
public class WeaponComponent : MonoBehaviour
{

    public int ammo;

    void Start()
    {
       ammo = 30; // full strength
    }

    /**
	 * Use up the tool by causing damage. Damage should be a percent
	 * from 0 to 1, where 1 is 100%.
	 */
    public void use(int fireMode)
    {
        if (fireMode == 2)
            ammo -= 3;
        else
            ammo -= 3;
        
    }
}

