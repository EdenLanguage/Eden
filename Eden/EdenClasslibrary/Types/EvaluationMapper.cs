using EdenClasslibrary.Errors;
using EdenClasslibrary.Types.LanguageTypes;

namespace EdenClasslibrary.Types
{
    /// <summary>
    /// This class task is to map function to evaluate (for noew only binary) expression between different language data types.
    /// </summary>
    public class EvaluationMapper
    {
        #region Own types
        private class UnaryFuncArgsWrapper
        {
            public TokenType OperatorToken { get; }
            public Type Type { get; }

            private UnaryFuncArgsWrapper(TokenType op, Type type)
            {
                OperatorToken = op;
                Type = type;
            }

            public static UnaryFuncArgsWrapper Create(TokenType op, Type type)
            {
                return new UnaryFuncArgsWrapper(op, type);
            }

            public override bool Equals(object? obj)
            {
                if (obj is UnaryFuncArgsWrapper other)
                {
                    return OperatorToken == other.OperatorToken && Type == other.Type;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(OperatorToken, Type);
            }
        }

        private class BinaryFuncArgsWrapper
        {
            public Type Left { get; }
            public TokenType OperatorToken { get; }
            public Type Right { get; }
            
            private BinaryFuncArgsWrapper(Type left, TokenType op, Type right)
            {
                Left = left;
                OperatorToken = op;
                Right = right;
            }

            public static BinaryFuncArgsWrapper Create(Type left, TokenType op, Type right)
            {
                return new BinaryFuncArgsWrapper(left, op, right);
            }

            public override bool Equals(object? obj)
            {
                if (obj is BinaryFuncArgsWrapper other)
                {
                    return Left == other.Left && OperatorToken == other.OperatorToken && Right == other.Right;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Left, OperatorToken, Right);
            }
        }
        #endregion

        #region Fields
        private ErrorsManager _errorsManager;
        private Dictionary<BinaryFuncArgsWrapper, Func<IObject, IObject, IObject>> _binaryMappings;
        private Dictionary<UnaryFuncArgsWrapper, Func<IObject, IObject>> _unaryMappings;
        #endregion

