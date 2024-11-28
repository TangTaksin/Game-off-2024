using System.Collections;
using UnityEngine;
using UnityEngine.Playables; // Required to access the PlayableDirector
using UnityEngine.SceneManagement; // Required to change scenes
using UnityEngine.UI; // Required for UI components

public class GoToNextSceneTimeline : MonoBehaviour
{
    public PlayableDirector playableDirector; // Reference to the PlayableDirector component
    public string nextSceneName; // Name of the next scene to load
    public Image LoadingProgressBar; // Reference to the UI Image for progress bar

    public Transition transition;

    // Start is called before the first frame update
    void Start()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }

        if (playableDirector != null)
        {
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playableDirector != null && LoadingProgressBar != null)
        {
            if (playableDirector.state == PlayState.Playing && playableDirector.duration > 0)
            {
                LoadingProgressBar.fillAmount = Mathf.Clamp01((float)(playableDirector.time / playableDirector.duration));
            }
        }
        else
        {
            if (playableDirector == null)
            {
                Debug.LogWarning("PlayableDirector is not assigned.");
            }

            if (LoadingProgressBar == null)
            {
                Debug.LogWarning("LoadingProgressBar is not assigned.");
            }
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            // Start the scene change process when the timeline stops
            StartCoroutine(ChangeSceneRoutine(nextSceneName));
        }
    }

    public void ChangeScene(string nextSceneName)
    {
        StartCoroutine(ChangeSceneRoutine(nextSceneName));

        if (nextSceneName == "EndScene")
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.endMusic);
        }
    }

    private void OnDestroy()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

    private IEnumerator ChangeSceneRoutine(string nextSceneName)
    {
        // Play the fade-in transition before switching scenes
        transition.PlayFadeIn();
        yield return new WaitForSeconds(1f); // Adjust for the duration of the transition

        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Next scene name is null or empty. Cannot load scene.");
            yield break;
        }

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
