using EdenClasslibrary.Errors;
using EdenClasslibrary.Types.EnvironmentTypes;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.LanguageTypes.Collections;
using System.Text;

namespace EdenClasslibrary.Types
{
    public class BuildIn
    {
        private static BuildIn instance;
        private Dictionary<string, FunctionPayload> _functions;
        private BuildIn()
        {
            _functions = new Dictionary<string, FunctionPayload>();

            RegisterFunc("PrintLine", FunctionPayload.Create(typeof(IObject), [ObjectSignature.Create("input", typeof(IObject))], null));
            RegisterFunc("Print", FunctionPayload.Create(typeof(IObject), [ObjectSignature.Create("input", typeof(IObject))], null));

            RegisterFunc("Length", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("input", typeof(IIndexable))], null));
            RegisterFunc("Min", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("input", typeof(IIndexable))], null));
            RegisterFunc("Max", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("input", typeof(IIndexable))], null));
            RegisterFunc("SinusR", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("radians", typeof(IObject))], null));
            RegisterFunc("SinusD", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("degrees", typeof(IObject))], null));
            RegisterFunc("CosinusR", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("radians", typeof(IObject))], null));
            RegisterFunc("CosinusD", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("degrees", typeof(IObject))], null));
        }

        public IObject CallBuildInFunc(string name, params IObject[] arguments)
        {
            switch (name)
            {
                case "PrintLine":
                    return PrintLine(arguments);
                case "Print":
                    return Print(arguments);
                case "Length":
                    return Length(arguments);
                case "Min":
                    return Min(arguments);
                case "Max":
                    return Max(arguments);
                case "SinusR":
                    return SinusR(arguments);
                case "SinusD":
                    return SinusD(arguments);
                case "CosinusR":
                    return CosinusR(arguments);
                case "CosinusD":
                    return CosinusD(arguments);
                default:
                    //  TODO: Fix
                    return new ErrorObject(new ErrorInvalidBuildInFuncCall());
            }
        }

        public IObject SinusR(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IntObject)
            {
                result = FloatObject.Create(MathF.Sin((arguments[0] as IntObject).Value));
            }
            else if (arguments[0] is FloatObject)
            {
                result = FloatObject.Create(MathF.Sin((arguments[0] as FloatObject).Value));
            }
            else
            {
                result = ErrorObjectTypeIsNotIndexable.CreateErrorObject(arguments[0]);
            }
            return result;
        }

        public IObject SinusD(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IntObject)
            {
                float input = ((arguments[0] as IntObject).Value * MathF.PI) / 180;
                float value = MathF.Sin(input);
                result = FloatObject.Create(value);
            }
            else if (arguments[0] is FloatObject)
            {
                float input = ((arguments[0] as FloatObject).Value * MathF.PI) / 180;
                float value = MathF.Sin(input);
                result = FloatObject.Create(value);
            }
            else
            {
                result = ErrorObjectTypeIsNotIndexable.CreateErrorObject(arguments[0]);
            }
            return result;
        }

        public IObject CosinusR(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IntObject)
            {
                result = FloatObject.Create(MathF.Cos((arguments[0] as IntObject).Value));
            }
            else if (arguments[0] is FloatObject)
            {
                result = FloatObject.Create(MathF.Cos((arguments[0] as FloatObject).Value));
            }
            else
            {
                result = ErrorObjectTypeIsNotIndexable.CreateErrorObject(arguments[0]);
            }
            return result;
        }

        public IObject CosinusD(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IntObject)
            {
                float input = ((arguments[0] as IntObject).Value * MathF.PI) / 180;
                float value = MathF.Cos(input);
                result = FloatObject.Create(value);
            }
            else if (arguments[0] is FloatObject)
            {
                float input = ((arguments[0] as FloatObject).Value * MathF.PI) / 180;
                float value = MathF.Cos(input);
                result = FloatObject.Create(value);
            }
            else
            {
                result = ErrorObjectTypeIsNotIndexable.CreateErrorObject(arguments[0]);
            }
            return result;
        }
        public IObject Length(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IIndexable asIndexable)
            {
                result = asIndexable.Length;
            }
            else
            {
                result = ErrorObjectTypeIsNotIndexable.CreateErrorObject(arguments[0]);
            }
            return result;
        }

        public IObject PrintLine(IObject[] arguments)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < arguments.Length; i++)
            {
                sb.Append($"{arguments[i].AsString()}");
                if(i < arguments.Length - 1)
                {
                    sb.Append(" ");
                }
            }
            string result = sb.ToString();

            
            Console.WriteLine(result);

            return StringObject.Create(result);
        }

        public IObject Print(IObject[] arguments)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < arguments.Length; i++)
            {
                sb.Append($"{arguments[i].AsString()}");
                if (i < arguments.Length - 1)
                {
                    sb.Append(" ");
                }
            }
            string result = sb.ToString();

            Console.Write(result);

            return StringObject.Create(result);
        }

        public IObject Min(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IIndexable asIndexable)
            {
                if(arguments.Length > 0)
                {
                    IIndexable obj = arguments[0] as IIndexable;

                    if (obj.Length.Value > 0)
                    {
                        IObject min = obj[0];
                        for(int i = 0; i < obj.Length.Value; i++)
                        {
                            if(min is IObjectComparable && obj[i] is IObjectComparable)
                            {
                                if((min as IObjectComparable).Greater(obj[i] as IObjectComparable))
                                {
                                    min = obj[i];
                                }
                            }
                            else
                            {
                                return min;
                            }
                        }
                        result = min;
                    }
                    else
                    {
                        return NullObject.Create();
                    }
                }
                else
                {
                    //  TODO: return error object 'Collection empty'
                    return null;
                }
            }
            else
            {
                result = ErrorObjectTypeIsNotIndexable.CreateErrorObject(arguments[0]);
            }
            return result;
        }
        public IObject Max(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IIndexable asIndexable)
            {
                if (arguments.Length > 0)
                {
                    IIndexable obj = arguments[0] as IIndexable;

                    if (obj.Length.Value > 0)
                    {
                        IObject max = obj[0];
                        for (int i = 0; i < obj.Length.Value; i++)
                        {
                            if (max is IObjectComparable && obj[i] is IObjectComparable)
                            {
                                if ((max as IObjectComparable).Lesser(obj[i] as IObjectComparable))
                                {
                                    max = obj[i];
                                }
                            }
                            else
                            {
                                return max;
                            }
                        }
                        result = max;
                    }
                    else
                    {
                        return NullObject.Create();
                    }
                }
                else
                {
                    //  TODO: return error object 'Collection empty'
                    return null;
                }
            }
            else
            {
                result = ErrorObjectTypeIsNotIndexable.CreateErrorObject(arguments[0]);
            }
            return result;
        }
        public bool FunctionExists(string name)
        {
            return _functions.ContainsKey(name);
        }

        public FunctionPayload GetFunctionSignature(string name)
        {
            if (!FunctionExists(name))
            {
                return null;
            }
            return _functions[name];
        }

        private void RegisterFunc(string name, FunctionPayload data)
        {
            _functions.Add(name, data);
        }

        public static BuildIn GetInstance()
        {
            if(instance == null)
            {
                instance = new BuildIn();
            }
            return instance;
        }
    }
}
