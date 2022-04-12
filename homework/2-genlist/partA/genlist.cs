public class genlist<T>
{
//A generic list of type T wit hdynamic size
    public T[] data;//I do not like having the data be public, I really want to use a get function instead, but if the exercise asks me for this, I will do so
    private int n;
    public genlist()
    {
        data = new T[0];
        n=0;//Empty list
    }

    //I like to specify where we push, i.e. push_back (maybe I am just biased in favor of c++ std::vector), but the exercise asked me for this name
    public void push(T item)
    {
        T[] newdata = new T[n+1];

        for (int i = 0; i<n; ++i)
            newdata[i]=data[i];
        data=newdata;//Move by reference, old data will be picked up by the garbage
        data[n]=item;
        ++n;
    }

    public T get(int i)
    {
        if (i>=0)
            return data[i];//if i>n we get an error, so no reason to throw anything manually
        else
            return data[n-1-i];//I like the python way of accessing the back of the array, lets copy that
    }
    //Ok, the exercise wanted a signed integer ... I want to use an unsigned integer, but C# is much worse at implicit conversions than c++, so ok, signed it is
	public int size {get{return n;}} // property, ok this is kind of cool that this looks like a variable without being one
}
