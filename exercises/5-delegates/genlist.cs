public class genlist<T>
{
//A generic list of type T wit hdynamic size
    public T[] data;//Public type array
    public genlist()
    {
        data = new T[0];
    }

    //Specify where we push
    public void push(T item)
    {
        int n = data.Length;
        T[] newdata = new T[n+1];

        for (int i = 0; i<n; ++i)
            newdata[i]=data[i];
        data=newdata;//Move by reference, old data will be picked up by the garbage
        data[n]=item;
    }

}