        #region Constructor
        public EvaluationMapper()
        {
            _errorsManager = new ErrorsManager();
            _binaryMappings = new Dictionary<BinaryFuncArgsWrapper, Func<IObject, IObject, IObject>>();
            _unaryMappings = new Dictionary<UnaryFuncArgsWrapper, Func<IObject, IObject>>();

            #region Register binary functions

            //  Assign
            RegisterMethod(typeof(IntObject), TokenType.Assign, typeof(IntObject), Int_Assign_All_Func);
            RegisterMethod(typeof(IntObject), TokenType.Assign, typeof(CharObject), Int_Assign_All_Func);
            RegisterMethod(typeof(IntObject), TokenType.Assign, typeof(FloatObject), Int_Assign_All_Func);
            
            RegisterMethod(typeof(CharObject), TokenType.Assign, typeof(CharObject), Char_Assign_All_Func);

            RegisterMethod(typeof(FloatObject), TokenType.Assign, typeof(FloatObject), Float_Assign_All_Func);
            RegisterMethod(typeof(FloatObject), TokenType.Assign, typeof(IntObject), Float_Assign_All_Func);
            RegisterMethod(typeof(FloatObject), TokenType.Assign, typeof(CharObject), Float_Assign_All_Func);

            RegisterMethod(typeof(StringObject), TokenType.Assign, typeof(StringObject), String_Assign_String_Func);
            
            //  Int
            RegisterMethod(typeof(IntObject), TokenType.Plus, typeof(IntObject), Int_Add_Int_Func);
            RegisterMethod(typeof(IntObject), TokenType.Plus, typeof(FloatObject), Func_Float_Add_Float);
            RegisterMethod(typeof(IntObject), TokenType.Plus, typeof(CharObject), Func_Float_Add_Float);

            RegisterMethod(typeof(IntObject), TokenType.Minus, typeof(IntObject), Int_Subtract_Int_Func);
            RegisterMethod(typeof(IntObject), TokenType.Minus, typeof(FloatObject), Func_Float_Subtract_Float);
            RegisterMethod(typeof(IntObject), TokenType.Minus, typeof(CharObject), Func_Float_Subtract_Float);

            RegisterMethod(typeof(IntObject), TokenType.Star, typeof(IntObject), Int_Multiply_Int_Func);
            RegisterMethod(typeof(IntObject), TokenType.Star, typeof(FloatObject), Func_Float_Multiply_Float);
            RegisterMethod(typeof(IntObject), TokenType.Star, typeof(CharObject), Func_Float_Multiply_Float);

            RegisterMethod(typeof(IntObject), TokenType.Slash, typeof(IntObject), Int_Devide_Int_Func);
            RegisterMethod(typeof(IntObject), TokenType.Slash, typeof(FloatObject), Func_Float_Division_Float);
            RegisterMethod(typeof(IntObject), TokenType.Slash, typeof(CharObject), Func_Float_Division_Float);

            RegisterMethod(typeof(IntObject), TokenType.Equal, typeof(IntObject), Int_Equal_Int_Func);
            RegisterMethod(typeof(IntObject), TokenType.Equal, typeof(FloatObject), Func_Float_Equal_Float);
            RegisterMethod(typeof(IntObject), TokenType.Equal, typeof(CharObject), Func_Float_Equal_Float);

            RegisterMethod(typeof(IntObject), TokenType.Inequal, typeof(IntObject), Int_InEqual_Int_Func);
            RegisterMethod(typeof(IntObject), TokenType.Inequal, typeof(FloatObject), Func_Float_InEqual_Float);
            RegisterMethod(typeof(IntObject), TokenType.Inequal, typeof(CharObject), Func_Float_InEqual_Float);

            RegisterMethod(typeof(IntObject), TokenType.LeftArrow, typeof(IntObject), Int_Smaller_Int_Func);
            RegisterMethod(typeof(IntObject), TokenType.LeftArrow, typeof(FloatObject), Func_Float_Lesser_Float);
            RegisterMethod(typeof(IntObject), TokenType.LeftArrow, typeof(CharObject), Func_Float_Lesser_Float);

            RegisterMethod(typeof(IntObject), TokenType.RightArrow, typeof(IntObject), Int_Bigger_Int_Func);
            RegisterMethod(typeof(IntObject), TokenType.RightArrow, typeof(FloatObject), Func_Float_Greater_Float);
            RegisterMethod(typeof(IntObject), TokenType.RightArrow, typeof(CharObject), Func_Float_Greater_Float);

            RegisterMethod(typeof(IntObject), TokenType.GreaterOrEqual, typeof(IntObject), Int_GreaterEqual_Int_Func);
            RegisterMethod(typeof(IntObject), TokenType.GreaterOrEqual, typeof(FloatObject), Func_Float_GreaterEqual_Float);
            RegisterMethod(typeof(IntObject), TokenType.GreaterOrEqual, typeof(CharObject), Func_Float_GreaterEqual_Float);

            RegisterMethod(typeof(IntObject), TokenType.LesserOrEqual, typeof(IntObject), Int_SmallerEqual_Int_Func);
            RegisterMethod(typeof(IntObject), TokenType.LesserOrEqual, typeof(FloatObject), Func_Float_LesserEqual_Float);
            RegisterMethod(typeof(IntObject), TokenType.LesserOrEqual, typeof(CharObject), Func_Float_LesserEqual_Float);

            //  Char
            RegisterMethod(typeof(CharObject), TokenType.Plus, typeof(CharObject), Char_Add_Char_Func);
            RegisterMethod(typeof(CharObject), TokenType.Plus, typeof(IntObject), Func_Float_Add_Float);

            RegisterMethod(typeof(CharObject), TokenType.Minus, typeof(CharObject), Char_Subtract_Char_Func);
            RegisterMethod(typeof(CharObject), TokenType.Minus, typeof(IntObject), Func_Float_Subtract_Float);

            RegisterMethod(typeof(CharObject), TokenType.Star, typeof(CharObject), Char_Multiply_Char_Func);
            RegisterMethod(typeof(CharObject), TokenType.Star, typeof(IntObject), Func_Float_Multiply_Float);

            RegisterMethod(typeof(CharObject), TokenType.Slash, typeof(CharObject), Char_Divide_Char_Func);
            RegisterMethod(typeof(CharObject), TokenType.Slash, typeof(IntObject), Func_Float_Division_Float);

            RegisterMethod(typeof(CharObject), TokenType.Equal, typeof(CharObject), Char_Equal_Char_Func);
            RegisterMethod(typeof(CharObject), TokenType.Equal, typeof(IntObject), Func_Float_Equal_Float);

            RegisterMethod(typeof(CharObject), TokenType.Inequal, typeof(CharObject), Char_InEqual_Char_Func);
            RegisterMethod(typeof(CharObject), TokenType.Inequal, typeof(IntObject), Func_Float_InEqual_Float);

            RegisterMethod(typeof(CharObject), TokenType.LeftArrow, typeof(CharObject), Char_Smaller_Char_Func);
            RegisterMethod(typeof(CharObject), TokenType.LeftArrow, typeof(IntObject), Func_Float_Lesser_Float);

            RegisterMethod(typeof(CharObject), TokenType.RightArrow, typeof(CharObject), Char_Greater_Char_Func);
            RegisterMethod(typeof(CharObject), TokenType.RightArrow, typeof(IntObject), Func_Float_Greater_Float);

            RegisterMethod(typeof(CharObject), TokenType.GreaterOrEqual, typeof(CharObject), Char_BiggerEqual_Char_Func);
            RegisterMethod(typeof(CharObject), TokenType.GreaterOrEqual, typeof(IntObject), Func_Float_GreaterEqual_Float);

            RegisterMethod(typeof(CharObject), TokenType.LesserOrEqual, typeof(CharObject), Char_SmallerEqual_Char_Func);
            RegisterMethod(typeof(CharObject), TokenType.LesserOrEqual, typeof(IntObject), Func_Float_LesserEqual_Float);

            //  Bool
            RegisterMethod(typeof(BoolObject), TokenType.Equal, typeof(BoolObject), Bool_Equal_Bool_Func);
            RegisterMethod(typeof(BoolObject), TokenType.Inequal, typeof(BoolObject), Bool_InEqual_Bool_Func);

            //  Float
            RegisterMethod(typeof(FloatObject), TokenType.Plus, typeof(FloatObject), Func_Float_Add_Float);
            RegisterMethod(typeof(FloatObject), TokenType.Plus, typeof(IntObject), Func_Float_Add_Float);

            RegisterMethod(typeof(FloatObject), TokenType.Minus, typeof(FloatObject), Func_Float_Subtract_Float);
            RegisterMethod(typeof(FloatObject), TokenType.Minus, typeof(IntObject), Func_Float_Subtract_Float);

            RegisterMethod(typeof(FloatObject), TokenType.Star, typeof(FloatObject), Func_Float_Multiply_Float);
            RegisterMethod(typeof(FloatObject), TokenType.Star, typeof(IntObject), Func_Float_Multiply_Float);

            RegisterMethod(typeof(FloatObject), TokenType.Slash, typeof(FloatObject), Func_Float_Division_Float);
            RegisterMethod(typeof(FloatObject), TokenType.Slash, typeof(IntObject), Func_Float_Division_Float);

            RegisterMethod(typeof(FloatObject), TokenType.Equal, typeof(FloatObject), Func_Float_Equal_Float);
            RegisterMethod(typeof(FloatObject), TokenType.Equal, typeof(IntObject), Func_Float_Equal_Float);

            RegisterMethod(typeof(FloatObject), TokenType.Inequal, typeof(FloatObject), Func_Float_InEqual_Float);
            RegisterMethod(typeof(FloatObject), TokenType.Inequal, typeof(IntObject), Func_Float_InEqual_Float);

            RegisterMethod(typeof(FloatObject), TokenType.LeftArrow, typeof(FloatObject), Func_Float_Lesser_Float);
            RegisterMethod(typeof(FloatObject), TokenType.LeftArrow, typeof(IntObject), Func_Float_Lesser_Float);

            RegisterMethod(typeof(FloatObject), TokenType.RightArrow, typeof(FloatObject), Func_Float_Greater_Float);
            RegisterMethod(typeof(FloatObject), TokenType.RightArrow, typeof(IntObject), Func_Float_Greater_Float);

            RegisterMethod(typeof(FloatObject), TokenType.GreaterOrEqual, typeof(FloatObject), Func_Float_GreaterEqual_Float);
            RegisterMethod(typeof(FloatObject), TokenType.GreaterOrEqual, typeof(IntObject), Func_Float_GreaterEqual_Float);

            RegisterMethod(typeof(FloatObject), TokenType.LesserOrEqual, typeof(FloatObject), Func_Float_LesserEqual_Float);
            RegisterMethod(typeof(FloatObject), TokenType.LesserOrEqual, typeof(IntObject), Func_Float_LesserEqual_Float);

            //  Null
            RegisterMethod(typeof(NullObject), TokenType.Equal, typeof(NullObject), Null_Equal_Null_Func);
            RegisterMethod(typeof(NullObject), TokenType.Inequal, typeof(NullObject), Null_InEqual_Null_Func);

            //  String
            RegisterMethod(typeof(StringObject), TokenType.Plus, typeof(CharObject), String_Plus_Any_Func);
            RegisterMethod(typeof(StringObject), TokenType.Plus, typeof(StringObject), String_Plus_Any_Func);
            RegisterMethod(typeof(StringObject), TokenType.Plus, typeof(IntObject), String_Plus_Any_Func);
            RegisterMethod(typeof(StringObject), TokenType.Plus, typeof(FloatObject), String_Plus_Any_Func);
            RegisterMethod(typeof(StringObject), TokenType.Plus, typeof(NullObject), String_Plus_Any_Func);
            RegisterMethod(typeof(StringObject), TokenType.Plus, typeof(BoolObject), String_Plus_Any_Func);

            #endregion

            #region Register unary functions
            RegisterMethod(TokenType.Minus, typeof(IntObject), Unary_Minus_Numberic);
            RegisterMethod(TokenType.Minus, typeof(FloatObject), Unary_Minus_Numberic);

            RegisterMethod(TokenType.QuenstionMark, typeof(IntObject), Unary_Question);
            RegisterMethod(TokenType.QuenstionMark, typeof(FloatObject), Unary_Question);
            RegisterMethod(TokenType.QuenstionMark, typeof(BoolObject), Unary_Question);

            RegisterMethod(TokenType.ExclemationMark, typeof(IntObject), Unary_Negation_Numberic);
            RegisterMethod(TokenType.ExclemationMark, typeof(FloatObject), Unary_Negation_Numberic);
            RegisterMethod(TokenType.ExclemationMark, typeof(BoolObject), Unary_Negation_Bool);
            
            RegisterMethod(TokenType.Tilde, typeof(IntObject), Unary_Reverse_Integer);
            RegisterMethod(TokenType.Tilde, typeof(BoolObject), Unary_Reverse_Bool);
            #endregion
        }
        #endregion

