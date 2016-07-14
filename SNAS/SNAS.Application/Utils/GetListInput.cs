using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SNAS.Application.Utils
{

    /// <summary>
    /// 获取列表，输入条件
    /// </summary>
    public class GetListInput : IShouldNormalize
    {
        public GetListInput()
        {
            Filter=new List<FilterCondition>();
        }

        /// <summary>
        /// 排序字段，格式 字段名,升降序|字段名,升降序,如(id,0|name,1)
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 查询参数 格式
        /// </summary>
        public List<FilterCondition> Filter { get; set; }

        public void Normalize()
        {

        }


        //List<FilterItem> filters = new List<FilterItem>();


        //public bool HasFilter(string fieldName)
        //{
        //    prepareFilter();
        //    return filters.ContainsKey(fieldName.ToLower());
        //}

        //public T GetFilter<T>(string fieldName)
        //{
        //    prepareFilter();
        //    string key = fieldName.ToLower();
        //    if (!filters.ContainsKey(fieldName))
        //    {
        //        return default(T);
        //    }

        //    if (typeof(T) == typeof(string))
        //    {
        //        return (T)Convert.ChangeType(filters[key], typeof(T));
        //    }
        //    return default(T);
        //}

        bool hasPrepareFilter = false;
        //private void prepareFilter()
        //{
        //    if (hasPrepareFilter) return;
        //    filters = new Dictionary<string, string>();

        //    hasPrepareFilter = true;
        //    if (string.IsNullOrEmpty(Filter)) return;
        //    var kvs = Filter.Split('|');
        //    foreach (var item in kvs)
        //    {
        //        var kv = item.Split(':');
        //        if (kv.Length != 2) continue;
        //        string key = kv[0];
        //        string value = kv[1];
        //        if (string.IsNullOrEmpty(value)) continue;//字段值为空时,表示该条件未填写                
        //        filters.Add(key, value);
        //    }
        //}


        public List<OrderModelField> GetOrders(string defaultOrderField = "id", bool isDescending = false)
        {
            List<OrderModelField> list = new List<OrderModelField>();
            if (!string.IsNullOrEmpty(Sort))
            {
                var kvs = Sort.Split('|');
                foreach (var item in kvs)
                {
                    var kv = item.Split(',');
                    if (kv.Length != 2) continue;
                    string key = kv[0];
                    bool isdescending = kv[1] == "1";
                    list.Add(new OrderModelField() { PropertyName = key, IsDescending = isdescending });
                }
            }

            if (list.Count == 0)
            {
                //没有获取排序字段,则使用默认排序字段
                list.Add(new OrderModelField() { PropertyName = defaultOrderField, IsDescending = isDescending });
            }

            return list;
        }
    }

    public static class OrderByExtensionses
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, List<OrderModelField> orders) where T : class
        {
            Type type = typeof(T);

            int idx = 0;
            foreach (var item in orders)
            {
                PropertyInfo property = type.GetProperty(item.PropertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property == null)
                    throw new ArgumentException("propertyName", "Not Exist");

                ParameterExpression param = Expression.Parameter(type, "p");
                Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
                LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

                string methodName = "";
                if (idx++ > 0)
                {
                    methodName = !item.IsDescending ? "ThenBy" : "ThenByDescending";
                }
                else
                {
                    methodName = !item.IsDescending ? "OrderBy" : "OrderByDescending";
                }

                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
                source = source.Provider.CreateQuery<T>(resultExp);
            }


            return source;
        }
    }
}
