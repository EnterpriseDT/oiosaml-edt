using System.Collections.Generic;
using dk.nita.saml20.config;
using System;

namespace dk.nita.saml20.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public class Actions
    {
        private static List<IAction> defaultActions = new List<IAction>( new IAction[] { new RedirectAction() } );

        /// <summary>
        /// Gets the default actions. 
        /// </summary>
        /// <returns></returns>
        public static List<IAction> GetDefaultActions()
        {
            //List<IAction> actions = new List<IAction>();
            //actions.Add(new SamlPrincipalAction());
            //actions.Add(new RedirectAction());
            //return actions;
            return defaultActions;
        }

        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <returns></returns>
        public static List<IAction> GetActions()
        {
            List<IAction> actions = GetDefaultActions();
            FederationConfig config = FederationConfig.GetConfig();

            foreach (ActionConfigAbstract ac in config.Actions.ActionList)
            {
                if (ac is ActionConfigClear)
                    actions.Clear();
                else if (ac is ActionConfigRemove)
                {
                    actions.RemoveAll(delegate(IAction a) { return a.Name == ac.Name; });
                }
                else if(ac is ActionConfigAdd)
                {
                    ActionConfigAdd addAction = (ActionConfigAdd)ac;
                    IAction add = (IAction)Activator.CreateInstance(Type.GetType(addAction.Type));
                    actions.Add(add);
                }

            }

            return actions;
        }
    }
}
