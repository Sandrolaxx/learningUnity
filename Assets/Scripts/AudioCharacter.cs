using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCharacter : MonoBehaviour {
    [SerializeField] AudioSource footstepsAudioSource = null;//Associado ao GameObject empty  Audio-Player-Footsteps
    [Header("Audio Clips")]
    [SerializeField] AudioClip[] softSteps = null;
    [SerializeField] AudioClip[] hardSteps = null;

    [Header("Steps")]
    [SerializeField] float timer = 0.5f;

    private float stepsTimer;

    public void PlaySteps(GroundType groundType, float speedNormalized) {//Enum GroundType
        if (groundType == GroundType.Nome) {
            return;
        }

        stepsTimer += Time.fixedDeltaTime * speedNormalized;

        if (stepsTimer >= timer) {//Sons diferentes de acordo com a layer que ele está pisando
            var steps = groundType == GroundType.Hard ? hardSteps : softSteps;
            int index = Random.Range(0, steps.Length);//Array de áudios de pessos
            footstepsAudioSource.PlayOneShot(steps[index]);//Seleciona um index aleatório no array de áudios

            stepsTimer = 0;
        }

    }

}
