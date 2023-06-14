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

        public void ExecuteAbility()
        {
            // Define the behavior of the ability here
            // Replace this method with the specific behavior for each ability

            switch (abilityObject.name)
            {
                case "Ability1":
                    // Behavior for Ability 1
                    Debug.Log("Executing Ability 1");
                    // Add your code for Ability 1 here
                    break;

                case "Ability2":
                    // Behavior for Ability 2
                    Debug.Log("Executing Ability 2");
                    // Add your code for Ability 2 here
                    break;

                case "Ability3":
                    // Behavior for Ability 3
                    Debug.Log("Executing Ability 3");
                    // Add your code for Ability 3 here
                    break;

                case "Ability4":
                    // Behavior for Ability 4
                    Debug.Log("Executing Ability 4");
                    // Add your code for Ability 4 here
                    break;

                default:
                    Debug.Log("Unknown ability: " + abilityObject.name);
                    break;
            }
        }
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
                abilityData.ExecuteAbility(); // Execute the ability's behavior
            }
        }
    }
}


