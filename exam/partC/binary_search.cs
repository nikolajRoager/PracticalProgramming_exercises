
public static class binary_search
{
    //Get the ID of x in the list of (ordered) data
    public static (int,double) binsearch(double x,  vector data)
        {

            //Check that we are in range
            if (data[0] <= x && x<= data[data.size-1])
            {
                //We know we start somewhere in this range
                int i=0, j=data.size-1;
                while(j-i>1)
                {//If the indices are not next to one another
                    int mid=(i+j)/2;//Rounds down automatically
                    if(x>data[mid]) i=mid;//Adjust the bounds accordingly
                    else            j=mid;
                }
                return (i,x);//we are in range, and x is ok

            }
            else
            {
                //We are outside range, so adjust x until it is at the border
                if (x<data[0])
                    return (0,data[0]);
                else
                    return (data.size-1,data[data.size-1]);
            }
        }
}