        #region Public
        /// <summary>
        /// Checks whether there is function registered for such operator and types.
        /// </summary>
        /// <param name="leftObj"></param>
        /// <param name="expOperator"></param>
        /// <param name="rightObj"></param>
        /// <returns></returns>
        public bool CheckEvaluationFunc(IObject leftObj, Token expOperator, IObject rightObj)
        {
            return CheckEvaluationFunc(leftObj.Type, expOperator.Keyword, rightObj.Type);
        }

        public bool CheckEvaluationFunc(Type leftType, TokenType opToken, Type rightType)
        {
            BinaryFuncArgsWrapper argPacket = BinaryFuncArgsWrapper.Create(leftType, opToken, rightType);

            bool exists = _binaryMappings.ContainsKey(argPacket);

            return exists;
        }

        public bool CheckEvaluationFunc(Token expOperator, IObject rightObj)
        {
            return CheckEvaluationFunc(expOperator.Keyword, rightObj.Type);
        }

        public bool CheckEvaluationFunc(TokenType opToken, Type rightType)
        {
            UnaryFuncArgsWrapper argPacket = UnaryFuncArgsWrapper.Create(opToken, rightType);

            bool exists = _unaryMappings.ContainsKey(argPacket);

            return exists;
        }

        /// <summary>
        /// Retrieves evaluation function.
        /// </summary>
        /// <param name="leftObj"></param>
        /// <param name="expOperator"></param>
        /// <param name="rightObj"></param>
        /// <returns></returns>
        public Func<IObject, IObject, IObject> GetEvaluationFunc(IObject leftObj, Token opToken, IObject rightObj)
        {
            bool exists = CheckEvaluationFunc(leftObj, opToken, rightObj);

            if(exists == false)
            {
                //  TODO: Handle error -> I think every return null in this class should return ErrorObj with details about what happend. This will clear the evaluation by a great bit.
                RegisterError(ErrorUndefinedOperation.Create(leftObj, opToken.Keyword, rightObj));
                return null;
            }

            return _binaryMappings[BinaryFuncArgsWrapper.Create(leftObj.Type, opToken.Keyword, rightObj.Type)];
        }

