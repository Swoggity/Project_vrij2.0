using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPlaceholder : MonoBehaviour
{
    public float speed = 1f;

    private bool isUsingAbility = false;
    private Animator mAnimator;
    private Slider slider;

    private void Start()
    {
        // Assuming "Locked" parameter is initially set to true in the animator controller
        mAnimator = GetComponent<Animator>();
        slider = GetComponent<Slider>();
        mAnimator.SetBool("Locked", false);
    }

    public void UseAbility(float speedValue)
    {
        speed = speedValue;
        if (!isUsingAbility)
        {
            isUsingAbility = true;

            // Set the animator parameter to indicate that the ability is active
            mAnimator.SetBool("CdActive", true);

            // Reset the slider value to 0
            slider.value = 0f;

            // Start the coroutine to animate the slider value back to 100
            StartCoroutine(AnimateSlider());
        }
    }

    private IEnumerator AnimateSlider()
    {
        float startTime = Time.time;
        float endTime = startTime + (1f / speed);
        float elapsedTime = 0f;

        while (elapsedTime < endTime - startTime)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the current value based on the elapsed time and speed
            float currentValue = Mathf.Lerp(0f, 100f, elapsedTime / (endTime - startTime));

            // Update the slider value
            slider.value = currentValue;

            yield return null;
        }

        // Set the animator parameter to indicate that the ability is no longer active
        mAnimator.SetBool("CdActive", false);

        // Reset the flag
        isUsingAbility = false;
    }
}
