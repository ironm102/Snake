using UnityEngine;

public class AppleCollision : MonoBehaviour
{
    int randX;
    int randY;

    //Sets the position of the apple to a random positino on the map
    private void Awake()
    {
        randX = Random.Range(0, 19);
        randY = Random.Range(0, 19);

        transform.position = new Vector3(randX, randY);
    }

    //when the apple collides with the snake head, it moves to a new random location
    //this is to not waste power on creating new instances of apples
    //if the apple spawns under the tail, it will automatically move to a new position
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snake"|| collision.gameObject.tag == "Tail")
        {
            appleCollided();
        }
    }

    void appleCollided()
    {
        randX = Random.Range(0, 19);
        randY = Random.Range(0, 19);

        transform.position = new Vector3(randX, randY);
    }
}
