namespace HuaweiARInternal
{
    using System;

    public class ValueLegalityChecker
    {
        public static bool CheckInt(string methodName, int toCheckValue,int minValue, int maxValue=Int32.MaxValue) 
        {
            if (toCheckValue < minValue || toCheckValue > maxValue)
            {
                ARDebug.LogWarning("{0}: value is {1}, while legal min value is {2}, max value is {3}",
                    methodName,toCheckValue,minValue,maxValue);
                return false;
            }
            return true;
        }

    }
}
