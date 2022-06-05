
try
{

    Calculate();
}
catch(Exception ex)
{
    Console.WriteLine($"В калькуляторе произошла ошибка:{ex.Message} ") ;
}




void Calculate()
{

    
  do
  {
      try
      {
        List<object> values = GetNumber("Пожайлуйста, введите желаемые значения через пробел : ");
        int result = GetResult(values);

        Console.WriteLine(result);       
      }
      catch (OverflowException)
      {
          Console.BackgroundColor = ConsoleColor.Blue;
          Console.WriteLine("Результат выражения вышел за границы int");
          Console.BackgroundColor = ConsoleColor.Black;
      }
      catch (RangeException)
      {
          Console.BackgroundColor = ConsoleColor.Black;
      }

      catch (OperatorException ex)
      {
          
          Console.WriteLine("Укажите в выражении оператор: +,-,*,/   {0} - {1}", ex.Message, ex.Data["user"]);
          Console.BackgroundColor = ConsoleColor.Black;
      }

      catch (FormatException)
      {
          Console.BackgroundColor = ConsoleColor.Magenta;
          Console.WriteLine("операнд не является числом! Введите корректное число");
          Console.BackgroundColor = ConsoleColor.Black;
      }
      catch (Exception)
      {
          Console.WriteLine("Я не смог обработать ошибку");
          Console.WriteLine("Попробуй еще раз");
          throw;
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

int GetResult(List<object> values)
{
    int x = (int)values[0];
    string cmd = (string)values[1];
    int y = (int)values[2];
    
    int result=0;

    try
    {

        switch (cmd)
        {
            case "+":
                result = Sum(x,y);
                break;
            case "-":
                result = Sub(x, y);
                break;
            case "*":
                result = Mul(x, y);
                break;
            case "/":
                result = Div(x, y);
                break;
            default:
                result = 0;
                break;
        }

        if (result == 13)
            throw new OperandThirteen("вы получили ответ 13!");// проблема
        return result;

    }
     catch (OperandThirteen)
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }
    return result;

}

int Sum(int x, int y)
{
    return x + y;
}

int Sub(int x, int y)
{
    return x - y;
}

int Mul(int x, int y)
{
    return x * y;
}

int Div(int x, int y)
{
    try
    {
        return x / y;
    }
    catch(DivideByZeroException)
    {
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Деление на ноль!");
        Console.BackgroundColor = ConsoleColor.Black;
        return 0;
    }
}



List<object> GetNumber(string text)
{
    
    Console.WriteLine(text);
        
    string UserInput = Console.ReadLine();
    string[] values = UserInput.Split(' ');
    if (values.Length != 3)
        throw new RangeException("Выражение некорректное, попробуйте написать в формате \na + b\na - b\na * b\na / b");
    int x = Int32.Parse(values[0]);
    string cmd = values[1];
    int y = Int32.Parse(values[2]);

    if ((cmd != "+") && (cmd != "-") && (cmd != "*") && (cmd != "/") )
    {
        var exception = new OperatorException($"Я пока не умею работать с оператором {cmd}");
        exception.Data.Add("user", "Name of operator");
        throw exception;
    }
    
   
    List<object> str = new List<object> { x, cmd, y };  

    return str;
}

public class RangeException: Exception
{
    public RangeException(string message)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
    }

   
}
public class OperatorException : Exception
{
    public OperatorException(string message)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
    }
}
public class OperandThirteen : Exception
{
    public OperandThirteen(string message)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
    }
}



