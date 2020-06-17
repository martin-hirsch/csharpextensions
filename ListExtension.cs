public static (List<T> firstHalf, List<T> secondHalf, bool divisionByTwoIsPossible) HalfList<T>(this List<T> me)
        {
            var firstHalf = new List<T>();
            var secondHalf = new List<T>();
            var divisionByTwoIsPossible = false;
            if (me.Count == 1)
            {
                firstHalf.Add(me.Single());
            }
            else if (me.Count % 2 != 0)
            {
                var unevenHalfSize = me.Count / 2d;
                var evenHalfSize = (int)Math.Round(r, MidpointRounding.ToEven);
                firstHalf.AddRange(me.GetRange(0, evenHalfSize));
                secondHalf.AddRange(me.Where(x => !firstHalf.Contains(x)));
            }
            else
            {
                var halfSize = me.Count / 2;
                firstHalf.AddRange(me.GetRange(0, halfSize));
                secondHalf.AddRange(me.Where(x => !firstHalf.Contains(x)));

                divisionByTwoIsPossible = true;
              }
            
  return (firstHalf, secondHalf, divisionByTwoIsPossible);
}
