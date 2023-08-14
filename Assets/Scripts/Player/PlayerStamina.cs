using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : Singleton<PlayerStamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] Sprite fullStaminaImage, emptyStaminaImage;
    [SerializeField] float staminaRefreshCD = 3;

    int startingStamina = 3;
    int maxStamina;
    Transform staminaContainer;

    const string STAMINA_CONTAINER_TEXT = "StaminaContainer";

    protected override void Awake()
    {
        base.Awake();

        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }
    private void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }
    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaImage();
    }
    public void RefreshStamina()
    {
        if (CurrentStamina < maxStamina)
        {
            CurrentStamina++;
        }
        UpdateStaminaImage() ;
    }

    private IEnumerator RefreshStaminaRoutine()
    {
        yield return new WaitForSeconds(staminaRefreshCD);
        RefreshStamina();
    }

    private void UpdateStaminaImage()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            if (i <= CurrentStamina - 1)
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
            }
            else
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;
            }
        }
        if (CurrentStamina<maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
