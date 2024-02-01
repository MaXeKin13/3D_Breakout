using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnumerableTest
{
    // Start is called before the first frame update
    void Start()
    {
        Shelter shelter = new Shelter();

        foreach (Dog2 _dog in shelter)
        {
            
        }
    }

    public class Dog2
    {
        
    }
    public class Shelter : IEnumerable<Dog2>
    {
        public List<Dog2> dogs;
        public IEnumerator<Dog2> GetEnumerator()
        {
            return dogs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
