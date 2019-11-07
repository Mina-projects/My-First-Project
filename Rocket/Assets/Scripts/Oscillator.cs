using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace roket
{
    [DisallowMultipleComponent]
    public class Oscillator : MonoBehaviour
    {
        #region variables

        [Header("The movement vector: ")]
        [SerializeField]
        Vector3 movementVector = new Vector3(10f, 10f, 10f);

        [Range(0,1)] 
        [SerializeField] float movementFactor;

        [SerializeField] float period = 2f;   // the oscillation period is 2 seconds. 

        Vector3 startingPos;

        #endregion


        #region unity functions
        // Start is called before the first frame update
        void Start()
        {
            startingPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if(period <= Mathf.Epsilon) { return; }
            float cycle = Time.time / period;   // game time divided by the oscillation period  gives the number of the repeating of oscillation. grows up from 0. dar vaghe frequence ro mide. 
            const float tau = Mathf.PI * 2f;  // Mathf.PI gives the exact nymber of the Pi number. we used "const" because it's never gonna change.
            float rawSineWave = Mathf.Sin(cycle*tau);
            movementFactor = rawSineWave / 2f + 0.5f; // vaghti sin ro taghsim bar 2 mikonim meghdare baghi moonde bein -0.5 va 0.5 darmiad 
                                                      // vali ma maghadir bein 1 va 0 ro mikhaim pas be yek nim ezafash mikonim. 
                                                      // movement factor dar vaghe damaneE hast ke ma bar hasbe zamane bazi be dast miarim ta be position asli objectemoon ezafe konim. 

            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPos + offset;
        }
        #endregion

        #region functions and methods
        #endregion
    }

}
