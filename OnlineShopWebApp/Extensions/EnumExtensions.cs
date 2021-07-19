using System;
using System.ComponentModel;
using System.Reflection;

namespace OnlineShopWebApp.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Возвращает значение свойства string Description атрибута DescriptionAttribute, которым помечен член перечисления. Возвращает enumElement.ToString(), если член не помечен атрибутом
        /// </summary>
        /// <param name="enumElement">Член перечисления для которого нужно получить Description</param>
        /// <returns>Значение Description из атрибута DescriptionAttribute</returns>
        public static string GetDescription(this Enum enumElement)
        {
            var enumItemName = enumElement.ToString();
            var memberInfo = enumElement.GetType()
                                        .GetMember(enumItemName);
            if (memberInfo == null && memberInfo.Length == 0)
            {
                return enumItemName;
            }           
            var descriptionAttr = memberInfo[0].GetCustomAttribute<DescriptionAttribute>();
            return descriptionAttr?.Description ?? enumItemName;
        }
    }
}