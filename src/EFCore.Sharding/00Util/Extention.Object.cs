﻿using Newtonsoft.Json;
using System;
using System.Reflection;

namespace EFCore.Sharding.Util
{
    public static partial class PublicExtention
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        static PublicExtention()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                //日期类型默认格式化处理
                setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                return setting;
            });
        }

        private static BindingFlags _bindingFlags { get; }
            = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static;

        /// <summary>
        /// 判断是否为Null或者空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null)
                return true;
            else
            {
                string objStr = obj.ToString();
                return string.IsNullOrEmpty(objStr);
            }
        }

        /// <summary>
        /// 将对象序列化成Json字符串
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 获取某属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName, _bindingFlags).GetValue(obj);
        }

        /// <summary>
        /// 改变实体类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        public static object ChangeType(this object obj, Type targetType)
        {
            return obj.ToJson().ToObject(targetType);
        }

        /// <summary>
        /// 改变实体类型
        /// </summary>
        /// <typeparam name="T">目标泛型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static T ChangeType<T>(this object obj)
        {
            return obj.ToJson().ToObject<T>();
        }
    }
}
