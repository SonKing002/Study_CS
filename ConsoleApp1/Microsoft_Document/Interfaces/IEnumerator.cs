using System;
using System.Collections;

public class Person
{
    //생성자
    public Person(string fName, string lName)
    {
        this.firstName = fName;
        this.lastName = lName;
    }

    private string _firstName;
    private string _lastName;

    public string firstName
    { get { return _firstName; } set {  _firstName = value; } }

    public string lastName
    { get { return _lastName; } set { _lastName = value; } }
}

//Person 오브젝트들의 컬렉션으로, 이 클래스는 For문으로 IEnumerable를 구현
public class People : IEnumerable 
{
    private Person[] _people;
    public People(Person[] pArray)
    {
        _people = new Person[pArray.Length];//매개변수의 배열와 동일한 크기 할당

        for (int i = 0; i < pArray.Length; i++) //전체 할당 : IEnumerable O(n)의 시간이 걸릴 것이다.
        {
            _people[i] = pArray[i];
        }
    }

    //GetEnumerator 메서드 구현
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public PeopleEnum GetEnumerator()
    {
        return new PeopleEnum(_people);
    }
}

//IEnumerable 를 구현하면, 반드시 IEnumerator도 구현해야한다.
public class PeopleEnum : IEnumerator
{
    public Person[] _people;

    //첫번째 MoveNext Call을 하기 전까지 첫번째 열거자 주소는 -1로 가르킨다
    int position = -1;

    //매개변수 배열을 할당
    public PeopleEnum(Person[] list)
    { 
        _people= list;
    }

    public bool MoveNext()
    {
        position++;//다음 요소
        return (position < _people.Length);//true false
    }

    public void Reset()
    {
        position = -1;
    }

    object IEnumerator.Current 
    {
        get { return Current; }//인덱스가 유효하다면, _people[position]요소 값을 리턴
    }

    public Person Current//현재
    {
        get
        {
            try
            {
                return _people[position];//해당 값 리턴
            }
            catch (IndexOutOfRangeException) //인덱스 범위 밖
            {
                throw new InvalidOperationException();
            }
        }
    }
}

class App
{
    static void Main()
    {
        //배열 클래스
        Person[] peopleArray = new Person[3]
        {
            new Person("John", "Smith"),
            new Person("Jim", "Johnson"),
            new Person("Sue", "Rabon"),
        };

        People peopleList = new People(peopleArray);
        foreach (Person p in peopleList)
            Console.WriteLine(p.firstName + " " + p.lastName);
    }
}