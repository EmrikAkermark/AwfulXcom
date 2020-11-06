using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xcom
{
	public class EnemyManager : MonoBehaviour
	{
		public List<Enemy> enemies = new List<Enemy>();

		private int teamTurn;


		//Nothing in here matters
		private void Start()
		{

			//this is an ugly hack please don't look
			teamTurn = 1;
			SwitchTeams();
		}

		public void SwitchTeams()
		{
			if(teamTurn == 0)
			{
				teamTurn = 1;
			}
			else
			{
				teamTurn = 0;
			}
			foreach (var enemy in enemies)
			{
				if(enemy.Team == teamTurn)
				{
					enemy.CanMove = true;
				}
				else
				{
					enemy.CanMove = false;
				}
			}
		}
	}
}