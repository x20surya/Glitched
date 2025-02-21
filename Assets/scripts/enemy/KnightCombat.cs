using UnityEngine;

public class KnightCombat : MonoBehaviour
{
    public Transform attackPos;
    public float attackRange;
    public LayerMask attackMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void Attack()
    {
        Vector2 pos = new Vector2(attackPos.position.x,attackPos.position.y);

        Collider2D[] colInfo = Physics2D.OverlapCircleAll(pos, attackRange, attackMask);

        if(colInfo.Length > 0)
        {
            foreach (Collider2D col in colInfo)
            {
                if (col != null)
                {
                    if (col.GetComponent<Health>() != null)
                    {
                        col.GetComponent<Health>().TakeDamage(30);
                    }
                    Debug.Log("attack hit");
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
