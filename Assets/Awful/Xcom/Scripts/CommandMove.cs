using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xcom
{
    public class CommandMove : Command
    {
		protected Transform transform = null;
		Vector3 position;

		public CommandMove(Transform unit, Vector3 newPosition)
		{
			transform = unit;
			position = newPosition;
		}

	public override void Execute()
		{
			transform.position = position;
		}
	}
}

