using PostSharp.Aspects;
using System;
using System.Diagnostics;

namespace Wunpus.Aspects
{
    public static class LoggerHelper
    {
        private static readonly int logStartRow = 25; // Ajusta este valor según la altura de tu escenario
        private static readonly int maxLogLines = 5; // Número máximo de líneas de log que se mostrarán
        private static readonly Queue<string> logMessages = new Queue<string>();

        public static void LogMessage(string message)
        {
            // Agregar el nuevo mensaje a la cola
            logMessages.Enqueue(message);

            // Si superamos el número máximo de líneas, eliminamos la más antigua
            if (logMessages.Count > maxLogLines)
            {
                logMessages.Dequeue();
            }

            // Ahora, mostramos todos los mensajes
            int currentRow = logStartRow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            foreach (var msg in logMessages)
            {
                Console.SetCursorPosition(0, currentRow);
                Console.Write(new string(' ', Console.WindowWidth)); // Limpiar la línea
                Console.SetCursorPosition(0, currentRow);
                Console.Write(msg);
                currentRow++;
            }

            Console.ResetColor();
        }
    }



    [Serializable]
    public class LoggingAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            LoggerHelper.LogMessage($"Entrando al método {args.Method.Name}.");
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            LoggerHelper.LogMessage($"Saliendo del método {args.Method.Name}.");
        }
    }


    [Serializable]
    public class ExceptionHandlingAspect : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            LoggerHelper.LogMessage($"Se ha producido una excepción en {args.Method.Name}: {args.Exception.Message}");
            args.FlowBehavior = FlowBehavior.Continue;
        }
    }



    [Serializable]
    public class PerformanceAspect : OnMethodBoundaryAspect
    {
        [NonSerialized]
        private Stopwatch stopwatch;

        public override void OnEntry(MethodExecutionArgs args)
        {
            stopwatch = Stopwatch.StartNew();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            stopwatch.Stop();
            LoggerHelper.LogMessage($"El método {args.Method.Name} tardó {stopwatch.ElapsedMilliseconds} ms en ejecutarse.");
        }
    }


    [Serializable] 
    public class NotNullAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            foreach (var argument in args.Arguments)
            {
                if (argument == null)
                {
                    throw new ArgumentNullException("Un argumento fue nulo.");
                }
            }
        }
    }
}