        public Func<IObject, IObject> GetEvaluationFunc(Token opToken, IObject rightObj)
        {
            bool exists = CheckEvaluationFunc(opToken, rightObj);

            if (exists == false)
            {
                //  TODO: Handle error -> I think every return null in this class should return ErrorObj with details about what happend. This will clear the evaluation by a great bit.
                RegisterError(ErrorUndefinedUnaryOperation.Create(opToken.Keyword, rightObj));
                return null;
            }

            return _unaryMappings[UnaryFuncArgsWrapper.Create(opToken.Keyword, rightObj.Type)];
        }

        public AError[] PopErrors()
        {
            return _errorsManager.PopErrors();
        }
        #endregion

        #region Private 
        /// <summary>
        /// This method registers function for such combination of left, right type with given operator.
        /// </summary>
        /// <param name="leftObj"></param>
        /// <param name="opToken"></param>
        /// <param name="rightObj"></param>
        /// <param name="func"></param>
        private void RegisterMethod(Type leftObj, TokenType opToken, Type rightObj, Func<IObject, IObject, IObject> func)
        {
            BinaryFuncArgsWrapper argPacket = BinaryFuncArgsWrapper.Create(leftObj, opToken, rightObj);

            bool alreadyExists = CheckEvaluationFunc(leftObj, opToken, rightObj);

            if(alreadyExists == true)
            {
                //  TODO: Implement
            }
            else
            {
                bool addedSuccessfully = _binaryMappings.TryAdd(argPacket, func);

                if(addedSuccessfully == false)
                {
                    //  TODO: Handle error
                }
            }
        }

