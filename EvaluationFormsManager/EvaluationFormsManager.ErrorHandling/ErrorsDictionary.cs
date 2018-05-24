using System;
using System.ComponentModel;
using System.Reflection;

namespace EvaluationFormsManager.ErrorHandling
{
    public static class ErrorsDictionary
    {
        public static string GetDescription(Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);

            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(
                        field,
                        typeof(DescriptionAttribute)
                    ) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }

            return null;
        }

        public static object GetResultObject(Enum value)
        {
            return new
            {
                error = GetDescription(value)
            };
        }
    }

    public enum ErrorCodes : uint
    {
        [Description("Form Identifier is invalid.")]
        ERR_FORM_ID_INVALID = 1,

        [Description("Form data object cannot be empty.")]
        ERR_FORM_OBJ_EMPTY = 2,

        [Description("Form has not been found.")]
        ERR_FORM_NOT_FOUND = 3,

        [Description("User Identifier provided is invalid.")]
        ERR_USER_ID_INVALID = 4,

        [Description("The list of Users to share the form with is empty or invalid.")]
        ERR_USERLIST_SHARE_INVALID = 5
    }
}
