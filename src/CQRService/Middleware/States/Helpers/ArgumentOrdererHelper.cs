
using CQRService.ExceptionHandling.MiddlewareExceptions;

namespace CQRService.Middleware.States
{
    internal static class ArgumentOrdererHelper
    {
        private static int GetIndexOfType(this object[] args, Type type)
        {

            for (int i = 0; i < args.Length; i++)
            {
                bool result = args[i].GetType().IsAssignableTo(type);
                if(!result) continue;
                if (result) return i;
            }
            return -1;
        }
        private static int GetDifferenceOfSize(this object[] args, object[] args2)
        {
            return args.Length - args2.Length;
        }
        internal static object[] OrderByTypeArray(this object[] args, Type[] types)
        {
            var difference = args.GetDifferenceOfSize(types);
            if (difference < 0)
            {
                throw new MissingCollectiveArgumentException(MiddlewareExceptionMessages.MissingCollectiveArgumentMessage);
            }
            else if (difference > 0)
            {
                throw new MissingHandlerArgumentException(MiddlewareExceptionMessages.MissingHandlerArgumentMessage);
            }
            else
            {
                var orderedArgs = new object[types.Length];
                for (int i = 0; i < types.Length; i++)
                {
                    var index = args.GetIndexOfType(types[i]);
                    if(index == -1) throw new NotMatchedArgumentException(MiddlewareExceptionMessages.NotMatchedArgumentException);
                    orderedArgs[i] = args[index];
                }
                return orderedArgs;
            }
        }
    }
}