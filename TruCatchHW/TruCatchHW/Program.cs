Calculate();


void Calculate()
{
    do
    {
        List<object> values = GetNumber("Пожайлуйста, введите желаемые значения через пробел : ") ;

        foreach(var item in values)
        {
            Console.WriteLine(item);
        }
       

    } while (AskContinue());
}

bool AskContinue()
{
    while (true)
    {
        Console.WriteLine("не хочешь продолжать? напиши STOP");
        var status = Console.ReadLine();
        if (status == "STOP" || status == "stop" || status == "Stop")
            return false;     
        else
        {
            Console.WriteLine("Продолжаем) ");
            return true;
        }
        
    }
}

int GetResult(int x, int y, string? cmd)
{
    throw new NotImplementedException();
}

List<object> GetNumber(string text)
{
    Console.WriteLine(text);
    int x = Int32.Parse(Console.ReadLine());
    var cmd = Console.ReadLine(); 
    int y = Int32.Parse(Console.ReadLine());
    List<object> str = new List<object> { x , cmd, y} ;

    return str;
      
}