namespace EdenClasslibrary.Types.LanguageTypes
{
    public static class ObjectHelpers
    {
        public static bool IsTruthy(IObject obj)
        {
            if(obj is not NullObject && obj is BoolObject asBool && asBool.Value == true)
            {
                return true;
            }
            return false;
        }
    }
}
