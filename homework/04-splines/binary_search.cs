

public static class binary_search
{
    //Get the ID of x in the list of (ordered) data
    public static int binsearch(double x, double[] data)
        {

        if (data[0] <= x && x<= data[data.Length-1])
        {
            //We know we start somewhere in this range
            int i=0, j=data.Length-1;
            while(j-i>1)
            {//If the indices are not next to one another
                int mid=(i+j)/2;//Rounds down automatically
                if(x>data[mid]) i=mid;//Adjust the bounds accordingly
                else            j=mid;
            }
            return i;

        }
        else
            throw new System.Exception($"binary search of {x} not in [{data[0]}:{data[data.Length-1]}] out of range");
        }
}
