using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xcom
{
    public class Enemy : MonoBehaviour
    {
        public bool isAlive = true;
        public int Damage = 3;
        public bool CanMove;
        public bool isSelected;
        public int Team = 0;
        public Material OwnMaterial, SelectedMaterial;
        private MeshRenderer myRenderer;

        private Health health;
      

        void Awake()
        {
            health = GetComponent<Health>();
            myRenderer = gameObject.GetComponent<MeshRenderer>();
            myRenderer.material = OwnMaterial;
        }

        //This would have done something when the thing died, so you couldn't use it again
        //Didn't get that far
		private void OnEnable()
		{
            health.Die += EnemyDead;
		}
        private void OnDisable()
        {
            health.Die -= EnemyDead;
        }

        public void Selected()
		{
            isSelected = true;
            myRenderer.material = SelectedMaterial;
        }

        public void Deselected()
		{
            isSelected = false;
            myRenderer.material = OwnMaterial;
        }



        //All of these ones get called via commands, that's neat at least.
        public void GetDamaged(int damage)
        {
            health.ChangeHealth(-damage);
        }

        public void GetHealed(int healing)
		{
            health.ChangeHealth(healing);
		}

        public void Heal(Enemy friend)
		{
            friend.GetHealed(Constants.Healing);
		}

        public void Attack(Enemy enemy)
		{
            enemy.GetDamaged(3);
		}
        //This gets called by the observer at least, doesn't do anything though
        private void EnemyDead()
		{
            isAlive = false;
		}
	}
}
