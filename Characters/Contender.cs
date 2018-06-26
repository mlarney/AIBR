﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Contender : PlayerCharacter
{
    /**
	 * Our only goal will ever be to mine ore.
	 * The MineOreAction will be able to fulfill this goal.
	 */
    public override HashSet<KeyValuePair<string, object>> createGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        goal.Add(new KeyValuePair<string, object>("hasPrimary", true));
        return goal;
    }

}

