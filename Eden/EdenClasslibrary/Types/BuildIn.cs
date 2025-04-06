using EdenClasslibrary.Errors.RuntimeErrors;
using EdenClasslibrary.Errors.SemanticalErrors;
using EdenClasslibrary.Types.EnvironmentTypes;
using EdenClasslibrary.Types.LanguageTypes;
using EdenClasslibrary.Types.LanguageTypes.Collections;
using System.Text;

namespace EdenClasslibrary.Types
{
    public class BuildIn
    {
        private Dictionary<string, FunctionPayload> _functions;
        private Parser _parser;
        public BuildIn(Parser parser)
        {
            _parser = parser;
            _functions = new Dictionary<string, FunctionPayload>();

            RegisterFunc("PrintLine", FunctionPayload.Create(typeof(IObject), [ObjectSignature.Create("input", typeof(IObject))], null));
            RegisterFunc("Print", FunctionPayload.Create(typeof(IObject), [ObjectSignature.Create("input", typeof(IObject))], null));

            RegisterFunc("Inc", FunctionPayload.Create(typeof(IObject), [ObjectSignature.Create("input", typeof(IObject))], null));
            
            RegisterFunc("Length", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("input", typeof(IIndexable))], null));
            RegisterFunc("Min", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("input", typeof(IIndexable))], null));
            RegisterFunc("Max", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("input", typeof(IIndexable))], null));
            RegisterFunc("SinusR", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("radians", typeof(IObject))], null));
            RegisterFunc("SinusD", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("degrees", typeof(IObject))], null));
            RegisterFunc("CosinusR", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("radians", typeof(IObject))], null));
            RegisterFunc("CosinusD", FunctionPayload.Create(typeof(IntObject), [ObjectSignature.Create("degrees", typeof(IObject))], null));

            //  Methods
            RegisterFunc("Add", FunctionPayload.Create(typeof(ObjectCollection),
                [
                    ObjectSignature.Create("ObjectClass", typeof(ObjectCollection)), 
                    ObjectSignature.Create("item", typeof(IObject))
                ], null));
            RegisterFunc("Clear", FunctionPayload.Create(typeof(ObjectCollection),
                [
                    ObjectSignature.Create("ObjectClass", typeof(ObjectCollection)),
                ], null));
            RegisterFunc("RemoveAt", FunctionPayload.Create(typeof(ObjectCollection),
                [
                    ObjectSignature.Create("ObjectClass", typeof(ObjectCollection)),
                    ObjectSignature.Create("item", typeof(IntObject))
                ], null));
        }

        public IObject CallBuildInMethod(IObject classObject, string name, params IObject[] arguments)
        {
            switch (name)
            {
                case "Length":
                    return Length(classObject);
                case "Add":
                    return Add(classObject, arguments[0]);
                case "Clear":
                    return Clear(classObject);
                case "RemoveAt":
                    return RemoveAt(classObject, arguments[0]);
                default:
                    return ErrorRuntimeFuncNotDefined.CreateErrorObject(arguments[0].Token, name);
            }
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
                case "Inc":
                    return Inc(arguments);
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
                    return ErrorRuntimeFuncNotDefined.CreateErrorObject(arguments[0].Token, name);
            }
        }

        #region Methods
        public IObject Length(IObject classObject)
        {
            IObject result = null;
            if (classObject is ObjectCollection asIndexable)
            {
                result = IntObject.Create(asIndexable.Token, asIndexable.Length);
            }
            else
            {
                result = ErrorSemanticalTypeNotIndexable.CreateErrorObject(classObject, classObject.Token, _parser.Lexer.GetLine(classObject.Token));
            }
            return result;
        }

        public IObject Clear(IObject classObject)
        {
            if (classObject is ObjectCollection asIndexable)
            {
                asIndexable.Collection.Clear();
            }
            else
            {
                return ErrorSemanticalTypeNotIndexable.CreateErrorObject(classObject, classObject.Token, _parser.Lexer.GetLine(classObject.Token));
            }
            return NoneObject.Create(classObject.Token);
        }

        public IObject RemoveAt(IObject classObject, IObject argument)
        {
            if (classObject.Type != argument.Type)
            {
                //  TODO
                return null;
            }

            if(argument is not IntObject)
            {
                //  TODO
                return null;
            }

            if (classObject is ObjectCollection classObjAsCol)
            {
                int index = (argument as IntObject).Value;
                try
                {
                    classObjAsCol.Collection.RemoveAt(index);
                }
                catch (Exception)
                {
                    return ErrorRuntimeArgOutOfRange.CreateErrorObject(classObjAsCol, index, argument.Token, _parser.Lexer.GetLine(argument.Token));
                }
            }
            else
            {
                //  TODO
                return null;
            }

            return NoneObject.Create(argument.Token);
        }

        public IObject Add(IObject classObject, IObject argument)
        {
            if(classObject.Type != argument.Type)
            {
                return ErrorSemanticalCollectionArgTypeMismatch.CreateErrorObject(classObject.Type, argument.Type, argument.Token, _parser.Lexer.GetLine(argument.Token));
            }

            if(classObject is ObjectCollection classObjAsCol)
            {
                classObjAsCol.Add(argument);
            }
            else
            {
                ErrorSemanticalTypeNotIndexable.CreateErrorObject(classObject, classObject.Token, _parser.Lexer.GetLine(classObject.Token));
            }

            return NoneObject.Create(argument.Token);
        }
        #endregion

        #region Functions
        public IObject SinusR(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IntObject)
            {
                result = FloatObject.Create(arguments[0].Token, MathF.Sin((arguments[0] as IntObject).Value));
            }
            else if (arguments[0] is FloatObject)
            {
                result = FloatObject.Create(arguments[0].Token, MathF.Sin((arguments[0] as FloatObject).Value));
            }
            else
            {
                result = ErrorSemanticalTypeNotIndexable.CreateErrorObject(arguments[0], arguments[0].Token, _parser.Lexer.GetLine(arguments[0].Token));
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
                result = FloatObject.Create(arguments[0].Token, value);
            }
            else if (arguments[0] is FloatObject)
            {
                float input = ((arguments[0] as FloatObject).Value * MathF.PI) / 180;
                float value = MathF.Sin(input);
                result = FloatObject.Create(arguments[0].Token, value);
            }
            else
            {
                result = ErrorSemanticalTypeNotIndexable.CreateErrorObject(arguments[0], arguments[0].Token, _parser.Lexer.GetLine(arguments[0].Token));
            }
            return result;
        }

        public IObject CosinusR(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IntObject)
            {
                result = FloatObject.Create(arguments[0].Token, MathF.Cos((arguments[0] as IntObject).Value));
            }
            else if (arguments[0] is FloatObject)
            {
                result = FloatObject.Create(arguments[0].Token, MathF.Cos((arguments[0] as FloatObject).Value));
            }
            else
            {
                result = ErrorSemanticalTypeNotIndexable.CreateErrorObject(arguments[0], arguments[0].Token, _parser.Lexer.GetLine(arguments[0].Token));
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
                result = FloatObject.Create(arguments[0].Token, value);
            }
            else if (arguments[0] is FloatObject)
            {
                float input = ((arguments[0] as FloatObject).Value * MathF.PI) / 180;
                float value = MathF.Cos(input);
                result = FloatObject.Create(arguments[0].Token, value);
            }
            else
            {
                result = ErrorSemanticalTypeNotIndexable.CreateErrorObject(arguments[0], arguments[0].Token, _parser.Lexer.GetLine(arguments[0].Token));
            }
            return result;
        }
        public IObject Length(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IIndexable asIndexable)
            {
                //result = asIndexable.Length;
                result = IntObject.Create(arguments[0].Token, asIndexable.Length);
            }
            else
            {
                result = ErrorSemanticalTypeNotIndexable.CreateErrorObject(arguments[0], arguments[0].Token, _parser.Lexer.GetLine(arguments[0].Token));
            }
            return result;
        }
        public IObject Inc(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IObject AsIObj)
            {
                if(AsIObj is IntObject AsInt)
                {
                    result = IntObject.Create(arguments[0].Token, AsInt.Value + 1);
                }
                else if (AsIObj is FloatObject AsFloat)
                {
                    result = FloatObject.Create(arguments[0].Token, AsFloat.Value + 1);
                }
                else if (AsIObj is CharObject AsChar)
                {
                    result = CharObject.Create(arguments[0].Token, ++AsChar.Value);
                }
                else if (AsIObj is StringObject AsString)
                {
                    result = StringObject.Create(arguments[0].Token, AsString.Value + " ");
                }
                else
                {
                    result = ErrorRuntimeFunctionInvalidArg.CreateErrorObject("Inc", arguments[0], arguments[0].Token, _parser.Lexer.GetLine(arguments[0].Token));
                }
            }
            else
            {
                result = ErrorRuntimeFunctionInvalidArg.CreateErrorObject("Inc", arguments[0], arguments[0].Token, _parser.Lexer.GetLine(arguments[0].Token));
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

            return NoneObject.Create(arguments[0].Token);
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

            return NoneObject.Create(arguments[0].Token);
        }

        public IObject Min(IObject[] arguments)
        {
            IObject result = null;
            if (arguments[0] is IIndexable asIndexable)
            {
                if(arguments.Length > 0)
                {
                    IIndexable obj = arguments[0] as IIndexable;

                    if (obj.Length > 0)
                    {
                        IObject min = obj[0];
                        for(int i = 0; i < obj.Length; i++)
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
                        return NullObject.Create(arguments[0].Token);
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
                result = ErrorSemanticalTypeNotIndexable.CreateErrorObject(arguments[0], arguments[0].Token, _parser.Lexer.GetLine(arguments[0].Token));
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

                    if (obj.Length > 0)
                    {
                        IObject max = obj[0];
                        for (int i = 0; i < obj.Length; i++)
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
                        return NullObject.Create(arguments[0].Token);
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
                result = ErrorSemanticalTypeNotIndexable.CreateErrorObject(arguments[0], arguments[0].Token, _parser.Lexer.GetLine(arguments[0].Token));
            }
            return result;
        }
        #endregion
        public bool FunctionExists(string name, IObject[] args)
        {
            bool doesExist = false;
            if (_functions.ContainsKey(name))
            {
                FunctionPayload fp = _functions[name];

                doesExist = fp.ArgumentsSignatureMatch(args);
            }

            return doesExist;
        }

        public FunctionPayload GetFunctionSignature(string name, IObject[] args)
        {
            if (!FunctionExists(name, args))
            {
                return null;
            }
            return _functions[name];
        }

        private void RegisterFunc(string name, FunctionPayload data)
        {
            _functions.Add(name, data);
        }
    }
}
