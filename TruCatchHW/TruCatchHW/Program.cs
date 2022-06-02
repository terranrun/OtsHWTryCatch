

try
{
Calculate();

}catch
{
    Console.WriteLine("В калькуляторе произошла ошибка: текст ошибки");
}


void Calculate()
{
    do
    {
        List<object> values = GetNumber("Пожайлуйста, введите желаемые значения через пробел : ") ;
        int result = GetResult(values);

        Console.WriteLine(result);

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
    
    int result;

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
    {
        throw new OperandThirteen("вы получили ответ 13!");
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
    catch(DivideByZeroException ex)
    {
        Console.BackgroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Деление на ноль!");
        return 0;
    }
}



List<object> GetNumber(string text)
{
    List<object> str = new List<object>();
    Console.WriteLine(text);
    bool xDone = false;
    try
    {
        while(xDone == false)
        {
            try
            {
                string UserInput = Console.ReadLine();
                string[] values = UserInput.Split(' ');
                if (values.Length != 3)
                    throw new RangeException("Выражение некорректное, попробуйте написать в формате \na + b\na - b\na * b\na / b");
                int x = Int32.Parse(values[0]);
                string cmd = values[1];
                int y = Int32.Parse(values[2]);
               
                if ((cmd != "+") && (cmd != "-") && (cmd != "*") && (cmd != "/"))
                     throw new OperatorException($"Я пока не умею работать с оператором {cmd}");
  
                str = new List<object> { x, cmd, y };  
               
                xDone = true;
                return str;

            }
            catch (OverflowException ex)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("Результат выражения вышел за границы int");
            }
            catch (RangeException)
            {

            }
            catch(OperandThirteen)
            {

            }
            catch(OperatorException)
            {           
                Console.WriteLine("Укажите в выражении оператор: +,-,*,/ ");
            }
           
            catch (FormatException)
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.WriteLine("операнд не является числом! Введите корректное число");           
            }
            catch (Exception)
            {
                Console.WriteLine("Я не смог обработать ошибку");
                Console.WriteLine("Попробуй еще раз");
                throw;
            }
        }
        return str;
    }
    catch 
    {
        Console.WriteLine("------------");
        return str;
    }

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



