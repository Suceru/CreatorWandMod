using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CreatorModAPI
{
    public static class ProbeFunctions
    {
        //obj.GetPrivateField<int>("privatefield1")
        public static T GetPrivateField<T>(this object instance, string fieldname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            return (T)field.GetValue(instance);
        }
        //obj.GetPrivateProperty<string>("PrivateFieldA")
        //var sd = ProbeFunctions.GetPrivateField<Dictionary<string, string>>(this, "io");
        public static T GetPrivateProperty<T>(this object instance, string propertyname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            PropertyInfo field = type.GetProperty(propertyname, flag);
            return (T)field.GetValue(instance, null);
        }
        // ProbeFunctions.SetPrivateField(item.ComponentFlu, "m_fluDuration", 900);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance">Instance</param>
        /// <param name="fieldname">FieldName</param>
        /// <param name="value">Value</param>
        public static void SetPrivateField(this object instance, string fieldname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            FieldInfo field = type.GetField(fieldname, flag);
            field.SetValue(instance, value);
        }
        //obj.SetPrivateProperty("PrivateFieldA", "hello");
        public static void SetPrivateProperty(this object instance, string propertyname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            PropertyInfo field = type.GetProperty(propertyname, flag);
            field.SetValue(instance, value, null);
        }
        //obj.CallPrivateMethod<int>("Add",null)
        public static T CallPrivateMethod<T>(this object instance, string name, params object[] param)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = instance.GetType();
            MethodInfo method = type.GetMethod(name, flag);
            return (T)method.Invoke(instance, param);
        }
        /// 获取类中的属性
        /// </summary>
        /// <returns>所有属性名称</returns>
        public static List<string> GetProperties<T>(T t)
        {
            List<string> ListStr = new List<string>();
            if (t == null)
            {
                return ListStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return ListStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name; //名称
                object value = item.GetValue(t, null);  //值

                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ListStr.Add(name);
                }
                else
                {
                    GetProperties(value);
                }
            }
            return ListStr;
        }


        /// <summary>
        ///  获取类中的字段
        /// </summary>
        /// <returns>所有字段名称</returns>
        public static List<string> GetFields<T>(T t)
        {

            List<string> ListStr = new List<string>();
            if (t == null)
            {
                return ListStr;
            }
            System.Reflection.FieldInfo[] fields = t.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (fields.Length <= 0)
            {
                return ListStr;
            }
            foreach (System.Reflection.FieldInfo item in fields)
            {
                string name = item.Name; //名称
                object value = item.GetValue(t);  //值

                if (item.FieldType.IsValueType || item.FieldType.Name.StartsWith("String"))
                {
                    ListStr.Add(name);
                }
                else
                {
                    GetFields(value);
                }
            }
            return ListStr;


        }
    }
}
