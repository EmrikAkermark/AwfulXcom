using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
namespace Xcom
{
    public class Health : MonoBehaviour
    {
        //The delegates for updating the UI when your health changes,
        //works as indended, although I didn't manage to add all the
        //features I wanted to these delegates.
        public Action UpdateUI = delegate { };
        public Action Die = delegate { };

        public int HitPoints = 10;

        public void ChangeHealth(int change)
		{
            HitPoints = Mathf.Clamp(HitPoints + change, 0, 10);
            if(HitPoints == 0)
			{
                Die();
			}
            UpdateUI();
		}
    }
}
