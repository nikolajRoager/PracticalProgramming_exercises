//Implement a linked list (the exercise never calls it that, but this is what it is)

public class linkedList<T>
{
    //With default values, we do not need a constructor
    public node<T> first=null,current=null;
    private int n = 0;

    public void push(T item)
    {
        if(first==null)
        {
                first=new node<T>(item);
                current=first;
                n=0;
        }
        else
        {
                current.next = new node<T>(item);
                current=current.next;
        }

        ++n;
    }
    //Reset to first note
    public void start()
    {
        current=first;
    }
    //Travel the list
    public void next()
    {
        current=current.next;
    }

    //These functions were not required, but I like having them
    public int size
    {
        get{return n;}
    }

    public T get(int I)
    {
        //Do not overwrite current
        node<T> This=first;
        for (int i = 0; i < I; ++i)
            This=This.next;
        return  This.item;
    }

}
