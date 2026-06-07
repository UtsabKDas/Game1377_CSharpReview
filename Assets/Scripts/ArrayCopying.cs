using UnityEngine;

public class ArrayCopying : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject sphere;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int[] numbers = new int[] { 1, 2, 3, 4, 5 };
        int[] numbersAlias = numbers;
        int[] numbersCopy = (int[])numbers.Clone();
        
        numbersAlias[0] = 10;
        Debug.Log(numbers[0]);
        ArrayEndSwap<int>(numbers);
        Debug.Log(numbers[0]);
        numbersCopy[0] = 20;
        Debug.Log(numbers[0]);

        GameObject[] gameObjectsAlias = gameObjects;
        GameObject[] gameObjectsCopy = (GameObject[])gameObjects.Clone();

        gameObjectsAlias[0] = cube;
        gameObjectsCopy[0] = sphere;
        ArrayEndSwap<GameObject>(gameObjects);
    }

    void ArrayEndSwap<T>(T[] array)
    {
        T temp = array[0];
        array[0] = array[array.Length - 1];
        array[array.Length - 1] = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
