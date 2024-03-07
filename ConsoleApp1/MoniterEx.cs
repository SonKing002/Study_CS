using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class MoniterEx
    {
        private static object s_lock = new object();
        private static bool s_lockIsTake;
        public static int count;

        static void Main()
        {
            Console.WriteLine("Hello World");
        }

        static void IncreaseCount()
        {
            //Console.WriteLine(count++);
            //후위 증가연산자++
            
            //Lock 과 같은 내용, bool을 통해서 키를 받았는지 확인할 수 있다.
            Monitor.Enter(s_lock, ref s_lockIsTake);//wait until resource is released
            if (s_lockIsTake)
            {
                Console.WriteLine(PPAfter(ref count));//count = 1, origin = 0
            }
            else
            { 
                //받아오기 실패
            }

            if (s_lockIsTake)
                Monitor.Exit(s_lock);

            /*
            lock(s_lock) //lock 키워드 : 내부적으로 Monitor.Enter/Exit
            {
                Console.WriteLine(PPAfter(ref count));
            }
            */
        }

        static void IncreaseCount2()
        {
            try
            {
                Monitor.Enter(s_lock, ref s_lockIsTake);// Wait Until reasource is relreased
            }
            catch (Exception e)
            {
                #region 에러가 발생해도, 실행 

                //잡기 어려운 코드에서 사용하는 것, try가 다양한 시도를 할 때
                //존재여부는 list?.Sort(); 이런식으로 활용가능하기 때문
                /* 동적 배열의 공간이 실제 인덱스 갯수와 달라지는 경우
                 *동적 배열 기존 걸 복사해서 쓰니까 , 실제 Length Size != Capacity 용량 
                 *인덱서 접근해야할 때, 실제 5개, 공간 8이라면 5,6,7인덱스에 접근할 가능성이 있다.
                */

                /* 문제가 없는 버그, Console.WriteLine("이 문제는 에디터에서만 발생하는 문제라 무시해도 됨");
                 * 빌드 후에는 잘되는데
                 * 에디터에서는 버그가 발생
                 * ex. 인증 시스템
                 */

                /*
                 * Exception == 모든 에러를 의미
                 * 해당 메세지 별 출력
                 */
                if (e is IndexOutOfRangeException)
                {
                    Console.WriteLine("인덱스 초과함");
                }
                else if (e is NullReferenceException)
                {
                    Console.WriteLine("참조 없음");
                }
                Console.WriteLine(e.Message);//문제 메세지

                //직접 예외를 던지는 경우 if (index > =size) throw new IndexOutOfRangeException(); 
                #endregion
            }
            finally { }
        }
        static int PPAfter(ref int value) // reference 타입
        {
            int origin = value;
            value = value + 1; 
            return origin;
            //여러 쓰레드가 거의 동시 접근할때,
            //origin은 누적되지 않고, 
            //리턴은 0, value만 연산된다.

            //그래서 Lock 걸어준다.
        }

    }
}
