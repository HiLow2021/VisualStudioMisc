using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using My.Randomizer;

namespace Maze
{
    public static class IListExtensions
    {
        public static T ElementAtRandomly<T>(this IList<T> List, XorShift Randomizer)
        {
            return List[Randomizer.Next(List.Count)];
        }

        public static void Shuffle<T>(this IList<T> List, XorShift Randomizer)
        {
            for (int i = 0; i < List.Count; i++)
            {
                T tmp = List[i];
                int Index = Randomizer.Next(i, List.Count);
                List[i] = List[Index];
                List[Index] = tmp;
            }
        }
    }
}
