using System;
using UnityEngine;

public class PickUpWeapon : GoapAction
{
    private bool hasPrimary = false;
    public WeaponComponent targetWeapon; // where we get the tool from

    public void PickUpWeaponAction()
    {
        addPrecondition("hasWeapon", false); // don't get a tool if we already have one
        addEffect("hasWeapon", true); // we now have a tool
        addEffect("hasPrimary", true);

    }


    public override void reset()
    {
        hasPrimary = false;
        targetWeapon = null;
        cost = 5f;
    }

    public override bool isDone()
    {
        return hasPrimary;
    }

    public override bool requiresInRange()
    {
        return true; // yes we need to be near a supply pile so we can pick up the tool
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        // find the nearest supply pile that has spare tools
        WeaponComponent[] weapons = (WeaponComponent[])UnityEngine.GameObject.FindObjectsOfType(typeof(WeaponComponent));
        WeaponComponent closest = null;
        float closestDist = 0;

        foreach (WeaponComponent weapon in weapons)
        {
            if (GetComponent<BackpackComponent>().weapon == null)
            {
                if (closest == null)
                {
                    // first one, so choose     it for now
                    closest = weapon;
                    closestDist = (weapon.gameObject.transform.position - agent.transform.position).magnitude;
                }
                else
                {
                    // is this one closer than the last?
                    float dist = (weapon.gameObject.transform.position - agent.transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        // we found a closer one, use it
                        closest = weapon;
                        closestDist = dist;
                    }
                }
            }
            

        }
        if (closest == null)
            return false;

        targetWeapon = closest;
        target = targetWeapon.gameObject;

        return closest != null;
    }

    public override bool perform(GameObject agent)
    {
        BackpackComponent backpack = (BackpackComponent)agent.GetComponent(typeof(BackpackComponent));
        if (backpack.weapon == null)
        {
            targetWeapon.ammo = 30;
            hasPrimary = true;

            // create the tool and add it to the agent

           
            GameObject prefab = Resources.Load<GameObject>(backpack.weaponType);
            GameObject weapon = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
            backpack.weapon = weapon;
            weapon.transform.parent = transform; // attach the tool

            return true;
        }
        else
        {
            // we got there but there was no tool available! Someone got there first. Cannot perform action
            return false;
        }

       
    }

}


