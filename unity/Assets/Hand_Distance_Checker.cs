using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Distance_Checker : MonoBehaviour
{
    // Référence au GameObject "Left_Hand_Helper"
    GameObject Left_Hand;
    GameObject Right_Hand;
    // Référence au cube créé
    GameObject onde;
    GameObject debugCube;

    bool initCounter = false;
    float counter = 0f;

    Vector3 leftHandPosition;
    Vector3 rightHandPosition;

        // Déclarer une variable de RaycastHit pour stocker les informations sur l'intersection
    RaycastHit hit;
    bool isHit;
    // Start est appelée avant la première frame de mise à jour
    void Start()
    {
        // Chercher et attribuer les GameObjects "Left_Hand_Helper" et "Right_Hand_Helper"
        Left_Hand = GameObject.Find("Left_Hand_Helper");
        Right_Hand = GameObject.Find("Right_Hand_Helper");
    }

    // Update est appelée une fois par frame
    void Update()
    {
        
            // Debug.Log(counter+" : "+initCounter);
        // Utiliser la référence aux GameObjects "Left_Hand_Helper" et "Right_Hand_Helper"
        // pour accéder à leurs positions et calculer la distance entre elles
        if (Left_Hand != null && Right_Hand != null)
        {
            
            Vector3 previousLeftHandPosition = leftHandPosition;
            Vector3 previousRightHandPosition = rightHandPosition;

            leftHandPosition = Left_Hand.transform.position;
            rightHandPosition = Right_Hand.transform.position; 

            float hand_Distance = Vector3.Distance(leftHandPosition, rightHandPosition);

            Vector3 leftHandVelocity = (leftHandPosition - previousLeftHandPosition) / Time.deltaTime;
            Vector3 rightHandVelocity = (rightHandPosition - previousRightHandPosition) / Time.deltaTime;
            // && Mathf.Abs(leftHandVelocity.x) > 1.5f
            if (hand_Distance < 0.05f && Mathf.Abs(rightHandVelocity.x) > 1.5f  && onde == null)
            {   
                initCounter = true;
                Debug.Log("CLAP");
                Vector3 onde_ClapStartPosition = (leftHandPosition + rightHandPosition) / 2;

                shootRayCast(onde_ClapStartPosition,Camera.main.transform.forward);
                startOnde(onde_ClapStartPosition);
            }

            else if (counter >= 1.99f && onde != null)
            {
                    Destroy(onde);
                    onde = null;
                    Debug.Log("Ready To Clap again");
            }

            if(initCounter == true)
            {
                counter += Time.deltaTime;
                if(counter>2f){
                    counter = 0f;
                    initCounter = false;
                }
            }
        }
    }
    void shootRayCast(Vector3 startPosition, Vector3 targetPosition){
        Ray ray = new Ray(startPosition, targetPosition);
        Debug.Log("RAYCAST SHOOT");
        

        // Effectuer le raycast
        isHit = Physics.Raycast(ray, out hit);
        
        if (isHit)
        {
            // Vérifier si le raycast a frappé un objet
            if (hit.collider != null)
            {
                Vector3 hitPoint = hit.point;
                debugCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                debugCube.transform.position = hit.point;
                Vector3 newSize = new Vector3(0.5f, 0.5f, 0.5f); // Remplacez les valeurs pour définir la nouvelle taille du cube
                debugCube.transform.localScale = newSize;
                
            }
        }
    }
    void startOnde(Vector3 onde_ClapStartPosition){
                onde = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                onde.transform.position = hit.point;
    }
}
