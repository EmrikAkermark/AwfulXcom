using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcom;
public class CommandHeal : Command
{
	protected Enemy healSubject = null;
	protected Enemy healer = null;

	public CommandHeal(Enemy healSubject, Enemy healer)
	{
		this.healer = healer;
		this.healSubject = healSubject;
	}

	public override void Execute()
	{
		healer.Heal(healSubject);
	}

}
