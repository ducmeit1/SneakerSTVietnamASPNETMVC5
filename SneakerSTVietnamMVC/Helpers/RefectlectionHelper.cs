using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SneakerSTVietnamMVC.Helpers
{
    public class RefectlectionHelpers
    {
        public List<Type> GetControllers(string nameSpaces)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains(nameSpaces)).GroupBy(m=> m.Name).Select(g => g.FirstOrDefault()).OrderBy(x => x.Name);
            return types.Distinct().ToList();
        }

        public List<string> GetActions(Type controller)
        {
            List<string> listActions = new List<string>();
            IEnumerable<MemberInfo> memberInfo = controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public).Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any()).GroupBy(m => m.Name).Select(g => g.FirstOrDefault()).OrderBy(x => x.Name);
            foreach (MemberInfo method in memberInfo)
            {
                if (method.ReflectedType.IsPublic && !method.IsDefined(typeof(NonActionAttribute))) {
                    listActions.Add(method.Name.ToString());
                }
            }
            return listActions;
        }
    }
}