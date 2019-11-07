using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace roket
{
    public class Rocket : MonoBehaviour
    {
        #region variables
        [Header("for rotation")]
        [SerializeField]
        float rctThrust = 100f;

        [Header("for thrusting")]
        [SerializeField]
        float rcsThrust = 30f;

        [Header("The thrust sound")]
        [SerializeField]
        AudioClip mainEngine;

        [Header("The crush sound")]
        [SerializeField]
        AudioClip Crash;

        [Header("The begining of the level sound")]
        [SerializeField]
        AudioClip LevelBegins;

        [Header("Particles: ")]
        [SerializeField]
        ParticleSystem mainEngineParticles;
        [SerializeField]
        ParticleSystem CrashParticles;
        [SerializeField]
        ParticleSystem LevelBeginParticles;

        [Header("Level load delay")]
        [SerializeField]
        float lvlLoadDelay = 2f;

        Rigidbody rigidBody;
        AudioSource audioSource;

        enum State { Alive, Dead, Transform}
        State state = State.Alive;

       public bool collisionsDisabled = false;
        #endregion

        #region unity functions
        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if(state == State.Alive)
            {
                Thrust();
                Rotate();
            }
   
            
        }
        #endregion

        #region my functions

        void OnCollisionEnter(Collision collision)
        {
            if(state != State.Alive || collisionsDisabled) { return; }   //ignore the collision if we are not alive!
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    // do nothing
                    break;
                case "Finish":
                    state = State.Transform;
                    Finish();
                    break;
                default:
                    state = State.Dead;
                    Dead();
                    break;            
            }
        }


        //public void DebugKeyPerformance()
        //{
        //    if (Input.GetKeyDown(KeyCode.L))
        //    {
               // LoadNextLevel();
        //    }
        //    else if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        collisionsDisabled =!collisionsDisabled;
        //    }
        //}

        private void Finish()
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
            LevelBeginParticles.Play();
            audioSource.PlayOneShot(LevelBegins);
            Invoke("LoadNextScene", lvlLoadDelay);
        }

        private void Dead()
        {
            audioSource.Stop(); // stop the thrusting sound before starting the crash sound
            mainEngineParticles.Stop();
            CrashParticles.Play();
            audioSource.PlayOneShot(Crash);
            Invoke("LoadSameScene", lvlLoadDelay);
        }



        private void LoadSameScene()
        {
            // LoadLevels.instance.LoadSameLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadNextScene()
        {
            LoadLevels.instance.LoadNextLevelAuto();
        }

        public void Thrust()
        {

            if (Input.GetKey(KeyCode.Space))
            {
                ApplyMove();
            }
            else
            {
                audioSource.Stop();
                mainEngineParticles.Stop();
            }

        }


         private void ApplyMove()
        {
            float mainThrust = rcsThrust * Time.deltaTime;
            // we use Relative force because it will apply the force in direction that we want
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            mainEngineParticles.Play();
        }


        public void Rotate()
        {
           
            float rotationThrustFrame = rctThrust * Time.deltaTime;
            rigidBody.freezeRotation = true; // take manual control of the rotation
         
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * rotationThrustFrame);  // using transfom for scale, position and rotate the game object
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * rotationThrustFrame);
            }
            rigidBody.freezeRotation = false;
        }
    }
    #endregion
}

