using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [System.Serializable]
    public class AbilityData
    {
        public GameObject abilityObject;
        public float speed = 1f;
        public bool isLocked = true;
        public KeyCode button;
        [HideInInspector] public AbilityPlaceholder abilityPlaceholder;
    }

    public AbilityData[] abilities;

    private void Start()
    {
        // Initialize ability UI and attach the AbilityPlaceholder script
        foreach (AbilityData abilityData in abilities)
        {
            if (abilityData.abilityObject != null)
            {
                abilityData.abilityPlaceholder = abilityData.abilityObject.GetComponent<AbilityPlaceholder>();
            }
        }
    }

    private void Update()
    {
        // Check for key presses to trigger abilities
        for (int i = 0; i < abilities.Length; i++)
        {
            if (Input.GetKeyDown(abilities[i].button))
            {
                UseAbility(i);
            }
        }
    }

    public void UseAbility(int abilityIndex)
    {
        if (abilityIndex >= 0 && abilityIndex < abilities.Length)
        {
            AbilityData abilityData = abilities[abilityIndex];

            if (abilityData.abilityPlaceholder != null && !abilityData.isLocked)
            {
                abilityData.abilityPlaceholder.UseAbility(abilityData.speed);
            }
        }
    }
}
