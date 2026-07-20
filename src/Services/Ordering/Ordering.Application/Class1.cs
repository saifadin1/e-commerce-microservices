namespace Ordering.Application;


public class rectangle
{
    public int length { get; set; }
    public int width { get; set; }
    
    public int area()
    {
        return length * width;
    }
}

public class square : rectangle
{
    public new int length { get;
        set
        {
            base.length = base.width = value;
        }
    }
    
    
    
}

public class Class1
{
     rectangle x = new square();
        
    

}