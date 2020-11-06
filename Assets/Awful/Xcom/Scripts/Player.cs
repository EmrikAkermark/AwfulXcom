using System.Collections;
using UnityEngine;

namespace Xcom
{
    public class Player : MonoBehaviour
    {
        public int CurrentTeam;

        private Actions actions;
        private Enemy SelectedUnit;
        private Camera theCam;

        private bool hasSelected;
        private bool canClick = true;

        private EnemyManager enemyManager;

		private void Awake()
		{
            actions = new Actions();
            theCam = Camera.main;
            actions.State = Actions.ActionState.FirstAction;
            enemyManager = GetComponent<EnemyManager>();
		}

        //Uninteresting hack crap
        void Update()
        {
            if (canClick == false)
                return;
            if(Input.GetKeyDown(KeyCode.Mouse0))
                SelectPlayer();
            if(Input.GetKeyDown(KeyCode.Mouse1) && hasSelected)
                DoAction();
            if(Input.GetKeyDown(KeyCode.C))
                CommitActions();
            if (Input.GetKeyDown(KeyCode.Return))
                EndTurn();
        }


        //committs actions from one of the players to the round queue.
        public void CommitActions()
		{
            if (hasSelected == false)
                return;
            actions.CommitActions();
            SelectedUnit.CanMove = false;
		}

        public void EndTurn()
		{
            StartCoroutine(GoTroughTurn());
            if(CurrentTeam == 0)
			{
                CurrentTeam = 1;
			}
            else
			{
                CurrentTeam = 0;
			}
            enemyManager.SwitchTeams();
		}

        //This calls the commands committed during the turn, to be played up automatically.
        //I'm actually proud of this one, although everything else in this game is a trashfire unless
        //noted otherwise.
        private IEnumerator GoTroughTurn()
		{
            canClick = false;
            bool actionsLeft = true;
            while(actionsLeft)
			{
                yield return new WaitForSeconds(2f);
                actionsLeft = actions.ExecuteAction();
			}
            canClick = true;
		}


        //More awful crap
        void SelectPlayer()
		{
            RaycastHit hit;
            if (Physics.Raycast(theCam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Enemy SelectedUnitMaybe = hit.collider.GetComponent<Enemy>();
                if(SelectedUnitMaybe != null)
				{
                    if (SelectedUnit != SelectedUnitMaybe && hasSelected)
                        SelectedUnit.Deselected();
                    SelectedUnit = SelectedUnitMaybe;
                    if (SelectedUnit.CanMove == false)
                        return;
                    SelectedUnit.Selected();
                    hasSelected = true;
                }
                else
                {
                    if(SelectedUnit != null)
                        SelectedUnit.Deselected();
                    SelectedUnit = null;
                    hasSelected = false;
                }
            }
            
                
		}


        //This hacky thing decides what command the player chooses, if the player selects another friend,
        //a heal command is queued, the ground, a movement command, an enemy, n attack command!
        //Whilst the structure is a trash fire, what the code does isn't as much of a trashfire.
        void DoAction()
		{
			if (!hasSelected)
			{
                return;
			}

            RaycastHit hit;
            if(Physics.Raycast(theCam.ScreenPointToRay(Input.mousePosition), out hit))
			{
                if(hit.collider.gameObject.layer == 8)
				{
                    Enemy otherSelectedPlayer = hit.collider.GetComponent<Enemy>();
                    if(otherSelectedPlayer.Team == SelectedUnit.Team)
					{
                        Command heal = new CommandHeal(otherSelectedPlayer, SelectedUnit);
                        actions.AddAction(heal);
					}
                    else
					{
                        Command attack = new CommandAttack(otherSelectedPlayer, SelectedUnit);
                        actions.AddAction(attack);
					}
				}
                else
				{
                    Command move = new CommandMove(SelectedUnit.transform, hit.point + Vector3.up * 0.5f);
                    actions.AddAction(move);
				}
			}

		}
    }
}
