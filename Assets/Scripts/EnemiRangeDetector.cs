using UnityEngine;

public class EnemiRangeDetector : MonoBehaviour
{

    [SerializeField] EnemiController _enemiScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CompareTag("EnemiZone"))
                _enemiScript.CharInRange = true;
            if (CompareTag("EnemiAttack"))
                _enemiScript.CharInRangeAttack = true;

            _enemiScript.Player = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (CompareTag("EnemiAttack"))
                _enemiScript.CharInRangeAttack = false;
        }
    }
}
