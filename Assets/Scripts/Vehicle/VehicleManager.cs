using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{

    public Vehicle[] vehicles;          //Ż�� ��ü �迭 �����Ѵ�.

    public Car car;
    public Bicycle bicycle;

    float Timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 0; i < vehicles.Length; i ++)
        {
            vehicles[i].Move();
        }
        car.Move();
        bicycle.Move();

        Timer -= Time.deltaTime;

        if(Timer<0)         //1�ʸ��� ȣ��ǰ� �Ѵ� 
        {
            car.Horn();     // ���� �Լ� ȣ��
            bicycle.Horn();
            Timer = 1;
        }
    }
}
