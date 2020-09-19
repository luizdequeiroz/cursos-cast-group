using System;
using System.Linq;

namespace repository.Utilities
{
    public static class ObjectExtensions
    {
        private readonly static string KEY_ATTRIBUTE = "KeyAttribute";
        private readonly static string DATABASE_GENERATED_ATTRIBUTE = "DatabaseGeneratedAttribute";

        public static void SetProperties<T, F>(this T to, F from, params string[] exceptions)
        {
            foreach (var propTo in to.GetType().GetProperties())
            {
                foreach (var propFrom in from.GetType().GetProperties())
                {
                    if (propFrom.Name == propTo.Name)
                    {
                        if (propTo.CustomAttributes.Where(ca => ca.AttributeType.Name == KEY_ATTRIBUTE || ca.AttributeType.Name == DATABASE_GENERATED_ATTRIBUTE).Count() == 0)
                        {
                            if (!exceptions.Contains(propFrom.Name))
                            {
                                propTo.SetValue(to, propFrom.GetValue(from));
                            }
                        }
                    }
                }
            }
        }

        public static void SetDifferentProperties<T, F>(this T target, F payload, params string[] exceptions)
        {
            foreach (var propTo in target.GetType().GetProperties())
            {
                foreach (var propFrom in payload.GetType().GetProperties())
                {
                    if (propFrom.Name == propTo.Name)
                    {
                        if (propTo.CustomAttributes.Where(ca => ca.AttributeType.Name == KEY_ATTRIBUTE || ca.AttributeType.Name == DATABASE_GENERATED_ATTRIBUTE).Count() == 0)
                        {
                            if (!exceptions.Contains(propFrom.Name))
                            {
                                var payloadValue = propFrom.GetValue(payload);
                                if (payloadValue != null)
                                {
                                    propTo.SetValue(target, payloadValue);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
