
//A node in a one-way linked list (we do not know our parent node)
public class node<T>
{
    //I would much prefer using a get method, but maybe that is just me conforming to the norms of C++ which might not be valid in C#
	public T item;
	public node<T> next;
	public node(T item)
    {
        this.item=item;
    }
}
