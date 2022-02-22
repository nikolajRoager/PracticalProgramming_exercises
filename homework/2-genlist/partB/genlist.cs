public class genlist<T>
{
//A generic list of type T wit hdynamic size
    public T[] data;//I do not like having the data be public, I really want to use a get function instead, but if the exercise asks me for this, I will do so
    private int n;
    public int capacity;
    public genlist()
    {
        data = new T[1];
        n=0;//Empty list
        capacity=1;
    }

    //I like to specify where we push, i.e. push_back (maybe I am just biased in favor of c++ std::vector), but the exercise asked me for this name
    public void push(T item)
    {

        //Check if we need to increase size
        if (n==capacity)
        {
            capacity*=2;
            T[] newdata = new T[capacity];

            for (int i = 0; i<n; ++i)
                newdata[i]=data[i];
            data=newdata;//Move by reference, old data will be picked up by the garbage
        }

        data[n]=item;


        ++n;//Always increment the size
    }

    public T get(int i)
    {
        if (i>0)
            return data[i];//if i>n we get an error, so no reason to throw anything manually
        else
            return data[n-1-i];//I like the python way of accessing the back of the array, lets copy that
    }


    public void remove(int I)
    {
        //Move back all elements after part I
        for (int i = I; i<n-1; ++i)
            data[i]=data[i+1];
        --n;
        if (n*2<=capacity)//If we can half capacity again, do it
        {
            capacity=capacity/2;
            T[] newdata = new T[capacity];

            for (int i = 0; i<n; ++i)
                newdata[i]=data[i];
            data=newdata;//Move by reference, old data will be picked up by the garbage
        }
    }

    //Ok, the exercise wanted a signed integer ... I want to use an unsigned integer, but C# is much worse at implicit conversions than c++, so ok, signed it is
	public int size {get{return n;}} // property, ok this is kind of cool that this looks like a variable without being one
}
