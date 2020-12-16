using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protobuf;
using Google.Protobuf;


public class protoTest : MonoBehaviour
{
    private AnimalDataInner animalData = new AnimalDataInner();
    void Start()
    {
        Damand damand0 = new Damand();
        damand0.WeightMale = 10.5f;
        damand0.WeightFemale = 9.3f;
        damand0.Score = 55.3f;


        animalData.Reward0 = damand0;
        /*animalData.Reward0.WeightFemale = 18.4f;
        animalData.Reward0.WeightFemale = 23.5f;
        animalData.Reward0.Score = 45.6f;
        animalData.Reward1.WeightMale = 25.7f;
        animalData.Reward1.WeightFemale = 22.5f;
        animalData.Reward1.Score = 44.6f;
        animalData.Reward2.WeightMale = 24.7f;
        animalData.Reward2.WeightFemale = 21.5f;
        animalData.Reward2.Score = 43.6f;
        animalData.Time0.DrinkTime = "19:00--21:00";
        animalData.Time0.EatTime = "20:00--22:00";
        animalData.Time0.SleepTime = "19:00--23:00";*/

        byte[] databytes = animalData.ToByteArray();

        IMessage IMDataTest = new AnimalDataInner();
        AnimalDataInner p1 = new AnimalDataInner();
        p1 = (AnimalDataInner)IMDataTest.Descriptor.Parser.ParseFrom(databytes);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
