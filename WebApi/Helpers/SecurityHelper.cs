using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Helpers
{
    public enum Role
    {
        AktivtMedlem,
        PassivtMedlem,
        BestyrelseMedlem,
        ForældreMedlem
    }

    public enum ActionType
    {
        Create,
        Update,
        Delete,
        Read
    }

    public enum Controllers
    {
        Address,
        Update,
        Delete,
        Read
    }

    public class SecurityHelper
    {
        public static bool CanPerformAction(Role role, ActionType action, Controller Address)
        {
            switch (role)
            {
                case Role.AktivtMedlem:
                    return CanActivePerformAction(action);
                case Role.PassivtMedlem:
                    return CanPassivePerformAction(action);
                case Role.BestyrelseMedlem:
                    return CanBestyrelsePerformAction(action);
                case Role.ForældreMedlem:
                    return CanParentPerformAction(action);
                default:
                    throw new ArgumentException("Invalid role");
            }
        }
        private static bool CanActivePerformAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.Create:
                case ActionType.Update:
                    return true; // Users can update their data
                case ActionType.Delete:
                case ActionType.Read:
                    return true; // Users can read their own data
                default:
                    return false;
            }
        }

        private static bool CanPassivePerformAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.Create:
                case ActionType.Update:
                    return true; // Users can update their data
                case ActionType.Delete:
                case ActionType.Read:
                    return true; // Users can read their own data
                default:
                    return false;
            }
        }
        private static bool CanParentPerformAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.Create:
                    return true; // Parents can create children
                case ActionType.Update:
                    return true; // Parents can update their own children's information
                case ActionType.Delete:
                    return true; // Parents can delete their children's records
                case ActionType.Read:
                    return true; // Parents can read information about their children
                default:
                    return false;
            }
        }

        private static bool CanBestyrelsePerformAction(ActionType action)
        {
            switch (action)
            {
                case ActionType.Read:
                    return true; // Inactive users can read data (e.g., their profile)
                case ActionType.Update:
                case ActionType.Delete:
                case ActionType.Create:
                    return false; // Inactive users cannot create, update or delete
                default:
                    return false;
            }
        }
    }
}

