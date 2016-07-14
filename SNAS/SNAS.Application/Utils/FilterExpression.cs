using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Utils
{
    public static class FilterExpression
    {
        private static Expression ConditonToExpression(FilterCondition condition, Expression parameter, Type type)
        {
            Expression expr = null;
            PropertyInfo pi = type.GetProperty(condition.Field, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (pi == null)//字段名不存在
                return null;
            Expression left = Expression.Property(parameter, pi);

            switch (condition.@operator.ToLower())
            {
                case "equal":
                    {
                        object value = getValue(pi.PropertyType, condition.value);
                        Expression right = Expression.Constant(value);
                        expr = Expression.Equal(left, right);
                    }
                    break;
                case "like":
                    {
                        object value = getValue(pi.PropertyType, condition.value);
                        Expression right = Expression.Constant(value);
                        MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        expr = Expression.Call(left, method, right);
                    }
                    break;
                case "range":
                    //范围查询,暂时只支持date类型
                    if (!string.IsNullOrEmpty(condition.minValue) && string.IsNullOrEmpty(condition.maxValue))
                    {
                        object minValue = getValue(pi.PropertyType, condition.minValue + " 00:00:00");
                        Expression minright = Expression.Constant(minValue);
                        expr = Expression.GreaterThanOrEqual(left, minright);
                    }

                    if (string.IsNullOrEmpty(condition.minValue) && !string.IsNullOrEmpty(condition.maxValue))
                    {
                        object maxValue = getValue(pi.PropertyType, condition.maxValue + " 23:59:59");
                        Expression maxright = Expression.Constant(maxValue);
                        expr =Expression.LessThanOrEqual(left, maxright);
                    }

                    if (!string.IsNullOrEmpty(condition.minValue) && !string.IsNullOrEmpty(condition.maxValue))
                    {
                        object minValue = getValue(pi.PropertyType, condition.minValue + " 00:00:00");
                        object maxValue = getValue(pi.PropertyType, condition.maxValue + " 23:59:59");
                        Expression minright = Expression.Constant(minValue);
                        Expression maxright = Expression.Constant(maxValue);
                        expr = Expression.And(Expression.GreaterThanOrEqual(left, minright),
                            Expression.LessThanOrEqual(left, maxright));
                    }
                    break;
                    //case "<":
                    //    expr = Expression.LessThan(left, right);
                    //    break;
                    //case "<=":
                    //    expr = Expression.LessThanOrEqual(left, right);
                    //    break;
                    //case ">":
                    //    expr = Expression.GreaterThan(left, right);
                    //    break;
                    //case ">=":
                    //    expr = Expression.GreaterThanOrEqual(left, right);
                    //    break;
                    //case "!=":
                    //    expr = Expression.NotEqual(left, right);
                    //    break;
            }
            return expr;
        }


        /// <summary>
        /// 获取符合类型值,主要针对枚举，布尔
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static object getValue(Type type, string value)
        {
            if (type.BaseType == typeof(Enum))
            {
                var _enum = Enum.Parse(type, value);
                return _enum;
            }

            if (type == typeof(bool))
            {
                if (value == "1") return true;
                return false;
            }

            return Convert.ChangeType(value, type);
        }

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <typeparam name="T">实体模型</typeparam>
        /// <param name="conditions">过滤条件</param>
        /// <returns>表达式</returns>
        public static Expression<Func<T, bool>> FindByGroup<T>(List<FilterCondition> conditions)
        {
            //先清除不参与自动拼接的字段
            List<FilterCondition> _conditions=new List<FilterCondition>();
            _conditions.AddRange(conditions.FindAll(t=>!t.ignore));
            
            ParameterExpression parameter = Expression.Parameter(typeof(T), "r");
            Expression body = null;
            Type type = typeof(T);
            if (_conditions.Count > 0)
            {
                body = ConditonToExpression(_conditions[0], parameter, type);
                for (int i = 1; i < _conditions.Count; i++)
                {
                    Expression right = ConditonToExpression(_conditions[i], parameter, type);
                    
                    body = _conditions[i].logic.ToUpper().Equals("AND") ?
                        Expression.And(body, right) :
                        Expression.Or(body, right);
                }
            }
            if (body != null)
            {
                Expression<Func<T, bool>> expr = Expression.Lambda<Func<T, bool>>(body, parameter);
                return expr;
            }
            return PredicateExtensionses.True<T>();
        }

        public static bool HasValue(List<FilterCondition> conditions,string field)
        {
            foreach (var condition in conditions)
            {
                if (condition.Field.ToLower() == field.ToLower())
                {
                    if (!string.IsNullOrWhiteSpace(condition.value) ||
                        !string.IsNullOrWhiteSpace(condition.minValue) ||
                        !string.IsNullOrWhiteSpace(condition.maxValue))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string GetValue(List<FilterCondition> conditions, string field)
        {
            foreach (var condition in conditions)
            {
                if (condition.Field.ToLower() == field.ToLower())
                {
                    return condition.value;
                }
            }

            return string.Empty;
        }
    }
}