        private void RegisterMethod(TokenType opToken, Type typeObj, Func<IObject, IObject> func)
        {
            UnaryFuncArgsWrapper argPacket = UnaryFuncArgsWrapper.Create(opToken, typeObj);

            bool alreadyExists = CheckEvaluationFunc(opToken, typeObj);

            if (alreadyExists == true)
            {
                //  TODO: Implement
            }
            else
            {
                bool addedSuccessfully = _unaryMappings.TryAdd(argPacket, func);

                if (addedSuccessfully == false)
                {
                    //  TODO: Handle error
                }
            }
        }
        #endregion

        #region Helper methods
        private IObject RegisterError(AError error)
        {
            _errorsManager.AppendError(error);
            return new ErrorObject(error);
        }
        #endregion

        #region Binary evaluation functions
        #region Char
        private IObject Char_Add_Char_Func(IObject left, IObject right)
        {
            try
            {
                int result = (left as CharObject).Value + (right as CharObject).Value;
                int r = result % 255;
                char resAsChar = (char)r;
                return CharObject.Create(resAsChar);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        private IObject Char_Subtract_Char_Func(IObject left, IObject right)
        {
            try
            {
                int result = (left as CharObject).Value - (right as CharObject).Value;
                int r = result % 255;
                char res = '0';
                if (r < 0)
                {
                    res = (char)(255 + r);
                }
                else
                {
                    res = (char)r;
                }
                return CharObject.Create(res);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        private IObject Char_Multiply_Char_Func(IObject left, IObject right)
        {
            try
            {
                int result = (left as CharObject).Value * (right as CharObject).Value;
                int r = result % 255;
                char resAsChar = (char)r;
                return CharObject.Create(resAsChar);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        private IObject Char_Divide_Char_Func(IObject left, IObject right)
        {
            try
            {
                int result = (left as CharObject).Value / (right as CharObject).Value;
                int r = result % 255;
                char resAsChar = (char)r;
                return CharObject.Create(resAsChar);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        #endregion
        private IObject Int_Assign_All_Func(IObject left, IObject right)
        {
            try
            {
                if(right is IntObject IsInt)
                {
                    return IntObject.Create(IsInt.Value);
                }
                else if(right is CharObject AsChar)
                {
                    return IntObject.Create(AsChar.Value);
                }
                else if (right is FloatObject AsFloat)
                {
                    return FloatObject.Create(AsFloat.Value);
                }
                else
                {
                    return RegisterError(ErrorOperationFailed.Create(left, TokenType.Assign, right));
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Assign, right));
            }
        }

        private IObject Char_Assign_All_Func(IObject left, IObject right)
        {
            try
            {
                if (right is CharObject AsChar)
                {
                    return CharObject.Create(AsChar.Value);
                }
                else
                {
                    return RegisterError(ErrorOperationFailed.Create(left, TokenType.Assign, right));
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Assign, right));
            }
        }

        private IObject String_Assign_String_Func(IObject left, IObject right)
        {
            try
            {
                if (right is StringObject AsString)
                {
                    return StringObject.Create(AsString.Value);
                }
                else
                {
                    return RegisterError(ErrorOperationFailed.Create(left, TokenType.Assign, right));
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Assign, right));
            }
        }

        private IObject Float_Assign_All_Func(IObject left, IObject right)
        {
            try
            {
                if (right is IntObject IsInt)
                {
                    return FloatObject.Create(IsInt.Value);
                }
                else if (right is CharObject AsChar)
                {
                    return FloatObject.Create(AsChar.Value);
                }
                else if (right is FloatObject AsFloat)
                {
                    return FloatObject.Create(AsFloat.Value);
                }
                else
                {
                    return RegisterError(ErrorOperationFailed.Create(left, TokenType.Assign, right));
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Assign, right));
            }
        }
        private IObject Int_Add_Int_Func(IObject left, IObject right)
        {
            try
            {
                return IntObject.Create((left as IntObject).Value + (right as IntObject).Value);
            }
            catch(Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        private IObject Char_Equal_Char_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as CharObject).Value == (right as CharObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        private IObject Char_InEqual_Char_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as CharObject).Value != (right as CharObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        private IObject Char_Smaller_Char_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as CharObject).Value < (right as CharObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        private IObject Char_Greater_Char_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as CharObject).Value > (right as CharObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        private IObject Char_SmallerEqual_Char_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as CharObject).Value <= (right as CharObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        private IObject Char_BiggerEqual_Char_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as CharObject).Value >= (right as CharObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        #region String
        private IObject String_Plus_Any_Func(IObject left, IObject right)
        {
            try
            {
                string value = string.Empty;
                if(right is CharObject AsChar)
                {
                    value = AsChar.Value.ToString();
                }
                else if (right is StringObject AsString)
                {
                    value = AsString.Value.ToString();
                }
                else if (right is IntObject AsInt)
                {
                    value = AsInt.Value.ToString();
                }
                else if (right is BoolObject asBool)
                {
                    value = asBool.Value.ToString();
                }
                else if(right is NullObject AsNull)
                {
                    value = AsNull.Value.ToString();
                }
                else if(right is FloatObject AsFloat)
                {
                    value = AsFloat.Value.ToString();
                }
                else
                {

                }
                return StringObject.Create((left as StringObject).Value += value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }
        #endregion

        /// <summary>
        /// This function can handle adding of Int to Float, Float to Int and Float to Float.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private IObject Func_Float_Add_Float(IObject left, IObject right)
        {
            try
            {
                if(left is FloatObject OnlyLF && right is IntObject RI)
                {
                    return FloatObject.Create(OnlyLF.Value + RI.Value);
                }
                else if (right is FloatObject OnlyRF && left is IntObject LI)
                {
                    return FloatObject.Create(OnlyRF.Value + LI.Value);
                }
                else if (left is CharObject LeftChar && right is IntObject RightInt)
                {
                    return IntObject.Create(LeftChar.Value + RightInt.Value);
                }
                else if (left is IntObject LeftInt && right is CharObject RightChar)
                {
                    return IntObject.Create(LeftInt.Value + RightChar.Value);
                }
                else
                {
                    return FloatObject.Create((left as FloatObject).Value + (right as FloatObject).Value);
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }

        /// <summary>
        /// This function can handle subtraction of Int to Float, Float to Int and Float to Float.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private IObject Func_Float_Subtract_Float(IObject left, IObject right)
        {
            try
            {
                if (left is FloatObject OnlyLF && right is IntObject RI)
                {
                    return FloatObject.Create(OnlyLF.Value - RI.Value);
                }
                else if (right is FloatObject OnlyRF && left is IntObject LI)
                {
                    return FloatObject.Create(LI.Value - OnlyRF.Value);
                }
                else if (left is CharObject LeftChar && right is IntObject RightInt)
                {
                    return IntObject.Create(LeftChar.Value - RightInt.Value);
                }
                else if (left is IntObject LeftInt && right is CharObject RightChar)
                {
                    return IntObject.Create(LeftInt.Value - RightChar.Value);
                }
                else
                {
                    return FloatObject.Create((left as FloatObject).Value - (right as FloatObject).Value);
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }

        /// <summary>
        /// This function can handle multiplication of Int * Float, Float * Int and Float * Float.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private IObject Func_Float_Multiply_Float(IObject left, IObject right)
        {
            try
            {
                if (left is FloatObject OnlyLF && right is IntObject RI)
                {
                    return FloatObject.Create(OnlyLF.Value * RI.Value);
                }
                else if (right is FloatObject OnlyRF && left is IntObject LI)
                {
                    return FloatObject.Create(OnlyRF.Value * LI.Value);
                }
                else if (left is CharObject LeftChar && right is IntObject RightInt)
                {
                    return IntObject.Create(LeftChar.Value * RightInt.Value);
                }
                else if (left is IntObject LeftInt && right is CharObject RightChar)
                {
                    return IntObject.Create(LeftInt.Value * RightChar.Value);
                }
                else
                {
                    return FloatObject.Create((left as FloatObject).Value * (right as FloatObject).Value);
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }

        /// <summary>
        /// This function can handle division of Int * Float, Float * Int and Float * Float.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private IObject Func_Float_Division_Float(IObject left, IObject right)
        {
            try
            {
                if(right is IntObject RaI && RaI.Value == 0 || right is IntObject RaF && RaF.Value == 0)
                {
                    RegisterError(ErrorDevideByZero.Create());
                }


                if (left is FloatObject OnlyLF && right is IntObject RI)
                {
                    return FloatObject.Create(OnlyLF.Value / RI.Value);
                }
                else if (right is FloatObject OnlyRF && left is IntObject LI)
                {
                    return FloatObject.Create(LI.Value / OnlyRF.Value);
                }
                else if (left is CharObject LeftChar && right is IntObject RightInt)
                {
                    return IntObject.Create(LeftChar.Value / RightInt.Value);
                }
                else if (left is IntObject LeftInt && right is CharObject RightChar)
                {
                    return IntObject.Create(LeftInt.Value / RightChar.Value);
                }
                else
                {
                    return FloatObject.Create((left as FloatObject).Value / (right as FloatObject).Value);
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }

        /// <summary>
        /// This function can handle comparison of Int == Float, Float == Int and Float == Float.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private IObject Func_Float_Equal_Float(IObject left, IObject right)
        {
            try
            {
                if (left is FloatObject OnlyLF && right is IntObject RI)
                {
                    return BoolObject.Create(OnlyLF.Value == RI.Value);
                }
                else if (right is FloatObject OnlyRF && left is IntObject LI)
                {
                    return BoolObject.Create(OnlyRF.Value == LI.Value);
                }
                else if (left is CharObject LeftChar && right is IntObject RightInt)
                {
                    return BoolObject.Create(LeftChar.Value == RightInt.Value);
                }
                else if (left is IntObject LeftInt && right is CharObject RightChar)
                {
                    return BoolObject.Create(LeftInt.Value == RightChar.Value);
                }
                else
                {
                    return BoolObject.Create((left as FloatObject).Value == (right as FloatObject).Value);
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }

        private IObject Func_Float_InEqual_Float(IObject left, IObject right)
        {
            try
            {
                if (left is FloatObject OnlyLF && right is IntObject RI)
                {
                    return BoolObject.Create(OnlyLF.Value != RI.Value);
                }
                else if (right is FloatObject OnlyRF && left is IntObject LI)
                {
                    return BoolObject.Create(OnlyRF.Value != LI.Value);
                }
                else if (left is CharObject LeftChar && right is IntObject RightInt)
                {
                    return BoolObject.Create(LeftChar.Value != RightInt.Value);
                }
                else if (left is IntObject LeftInt && right is CharObject RightChar)
                {
                    return BoolObject.Create(LeftInt.Value != RightChar.Value);
                }
                else
                {
                    return BoolObject.Create((left as FloatObject).Value != (right as FloatObject).Value);
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }

        private IObject Func_Float_Greater_Float(IObject left, IObject right)
        {
            try
            {
                if (left is FloatObject OnlyLF && right is IntObject RI)
                {
                    return BoolObject.Create(OnlyLF.Value > RI.Value);
                }
                else if (right is FloatObject OnlyRF && left is IntObject LI)
                {
                    return BoolObject.Create(OnlyRF.Value > LI.Value);
                }
                else if (left is CharObject LeftChar && right is IntObject RightInt)
                {
                    return BoolObject.Create(LeftChar.Value > RightInt.Value);
                }
                else if (left is IntObject LeftInt && right is CharObject RightChar)
                {
                    return BoolObject.Create(LeftInt.Value > RightChar.Value);
                }
                else
                {
                    return BoolObject.Create((left as FloatObject).Value > (right as FloatObject).Value);
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }

        private IObject Func_Float_Lesser_Float(IObject left, IObject right)
        {
            try
            {
                if (left is FloatObject OnlyLF && right is IntObject RI)
                {
                    return BoolObject.Create(OnlyLF.Value < RI.Value);
                }
                else if (right is FloatObject OnlyRF && left is IntObject LI)
                {
                    return BoolObject.Create(OnlyRF.Value < LI.Value);
                }
                else if (left is CharObject LeftChar && right is IntObject RightInt)
                {
                    return BoolObject.Create(LeftChar.Value < RightInt.Value);
                }
                else if (left is IntObject LeftInt && right is CharObject RightChar)
                {
                    return BoolObject.Create(LeftInt.Value < RightChar.Value);
                }
                else
                {
                    return BoolObject.Create((left as FloatObject).Value < (right as FloatObject).Value);
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }

        private IObject Func_Float_GreaterEqual_Float(IObject left, IObject right)
        {
            try
            {
                if (left is FloatObject OnlyLF && right is IntObject RI)
                {
                    return BoolObject.Create(OnlyLF.Value >= RI.Value);
                }
                else if (right is FloatObject OnlyRF && left is IntObject LI)
                {
                    return BoolObject.Create(OnlyRF.Value >= LI.Value);
                }
                else if (left is CharObject LeftChar && right is IntObject RightInt)
                {
                    return BoolObject.Create(LeftChar.Value >= RightInt.Value);
                }
                else if (left is IntObject LeftInt && right is CharObject RightChar)
                {
                    return BoolObject.Create(LeftInt.Value >= RightChar.Value);
                }
                else
                {
                    return BoolObject.Create((left as FloatObject).Value >= (right as FloatObject).Value);
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }

        private IObject Func_Float_LesserEqual_Float(IObject left, IObject right)
        {
            try
            {
                if (left is FloatObject OnlyLF && right is IntObject RI)
                {
                    return BoolObject.Create(OnlyLF.Value <= RI.Value);
                }
                else if (right is FloatObject OnlyRF && left is IntObject LI)
                {
                    return BoolObject.Create(OnlyRF.Value <= LI.Value);
                }
                else if (left is CharObject LeftChar && right is IntObject RightInt)
                {
                    return BoolObject.Create(LeftChar.Value <= RightInt.Value);
                }
                else if (left is IntObject LeftInt && right is CharObject RightChar)
                {
                    return BoolObject.Create(LeftInt.Value <= RightChar.Value);
                }
                else
                {
                    return BoolObject.Create((left as FloatObject).Value <= (right as FloatObject).Value);
                }
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Plus, right));
            }
        }

        private IObject Int_Subtract_Int_Func(IObject left, IObject right)
        {
            try
            {
                return IntObject.Create((left as IntObject).Value - (right as IntObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Minus, right));
            }
        }

        private IObject Int_Multiply_Int_Func(IObject left, IObject right)
        {
            try
            {
                return IntObject.Create((left as IntObject).Value * (right as IntObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Star, right));
            }
        }

        private IObject Int_Devide_Int_Func(IObject left, IObject right)
        {
            try
            {
                if(right is IntObject isInt && isInt.Value == 0)
                {
                    RegisterError(ErrorDevideByZero.Create());
                }
                return IntObject.Create((left as IntObject).Value / (right as IntObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Slash, right));
            }
        }

        private IObject Int_Smaller_Int_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as IntObject).Value < (right as IntObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.LeftArrow, right));
            }
        }

        private IObject Int_Bigger_Int_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as IntObject).Value > (right as IntObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.RightArrow, right));
            }
        }

        private IObject Int_Equal_Int_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as IntObject).Value == (right as IntObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Equal, right));
            }
        }

        private IObject Int_InEqual_Int_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as IntObject).Value != (right as IntObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Inequal, right));
            }
        }

        private IObject Int_GreaterEqual_Int_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as IntObject).Value >= (right as IntObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.GreaterOrEqual, right));
            }
        }

        private IObject Int_SmallerEqual_Int_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as IntObject).Value <= (right as IntObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.LesserOrEqual, right));
            }
        }

