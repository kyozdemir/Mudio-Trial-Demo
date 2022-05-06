using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CustomBehaviour
{

    private float mMovementSpeed = 0f;
    private static float randX = 0f;
    private static float randZ = 0f;
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
    }


    public void Activate(float speed)
    {
        mMovementSpeed = speed;
        gameObject.SetActive(true);
        StartCoroutine(StartPatrolling());
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        mMovementSpeed = 0f;
    }

    private IEnumerator StartPatrolling()
    {
        while (true)
        {
            Vector3 targetPoint = Vector3.zero;

            do
            {
                randX = Random.Range(18f, -18f);
                randZ = Random.Range(18f, -18f);
                targetPoint = new Vector3(randX, 1.1f, randZ);
            } while (Vector3.Distance(targetPoint, transform.position) > 5);



                while (Vector3.Distance(transform.position,targetPoint) > .1f)
            {

                transform.position = Vector3.MoveTowards(transform.position, targetPoint, mMovementSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(Random.Range(.5f, 2f));
        }
    }

    public void DestroyEnemy()
    {
        GameManager.WaveManager.DestroyEnemy(this);
    }

}
