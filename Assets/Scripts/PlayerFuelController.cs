using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerFuelController : MonoBehaviour
{
    [SerializeField] [Tooltip("Max fuel quantity")]
    public float _maxFuelQuantity = 100;

    [SerializeField] [Tooltip("Quantity used per seconds")]
    private float _fuelUsage = 1.0f;

    [SerializeField] public float currentFuelQuantity;

    private PlayerMovementController playerMovementController;

    private bool canDecreaseFuel = true;

    private void Awake()
    {
        currentFuelQuantity = _maxFuelQuantity;
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    private void Update()
    {
        if (canDecreaseFuel)
            StartCoroutine(UseFuel());
    }

    private IEnumerator UseFuel()
    {
        canDecreaseFuel = false;
        while (!canDecreaseFuel)
        {
            if (playerMovementController.IsMoving && playerMovementController.CanMove)
            {
                canDecreaseFuel = false;
                currentFuelQuantity -= _fuelUsage;
                canDecreaseFuel = false;

                if (currentFuelQuantity <= 0)
                {
                    currentFuelQuantity = 0;
                    playerMovementController.CanMove = false;
                }

                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                canDecreaseFuel = true;
            }
        }
    }

    public void RestoreFuel(float percent)
    {
        currentFuelQuantity += _maxFuelQuantity * (percent / 100);
        if (currentFuelQuantity > _maxFuelQuantity)
            currentFuelQuantity = _maxFuelQuantity;
    }
}