        #region Bool
        private IObject Bool_Equal_Bool_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as BoolObject).Value == (right as BoolObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Equal, right));
            }
        }

        private IObject Bool_InEqual_Bool_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as BoolObject).Value != (right as BoolObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Inequal, right));
            }
        }
        #endregion

        #region Null
        private IObject Null_Equal_Null_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as NullObject).Value == (right as NullObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Equal, right));
            }
        }

        private IObject Null_InEqual_Null_Func(IObject left, IObject right)
        {
            try
            {
                return BoolObject.Create((left as NullObject).Value != (right as NullObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorOperationFailed.Create(left, TokenType.Inequal, right));
            }
        }
        #endregion


        #endregion

        #region Unary evaluation functions
        private IObject Unary_Minus_Numberic(IObject type)
        {
            try
            {
                if(type is IntObject AsInt) return IntObject.Create(-1 * AsInt.Value);
                else return FloatObject.Create(-1 * (type as FloatObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorUnaryOperationFailed.Create(TokenType.Minus, type));
            }
        }

        private IObject Unary_Negation_Numberic(IObject type)
        {
            try
            {
                if (type is IntObject AsInt) return IntObject.Create(-1 * AsInt.Value);
                else return FloatObject.Create(-1 * (type as FloatObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorUnaryOperationFailed.Create(TokenType.ExclemationMark, type));
            }
        }

        private IObject Unary_Negation_Bool(IObject type)
        {
            try
            {
                return BoolObject.Create(!(type as BoolObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorUnaryOperationFailed.Create(TokenType.ExclemationMark, type));
            }
        }

        private IObject Unary_Reverse_Integer(IObject type)
        {
            try
            {
                return IntObject.Create(~(type as IntObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorUnaryOperationFailed.Create(TokenType.Tilde, type));
            }
        }

        private IObject Unary_Reverse_Bool(IObject type)
        {
            try
            {
                return BoolObject.Create(!(type as BoolObject).Value);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorUnaryOperationFailed.Create(TokenType.Tilde, type));
            }
        }

        private IObject Unary_Question(IObject type)
        {
            try
            {
                if (type is IntObject AsInt) return BoolObject.Create(AsInt.Value == 0);
                else if (type is FloatObject AsFloat) return BoolObject.Create(AsFloat.Value == 0f);
                else return BoolObject.Create((type as BoolObject).Value == false);
            }
            catch (Exception exception)
            {
                return RegisterError(ErrorUnaryOperationFailed.Create(TokenType.QuenstionMark, type));
            }
        }
        #endregion
    }
}
