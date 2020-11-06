using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcom;

public class CommandAttack : Command
{
    protected Enemy enemy = null;
    protected Enemy attacker = null;

	public CommandAttack(Enemy enemy, Enemy attacker)
	{
		this.enemy = enemy;
		this.attacker = attacker;
	}

	public override void Execute()
	{
		attacker.Attack(enemy);
	}

}
